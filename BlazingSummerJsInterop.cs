using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazingComponents.Summernote
{
    internal class BlazingSummerJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        private readonly DotNetObjectReference<BlazingSummerJsInterop> _dotNetObjectReference;
        private readonly string _id;

        public BlazingSummerJsInterop(IJSRuntime jsRuntime, string id, EventHandler<string> onChange)
        {
            _dotNetObjectReference = DotNetObjectReference.Create(this);
            EditorUpdate += onChange;
            JsRuntime = jsRuntime;
            _id = id;
            _moduleTask = new Lazy<Task<IJSObjectReference>>(() =>
                JsRuntime.InvokeAsync<IJSObjectReference>("import",
                    "./_content/BlazingComponents.Summernote/SummernoteInterop.js").AsTask());
        }

        private IJSRuntime JsRuntime { get; }

        public async ValueTask DisposeAsync()
        {
            _dotNetObjectReference.Dispose();
            var x = await _moduleTask.Value;
            await x.DisposeAsync();
        }

        public async Task Init()
        {
            if (_moduleTask == null) return;
            var module = await _moduleTask.Value;
            
            //This can be used to embed the js and css without having to add to the Index.html
            //However due to the loading sequence, the editor cannot be loaded during render 
            //Will leave this here as will try to resolve in the future.
            /*await module.InvokeVoidAsync("init", _id, DotNetObjectReference.Create(this), "OnTextChange");
            await module.InvokeVoidAsync("includeCss");*/
            
            await module.InvokeVoidAsync("edit", _id, DotNetObjectReference.Create(this), "OnTextChange");
        }

        [JSInvokable]
        public async Task<bool> OnTextChange(string editorText)
        {
            EditorUpdate?.Invoke(null, editorText);
            return await Task.FromResult(true);
        }

        public event EventHandler<string> EditorUpdate;

        public async Task<bool> Edit(string content)
        {
            Console.WriteLine("Edit()ModuleTask");
            var module = await _moduleTask.Value;
            Console.WriteLine($"Edit()InvokeVoid {_id}");
            try
            {
                await module.InvokeVoidAsync("edit", _id, DotNetObjectReference.Create(this), "OnTextChange");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public async Task Save()
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("save", _id, DotNetObjectReference.Create(this), "OnTextChange");
        }
    }
}