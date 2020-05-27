/**
*
* name:cnGame.js	
*`author:cson
*`date:2012-2-7
*`version:1.0
*
**/	

(function(win,undefined){
	var canvasPos;
	/**
	*获取canvas在页面的位置
	**/	  
	var getCanvasPos=function(canvas){
		var left = 0;
		var top = 0;
		while (canvas.offsetParent) {
			left += canvas.offsetLeft;
			top += canvas.offsetTop;
			canvas = canvas.offsetParent;

		}
		return [left, top];

	}
	
	var _cnGame={
		/**
		 *初始化
		**/
		init:function(id,options){
			options=options||{};
			this.canvas = this.core.$(id||"canvas");	
			this.context = this.canvas.getContext('2d');
			this.width = options.width||800;
			this.height = options.height||600;
			this.title = this.core.$$('title')[0];
			canvasPos=getCanvasPos(this.canvas);
			this.x=canvasPos[0]||0;
			this.y=canvasPos[1]||0;
			this.canvas.width=this.width;
			this.canvas.height=this.height;
			this.canvas.style.left=this.x +"px";
			this.canvas.style.top=this.y +"px";
			
		},
		/**
		 *生成命名空间,并执行相应操作
		**/
		register:function(nameSpace,func){
			var nsArr=nameSpace.split(".");
			var parent=win;
			for(var i=0,len=nsArr.length;i<len;i++){
				(typeof parent[nsArr[i]]=='undefined')&&(parent[nsArr[i]]={});
				parent=parent[nsArr[i]];
			}
			if(func){
				func.call(parent,this);	
			}
			return parent;
		},
		/**
		 *清除画布
		**/
		clean:function(){
			this.context.clearRect(this.width,this.height);
		}
		
		
		
			
	}
			  
		  	  
	win["cnGame"]=_cnGame;		  
		  
		  



/**
 *
 *基本工具函数
 *
**/
cnGame.register("cnGame.core",function(cg){
		/**
		按id获取元素
		**/
		this.$=function(id){
			return document.getElementById(id);		
		};
		/**
		按标签名获取元素
		**/
		this.$$=function(tagName,parent){
			parent=parent||document;
			return parent.getElementsByTagName(tagName);	
		};
		/**
		按类名获取元素
		**/
		this.$Class=function(className,parent){
			var arr=[],result=[];
			parent=parent||document;
			arr=this.$$("*");
			for(var i=0,len=arr.length;i<len;i++){
				if((" "+arr[i].className+" ").indexOf(" "+className+" ")>0){
					result.push(arr[i]);
				}
			}
			return result;	
		};
		/**
		事件绑定
		**/
		this.bindHandler=(function(){
							
						if(window.addEventListener){
							return function(elem,type,handler){
								elem.addEventListener(type,handler,false);
								
							}
						}
						else if(window.attachEvent){
							return function(elem,type,handler){
								elem.attachEvent("on"+type,handler);
							}
						}
		})();
		/**
		事件解除
		**/
		this.removeHandler=(function(){
						if(window.removeEventListerner){
							return function(elem,type,handler){
								elem.removeEventListerner(type,handler,false);
								
							}
						}
						else if(window.detachEvent){
							return function(elem,type,handler){
								elem.detachEvent("on"+type,handler);
							}
						}
		})();
		/**
		获取事件对象
		**/
		this.getEventObj=function(eve){
			return eve||win.event;
		};
		/**
		获取事件目标对象
		**/
		this.getEventTarget=function(eve){
			var eve=this.getEventObj(eve);
			return eve.target||eve.srcElement;
		};
		/**
		禁止默认行为
		**/
		this.preventDefault=function(eve){
			if(eve.preventDefault){
				eve.preventDefault();
			}
			else{
				eve.returnValue=false;
			}
			
		};
		/**
		获取对象计算的样式
		**/
		this.getComputerStyle = (function() {
			var body=document.body||document.documentElement;
			if(body.currentStyle){
				return function(elem){
					return elem.currentStyle;
				}
			}
			else if(document.defaultView.getComputedStyle){
				return function(elem){
					return document.defaultView.getComputedStyle(elem, null);	
				}
			}
			
		})();
		/**
		是否为undefined
		**/
		this.isUndefined=function(elem){
			return typeof elem==='undefined';
		},
		/**
		是否为数组
		**/
		this.isArray=function(elem){
			return Object.prototype.toString.call(elem)==="[object Array]";
		};
		/**
		是否为Object类型
		**/
		this.isObject=function(elem){
			return elem===Object(elem);
		};
		/**
		是否为字符串类型
		**/
		this.isString=function(elem){
			return Object.prototype.toString.call(elem)==="[object String]";
		};
		/**
		是否为数值类型
		**/
		this.isNum=function(elem){
			return Object.prototype.toString.call(elem)==="[object Number]";
		};
		/**
		 *复制对象属性
		**/
		this.extend=function(destination,source,isCover){
			var isUndefined=this.isUndefined;
			(isUndefined(isCover))&&(isCover=true);
			for(var name in source){
				if(isCover||isUndefined(destination[name])){
					destination[name]=source[name];
				}
			
			}
			return destination;
		};
		/**
		 *原型继承对象
		**/
		this.inherit=function(child,parent){
			var func=function(){};
			func.prototype=parent.prototype;
			child.prototype=new func();
			child.prototype.constructor=child;
			child.prototype.parent=parent;
		};
	
});

/**
 *
 *资源加载器
 *
**/
cnGame.register("cnGame",function(cg){
	
	var file_type = {}
 	file_type["json"] = "json"
 	file_type["wav"] = "audio"
 	file_type["mp3"] = "audio"
	file_type["ogg"] = "audio"
 	file_type["png"] = "image"
	file_type["jpg"] = "image"
 	file_type["jpeg"] = "image"
	file_type["gif"] = "image"
	file_type["bmp"] = "image"
	file_type["tiff"] = "image"
	var postfix_regexp = /\.([a-zA-Z0-9]+)/;
	/**
	 *资源加载完毕的处理程序
	**/	
	var resourceLoad=function(self,type){
		return function(){
			self.loadedCount+=1;
			type=="image"&&(self.loadedImgs[this.srcPath]=this);
			type=="audio"&&(self.loadedAudios[this.srcPath]=this);
			this.onLoad=null;					//保证图片的onLoad执行一次后销毁
			self.loadedPercent=Math.floor(self.loadedCount/self.sum*100);
			self.onLoad&&self.onLoad(self.loadedPercent);
			if(self.loadedPercent===100){
				self.loadedCount=0;
				self.loadedPercent=0;
				type=="image"&&(self.loadingImgs={});
				type=="audio"&&(self.loadingAudios={});
				if(self.gameObj&&self.gameObj.initialize){
					self.gameObj.initialize(self.startOptions);
					if(cg.loop&&!cg.loop.stop){//结束上一个循环
						cg.loop.end();
					}
					cg.loop=new cg.GameLoop(self.gameObj);//开始新游戏循环
					cg.loop.start();
				}	
			}
		}
	}
	
	/**
	 *图像加载器
	**/	
	var loader={
		sum:0,			//图片总数
		loadedCount:0,	//图片已加载数
		loadingImgs:{}, //未加载图片集合
		loadedImgs:{},	//已加载图片集合
		loadingAudios:{},//未加载音频集合
		loadedAudios:{},//已加载音频集合
		/**
		 *图像加载，之后启动游戏
		**/	
		start:function(gameObj,options){//options:srcArray,onload
			var srcArr=options.srcArray;
			this.gameObj=gameObj;
			this.startOptions=options.startOptions;//游戏开始需要的初始化参数
			this.onLoad=options.onLoad;
			cg.spriteList.clean();
			
			if(cg.core.isArray(srcArr)){ 
				this.sum=srcArr.length;
				for(var i=0,len=srcArr.length;i<len;i++){
					var path=srcArr[i];
					var suffix=srcArr[i].substring(srcArr[i].lastIndexOf(".")+1);
					var type=file_type[suffix];
					if(type=="image"){		
						this.loadingImgs[path]=new Image();
						cg.core.bindHandler(this.loadingImgs[path],"load",resourceLoad(this,type));
						this.loadingImgs[path].src=path;
						this.loadingImgs[path].srcPath=path;//没有经过自动变换的src
					}
					else if(type=="audio"){
						this.loadingAudios[path]=new Audio(path);
						cg.core.bindHandler(this.loadingAudios[path],"canplay",resourceLoad(this,type));
						this.loadingAudios[path].onload=resourceLoad(this,type);
						this.loadingAudios[path].src=path;
						this.loadingAudios[path].srcPath=path;//没有经过自动变换的src
					}
				}
					
			}
			
		}
		
	}
	
	
	this.loader=loader;
});




/**
 *
 *canvas基本形状对象
 *
**/
cnGame.register("cnGame.shape",function(cg){

	/**
	 *更新right和bottom
	**/	
	var resetRightBottom=function(elem){
		elem.right=elem.x+elem.width;
		elem.bottom=elem.y+elem.height;	
	}
	/**
	 *矩形对象
	**/										
	var rect=function(options){
		if(!(this instanceof arguments.callee)){
			return new arguments.callee(options);
		}
		this.init(options);
	}
	rect.prototype={
		/**
		 *初始化
		**/
		init:function(options){
			/**
			 *默认值对象
			**/												
			var defaultObj={
				x:0,
				y:0,
				width:100,
				height:100,
				style:"red",
				isFill:true
				
			};
			options=options||{};
			options=cg.core.extend(defaultObj,options);
			this.setOptions(options);
		
			resetRightBottom(this);
		},
		/**
		 *设置参数
		**/	
		setOptions:function(options){
			this.x=options.x||this.x;
			this.y=options.y||this.y;
			this.width=options.width||this.width;
			this.height=options.height||this.height;	
			this.style=options.style||this.style;
			this.isFill=options.isFill||this.isFill;
		},
		/**
		 *绘制矩形
		**/	
		draw:function(){
			var context=cg.context;
			if(this.isFill){
				context.fillStyle = this.style;
				context.fillRect(this.x, this.y, this.width, this.height);
			}
			else{
				context.strokeStyle = this.style;
				context.strokeRect(this.x, this.y, this.width, this.height);
			}
			
  			return this;
			
		},
		/**
		 *将矩形移动一定距离
		**/	
		move:function(dx,dy){
			dx=dx||0;
			dy=dy||0;
			this.x+=dx;
			this.y+=dy;
			resetRightBottom(this);
			return this;
		},
		/**
		 *将矩形移动到特定位置
		**/	
		moveTo:function(x,y){
			x=x||this.x;
			y=y||this.y;
			this.x=x;
			this.y=y;
			resetRightBottom(this);
			return this;
		},
		/**
		 *将矩形改变一定大小
		**/	
		resize:function(dWidth,dHeight){
			dWidth=dWidth||0;
			dHeight=dHeight||0;
			this.width+=dWidth;
			this.height+=dHeight;
			resetRightBottom(this);
			return this;
			
		},
		/**
		 *将矩形改变到特定大小
		**/	
		resizeTo:function(width,height){
			width=width||this.width;
			height=height||this.height;
			this.width=width;
			this.height=height;
			resetRightBottom(this);
			return this;
		}
	}
	
	/**
	 *圆形对象
	**/		
	var circle=function(options){
		if(!(this instanceof arguments.callee)){
			return new arguments.callee(options);
		}
		this.init(options);
	}
	circle.prototype={
		/**
		 *初始化
		**/
		init:function(options){
			/**
			 *默认值对象
			**/
			var defaultObj={
				x:100,
				y:100,
				r:100,
				startAngle:0,
				endAngle:Math.PI*2,
				antiClock:false,
				style:"red",
				isFill:true
			};
			options=options||{};
			options=cg.core.extend(defaultObj,options);
			this.setOptions(options);
		
		},
		/**
		 *设置参数
		**/
		setOptions:function(options){
			this.x=options.x||this.x;
			this.y=options.y||this.y;
			this.r=options.r||this.r;
			this.startAngle=options.startAngle||this.startAngle;
			this.endAngle=options.endAngle||this.endAngle;
			this.antiClock=options.antiClock||this.antiClock;
			this.isFill=options.isFill||this.isFill;
			this.style=options.style||this.style;
		},
		/**
		 *绘制圆形
		**/
		draw:function(){
			var context=cg.context;
			context.beginPath();
			context.arc(this.x,this.y,this.r,this.startAngle,this.endAngle,this.antiClock);
			context.closePath();
			if(this.isFill){
				context.fillStyle=this.style;
				context.fill();
			}
			else{
				context.strokeStyle=this.style;
				context.stroke();
			}
			
		},
		/**
		 *将圆形移动一定距离
		**/	
		move:function(dx,dy){
			dx=dx||0;
			dy=dy||0;
			this.x+=dx;
			this.y+=dy;
			return this;
		},
		/**
		 *将圆形移动到特定位置
		**/	
		moveTo:function(x,y){
			x=x||this.x;
			y=y||this.y;
			this.x=x;
			this.y=y;
			return this;
		},
		/**
		 *将圆形改变一定大小
		**/	
		resize:function(dr){
			dr=dr||0;
			this.r+=dr;
			return this;
			
		},
		/**
		 *将圆形改变到特定大小
		**/	
		resizeTo:function(r){
			r=r||this.r;
			this.r=r;
			return this;
		}	
	}
	/**
	 *将圆形改变到特定大小
	**/	
	var text=function(text,options){
		if(!(this instanceof arguments.callee)){
			return new arguments.callee(text,options);
		}
		this.init(text,options);
	
	}
	text.prototype={
		/**
		 *初始化
		**/
		init:function(text,options){
			/**
			 *默认值对象
			**/
			var defaultObj={
				x:100,
				y:100,
				style:"red",
				isFill:true
				
			};
			options=options||{};
			options=cg.core.extend(defaultObj,options);
			this.setOptions(options);
			this.text=text;		
		},
		/**
		*绘制
		**/
		draw:function(){
			var context=cg.context;
			(!cg.core.isUndefined(this.font))&&(context.font=this.font);
			(!cg.core.isUndefined(this.textBaseline))&&(context.textBaseline=this.textBaseline);
			(!cg.core.isUndefined(this.textAlign))&&(context.textAlign=this.textAlign);
			(!cg.core.isUndefined(this.maxWidth))&&(context.maxWidth=this.maxWidth);
			if(this.isFill){
				context.fillStyle=this.style;
				this.maxWidth?context.fillText(this.text,this.x,this.y,this.maxWidth):context.fillText(this.text,this.x,this.y);
			}
			else{
				context.strokeStyle=this.style;
				this.maxWidth?context.strokeText(this.text,this.x,this.y,this.maxWidth):context.strokeText(this.text,this.x,this.y);
			}
		},
		/**
		*设置参数
		**/
		setOptions:function(options){
			this.x=options.x||this.x;
			this.y=options.y||this.y;
			this.maxWidth=options.maxWidth||this.maxWidth;
			this.font=options.font||this.font;
			this.textBaseline=options.textBaseline||this.textBaseline;
			this.textAlign=options.textAlign||this.textAlign;
			this.isFill=options.isFill||this.isFill;
			this.style=options.style||this.style;
			
		}
	}
	
	this.Text=text;
	this.Rect=rect;
	this.Circle=circle;
	
});



/**
 *
 *输入记录模块
 *
**/
cnGame.register("cnGame.input",function(cg){
											
	this.mouseX=0;
	this.mouseY=0;
	/**
	 *记录鼠标在canvas内的位置
	**/	
	var recordMouseMove=function(eve){
		var pageX,pageY,x,y;
		eve=cg.core.getEventObj(eve);
	    pageX = eve.pageX || eve.clientX + document.documentElement.scrollLeft - document.documentElement.clientLeft;
		pageY = eve.pageY || eve.clientY + document.documentElement.scrollTop - document.documentElement.clientTop;
		cg.input.mouseX=pageX-cg.x;
		cg.input.mouseY=pageY-cg.y;
	}		
	
	cg.core.bindHandler(window,"mousemove",recordMouseMove);
	
	/**
	 *被按下的键的集合
	**/	
	var pressed_keys={};
	/**
	 *要求禁止默认行为的键的集合
	**/	
	var preventDefault_keys={};
	/**
	 *键盘按下触发的处理函数
	**/	
	var keydown_callbacks={};
	/**
	 *键盘弹起触发的处理函数
	**/	
	var keyup_callbacks={};

	
	/**
	 *键盘按键编码和键名
	**/	
	var k=[];
	k[8] = "backspace"
	k[9] = "tab"
	k[13] = "enter"
	k[16] = "shift"
	k[17] = "ctrl"
	k[18] = "alt"
	k[19] = "pause"
	k[20] = "capslock"
	k[27] = "esc"
	k[32] = "space"
	k[33] = "pageup"
	k[34] = "pagedown"
	k[35] = "end"
	k[36] = "home"
	k[37] = "left"
	k[38] = "up"
	k[39] = "right"
	k[40] = "down" 
	k[45] = "insert"
	k[46] = "delete"
	
	k[91] = "leftwindowkey"
	k[92] = "rightwindowkey"
	k[93] = "selectkey"
	k[106] = "multiply"
	k[107] = "add"
	k[109] = "subtract"
	k[110] = "decimalpoint"
	k[111] = "divide"
	
	k[144] = "numlock"
	k[145] = "scrollock"
	k[186] = "semicolon"
	k[187] = "equalsign"
	k[188] = "comma"
	k[189] = "dash"
	k[190] = "period"
	k[191] = "forwardslash"
	k[192] = "graveaccent"
	k[219] = "openbracket"
	k[220] = "backslash"
	k[221] = "closebracket"
	k[222] = "singlequote"
	
	var numpadkeys = ["numpad1","numpad2","numpad3","numpad4","numpad5","numpad6","numpad7","numpad8","numpad9"]
	var fkeys = ["f1","f2","f3","f4","f5","f6","f7","f8","f9"]
	var numbers = ["0","1","2","3","4","5","6","7","8","9"]
	var letters = ["a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"]
	for(var i = 0; numbers[i]; i++)     { k[48+i] = numbers[i] }
	for(var i = 0; letters[i]; i++)     { k[65+i] = letters[i] }
	for(var i = 0; numpadkeys[i]; i++)  { k[96+i] = numpadkeys[i] }
	for(var i = 0; fkeys[i]; i++)       { k[112+i] = fkeys[i] }
	
	/**
	 *记录键盘按下的键
	**/	
	var recordPress=function(eve){
		eve=cg.core.getEventObj(eve);
		var keyName=k[eve.keyCode];
		pressed_keys[keyName]=true;	
		if(keydown_callbacks[keyName]){
			for(var i=0,len=keydown_callbacks[keyName].length;i<len;i++){
				keydown_callbacks[keyName][i]();
				
			}
		
		}
		if(keydown_callbacks["allKeys"]){
			for(var i=0,len=keydown_callbacks["allKeys"].length;i<len;i++){
				keydown_callbacks["allKeys"][i]();
				
			}
		}
		if(preventDefault_keys[keyName]){
			cg.core.preventDefault(eve);
		}
	}
	/**
	 *记录键盘松开的键
	**/	
	var recordUp=function(eve){
		eve=cg.core.getEventObj(eve);
		var keyName=k[eve.keyCode];
		pressed_keys[keyName]=false;
		if(keyup_callbacks[keyName]){
			for(var i=0,len=keyup_callbacks[keyName].length;i<len;i++){
				keyup_callbacks[keyName][i]();
				
			}	
		}
		if(keyup_callbacks["allKeys"]){
			for(var i=0,len=keyup_callbacks["allKeys"].length;i<len;i++){
				keyup_callbacks["allKeys"][i]();
				
			}
		}
		if(preventDefault_keys[keyName]){
			cg.core.preventDefault(eve);
		}
	}
	cg.core.bindHandler(window,"keydown",recordPress);
	cg.core.bindHandler(window,"keyup",recordUp);
	
	/**
	 *判断某个键是否按下
	**/	
	this.isPressed=function(keyName){
		return !!pressed_keys[keyName];	
	};
	/**
	 *禁止某个键按下的默认行为
	**/	
	this.preventDefault=function(keyName){
		if(cg.core.isArray(keyName)){
			for(var i=0,len=keyName.length;i<len;i++){
				arguments.callee.call(this,keyName[i]);
			}
		}
		else{
			preventDefault_keys[keyName]=true;
		}
	}
	/**
	 *绑定键盘按下事件
	**/	
	this.onKeyDown=function(keyName,handler){
		keyName=keyName||"allKeys";
		if(cg.core.isUndefined(keydown_callbacks[keyName])){
			keydown_callbacks[keyName]=[];							
		}
		keydown_callbacks[keyName].push(handler);
	
	}
	/**
	 *绑定键盘弹起事件
	**/	
	this.onKeyUp=function(keyName,handler){
		keyName=keyName||"allKeys";
		if(cg.core.isUndefined(keyup_callbacks[keyName])){
			keyup_callbacks[keyName]=[];							
		}
		keyup_callbacks[keyName].push(handler);
	
	}
	/**
	 *清除键盘按下事件处理程序
	**/	
	this.clearDownCallbacks=function(keyName){
		if(keyName){
			keydown_callbacks[keyName]=[];
		}
		else{
			keydown_callbacks={};
		}
		
	}
	/**
	 *清除键盘弹起事件处理程序
	**/	
	this.clearUpCallbacks=function(keyName){
		if(keyName){
			keyup_callbacks[keyName]=[];
		}
		else{
			keyup_callbacks={};
		}
		
	}											
});
		
/**
 *
 *碰撞检测
 *
**/
cnGame.register("cnGame.collision",function(cg){
	/**
	 *点和矩形间的碰撞
	**/	
	this.col_Point_Rect=function(pointX,pointY,rectObj){
		return (pointX>=rectObj.x&&pointX<=rectObj.right||pointY>=rectObj.y&&pointY<=rectObj.bottom);		
	}
	/**
	 *矩形和矩形间的碰撞
	**/	
	this.col_Between_Rects=function(rectObjA,rectObjB){
		return ((rectObjA.right>=rectObjB.x&&rectObjA.right<=rectObjB.right||rectObjA.x>=rectObjB.x&&rectObjA.x<=rectObjB.right)&&(rectObjA.bottom>=rectObjB.y&&rectObjA.bottom<=rectObjB.bottom||rectObjA.y<=rectObjB.bottom&&rectObjA.bottom>=rectObjB.y));		
	}
	/**
	 *点和圆形间的碰撞
	**/	
	this.col_Point_Circle=function(pointX,pointY,circleObj){
		return(Math.pow((pointX-circleObj.x),2)+Math.pow((pointY-circleObj.y),2)<Math.pow(circleObj.r,2));
		
	}
	/**
	 *圆形和圆形间的碰撞
	**/	
	this.col_between_Circles=function(circleObjA,circleObjB){
		return(Math.pow((circleObjA.x-circleObjB.x),2)+Math.pow((circleObjA.y-circleObjB.y),2)<Math.pow((circleObjA.r+circleObjB).r,2));
		
	}
										
});

/**
 *
 *动画
 *
**/
cnGame.register("cnGame",function(cg){
	
	/**
	 *帧的增量
	**/
	var path=1;
		
	/**
	 *获取帧集合
	**/
	var caculateFrames=function(options){
		var frames=[];
		var width=options.width;
		var height=options.height;
		var beginX=options.beginX;
		var beginY=options.beginY;
		var frameSize=options.frameSize;
		var direction=options.direction;
		var x,y;
		/* 保存每一帧的精确位置 */
		if(direction=="right"){
			for(var y=beginY;y<height;y+=frameSize[1]){
				for(var x=beginX;x<width;x+=frameSize[0]){
					var frame={};
					frame.x=x;
					frame.y=y;
					frames.push(frame);
				
				}
				
			}
		}
		else{
			for(var x=beginX;x<width;x+=frameSize[0]){
				for(var y=beginY;y<height;y+=frameSize[1]){
					var frame={};
					frame.x=x;
					frame.y=y;
					frames.push(frame);
				
				}
				
			}		
			
		}
		return frames;
		
	}
	/**
	 *包含多帧图像的大图片
	**/	
	spriteSheet=function(id,src,options){
		if(!(this instanceof arguments.callee)){
			return new arguments.callee(id,src,options);
		}
		this.init(id,src,options);
	}
	spriteSheet.prototype={
		/**
		 *初始化
		**/
		init:function(id,src,options){
			
			/**
			 *默认对象
			**/	
			var defaultObj={
				x:0,
				y:0,
				width:120,
				height:40,
				frameSize:[40,40],
				frameDuration:100,
				direction:"right",	//从左到右
				beginX:0,
				beginY:0,
				loop:false,
				bounce:false		
			};
			options=options||{};
			options=cg.core.extend(defaultObj,options);
			this.id=id;									//spriteSheet的id
			this.src=src;								//图片地址
			this.x=options.x;							//动画X位置
			this.y=options.y;							//动画Y位置
			this.width=options.width;					//图片的宽度
			this.height=options.height;					//图片的高度
			this.image=cg.loader.loadedImgs[this.src]; //图片对象
			this.frameSize=options.frameSize;			//每帧尺寸
			this.frameDuration=options.frameDuration;	//每帧持续时间
			this.direction=options.direction;			//读取帧的方向（从做到右或从上到下）
			this.currentIndex=0;						//目前帧索引
			this.beginX=options.beginX;					//截取图片的起始位置X
			this.beginY=options.beginY;					//截图图片的起始位置Y
			this.loop=options.loop;						//是否循环播放
			this.bounce=options.bounce;					//是否往返播放
			this.onFinish=options.onFinish;				//播放完毕后的回调函数
			this.frames=caculateFrames(options);		//帧信息集合
			this.now=new Date().getTime();				//当前时间
			this.last=new Date().getTime();			//上一帧开始时间
		},
		/**
		 *更新帧
		**/	
		update:function(){
			
			this.now=new Date().getTime();
			var frames=this.frames;
			if((this.now-this.last)>this.frameDuration){//如果间隔大于帧间间隔，则update
				var currentIndex=this.currentIndex;
				var length=this.frames.length;
				this.last=this.now;
				
				if(currentIndex>=length-1){
					if(this.loop){	//循环
						return frames[this.currentIndex=0];	
					}
					else if(!this.bounce){//没有循环并且没有往返滚动，则停止在最后一帧
						this.onFinish&&this.onFinish();
						this.onFinish=undefined;
						return frames[currentIndex];
					}
				}
				if((this.bounce)&&((currentIndex>=length-1&&path>0)||(currentIndex<=0&&path<0))){	//往返
					path*=(-1);
				}
				this.currentIndex+=path;
				
			}
			return frames[this.currentIndex];
		},
		/**
		 *跳到特定帧
		**/
		index:function(index){
			this.currentIndex=index;
			return this.frames[this.currentIndex];	
		},
		/**
		 *获取现时帧
		**/
		getCurrentFrame:function(){
			return this.frames[this.currentIndex];	
		},
		/**
		 *在特定位置绘制该帧
		**/
		draw:function(){
			
			var currentFrame=this.getCurrentFrame();
			var width=this.frameSize[0];
			var height = this.frameSize[1];
			cg.context.drawImage(this.image,currentFrame.x,currentFrame.y,width,height,this.x,this.y,width,height);
		}
		
	}
	this.SpriteSheet=spriteSheet;
										
});

/**
 *
 *sprite对象
 *
**/
cnGame.register("cnGame",function(cg){
								  
	var postive_infinity=Number.POSITIVE_INFINITY;			
	
	var sprite=function(id,options){
		if(!(this instanceof arguments.callee)){
			return new arguments.callee(id,options);
		}
		this.init(id,options);
	}
	sprite.prototype={
		/**
		 *初始化
		**/
		init:function(options){
			
			/**
			 *默认对象
			**/	
			var defaultObj={
				x:0,
				y:0,
				imgX:0,
				imgY:0,
				width:32,
				height:32,
				angle:0,
				speedX:0,
				speedY:0,
				rotateSpeed:0,
				aR:0,
				aX:0,
				aY:0,
				maxSpeedX:postive_infinity,
				maxSpeedY:postive_infinity,
				maxX:postive_infinity,
				maxY:postive_infinity,
				minX:-postive_infinity,
				minY:-postive_infinity,
				minAngle:-postive_infinity,
				maxAngle:postive_infinity
			};
			options=options||{};
			options=cg.core.extend(defaultObj,options);
			this.x=options.x;
			this.y=options.y;
			this.angle=options.angle;
			this.width=options.width;
			this.height=options.height;
			this.angle=options.angle;
			this.speedX=options.speedX;
			this.speedY=options.speedY;
			this.rotateSpeed=options.rotateSpeed;
			this.aR=options.aR;
			this.aX=options.aX;
			this.aY=options.aY;
			this.maxSpeedX=options.maxSpeedX;
			this.maxSpeedY=options.maxSpeedY;
			this.maxX=options.maxX;
			this.maxY=options.maxY;
			this.maxAngle=options.maxAngle;
			this.minAngle=options.minAngle;
			this.minX=options.minX;
			this.minY=options.minY;
			this.spriteSheetList={};
			
			if(options.src){	//传入图片路径
				this.setCurrentImage(options.src,options.imgX,options.imgY);
			}
			else if(options.spriteSheet){//传入spriteSheet对象
				this.addAnimation(options.spriteSheet);		
				setCurrentAnimation(options.spriteSheet);
			}
			
		},
		/**
		 *返回包含该sprite的矩形对象
		**/
		getRect:function(){
			return new cg.shape.Rect({x:this.x,y:this.y,width:this.width,height:this.height});
			
		},
		/**
		 *添加动画
		**/
		addAnimation:function(spriteSheet){
			this.spriteSheetList[spriteSheet.id]=spriteSheet;	
		},
		/**
		 *设置当前显示动画
		**/
		setCurrentAnimation:function(id){//可传入id或spriteSheet
			if(!this.isCurrentAnimation(id)){
				if(cg.core.isString(id)){
					this.spriteSheet=this.spriteSheetList[id];
					this.image=this.imgX=this.imgY=undefined;
				}
				else if(cg.core.isObject(id)){
					this.spriteSheet=id;
					this.addAnimation(id);
					this.image=this.imgX=this.imgY=undefined;
				}
			}
		
		},
		/**
		 *判断当前动画是否为该id的动画
		**/
		isCurrentAnimation:function(id){
			if(cg.core.isString(id)){
				return (this.spriteSheet&&this.spriteSheet.id===id);
			}
			else if(cg.core.isObject(id)){
				return this.spriteSheet===id;
			}
		},
		/**
		 *设置当前显示图像
		**/
		setCurrentImage:function(src,imgX,imgY){
			if(!this.isCurrentImage(src,imgX,imgY)){
				imgX=imgX||0;
				imgY=imgY||0;
				this.image=cg.loader.loadedImgs[src];	
				this.imgX=imgX;
				this.imgY=imgY;	
				this.spriteSheet=undefined;
			}
		},
		/**
		 *判断当前图像是否为该src的图像
		**/
		isCurrentImage:function(src,imgX,imgY){
			imgX=imgX||0;
			imgY=imgY||0;
			var image=this.image;
			if(cg.core.isString(src)){
				return (image&&image.srcPath===src&&this.imgX===imgX&&this.imgY===imgY);
			}
		},
			/**
		 *设置移动参数
		**/
		setMovement:function(options){
			isUndefined=cg.core.isUndefined;
			isUndefined(options.speedX)?this.speedX=this.speedX:this.speedX=options.speedX;
			isUndefined(options.speedY)?this.speedY=this.speedY:this.speedY=options.speedY;
			isUndefined(options.rotateSpeed)?this.rotateSpeed=this.rotateSpeed:this.rotateSpeed=options.rotateSpeed;
			isUndefined(options.aX)?this.aR=this.aR:this.aR=options.aR;
			isUndefined(options.aX)?this.aX=this.aX:this.aX=options.aX;
			isUndefined(options.aY)?this.aY=this.aY:this.aY=options.aY;
			isUndefined(options.maxX)?this.maxX=this.maxX:this.maxX=options.maxX;
			isUndefined(options.maxY)?this.maxY=this.maxY:this.maxY=options.maxY;
			isUndefined(options.maxAngle)?this.maxAngle=this.maxAngle:this.maxAngle=options.maxAngle;
			isUndefined(options.minAngle)?this.minAngle=this.minAngle:this.minAngle=options.minAngle;
			isUndefined(options.minX)?this.minX=this.minX:this.minX=options.minX;
			isUndefined(options.minY)?this.minY=this.minY:this.minY=options.minY;
			isUndefined(options.maxSpeedX)?this.maxSpeedX=this.maxSpeedX:this.maxSpeedX=options.maxSpeedX;	
			isUndefined(options.maxSpeedY)?this.maxSpeedY=this.maxSpeedY:this.maxSpeedY=options.maxSpeedY;	
			
			
		},
		/**
		 *重置移动参数回到初始值
		**/
		resetMovement:function(){
			this.speedX=0;
			this.speedY=0;
			this.rotateSpeed=0;
			this.aX=0;
			this.aY=0;
			this.aR=0;
			this.maxSpeedX=postive_infinity;
			this.maxSpeedY=postive_infinity;
			this.maxX=postive_infinity;
			this.minX=-postive_infinity;
			this.maxY=postive_infinity;
			this.minY=-postive_infinity;
			this.maxAngle=postive_infinity;
			this.minAngle=-postive_infinity;
		},
			/**
		 *更新位置和帧动画
		**/
		update:function(duration){//duration:该帧历时 单位：秒
			this.speedX=this.speedX+this.aX*duration;	
			if(this.maxSpeedX<0){
				this.maxSpeedX*=-1;
			}
			if(this.speedX<0){
				this.speedX=Math.max(this.speedX,this.maxSpeedX*-1)	;
			}
			else{
				this.speedX=Math.min(this.speedX,this.maxSpeedX);
			}
	
			this.speedY=this.speedY+this.aY*duration;	
			if(this.maxSpeedY<0){
				this.maxSpeedY*=-1;
			}
			if(this.speedY<0){
				this.speedY=Math.max(this.speedY,this.maxSpeedY*-1)	;
			}
			else{
				this.speedY=Math.min(this.speedY,this.maxSpeedY);
			}
			this.rotateSpeed=this.rotateSpeed+this.aR*duration;	
		
			this.rotate(this.rotateSpeed).move(this.speedX,this.speedY);
		
			if(this.spriteSheet){//更新spriteSheet动画
				this.spriteSheet.x=this.x
				this.spriteSheet.y=this.y;
				this.spriteSheet.update();
			}
		},
		/**
		 *绘制出sprite
		**/
		draw:function(){
			var context=cg.context;
			var halfWith;
			var halfHeight;
			if(this.spriteSheet){
				this.spriteSheet.x=this.x
				this.spriteSheet.y=this.y;
				this.spriteSheet.draw();
			}
			else if(this.image){
				context.save()
				halfWith=this.width/2;
				halfHeight=this.height/2;
				context.translate(this.x+halfWith, this.y+halfHeight);
				context.rotate(this.angle * Math.PI / 180*-1);
				context.drawImage(this.image,this.imgX,this.imgY,this.width,this.height,-halfWith,-halfHeight,this.width,this.height);
				context.restore();
			}
		
		},
		/**
		 *移动一定距离
		**/
		move:function(dx,dy){
			dx=dx||0;
			dy=dy||0;
			var x=this.x+dx;
			var y=this.y+dy;
			this.x=Math.min(Math.max(this.minX,x),this.maxX);
			this.y=Math.min(Math.max(this.minY,y),this.maxY);
			return this;
			
		},
		/**
		 *移动到某处
		**/
		moveTo:function(x,y){
			this.x=Math.min(Math.max(this.minX,x),this.maxX);
			this.y=Math.min(Math.max(this.minY,y),this.maxY);
			return this;
		},
		/**
		 *旋转一定角度
		**/
		rotate:function(da){
			da=da||0;
			var angle=this.angle+da;
			
			this.angle=Math.min(Math.max(this.minAngle,angle),this.maxAngle);
			return this;
		},
		/**
		 *旋转到一定角度
		**/
		rotateTo:function(a){
			this.angle=Math.min(Math.max(this.minAngle,a),this.maxAngle);
			return this;
			
		},
		/**
		 *改变一定尺寸
		**/
		resize:function(dw,dh){
			this.width+=dw;
			this.height+=dh;
			return this;
		},
		/**
		 *改变到一定尺寸
		**/
		resizeTo:function(width,height){
			this.width=width;
			this.height=height;
			return this;
		}
		
	}
	this.Sprite=sprite;							  
								  
});
/**
 *
 *sprite列表
 *
**/
cnGame.register("cnGame",function(cg){
								  
	var spriteList={
		length:0,
		add:function(sprite){
			Array.prototype.push.call(this,sprite);
		},
		remove:function(sprite){
			for(var i=0,len=this.length;i<len;i++){
				if(this[i]===sprite){
					Array.prototype.splice.call(this,i,1);
				}
			}
		},
		clean:function(){
			for(var i=0,len=this.length;i<len;i++){
				Array.prototype.pop.call(this);
			}	
		}
	}
	this.spriteList=spriteList;
});

/**
 *
 *游戏循环
 *
**/
cnGame.register("cnGame",function(cg){

	var timeId;
	var interval;
	/**
	*循环方法
	**/	
	var loop=function(){
		var self=this;
		return function(){
			if(!self.pause&&!self.stop){
				var now=new Date().getTime();
				var duration=(now-self.lastTime)/1000;//帧历时
				var spriteList=cg.spriteList;
				self.loopDuration=(self.startTime-self.now)/1000;
		
				if(self.gameObj.update){//调用游戏对象的update
					self.gameObj.update(duration);
				}
				if(self.gameObj.draw){
					cg.context.clearRect(0,0,cg.width,cg.height);
					self.gameObj.draw();
				}
				for(var i=0,len=spriteList.length;i<len;i++){//更新所有sprite
				
					spriteList[i].update(duration);
					spriteList[i].draw();
				}
				self.lastTime=now;
			}
			timeId=window.setTimeout(arguments.callee,interval);
		}
	}
	
	var gameLoop=function(gameObj,options){
	
		if(!(this instanceof arguments.callee)){
			return new arguments.callee(gameObj,options);
		}
		this.init(gameObj,options);	
	}
	gameLoop.prototype={
		/**
		 *初始化
		**/
		init:function(gameObj,options){
			/**
			 *默认对象
			**/	
			var defaultObj={
				fps:30
			};
			options=options||{};
			
			options=cg.core.extend(defaultObj,options);
			this.gameObj=gameObj;
			this.fps=options.fps;
			interval=1000/this.fps;
			
			this.pause=false;
			this.stop=true;
		},
			
		/**
		 *开始循环
		**/	
		start:function(){
			if(this.stop){		//如果是结束状态则可以开始
				this.stop=false;
				var now=new Date().getTime();
				this.startTime=now;
				this.lastTime=now;
				this.loopDuration=0;	
				loop.call(this)();	
			}	
		},		/**
		 *继续循环
		**/	
		run:function(){
			this.pause=false;	
		},
		/**
		 *暂停循环
		**/	
		pause:function(){
			this.pause=true;	
		},
		/**
		 *停止循环
		**/	
		end:function(){
			this.stop=true;
			window.clearTimeout(timeId);
		}
		
		
	}
	this.GameLoop=gameLoop;
});

/**
 *
 *地图
 *
**/
cnGame.register("cnGame",function(cg){
							  							  
	var map=function(mapMatrix,options){
		
		if(!(this instanceof arguments.callee)){
			return new arguments.callee(mapMatrix,options);
		}
		this.init(mapMatrix,options);
	}
	map.prototype={
		/**
		 *初始化
		**/	
		init:function(mapMatrix,options){
			/**
			 *默认对象
			**/	
			var defaultObj={
				cellSize:[32,32],   //方格宽，高
				beginX:0,		    //地图起始x
				beginY:0			//地图起始y
		
			};
			options=options||{};
			options=cg.core.extend(defaultObj,options);
			this.mapMatrix=mapMatrix;
			this.cellSize=options.cellSize;
			this.beginX=options.beginX;
			this.beginY=options.beginY;
			this.row=mapMatrix.length;//有多少行
				
		},
		/**
		 *根据map矩阵绘制map
		**/	
		draw:function(options){//options：{"1":{src:"xxx.png",x:0,y:0},"2":{src:"xxx.png",x:1,y:1}}
			var mapMatrix=this.mapMatrix;
			var beginX=this.beginX;
			var beginY=this.beginY;
			var cellSize=this.cellSize;
			var currentRow;
			var currentCol
			var currentObj;
			var row=this.row;
			var img;
			for(var i=beginY,ylen=beginY+row*cellSize[1];i<ylen;i+=cellSize[1]){	//根据地图矩阵，绘制每个方格
					currentRow=(i-beginY)/cellSize[1];
				for(var j=beginX,xlen=beginX+mapMatrix[currentRow].length*cellSize[0];j<xlen;j+=cellSize[0]){
					currentCol=(j-beginX)/cellSize[0];
					currentObj=options[mapMatrix[currentRow][currentCol]];
					currentObj.x=currentObj.x||0;
					currentObj.y=currentObj.y||0;
					img=cg.loader.loadedImgs[currentObj.src];
					cg.context.drawImage(img,currentObj.x,currentObj.y,cellSize[0],cellSize[1],j,i,cellSize[0],cellSize[1]);//绘制特定坐标的图像
				}
			}
		
		},
		/**
		 *获取特定对象在地图中处于的方格的值
		**/
		getPosValue:function(x,y){
			if(cg.core.isObject(x)){
				y=x.y;
				x=x.x;
			}
			var isUndefined=cg.core.isUndefined;
			y=Math.floor(y/this.cellSize[1]);
			x=Math.floor(x/this.cellSize[0]);
			if(!isUndefined(this.mapMatrix[y])&&!isUndefined(this.mapMatrix[y][x])){
				return this.mapMatrix[y][x];
			}
			return undefined;
		},
		/**
		 *获取特定对象在地图中处于的方格索引
		**/
		getCurrentIndex:function(x,y){
			if(cg.core.isObject(x)){
				y=x.y;
				x=x.x;
			}
			return [Math.floor(x/this.cellSize[0]),Math.floor(y/this.cellSize[1])];
		},
		/**
		 *获取特定对象是否刚好与格子重合
		**/
		isMatchCell:function(x,y){
			if(cg.core.isObject(x)){
				y=x.y;
				x=x.x;
			}
			return (x%this.cellSize[0]==0)&&(y%this.cellSize[1]==0);
		},
		/**
		 *设置地图对应位置的值
		**/
		setPosValue:function(x,y,value){
			this.mapMatrix[y][x]=value;	
		}
		
	}
	this.Map=map;
									   
});

/**
 *
 *场景
 *
**/
cnGame.register("cnGame",function(cg){
	
	/**
	 *使指定对象在可视区域view内
	**/
	var inside=function(sprite){
		var dir=sprite.insideDir;
		if(dir!="y"){
			if(sprite.x<0){
				sprite.x=0;
			}
			else if(sprite.x>this.width-sprite.width){
				sprite.x=this.width-sprite.width;
			}
		}
		if(dir!="x"){
			if(sprite.y<0){
				sprite.y=0;
			}
			else if(sprite.y>this.height-sprite.height){
				sprite.y=this.height-sprite.height;
			}
		}
			
	}
	
	var view=function(options){
		this.init(options);
		
	}
	view.prototype={
	
		/**
		 *初始化
		**/
		init:function(options){
			/**
			 *默认对象
			**/
			var defaultObj={
				width:cg.width,
				height:cg.height,
				imgWidth:cg.width,
				imgHeight:cg.height,
				x:0,
				y:0
				
			}
			options=options||{};
			options=cg.core.extend(defaultObj,options);
			this.player=options.player;
			this.width=options.width;
			this.height=options.height;
			this.imgWidth=options.imgWidth;
			this.imgHeight=options.imgHeight;
			this.centerX=this.width/2;
			this.src=options.src;
			this.x=options.x;
			this.y=options.y;
			this.insideArr=[];
			this.isLoop=false;;
			this.isCenterPlayer=false;
			this.onEnd=options.onEnd;
			
		},
		/**
		 *使player的位置保持在场景中点之前的移动背景
		**/
		centerPlayer:function(isLoop){
			isLoop=isLoop||false;
			this.isLoop=isLoop;
			this.isCenterPlayer=true;
		},
		/**
		 *使对象的位置保持在场景内
		**/
		insideView:function(sprite,dir){//dir为限定哪个方向在view内，值为x或y，不传则两个方向皆限定
			if(cg.core.isArray(sprite)){
				for(var i=0,len=sprite.length;i<len;i++){
					arguments.callee.call(this,sprite[i],dir);
				}
			}
			else{
				sprite.insideDir=dir;
				this.insideArr.push(sprite);
			}
		},
		/**
		 *背景移动时的更新
		**/
		update:function(spritelist){//传入所有sprite的数组
			if(this.isCenterPlayer){
				if(this.player.x>this.centerX){
					if(this.x<this.imgWidth-this.width){
						var marginX=this.player.x-this.centerX;	
						this.x+=marginX;
						if(spritelist){
							for(var i=0,len=spritelist.length;i<len;i++){
								if(spritelist[i]==this.player){
									spritelist[i].x=this.centerX;
								}
								else{
									spritelist[i].x-=marginX;	
								}
							}
						}
					}
					else if(this.isLoop){
						if(spritelist){
							for(var i=0,len=spritelist.length;i<len;i++){
								if(spritelist[i]!=this.player){
									spritelist[i].move(this.imgWidth-this.width);
								}
							}
						}
						this.x=0;
					}
					else{
						this.onEnd&&this.onEnd();
					}
				}
			}
			for(var i=0,len=this.insideArr.length;i<len;i++){
				inside.call(this,this.insideArr[i]);
			}
		},
		/**
		 *绘制场景
		**/
		draw:function(){
			cg.context.drawImage(cg.loader.loadedImgs[this.src],this.x,this.y,this.width,this.height,0,0,this.width,this.height);
		}
		
		
	}
	this.View=view;
});

})(window,undefined);

