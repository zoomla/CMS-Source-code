$(function(){
	$("#List1 a").click(function(){
		$("#"+this.id).addClass("selected").siblings().removeClass("selected");
		$('#skinCss').attr("href","/Template/blue/style/"+(this.id)+".css");
		});
	})
	// The second line`s effect is finding the Dom tree "li".
	// The third line:add selected.
	// The forth line:change css.