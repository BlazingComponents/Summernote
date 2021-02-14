# BlazingComponents
[Blazing Components](https://github.com/BlazingComponents) is a collection of easy to use and implement [Razor components class libraries](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/class-libraries?view=aspnetcore-5.0)

## Summernote
[Summernote](https://summernote.org/)
is a Super simple WYSIWYG Editor, 

Check their homepage for more details.

### Setup

Add reference to style sheet & javascript references
Add the following line to the head tag of your _Host.cshtml (Blazor Server) or index.html (Blazor WebAssembly).

#### Head Inclusions
```html
<link href="./_content/BlazingComponents.Summernote/summernote/summernote-lite.min.css" rel="stylesheet" />
```
##### Example: (head)
```html
    ....
    <link href="{YourBlazorProject}.styles.css" rel="stylesheet" />
    <link href="./_content/BlazingComponents.Summernote/summernote/summernote-lite.min.css" rel="stylesheet" />
</head>

```


Then add a reference to the Summernode JavaScript file at the bottom of the respective page after the reference to the Blazor file.
#### Body Inclusions
```html
<script src="./_content/BlazingComponents.Summernote/summernote/jquery-3.4.1.slim.min.js"></script>
<script src="./_content/BlazingComponents.Summernote/summernote/summernote-lite.min.js"></script>
```

##### Example: (body)
```html
    ....
    <script src="_framework/blazor.webassembly.js"></script>
    <script src="./_content/BlazingComponents.Summernote/summernote/jquery-3.4.1.slim.min.js"></script>
    <script src="./_content/BlazingComponents.Summernote/summernote/summernote-lite.min.js"></script>
    <script>navigator.serviceWorker.register('service-worker.js');</script>
</body>
```

### Usage
BlazingComponents.Summernote will update the bound content variable on change in the editor.

``` html
@page "/"

<BlazingComponents.Summernote.Editor @bind-content="@content" />    

<h2>@content</h2>

@code{
    private string content;
}
```

## ToDo
Add additional configuration and customiztion options.