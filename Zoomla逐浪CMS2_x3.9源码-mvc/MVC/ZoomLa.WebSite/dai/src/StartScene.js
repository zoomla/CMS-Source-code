
var StartLayer = cc.Layer.extend({
	bgSprite:null,
	scoreLabel:null,
	ctor:function () {
		//////////////////////////////
		// 1. super init first
		this._super();
		var size = cc.winSize;
		// add bg
		this.bgSprite = new cc.Sprite(res.BackGround_png);
		this.bgSprite.attr({
			x: size.width / 2,
			y: size.height / 2,
		});
		this.addChild(this.bgSprite, 0);
		
		//add start menu
		var startItem = new cc.MenuItemImage(
				res.Start_N_png,
				res.Start_S_png,
				function () {
					cc.log("Menu is clicked!");
					cc.director.runScene(new PlayScene());
					//cc.director.replaceScene( cc.TransitionPageTurn(1, new PlayScene(), false) );//浏览器不支持
				}, this);
		startItem.attr({
			x: size.width/2,
			y: size.height/2,
			anchorX: 0.5,
			anchorY: 0.5
		});

		var menu = new cc.Menu(startItem);
		menu.x = 0;
		menu.y = 0;
		this.addChild(menu, 1);


		return true;
	}
});

var StartScene = cc.Scene.extend({
	onEnter:function () {
		this._super();
		var layer = new StartLayer();
		this.addChild(layer);
	}
});