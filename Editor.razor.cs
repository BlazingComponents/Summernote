using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazingComponents.Summernote
{
    public partial class Editor
    {
        private BlazingSummerJsInterop _blazingSummerJsInterop;
        private bool _edit = true;
        private MarkupString _markupContent = new MarkupString();

        [Parameter]
        public string Content
        {
            get
            {
                return _markupContent.ToString();
            }

            set
            {
                _markupContent = (MarkupString)value;
            }
        }

        [Parameter] public EventCallback<string> ContentChanged { get; set; }

        private string NoteId { get; } = $"BlazingSummerNote{new Random().Next(0, 1000000).ToString()}";

        private void EditorUpdate(object sender, MarkupString editorText)
        {
           _markupContent = editorText;
            ContentChanged.InvokeAsync(Content);
        }

        protected override async Task<Task> OnInitializedAsync()
        {
            _blazingSummerJsInterop = new BlazingSummerJsInterop(Js, NoteId, EditorUpdate);
            await _blazingSummerJsInterop.Init();
            return base.OnInitializedAsync();
        }

        private async Task Save()
        {
            _edit = false;
            await _blazingSummerJsInterop.Save();
            StateHasChanged();
        }

        private async Task Edit()
        {
            _edit = true;
            var response = await _blazingSummerJsInterop.Edit(_markupContent);
            StateHasChanged();
        }
    }
}
