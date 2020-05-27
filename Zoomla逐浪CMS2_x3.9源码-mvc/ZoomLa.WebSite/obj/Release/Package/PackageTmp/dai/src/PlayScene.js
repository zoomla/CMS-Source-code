var PlayLayer = cc.Layer.extend({
    bgSprite: null,
    scoreLabel: null,
    score: 0,
    timeoutLabel: null,
    timeout: 60,
    GlassArr: null,
    ctor: function () {
        this._super();
        this.GlassArr = [];
        //this.addGlass("res/glass_5.png");
        //this.changeBG("res/man/girl1.png");
        return true;
    },
    changeBG: function (imgurl) {//更换背景
        var size = cc.winSize;
        this.bgSprite = new cc.Sprite(imgurl);
        this.bgSprite.attr({
            x: size.width / 2,
            y: size.height / 2,
            //scale: 0.5,
            //rotation: 180
        });
        this.removeAllChildren();//清除所有图片
        this.addChild(this.bgSprite, 1);
        var whiteBG = new cc.LayerColor(cc.color(255, 255, 255));
        this.addChild(whiteBG, 0);
    },
    update: function () {
        //this.clearGlass();
        //this.addGlass();
        //this.removeGlass();
    },
    addGlass: function (imgurl) {
        this.clearGlass();
        var glass = new GlassSprite(imgurl);
        var size = cc.winSize;
        //var x = glass.width / 2 + size.width / 2 * cc.random0To1();
        var x = size.width / 2;
        glass.attr({
            x: x,
            y: (size.height / 2)+70
        });
        this.GlassArr.push(glass);
        this.addChild(glass, 5);
    },
    clearGlass: function () {
        if (this.GlassArr == null || this.GlassArr.length < 1) return;
        for (var i = 0; i < this.GlassArr.length; i++) {
            this.GlassArr[i].removeFromParent();
            this.GlassArr[i] = undefined;
            this.GlassArr.splice(i, 1);
            i = i - 1;
        }
    },
    addScore: function () {
        this.score += 1;
        this.scoreLabel.setString("score:" + this.score);
    }
});
var curLayer = null;
var PlayScene = cc.Scene.extend({
    onEnter: function () {
        this._super();
        curLayer = new PlayLayer();
        this.addChild(curLayer);
    }
});