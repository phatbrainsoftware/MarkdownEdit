using Microsoft.AspNetCore.Components;

namespace PhatBrainSoftware.MarkdownEdit
{
    public partial class ToolbarButtonGroup
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
