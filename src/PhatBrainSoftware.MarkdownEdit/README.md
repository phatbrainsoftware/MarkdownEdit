# Introduction

https://www.meziantou.net/publishing-a-self-contained-blazor-component-razor-css-js-as-a-nuget-package.htm

Build the nuget package.

```bash
dotnet pack PhatBrainSoftware.MarkdownEdit.csproj --configuration Release

dotnet nuget push bin/Release/PhatBrainSoftware.MarkdownEdit.1.0.0.nupkg --api-key "<your api key>" --source https://api.nuget.org/v3/index.json
```