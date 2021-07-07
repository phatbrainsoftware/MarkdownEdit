using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace PhatBrainSoftware.MarkdownEdit
{
    public partial class SayHello : IAsyncDisposable
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        public string Name { get; set; }

        public Task<IJSObjectReference> _module;

        private readonly string _importPath = $"./_content/{typeof(SayHello).Namespace}/SayHello.js";

        private Task<IJSObjectReference> Module => _module ??= JSRuntime.InvokeAsync<IJSObjectReference>("import", _importPath).AsTask();

        async Task Submit()
        {
            var module = await Module;
            await module.InvokeVoidAsync("sayHi", this.Name);
        }

        [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "<Pending>")]
        public async ValueTask DisposeAsync()
        {
            if (_module != null)
            {
                var module = await _module;
                await module.DisposeAsync();
            }
        }
    }
}
