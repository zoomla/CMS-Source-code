/*
 * ******************************************************************************
 *  jquery.mb.components
 *  file: jquery.mb.containerPlus.js
 *
 *  Copyright (c) 2001-2014. Matteo Bicocchi (Pupunzi);
 *  Open lab srl, Firenze - Italy
 *  email: matteo@open-lab.com
 *  site: 	http://pupunzi.com
 *  blog:	http://pupunzi.open-lab.com
 * 	http://open-lab.com
 *
 *  Licences: MIT, GPL
 *  http://www.opensource.org/licenses/mit-license.php
 *  http://www.gnu.org/licenses/gpl.html
 *
 *  last modified: 04/04/14 0.24
 *  *****************************************************************************
 */


(function(jQuery){

	jQuery.cMethods={};

	jQuery.containerize={
		author:"Matteo Bicocchi",
		version:"3.5.5",
		defaults:{
			containment:"document",
			mantainOnWindow:true,
			effectDuration:100,
			zIndexContext:"auto", // or your selector (ex: ".containerPlus")
			onLoad:function(o){},
			onCollapse:function(o){},
			onBeforeIconize:function(o){},
			onIconize:function(o){},
			onClose: function(o){},
			onBeforeClose: function(o){},
			onResize: function(o,w,h){},
			onDrag: function(o,x,y){},
			onRestore:function(o){},
			onFullScreen:function(o){}
		},
		defaultButtons: {
			close : {
				idx:"close",
				label:"&#x2714;",
				className:"",
				action: function(el){jQuery(el).containerize("close")}
			},

			fullscreen :{
				idx: "fullscreen",
				label: "&#xe04e;",
				className:"",
				action: function(el, btn){
					jQuery(el).containerize("fullScreen");

					if(!el.fullscreen)
						jQuery(btn).html("&#xe04e;")
					else
						jQuery(btn).html("&#xe050;")
				}
			},

			dock : {
				idx: "dock",
				label: "&#xe002;",
				className:"",
				action: function(el, btn){
					var dock = jQuery(el).data("dock");
					jQuery(el).containerize("iconize", dock);
				}
			}
		},

		init:function(opt){
			if(typeof opt === "string"){

				var method=opt;
				delete arguments[0];
				var params = [];

				for (var i=0; i<= arguments.length; i++){
					if(arguments[i])
						params.push(arguments[i]);
				}

				if(jQuery.containerize.methods[method]){
					this.each(function(){
						var r = jQuery.containerize.methods[method].apply(this, params);

						return r;
					});
				}
				return false;
			}

			jQuery(window).on("resize",function(){
				jQuery(".mbc_container").each(function(){
					var el = this;
					el.$.containerize("windowResize");
				})
			});

			return this.each(function(){
				var el= this;
				el.$=jQuery(el);
				el.opt = {};
				jQuery.extend (el.opt, jQuery.containerize.defaults, opt);
				jQuery.containerize.build(el);
				el.$.css("visibility","visible");
			});
		},

		build:function(el){

			var opacity = el.$.css("opacity");

			el.$.css({opacity:0});
			el.id = el.id ? el.id : "mbc_" + new Date().getTime();
			var titleText = el.$.find("h2:first");
			el.$.find("h2:first").remove();
			var HTML = el.$.html();
			el.$.empty();
			el.$.addClass("mbc_container");

			if(el.$.data("icon")){
				var imgIco = jQuery("<img/>").attr("src", el.$.data("icon")).addClass("icon");
				titleText.prepend(imgIco);
				titleText.css({paddingLeft:45})
			}

			var header = jQuery("<div/>").addClass("mbc_header");
			var title = jQuery("<div/>").addClass("mbc_title").html(titleText);
			var buttonBar = jQuery("<div/>").addClass("mbc_buttonBar");
			var toolBar = jQuery("<div/>").addClass("mbc_toolBar");

			var content = jQuery("<div/>").addClass("mbc_content").html(HTML);
			var footer = jQuery("<div/>").addClass("mbc_footer").unselectable();

			header.append(title).append(buttonBar).append(toolBar).addTouch();
			el.$.append(content).append(header).append(footer);

			el.header = header;
			el.containerTitle = title;
			el.footer = footer;
			el.content = content;
			el.toolBar = toolBar;
			el.buttonBar = buttonBar;

			el.$.on("mousedown",function(){
				jQuery(this).mb_bringToFront(el.opt.zIndexContext);
			});

			el.content.on("touchmove",function(e){
				e.originalEvent.stopPropagation();
			});

			if(el.$.data("drag"))
				el.$.addClass("draggable");
			if(el.$.data("resize"))
				el.$.addClass("resizable");

			if(el.$.data("buttons")){
				var buttons = el.$.data("buttons").split(",");
				for(var i in buttons){
					el.$.containerize("addtobuttonbar", jQuery.containerize.defaultButtons[buttons[i]]);
				}
			}

			if(el.$.css("position") == "static"){
				el.$.css("position","relative");
			}

			setTimeout(function(){
				el.$.containerize("adjust");
				jQuery.containerize.applyMethods(el);
				$(el).addTouch();

				if(!el.isClosed)
					el.$.fadeTo(300,opacity, function(){
						if(typeof el.opt.onLoad === "function")
							el.opt.onLoad(el);
					});
				else
					el.$.css({opacity:opacity});

				el.$.trigger("ready");

				jQuery(window).trigger("resize");
			},100);
		},

		applyMethods:function(el, data){
			var properties = el.$.data();

			if(data)
				properties = properties[data];

			for (var els in properties){
				if(typeof jQuery.containerize.methods[els] == "function" && properties[els]){
					var params=[];
					if(typeof properties[els] != "boolean")
						params = properties[els].split(",");
					else
						params.push(properties[els].toString());

					jQuery.containerize.methods[els].apply(el,params);
				}
			}
			return el.$;
		},

		createButton:function(idx, label, action, className){
			return jQuery("<button/>").addClass("mbc_button"+ (className ? " "+ className : "") ).attr("idx", idx).html(label).click(function(e){
				var el = jQuery(this).parents(".mbc_container");
				action(el.get(0), this);
				e.stopPropagation();
				e.preventDefault();
			})
		},

		methods:{
			drag:function(){
				jQuery.cMethods.drag = {name: "drag", author:"pupunzi", type:"built-in"};
				var el = this;
				if(el.$.css("position") == "relative" || el.$.css("position") == "static"){
					el.$.css("position","absolute");
				}
				if(!el.isDraggable){
					el.$.draggable({
						handle:".mbc_header",
						start:function(e,ui){
							ui.helper.addClass("dragging");
						},
						drag:function(){
							if(typeof el.opt.onDrag === "function")
								el.opt.onDrag(el);
							el.$.trigger("drag");
						},
						stop:function(e,ui){
							ui.helper.removeClass("dragging");
							el.$.trigger("dragged");
						},
						containment: el.$.containerize("setContainment")
					});
					el.isDraggable=true;
					return el.$;
				}
			},

			resize:function(){
				jQuery.cMethods.resize = {name: "resize", author:"pupunzi", type:"built-in"};
				var el = this;
				el.position = el.$.data("drag") ?
						(el.$.css("position") == "relative" || el.$.css("position") == "static") ? el.$.css("position","absolute")
								: el.$.css("position") :  el.$.css("position");

				if(!el.isResizable){
					el.$.resizable({
						helper: "mbproxy",
						start:function(e,ui){
							el.$.css("position",el.position);
							var elH= el.$.data("containment")?el.$.parents().height():jQuery(window).height()+jQuery(window).scrollTop();
							var elW= el.$.data("containment")?el.$.parents().width():jQuery(window).width()+jQuery(window).scrollLeft();
							var elPos= el.$.data("containment")? el.$.position():el.$.offset();
							el.$.resizable('option', 'maxHeight',elH-(elPos.top+20));
							el.$.resizable('option', 'maxWidth',elW-(elPos.left+20));
							ui.helper.mb_bringToFront();
						},
						resize:function(){
							if(typeof el.opt.onResize === "function")
								el.opt.onResize(el);
							el.$.trigger("resize");
						},
						stop:function(e,ui){
							var container= ui.element;
							container.containerize("adjust");
							container.containerize("setContainment");
							ui.helper.mb_bringToFront();
							el.$.trigger("resized");
						}
					});
					el.isResizable=true;
				}
				return el.$;
			},

			setContainment:function(containment){
				jQuery.cMethods.setContainment = {name: "setContainment", author:"pupunzi", type:"built-in"};
				var el = this;

				containment = !containment ? el.$.data("containment") : containment;

				if(!containment)
					return false;

				el.$.data("containment", containment);
				if(containment == "document"){
					var dH=(jQuery(document).height()-(el.$.outerHeight()+10));
					var dW=(jQuery(document).width()-(el.$.outerWidth()+10));
					containment= [0,0,dW,dH]; //[x1, y1, x2, y2]
				}
				if(el.$.data("drag") && containment!="" && el.isDraggable){
					el.$.draggable('option', 'containment', containment);
				}
				return containment;
			},

			close:function(animate){
				jQuery.cMethods.close = {name: "close", author:"pupunzi", type:"built-in"};
				var el = this;

				if(el.isClosed)
					return;

				var time= animate ? animate : 0;

				if(typeof el.opt.onBeforeClose === "function")
					el.opt.onBeforeClose(el);

				el.$.fadeOut(time,function(){

					if(typeof el.opt.onClose === "function")
						el.opt.onClose(el);
				});

				el.isClosed=true;

				el.$.trigger("closed");

				return el.$;
			},

			open:function(animate,btf){
				jQuery.cMethods.open = {name: "open", author:"pupunzi", type:"built-in"};
				var el = this;

				var time= animate ? animate : 0;

				if(el.isClosed)
					el.$.fadeIn(time, function(){});

				if(typeof el.opt.onRestore === "function")
					el.opt.onRestore(el);

				el.isClosed=false;


				if(btf)
					el.$.mb_bringToFront(el.opt.zIndexContext);

				el.$.trigger("opened");

				return el.$;
			},

			collapse:function(){
				jQuery.cMethods.collapse = {name: "collapse", author:"pupunzi", type:"built-in"};
				var el = this;

				if(!el.isCollapsed){
					el.h= el.$.outerHeight();
					el.minH = el.$.css("min-height");

					el.$.css("min-height",0);

					el.content.hide();
					el.buttonBar.hide();
					el.footer.hide();

					var height = parseFloat(el.header.outerHeight());
					el.$.animate({height:height},el.opt.effectDuration,function(){
						el.$.containerize("setContainment");
					});

					if(el.isResizable)
						el.$.resizable("disable");

					el.isCollapsed = true;

					if(typeof el.opt.onCollapse === "function")
						el.opt.onCollapse(el);

					el.$.trigger("collapsed");
				}else{
					el.$.animate({height:el.h},el.opt.effectDuration,function(){
						el.$.css("min-height",el.minH);
						el.content.show();
						el.buttonBar.show();
						el.footer.show();

						if(el.isResizable)
							el.$.resizable("enable");
						el.$.containerize("setContainment");

						if(typeof el.opt.onRestore === "function")
							el.opt.onRestore(el);

						el.$.trigger("restored");
					});
					el.isCollapsed = false;
				}
			},

			collapsable: function(){
				jQuery.cMethods.collapsable = {name: "collapsable", author:"pupunzi", type:"built-in"};
				var el = this;

				el.containerTitle.on("dblclick",function(){
					el.$.containerize("collapse");
				})
			},

			skin:function(skin){
				jQuery.cMethods.skin = {name: "skin", author:"pupunzi", type:"built-in"};
				var el = this;
				if(!skin){
					skin = null
				}
				el.$.removeClass(el.$.data("skin"));

				if(skin)
					el.$.addClass(skin);

				el.$.data("skin",skin);
				el.$.trigger("changeSkin");
				el.$.containerize("adjust");

				return el.$;
			},

			adjust:function(){
				jQuery.cMethods.adjust = {name: "adjust", author:"pupunzi", type:"built-in"};
				var el = this;
				var h= parseFloat(el.$.outerHeight()) - parseFloat(el.$.find(".mbc_header").outerHeight()) - parseFloat(el.$.find(".mbc_footer").outerHeight());
				el.$.find(".mbc_content").css({height:h});
				el.$.find(".mbc_content").css({marginTop:parseFloat(el.$.find(".mbc_header").outerHeight())});
				return el.$;
			},

			storeView:function(){
				jQuery.cMethods.storeView = {name: "storeView", author:"pupunzi", type:"built-in"};
				var el = this;
				el.oWidth= el.$.css("width");
				el.oHeight= el.$.css("height");
				el.oTop= el.$.css("top");
				el.oLeft= el.$.css("left");
				return el.$;
			},

			restoreView:function(animate){
				jQuery.cMethods.restoreView = {name: "restoreView", author:"pupunzi", type:"built-in"};
				var el = this;

				el.$.containerize("open");
				el.$.animate({top:el.oTop, left:el.oLeft, width:el.oWidth, height: el.oHeight, opacity:1}, animate ? el.opt.effectDuration : 0, function(){
					el.content.css({overflow:"auto"});
					el.$.containerize("adjust");
				})
				return el.$;
			},

			windowResize:function(){
				jQuery.cMethods.windowResize = {name: "windowResize", author:"pupunzi", type:"built-in"};
				var el = this;
				el.$.containerize("setContainment", el.$.data("containment"))
				return el.$;
			},

			alwaisontop:function(){
				jQuery.cMethods.alwaisontop = {name: "alwaisontop", author:"pupunzi", type:"built-in"};
				var el = this;

				if(el.$.hasClass("alwaysOnTop")){
					el.$.removeClass("alwaysOnTop");
					el.$.mb_bringToFront();
					return;
				}

				el.zi = el.$.css("z-index");
				el.$.css("z-index",100000).addClass("alwaysOnTop");
			},

			draggrid:function(x,y){
				jQuery.cMethods.draggrid = {name: "draggrid", author:"pupunzi", type:"built-in"};
				var el = this;

				if(!x && !y)
					return;

				var grid = [parseFloat(x),parseFloat(y)];

				if(el.$.data("drag"))
					setTimeout(function(){
						el.$.draggable('option', 'grid', grid);
					},1);
				return grid;
			},

			resizegrid:function(x,y){
				jQuery.cMethods.resizegrid = {name: "resizegrid", author:"pupunzi", type:"built-in"};
				var el = this;

				if(!x && !y)
					return;

				var grid = [parseFloat(x),parseFloat(y)];

				if(el.$.data("drag"))
					setTimeout(function(){
						el.$.resizable('option', 'grid', grid);
					},1);
				return grid;
			},

			addtobuttonbar:function(btn, prepend){
				jQuery.cMethods.addtobuttonbar = {name: "addtobuttonbar", author:"pupunzi", type:"built-in"};
				var el = this;

				if(!btn)
					return;

				if(!btn.length){
					var arr = [];
					arr.push(btn);
					btn = arr;
				}

				for (var i=0; i<= btn.length; i++){

					if(btn[i]!=undefined){
						var button = jQuery.containerize.createButton(btn[i].idx, btn[i].label, btn[i].action, btn[i].className);

						if(!prepend)
							el.buttonBar.append(button);
						else
							el.buttonBar.prepend(button);

					}
				}
				return el.$;
			},

			addtotoolbar:function(btn, prepend){
				jQuery.cMethods.addtotoolbar = {name: "addtotoolbar", author:"pupunzi", type:"built-in"};
				var el = this;

				if(!btn.length){
					var arr = [];
					arr.push(btn);
					btn = arr;
				}

				for (var i=0; i<= btn.length; i++){

					if(btn[i]!=undefined){
						var button = jQuery.containerize.createButton(btn[i].idx, btn[i].label, btn[i].action, btn[i].className);

						if(!prepend)
							el.toolBar.append(button);
						else
							el.toolBar.prepend(button);
					}
				}
				return el.$;
			},

			iconize:function(dockId){
				jQuery.cMethods.iconize = {name: "iconize", author:"pupunzi", type:"built-in"};
				var el = this;

				if(el.fullscreen){
					el.$.containerize("fullScreen");
					setTimeout(function(){
						el.$.containerize("iconize", dockId);
					},el.opt.effectDuration);
					return;
				}

				var skin = el.$.data("skin");
				el.$.containerize("storeView");

				if(typeof el.opt.onBeforeIconize === "function")
					el.opt.onBeforeIconize(el);

				var existDock = jQuery("#"+dockId).length>0;
				var isAtInit = el.$.data("iconize");
				el.$.data("iconize", false);

				var t = existDock ? jQuery("#"+dockId).offset().top : el.$.css("top");
				var l = existDock ? jQuery("#"+dockId).offset().left : -10;

				el.content.css({overflow:"hidden"});
				el.$.animate({top:t,left:l,width:0,height:0,opacity:0},isAtInit ? 0 :el.opt.effectDuration, function(){
					jQuery(this).containerize("close");

					var text = el.containerTitle.html();
					if(el.$.data("icon")){
						var imgIco = jQuery("<img/>").attr("src", el.$.data("icon")).addClass("icon");
						text = imgIco;
					}

					if(!existDock){
						el.iconElement = jQuery("<div/>").addClass("containerIcon "+ skin).css({position:"absolute", top:t, left:l});
						var title = jQuery("<span/>").addClass("mbc_title").html(text);

						el.iconElement.append(title);
						jQuery("body").append(el.iconElement);

					}else{

						el.iconElement = jQuery("<span/>").addClass("containerDocked").html(text);
						jQuery("#"+dockId).append(el.iconElement);
					}

					var event = jQuery.Event("iconized");
					event.dock = existDock ? dockId : 0;

					el.$.trigger(event);

					if(typeof el.opt.onIconize === "function")
						el.opt.onIconize(el);

					el.iconElement.on("click",function(){
						jQuery(this).remove();
						el.$.containerize("restoreView",true);
						el.$.mb_bringToFront(el.opt.zIndexContext);
						el.$.trigger("restored");

						if(typeof el.opt.onRestore === "function")
							el.opt.onRestore(el);
					})
				});
				return el.$;
			},

			fullScreen:function(){
				jQuery.cMethods.fullScreen = {name: "fullScreen", author:"pupunzi", type:"built-in"};
				var el = this;

				if(!el.fullscreen){
					if(el.isResizable)
						el.$.resizable("disable");

					if(el.$.data("drag"))
						el.$.draggable("disable");

					el.oWidth= el.$.outerWidth();
					el.oHeight= el.$.outerHeight();
					el.oTop= el.$.position().top;
					el.oLeft= el.$.position().left;
					el.oPos= el.$.css("position");
					el.$.css({top:0,left:0, width:jQuery(window).width(), height:jQuery(window).height(), position:"fixed"});
					el.$.containerize("adjust");
					el.fullscreen=true;

					if(typeof el.opt.onFullScreen === "function")
						el.opt.onFullScreen(el);

				}else{

					if(el.isResizable)
						el.$.resizable("enable");

					if(el.$.data("drag"))
						el.$.draggable("enable");

					el.$.css({top:el.oTop,left:el.oLeft, width:el.oWidth, height:el.oHeight, position: el.oPos});
					el.$.containerize("adjust");
					el.fullscreen=false;

				}
				return el.$;
			},

			rememberme:function(){
				jQuery.cMethods.rememberme = {name: "rememberme", author:"pupunzi", type:"built-in"};
				var el = this;

				el.$.on("resized",function(){
					jQuery.mbCookie.set(el.id+"_w", el.$.outerWidth(),7);
					jQuery.mbCookie.set(el.id+"_h", el.$.outerHeight(),7);
				});

				el.$.on("dragged",function(){
					jQuery.mbCookie.set(el.id+"_t", el.$.css("top"),7);
					jQuery.mbCookie.set(el.id+"_l", el.$.css("left"),7);
				});

				el.$.on("iconized",function(e){
					var dock = e.dock ? e.dock : 0;
					jQuery.mbCookie.set(el.id+"_iconized", dock,7);

				});

				el.$.on("closed",function(){
					jQuery.mbCookie.set(el.id+"_closed", true,7);
					jQuery.mbCookie.remove(el.id+"_opened");
				});

				el.$.on("opened",function(){
					jQuery.mbCookie.set(el.id+"_opened", true,7);
					jQuery.mbCookie.remove(el.id+"_closed");

				});

				el.$.on("centeredOnWindow",function(){
					jQuery.mbCookie.set(el.id+"_t", el.$.css("top"),7);
					jQuery.mbCookie.set(el.id+"_l", el.$.css("left"),7);
				});

				el.$.on("restored",function(){
					jQuery.mbCookie.remove(el.id+"_iconized");
					jQuery.mbCookie.remove(el.id+"_closed");
				});

				el.$.on("changeSkin",function(){
					jQuery.mbCookie.set(el.id+"_skin", el.$.data("skin"),7);
				});

				var w = jQuery.mbCookie.get(el.id+"_w") ? jQuery.mbCookie.get(el.id+"_w") : el.$.css("width");
				var h = jQuery.mbCookie.get(el.id+"_h") ? jQuery.mbCookie.get(el.id+"_h") : el.$.css("height");
				var t = jQuery.mbCookie.get(el.id+"_t") ? jQuery.mbCookie.get(el.id+"_t") : el.$.css("top");
				var l = jQuery.mbCookie.get(el.id+"_l") ? jQuery.mbCookie.get(el.id+"_l") : el.$.css("left");
				el.$.css({width:w, height:h, left:l, top:t});

				el.$.containerize("adjust");


				if(jQuery.mbCookie.get(el.id+"_skin")){
					el.$.containerize("skin", jQuery.mbCookie.get(el.id+"_skin"));
				}

				if(jQuery.mbCookie.get(el.id+"_iconized")){
					el.$.containerize("iconize",jQuery.mbCookie.get(el.id+"_iconized"), el.$.data("skin"));
				}

				if(jQuery.mbCookie.get(el.id+"_opened")){
					el.$.containerize("open",false);
				}

				if(jQuery.mbCookie.get(el.id+"_closed")){
					el.$.containerize("close");
				}

				return el.$;
			},

			centeronwindow:function(anim){
				jQuery.cMethods.centeronwindow = {name: "centeronwindow", author:"pupunzi", type:"built-in"};

				var el=this;
				var nww=jQuery(window).width();
				var nwh=jQuery(window).height();
				var ow=el.$.attr("w") ? el.$.attr("w") : el.$.outerWidth();
				var oh= el.$.attr("h") ? el.$.attr("h") : el.$.outerHeight();
				var l= (nww-ow)/2;
				var t= ((nwh-oh)/2) > 0 ? (nwh-oh) / 2 : 10;

				if (el.$.css("position")!="fixed"){
					el.$.css("position","absolute");

					var lDiff = el.$.offset().left - el.$.position().left;
					var tDiff = el.$.offset().top - el.$.position().top;
					l=l + jQuery(window).scrollLeft()-lDiff;
					t=t + jQuery(window).scrollTop()-tDiff;

				}

				if (anim)
					el.$.animate({top:t,left:l},300,function(){
						el.$.trigger("centeredOnWindow")
					});
				else{
					el.$.css({top:t,left:l});
					el.$.trigger("centeredOnWindow")
				}
				el.$.attr("t",t);
				el.$.attr("l",l);
				return el.$;
			}
		},

		addMethod:function(name, fn){
			jQuery.containerize.methods[name]=fn;
		}

	};

	jQuery.fn.containerize = jQuery.containerize.init;

	jQuery.fn.unselectable=function(){
		return this.each(function(){
			jQuery(this).css({
				"-moz-user-select": "none",
				"-khtml-user-select": "none",
				"user-select": "none"
			}).attr("unselectable","on");
		});
	};

	jQuery.fn.clearUnselectable=function(){
		return this.each(function(){
			jQuery(this).css({
				"-moz-user-select": "auto",
				"-khtml-user-select": "auto",
				"user-select": "auto"
			});
			jQuery(this).removeAttr("unselectable");
		});
	};

	/*COOKIES
	 * -----------------------------------------------------------------*/
	jQuery.mbCookie = {
		set: function(name,value,days, domain) {
			if (!days) days=7;
			domain= domain ?  "; domain="+domain : "";
			var date = new Date(), expires;
			date.setTime(date.getTime()+(days*24*60*60*1000));
			expires = "; expires="+date.toGMTString();
			document.cookie = name + "="+value+expires+"; path=/" + domain;
		},
		get: function(name) {
			var nameEQ = name + "=";
			var ca = document.cookie.split(';');
			for(var i=0;i < ca.length;i++) {
				var c = ca[i];
				while (c.charAt(0)==' ') c = c.substring(1,c.length);
				if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
			}
			return null;
		},
		remove: function(name) {
			jQuery.mbCookie.set(name,"",-1);
		}
	};
	/*-----------------------------------------------------------------*/

	jQuery.fn.addTouch = function(){
		this.each(function(i,el){
			jQuery(el).on('touchstart touchmove touchend touchcancel',function(){
				//we pass the original event object because the jQuery event
				//object is normalized to w3c specs and does not provide the TouchList
				handleTouch(event);
			});
		});

		var handleTouch = function(event){
			var touches = event.changedTouches,
					first = touches[0],
					type = '';
			switch(event.type){
				case 'touchstart':
					type = 'mousedown';
					break;
				case 'touchmove':
					type = 'mousemove';
					event.preventDefault();
					break;
				case 'touchend':
					type = 'mouseup';
					break;
				default:
					return;
			}

			var simulatedEvent = document.createEvent('MouseEvent');
			simulatedEvent.initMouseEvent(type, true, true, window, 1, first.screenX, first.screenY, first.clientX, first.clientY, false, false, false, false, 0/*left*/, null);
			first.target.dispatchEvent(simulatedEvent);

		};
	};

	jQuery.fn.mb_bringToFront= function(zIndexContext){
		var zi=1;
		var els= zIndexContext && zIndexContext!="auto" ? jQuery(zIndexContext): jQuery("*");
		els.not(".alwaysOnTop").each(function() {
			if(jQuery(this).css("position")!="static"){
				var cur = parseInt(jQuery(this).css('zIndex'));
				zi = cur > zi ? parseInt(jQuery(this).css('zIndex')) : zi;
			}
		});
		jQuery(this).not(".alwaysOnTop").css('zIndex',zi+=1);
		return zi;
	};

})(jQuery);
