var EventBase = function () {
    this.listener = {};
};
EventBase.prototype = {
    add: function (actionName, fn) {
        if (typeof actionName === 'string' && typeof fn === 'function') {
            //如果不存在actionName，就新建一个
            if (typeof this.listener[actionName] === 'undefined') {
                this.listener[actionName] = [fn];
            }
                //否则，直接往相应actinoName里面塞
            else {
                this.listener[actionName].push(fn);
            }
        }
    },
    remove: function (actionName, fn) {
        var actionArray = this.listener[actionName];
        if (typeof actionName === 'string' && actionArray instanceof Array) {
            if (typeof fn === 'function') {
                //清除actionName中对应的fn方法
                for (var i = 0, len = actionArray.length; i < len; i++) {
                    if (actionArray[i] === fn) {
                        this.listener[actionName].splice(i, 1);
                    }
                }
            }
        }
        actionArray = null;
    },
    fire: function (actionName, params) {
        var actionArray = this.listener[actionName];
        //触发一系列actionName里的函数
        if (actionArray instanceof Array) {
            for (var i = 0, len = actionArray.length; i < len; i++) {
                if (typeof actionArray[i] === 'function') {
                    actionArray[i](params);
                }
            }
        }
        actionArray = null;
    }
};
var eventBase = new EventBase();
//--------------------------------------------------------------------
//eventBase.add("test", function (params) { console.log(params); });
//eventBase.fire("test", { "test": "test1" });
//eventBase.fire("test", "test2");