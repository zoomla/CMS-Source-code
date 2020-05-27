var Dialog = function () {
    var opts = {};
    if (arguments.length > 0)
        opts = arguments[0];
    this.Width = opts.Width;
    this.Height = opts.Height;
    this.Title = opts.Title;
    this.Type = opts.Type;
    this.Content = opts.Content;
    this.Foot = opts.Foot;
    this.obj = undefined;
    this.ifr = undefined;
};
Dialog.prototype = {
    ShowModal: function () {
        var html = "<div id='MyModal' class='modal'><div class='modal-dialog' style='width:@width'><div class='modal-content'><div class='modal-header'>@header</div><div class='modal-body'>@body</div>@Foot</div></div></div>";
        var header = "<button type='button' class='close' data-dismiss='modal'><span class='fa fa-times-circle-o'>Close</span></button>@Opion<span class='modal-title'><strong id='title'>@Title</strong></span>";
        var body = "";
        var Opion = "";
        var foot = "";
        switch (this.Type) {
            case 0:
                body = this.Content;
                break;
            case 1:
                body = "<iframe style='width:100%;border:none;height:" + (this.Height == undefined ? "auto;" : this.Height) + "px" + "' src='" + this.Content + "'></iframe>";
                Opion = "<button type='button' id='BigOpion' title='全屏' class='close' style='width:30px;'><i class='fa fa-arrows-alt'></i></button>";
                break;
        }
        if (this.Foot != "" && this.Foot != undefined) {
            foot = "<div class='modal-footer' style='text-align: center;'>" + this.Foot + "</div>";
        }
        html = html.replace(/@width/, this.Width + "px").replace(/@header/, header).replace(/@body/, body).replace(/@Title/, this.Title).replace(/@Opion/, Opion).replace(/@Foot/,foot);
        
        if ($("#MyModal").length <= 0) {
            $("body").append(html);
        } else {
            $("#MyModal").html($(html).html());
        }
        $("#MyModal").modal({});
        this.obj = $("#MyModal");
        this.ifr = $("#MyModal").find("iframe")[0];
        $("#BigOpion").click(function () {
            window.location = $("#MyModal").find("iframe")[0].contentWindow.location;
        });
    },
    CloseModal: function () {
        $(this.obj).modal("hide");
    }
}