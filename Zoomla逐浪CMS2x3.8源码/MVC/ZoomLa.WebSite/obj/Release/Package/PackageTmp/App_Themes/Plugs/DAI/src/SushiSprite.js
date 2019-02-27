var SushiSprite = cc.Sprite.extend({
    disappearAction: null,//消失动画
    touchListener: null,
    index: null,//在数组中的索引

    onEnter: function () {
        cc.log("onEnter");
        this._super();
        //this.disappearAction = this.createDisappearAction();
        //this.disappearAction.retain();

        this.addTouchEventListenser();
    },

    onExit: function () {
        cc.log("onExit");
        this.disappearAction.release();
        this._super();
    },

    createDisappearAction: function () {
        //var frames = [];
        //for (var i = 0; i < 11; i++) {
        //    var str = "sushi_1n_" + i + ".png"
        //    var frame = cc.spriteFrameCache.getSpriteFrame(str);
        //    frames.push(frame);
        //}

        //var animation = new cc.Animation(frames, 0.02);
        //var action = new cc.Animate(animation);

        //return action;
    },

    addTouchEventListenser: function () {
        //touch event
        this.touchListener = cc.EventListener.create({
            event: cc.EventListener.TOUCH_ONE_BY_ONE,
            // When "swallow touches" is true, then returning 'true' from the onTouchBegan method will "swallow" the touch event, preventing other listeners from using it.
            swallowTouches: true,
            //onTouchBegan event callback function                      
            onTouchBegan: function (touch, event) {
                //var pos = touch.getLocation();
                var target = event.getCurrentTarget();

                var locationInNode = target.convertToNodeSpace(touch.getLocation());
                var s = target.getContentSize();
                var rect = cc.rect(0, 0, s.width, s.height);

                if (cc.rectContainsPoint(rect, locationInNode)) {//这样直接可拖动,move则为移动动画
                    cc.log("sprite began... x = " + locationInNode.x + ", y = " + locationInNode.y);
                    target.setOpacity(180);
                    //target.getAnimation().play("move");
                    return true;
                }
                return false;
            },
            onTouchMoved: function (touch, event) {
                var target = event.getCurrentTarget();
                var delta = touch.getDelta();
                target.x += delta.x;
                target.y += delta.y;
                console.log("move");
            }
        });
        this.mouseListener = cc.EventListener.create({
            event: cc.EventListener.MOUSE,
            onMouseDown: function (event) { console.log("down"); },
            onMouseUp: function (event) { console.log("up"); },
        });
        cc.eventManager.addListener(this.touchListener, this);
        //cc.eventManager.addListener(this.mouseListener, this);
    },

    removeTouchEventListenser: function () {
        cc.eventManager.removeListener(this.touchListener);
    }
});
