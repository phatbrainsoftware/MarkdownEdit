# Introduction

Markdown Text Editor for Blazor applications - Uses [Bootstrap](https://getbootstrap.com), [Fontawesome](https://fontawesome.com), and [Markdig](https://github.com/xoofx/markdig)

## Getting Setup

You can install the package via the NuGet package manager just search for Blazored.Toast. You can also install via powershell using the following command.

```ps
Install-Package PhatBrainSoftware.MarkdownEdit
```

Or via the dotnet CLI.

```bash
dotnet add package PhatBrainSoftware.MarkdownEdit
```

If you're using Visual Studio you can also install via the built in NuGet package manager.

### Add Imports

Add the following to your `_Imports.razor`

```cs
@using PhatBrainSoftware.MarkdownEdit
```

### Basic Example

See code in the [Index.razor](samples/src/BlazorWebAssembly/Pages/Index.razor) page in the repo for more examples.

```cs
@page "/"

<h1>Hello, world!</h1>

<EditForm Model=@this.Model>

    @*https://icons.getbootstrap.com/#usage*@

    <MarkdownEdit @ref=@MarkdownEdit @bind-Value=@this.Model.Text ShowHelp=false>
        <Toolbar>
            <ToolbarButtonGroup>
                <ToolbarButton OnClickCallback=@(() => this.MarkdownEdit.InsertTextAsync("# "))><i class="bi-type-h1"></i></ToolbarButton>
                <ToolbarButton OnClickCallback=@(() => this.MarkdownEdit.InsertTextAsync("## "))><i class="bi-type-h2"></i></ToolbarButton>
                <ToolbarButton OnClickCallback=@(() => this.MarkdownEdit.InsertTextAsync("### "))><i class="bi-type-h3"></i></ToolbarButton>
            </ToolbarButtonGroup>
            <ToolbarButtonGroup>
                <ToolbarButton OnClickCallback=@(() => this.MarkdownEdit.WrapTextAsync("**"))><i class="bi-type-bold"></i></ToolbarButton>
                <ToolbarButton OnClickCallback=@(() => this.MarkdownEdit.WrapTextAsync("*"))><i class="bi-type-italic"></i></ToolbarButton>
            </ToolbarButtonGroup>
            <ToolbarButtonGroup>
                <ToolbarButton OnClickCallback=@(() => this.MarkdownEdit.InsertTextAsync("> "))><i class="bi-blockquote-left"></i></ToolbarButton>
            </ToolbarButtonGroup>
            <ToolbarButtonGroup>
                <ToolbarButton OnClickCallback=@(() => this.MarkdownEdit.InsertTextAsync("* "))><i class="bi-list-ul"></i></ToolbarButton>
                <ToolbarButton OnClickCallback=@(() => this.MarkdownEdit.InsertTextAsync("1. "))><i class="bi-list-ol"></i></ToolbarButton>
            </ToolbarButtonGroup>
            <ToolbarButtonGroup>
                <ToolbarButton OnClickCallback=@(() => this.MarkdownEdit.InsertImageAsync())><i class="bi-link"></i></ToolbarButton>
                <ToolbarButton OnClickCallback=@(() => this.MarkdownEdit.InsertLinkAsync())><i class="bi-image"></i></ToolbarButton>
            </ToolbarButtonGroup>
        </Toolbar>
    </MarkdownEdit>

</EditForm>

@code
{
    private MarkdownEdit MarkdownEdit;

    public class TestModel
    {
        public string Text { get; set; } = "Hello, world!";
    }

    public TestModel Model { get; set; }

    protected override void OnInitialized()
    {
        this.Model = new TestModel();
    }
}
```

### Screenshot

![Screenshot](/Screenshot1.jpg "Screenshot")