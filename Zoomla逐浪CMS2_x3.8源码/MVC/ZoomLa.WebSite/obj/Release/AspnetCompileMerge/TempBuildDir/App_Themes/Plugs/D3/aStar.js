/*  结点对象    */
var point = function(options) {
    this.x = options.x;
    this.y = options.y;
    this.parent = options.parent;
    this.G = options.G;
    this.H = options.H;
    this.value = options.value;
}
point.prototype = {
    /*  获取通过当前所选结点到该结点的G值 */
    caculateG: function(point) {
        point.G = point.G || 0;
        if (point.x != this.x && point.y != this.y) {
            return point.G + 14; //在所选点斜方向，耗散值加14
        }
        else {
            return point.G + 10; //在所选点垂直方向，耗散值加10
        }
    },
    /*  根据结束结点计算该点的H值   */
    updateH: function(endPoint) {
        this.H = (Math.abs(endPoint.x - this.x) + Math.abs(endPoint.y - this.y)) * 10;
    },
    /*  根据G和H值计算该点的F值 */
    updateF: function() {
        this.F = this.G + this.H;
    }


}
var aStarManager = (function() {
    var openArr = [];
    var closeArr = [];
    var path = [];
    var isInOpen = function(point) {
        for (var i = 0, len = openArr.length; i < len; i++) {
            if (openArr[i] == point) {
                return true;
            }
        }
        return false;
    }
    var isInClose = function(point) {
        for (var i = 0, len = closeArr.length; i < len; i++) {
            if (closeArr[i] == point) {
                return true;
            }
        }
        return false;

    }
    /*  获取开始列表中F值最小的结点 */
    var getPointWithMinF = function() {
        var minPoint;
        for (var i = 0, len = openArr.length; i < len; i++) {
            if (!minPoint || openArr[i].F < minPoint.F) {
                minPoint = openArr[i];
            }
        }
        return minPoint;
    }
    var addToClose = function(point) {
        if (isInOpen(point)) {//如果在开始列表中，则把结点从开始列表中删除，再添加到关闭列表中
            deleteFromOpen(point);
        }
        closeArr.push(point);
    }
    var deleteFromOpen = function(point) {
        for (var i = 0, len = openArr.length; i < len; i++) {
            if (openArr[i] == point) {
                openArr.splice(i, 1);
                break;
            }
        }

    }
    /*  保存路径    */
    var savePath = function(point) {
        path.unshift(point);
        if (point.parent) {
            arguments.callee(point.parent);
        }

    }
    /*  判断该点是否围墙    */
    var isWall = function(point, wallValueArr) {
        for (var i = 0, len = wallValueArr.length; i < len; i++) {
            if (wallValueArr[i] == point.value) {
                return true;
            }
        }
        return false;
    }
    /*  开始寻路    */
    var handle = function(point) {

        var beginX = point.x - 1;
        var beginY = point.y - 1;
        var endX = point.x + 1;
        var endY = point.y + 1;
        var pointsArr = this.pointsArr;

        addToClose(point); //把传入的当前结点放进关闭列表

        for (var i = beginY; i <= endY; i++) {//遍历传入结点的四周的结点
            for (var j = beginX; j <= endX; j++) {
                if (pointsArr[i] && pointsArr[i][j] && !isInClose(pointsArr[i][j])) {//如果该位置结点存在并且不是传入的结点


                    if (!isWall(pointsArr[i][j], this.wallValueArr)) {//不是障碍物
                        if (isWall(pointsArr[i][point.x], this.wallValueArr) || isWall(pointsArr[point.y][j], this.wallValueArr)) {//拐角规则，如果检测某点四周的点时，该点和四周上某点之间隔着一个障碍物，则忽略该点，暂不添加到开始列表

                            continue;
                        }
                        var G = pointsArr[i][j].caculateG(point);
                        if (isInOpen(pointsArr[i][j])) {//如果在开始列表，则根据传入的指定点计算是否改变G值
                            if (pointsArr[i][j].G > G) {//如果该结点的G值大于由传入结点计算出的G值，则该结点父指针指向传入结点
                                pointsArr[i][j].G = G;
                                pointsArr[i][j].updateF();
                                pointsArr[i][j].parent = point;

                            }
                        }
                        else if (!isInClose(pointsArr[i][j])) {//如果不在开始列表也不在关闭列表，则根据传入的指定点计算该点G值，并把该结点父节点指针指向传入结点
                            pointsArr[i][j].G = G;
                            pointsArr[i][j].updateH(this.endPoint);
                            pointsArr[i][j].updateF();
                            pointsArr[i][j].parent = point;
                            openArr.push(pointsArr[i][j]); //添加到开始列表

                        }
                    }
                    else {//如果是障碍物，则添加到关闭列表
                        addToClose(pointsArr[i][j]);

                    }
                }

            }
        }


        if (point != this.endPoint) {//如果被选中结点不是终点结点

            var nextPoint = getPointWithMinF(); //获取开始列表中F值最小的结点
            arguments.callee.call(this, nextPoint); //递归重复上述操作
        }
        else {

            savePath(point); //如果当前结点是终点结点并且该结点被添加到关闭列表，则保存路径，寻路结束

        }

    }



    return {//为墙的组
        init: function(map, startPointPos, endPointPos, wallValueArr) {

            this.wallValueArr = wallValueArr;
            this.pointsArr = [];
            openArr = [];
            closeArr = [];
            path = [];

            for (var i = 0, len = map.length; i < len; i++) {//列
                var row = map[i];
                this.pointsArr[i] = [];
                for (var j = 0, len2 = row.length; j < len2; j++) {//行
                    this.pointsArr[i][j] = new point({ x: j, y: i, value: row[j] }); //初始化point对象数组
                    if (i == startPointPos[1] && j == startPointPos[0]) {
                        this.startPoint = this.pointsArr[i][j];
                    }
                    else if (i == endPointPos[1] && j == endPointPos[0]) {
                        this.endPoint = this.pointsArr[i][j];
                    }
                }
            }
        },
        getPath: function() {
            handle.call(this, this.startPoint);
            return path;
        }


    }
})();






