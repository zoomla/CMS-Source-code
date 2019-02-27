

function OnWindowsReSize() {
    document.getElementById("divMain").style.height = document.body.clientHeight - 30;
   if ($("td").filter(".divCall").length > 0) {
//        var arr=new Array();
//        var i=0;
//        $("td").filter(".divCall").children("div").filter(function(){
//            arr[i]=$(this).attr("offsetWidth");
//            $(this).css("width", "1");
//            i++;
//        });

        $("td").filter(".divCall").children("div").css("width", "1");
        var tdWidth = $("td").filter(".divCall").attr("offsetWidth");
        //alert(tdWidth);
        $("td").filter(".divCall").children("div").filter(function (){
            var divWidth = tdWidth * parseInt($(this).attr("rel1")) / parseInt($(this).attr("rel2")) + 1;
            $(this).css("width", divWidth);
        });
//        i=0;
//        $("td").filter(".divCall").children("div").filter(function () {
//            var divWidth = tdWidth * parseInt($(this).attr("rel1")) / parseInt($(this).attr("rel2")) + 1;
//            $("td").filter(".divCall").children("div").css("width", arr[i])
//            //alert(divWidth);
//            $(this).animate({
//                width: (divWidth == 1 ? 1 : divWidth - 1)
//            }, 2000);
//            i++;
//        });
    }
}

function OnWindowsLoad() {
    document.getElementById("divMain").style.height = document.body.clientHeight - 30;
    setTimeout(setWidth);
}
function setWidth() {

    if ($("td").filter(".divCall").length > 0) {
        //$("tr").filter(".trRow").addClass("trRowTemp");
        //$("tr").filter(".trRow").removeClass("trRow");
        var tdWidth = $("td").filter(".divCall").attr("offsetWidth");
         
        $("td").filter(".divCall").children("div").css("display", "block");
        $("td").filter(".divCall").children("div").filter(function () {
            var divWidth = tdWidth * parseInt($(this).attr("rel1")) / parseInt($(this).attr("rel2")) + 1;
            $(this).css("width", "1");
            $(this).animate({
                width: (divWidth == 1 ? 1 : divWidth - 1)
            }, 2000,function(){$(this).parent().parent().addClass("trRow");});
        });
    }
}