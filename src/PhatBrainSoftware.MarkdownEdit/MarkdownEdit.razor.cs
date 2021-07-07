using Markdig;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace PhatBrainSoftware.MarkdownEdit
{
    public partial class MarkdownEdit : IAsyncDisposable
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public RenderFragment Toolbar { get; set; }

        [Parameter]
        public bool ShowHelp { get; set; } = true;

        [Parameter]
        public bool ShowPreview { get; set; } = true;

        protected string Preview => Markdown.ToHtml(this.CurrentValue ?? string.Empty, this.MarkdownPipeline);

        public string Id { get; private set; }

        // https://docs.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/call-javascript-from-dotnet?view=aspnetcore-5.0#javascript-isolation-in-javascript-modules

        protected IJSObjectReference Module { get; set; }

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
        private MarkdownPipeline MarkdownPipeline => new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

        protected override void OnInitialized()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        protected override async Task OnAfterRenderAsync(
            bool firstRender)
        {
            if (firstRender)
            {
                this.Module = await this.JSRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/{typeof(MarkdownEdit).Namespace}/MarkdownEdit.js");

                Console.WriteLine(this.Module.ToString());
            }
        }

        public async Task InsertTextAsync(string text)
        {
            this.CurrentValue = await this.Module.InvokeAsync<string>("insertText", this.Id, text);

            this.StateHasChanged();
        }

        public async Task WrapTextAsync(string tag)
        {
            this.CurrentValue = await this.Module.InvokeAsync<string>("wrapText", this.Id, tag);

            this.StateHasChanged();
        }

        public async Task InsertLinkAsync()
        {
            var link = await this.Module.InvokeAsync<string>("showPrompt", "Enter the url to the link, e.g., http://teamaloo.com:");

            var caption = await this.Module.InvokeAsync<string>("showPrompt", "Enter the title for the link, e.g., Teamaloo:");

            if (!string.IsNullOrWhiteSpace(link))
            {
                if (string.IsNullOrWhiteSpace(caption))
                {
                    caption = link;
                }

                this.CurrentValue = await this.Module.InvokeAsync<string>("insertText", this.Id, $"[{caption}]({link})");

                this.StateHasChanged();
            }
        }

        public async Task InsertImageAsync()
        {
            var link = await this.Module.InvokeAsync<string>("showPrompt", "Enter the url to the image, e.g., http://teamaloo.com/teamaloo.png:");

            var caption = await this.Module.InvokeAsync<string>("showPrompt", "Enter the title for the image, e.g., Teamaloo:");

            if (!string.IsNullOrWhiteSpace(link))
            {
                if (string.IsNullOrWhiteSpace(caption))
                {
                    caption = link;
                }

                this.CurrentValue = await this.Module.InvokeAsync<string>("insertText", this.Id, $"![{caption}]({link})");

                this.StateHasChanged();
            }
        }

        protected override bool TryParseValueFromString(
            string value,
            [MaybeNullWhen(false)] out string result,
            [NotNullWhen(false)] out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }

        [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "<Pending>")]
        public async ValueTask DisposeAsync()
        {
            if (this.Module is not null)
            {
                await this.Module.DisposeAsync();
            }
        }
    }
}
