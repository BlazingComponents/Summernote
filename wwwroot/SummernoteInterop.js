
/*
export function init(){
    import('./summernote/jquery-3.4.1.slim.min.js');
    import('./summernote/summernote-lite.min.js');
}
export function includeCss() {
    var element = document.createElement("link");
    element.setAttribute("rel", "stylesheet");
    element.setAttribute("type", "text/css");
    element.setAttribute("href", "./_content/BlazingComponents.Summernote/summernote/summernote-lite.min.css");//location of the css that we want     include for the page
    document.getElementsByTagName("head")[0].appendChild(element);
}
*/

export function size(id, instance, callback) {
    summernoteOptions.height = 300;
    let content = $('#' + id).summernote('code');
    $('#' + id).summernote('destroy');
    $('#' + id).summernote(summernoteOptions);
    $('#' + id).summernote('code', content);
    console.log("size called" + id)
}
export function edit(id, instance, callback) {
    let snid = '#' + id;
    console.log("edit called" + snid)
    $(snid).summernote({
        placeholder: 'write here...',
        focus: true,
        fontNames: ['Courier New','Times New Roman','Arial', 'Arial Black', 'Comic Sans MS'],
        toolbar: [
            ['edit',['undo','redo']],
            ['style', ['style']],
            ['fontname', ['fontname']],
            ['fontsize', ['fontsize']],
            ['font', ['strikethrough', 'superscript', 'subscript']],
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['table', ['table']],
            ['insert', ['link', 'picture', 'video','hr']],
            ['view', ['fullscreen', 'codeview']]
        ],
        popover: {
            air: [
                ['color', ['color']],
                ['font', ['bold', 'underline', 'clear']]
            ]
        },
        callbacks: {
            onChange: function (contents) {
                instance.invokeMethodAsync(callback, contents);
            }
        }
    });    
}

export function save(id, instance, callback) {
    let snid = '#' + id;
    let content = $(snid).summernote("code");
    instance.invokeMethodAsync(callback, content);
    $(snid).summernote("destroy");
}
export function disable(id) {
    let snid = '#' + id;
    $(snid).summernote('disable');
}
export function enable(id) {
    let snid = '#' + id;
    $(snid).summernote('enable');
}
