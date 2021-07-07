using Microsoft.AspNetCore.Components;

namespace PhatBrainSoftware.MarkdownEdit
{
    public partial class ToolbarButton
    {
        [Parameter]
        public string CssClass { get; set; } = "btn btn-link";

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback OnClickCallback { get; set; }
    }
}
