<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="ZoomLaCMS._3D.home" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>3DHome</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
      <div style="background: url('/3D/images/InHome.jpg') no-repeat; background-position: center; left: 0px; top: 0px; right: 0px; bottom: 0px; position: absolute; 
       background-repeat: no-repeat; background-size: cover;width:800px;height:800px;z-index:5">
        <canvas id="canvas" style="margin-top:150px;">请使用支持canvas的浏览器查看</canvas>
    </div>
    <div style="margin-left:800px;z-index:500;">
        <input type="text" id="uName" value="" runat="server" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript" src="cnGame_v1.2.js"></script>
    <script type="text/javascript" src="aStar.js"></script>
    <script type="text/javascript">
        /*  src地址对象 */
        var srcObj = {
            player: "images/C09.png",//280:496
            ground: "grass.png",
            wall: "grass.png",
            grass: "grass.png",
            path: "grass.png"
        }
        /*  地图矩阵 0 空地 1 墙壁 2 草    */
        var mapMatrix = [
                    [0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
                    [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
                    [0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 2],
                    [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2]
        ];
        cnGame.init("canvas", { width: 400, height: 600 });

        var Player = function (options) {
            this.init(options);
            this.name = "";
            this.path = [];
            this.index = 1;//用于path
            this.drawText = function () {
                cnGame.shape.Text(this.name, { x: this.x, y: this.y, style: "#FFF", font: "bold 15px sans-serif" }).draw();
            }
            this.identity = "";//身份，用于证明自己是玩家还是NPC  1:玩家,2:其他玩家,3:NPC
            this.desX = 0;
            this.desY = 0;
        }
        cnGame.core.inherit(Player, cnGame.Sprite);
        Player.prototype.moveSpeed = 2;

        var path = [];
        var index = 1;
        //280:496
        //var pw = 47, ph = 95;
        var pw = 70, ph = 124;
        var imgw = 188;
        var boss;
        setInterval(function () {
            if (boss) {
                boss.desX = GetRandomNum(40, 300);
                boss.desY = GetRandomNum(80, 300);
                GetPath(boss.desX, boss.desY, boss);
            }
        }, 3000);

        function GetRandomNum(Min, Max) {
            var Range = Max - Min;
            var Rand = Math.random();
            return (Min + Math.round(Rand * Range));
        }
        var gameObj = {
            initialize: function (options) {
                //32:48,128:192
                //47:95 188:380
                //图片所在列的宽与高(起始位),这列从何种开始,,下左右上
                this.map = new cnGame.Map(mapMatrix, { cellSize: [40, 40] });

                this.player = new Player({ src: srcObj.player, width: pw, height: ph, x: 80, y: 320 });
                this.player.addAnimation(new cnGame.SpriteSheet("right", srcObj.player, { width: imgw, height: ph * 3, beginX: 0, beginY: ph * 2, frameSize: [pw, ph], loop: true }));

                this.player.addAnimation(new cnGame.SpriteSheet("rightTop", srcObj.player, { width: imgw, height: ph * 3, beginX: 0, beginY: ph * 2, frameSize: [pw, ph], loop: true }));
                this.player.addAnimation(new cnGame.SpriteSheet("rightBottom", srcObj.player, { width: imgw, height: ph * 3, beginX: 0, beginY: ph * 2, frameSize: [pw, ph], loop: true }));

                this.player.addAnimation(new cnGame.SpriteSheet("left", srcObj.player, { width: imgw, height: ph * 2, beginX: 0, beginY: ph, frameSize: [pw, ph], loop: true }));

                this.player.addAnimation(new cnGame.SpriteSheet("leftBottom", srcObj.player, { width: imgw, height: ph * 2, beginX: 0, beginY: ph, frameSize: [pw, ph], loop: true }));
                this.player.addAnimation(new cnGame.SpriteSheet("leftTop", srcObj.player, { width: imgw, height: ph * 2, beginX: 0, beginY: ph, frameSize: [pw, ph], loop: true }));

                this.player.addAnimation(new cnGame.SpriteSheet("top", srcObj.player, { width: imgw, height: ph * 4, beginX: 0, beginY: ph * 3, frameSize: [pw, ph], loop: true }));

                this.player.addAnimation(new cnGame.SpriteSheet("bottom", srcObj.player, { width: imgw, height: ph, beginX: 0, beginY: 0, frameSize: [pw, ph], loop: true }));
                this.player.setCurrentAnimation("bottom");
                this.player.identity = 1;
                this.player.name = $("#uName").val() || "未定义";
                cnGame.spriteList.add(this.player);
            },
            update: function () {
                var cellSize = this.map.cellSize;
                for (var i = 0; i < cnGame.spriteList.length; i++) {
                    if (cnGame.spriteList[i].identity == 1) continue
                    if (!cnGame.spriteList[i].path) continue;
                    var op = cnGame.spriteList[i];
                    this.oPathObj = op.path[op.index];
                    if (!cnGame.core.isUndefined(this.oPathObj)) {
                        var odesX = this.oPathObj.x * cellSize[0];//下一步路径所在单元格位置
                        var odesY = this.oPathObj.y * cellSize[1];
                        if (op.x != odesX || op.y != odesY) {
                            if (op.x < odesX) {
                                op.speedX = op.moveSpeed;
                            }
                            else if (op.x > odesX) {
                                op.speedX = -op.moveSpeed;
                            }
                            else {
                                op.speedX = 0;
                            }
                            if (op.y < odesY) {
                                op.speedY = op.moveSpeed;
                            }
                            else if (op.y > odesY) {
                                op.speedY = -op.moveSpeed;
                            }
                            else {
                                op.speedY = 0;
                            }
                        }
                        else {
                            op.speedX = 0;
                            op.speedY = 0;
                            op.index++;
                        }
                    }
                    if (op.speedX > 0) {//动画效果
                        if (op.speedY == 0) {
                            op.setCurrentAnimation("right");
                        }
                        else if (op.speedY < 0) {
                            op.setCurrentAnimation("rightTop");
                        }
                        else {
                            op.setCurrentAnimation("rightBottom");
                        }
                    }
                    else if (op.speedX < 0) {
                        if (op.speedY == 0) {
                            op.setCurrentAnimation("left");
                        }
                        else if (op.speedY > 0) {
                            op.setCurrentAnimation("leftBottom");
                        }
                        else {
                            op.setCurrentAnimation("leftTop");
                        }
                    }
                    else if (op.speedX == 0) {
                        if (op.speedY < 0) {
                            op.setCurrentAnimation("top");
                        }
                        else if (op.speedY > 0) {
                            op.setCurrentAnimation("bottom");
                        }
                    }
                }
                //----------------------玩家
                var player = this.player;
                this.pathObj = path[index];//临时生成的pathObj,如果path数组中无下一步的信息，则生成path[];
                if (!cnGame.core.isUndefined(this.pathObj)) {//如果路径数组存在不为空
                    var desX = this.pathObj.x * cellSize[0];//下一步路径所在单元格位置
                    var desY = this.pathObj.y * cellSize[1];
                    if (player.x != desX || player.y != desY) {
                        if (player.x < desX) {
                            player.speedX = player.moveSpeed;
                        }
                        else if (player.x > desX) {
                            player.speedX = -player.moveSpeed;

                        }
                        else {
                            player.speedX = 0;
                        }
                        if (player.y < desY) {
                            player.speedY = player.moveSpeed;
                        }
                        else if (player.y > desY) {
                            player.speedY = -player.moveSpeed;
                        }
                        else {
                            player.speedY = 0;
                        }
                    }
                    else {
                        player.speedX = 0;
                        player.speedY = 0;
                        index++;
                    }
                }

                if (player.speedX > 0) {//动画效果
                    if (player.speedY == 0) {
                        player.setCurrentAnimation("right");
                    }
                    else if (player.speedY < 0) {
                        player.setCurrentAnimation("rightTop");
                    }
                    else {
                        player.setCurrentAnimation("rightBottom");
                    }
                }
                else if (player.speedX < 0) {
                    if (player.speedY == 0) {
                        player.setCurrentAnimation("left");
                    }
                    else if (player.speedY > 0) {
                        player.setCurrentAnimation("leftBottom");
                    }
                    else {
                        player.setCurrentAnimation("leftTop");
                    }
                }
                else if (player.speedX == 0) {
                    if (player.speedY < 0) {
                        player.setCurrentAnimation("top");
                    }
                    else if (player.speedY > 0) {
                        player.setCurrentAnimation("bottom");
                    }

                }
            },
            draw: function () {
                this.map.draw({ "0": { src: srcObj.ground }, "1": { src: srcObj.wall }, "2": { src: srcObj.grass } });
                for (var i = 0; i < cnGame.spriteList.length; i++) {
                    cnGame.spriteList[i].drawText();
                }
                for (var i = 1; i < path.length; i++) {//画出路径
                    var xPos = path[i].x * 40;
                    var yPos = path[i].y * 40;
                    cnGame.context.drawImage(cnGame.loader.loadedImgs[srcObj.path], 0, 0, 40, 40, xPos, yPos, 40, 40);
                }
            }

        }
        document.body.onmousedown = function () {
            var x = cnGame.input.mouseX;
            var y = cnGame.input.mouseY;
            var endPoint = gameObj.map.getCurrentIndex(x, y);
            var startPoint = gameObj.map.getCurrentIndex(gameObj.player.x, gameObj.player.y);
            aStarManager.init(mapMatrix, startPoint, endPoint, [1, 2]);
            path = aStarManager.getPath();
            index = 1;
            SendPos(x, y)
        }
        //目标x,目标y,对象
        function GetPath(x, y, obj) {
            try {
                var endPoint = gameObj.map.getCurrentIndex(x, y);
                var startPoint = gameObj.map.getCurrentIndex(obj.x, obj.y);
                aStarManager.init(mapMatrix, startPoint, endPoint, [1, 2]);
                obj.index = 1;
                obj.path = aStarManager.getPath();//再将index改为1
            } catch (e) { console.log(e); }
        }
        function Cenemy(posX, posY, name, img) {
            if (img && img != "") {
                pimg = img;
            }
            else {
                pimg = srcObj.player;
            }
            this.player2 = new Player({ src: pimg, width: pw, height: ph, x: posX, y: posY });
            this.player2.addAnimation(new cnGame.SpriteSheet("right", pimg, { width: imgw, height: ph * 3, beginX: 0, beginY: ph * 2, frameSize: [pw, ph], loop: true }));
            this.player2.addAnimation(new cnGame.SpriteSheet("rightTop", pimg, { width: imgw, height: ph * 3, beginX: 0, beginY: ph * 2, frameSize: [pw, ph], loop: true }));
            this.player2.addAnimation(new cnGame.SpriteSheet("rightBottom", pimg, { width: imgw, height: ph * 3, beginX: 0, beginY: ph * 2, frameSize: [pw, ph], loop: true }));
            this.player2.addAnimation(new cnGame.SpriteSheet("left", pimg, { width: imgw, height: ph * 2, beginX: 0, beginY: ph, frameSize: [pw, ph], loop: true }));
            this.player2.addAnimation(new cnGame.SpriteSheet("leftBottom", pimg, { width: imgw, height: ph * 2, beginX: 0, beginY: ph, frameSize: [pw, ph], loop: true }));
            this.player2.addAnimation(new cnGame.SpriteSheet("leftTop", pimg, { width: imgw, height: ph * 2, beginX: 0, beginY: ph, frameSize: [pw, ph], loop: true }));
            this.player2.addAnimation(new cnGame.SpriteSheet("top", pimg, { width: imgw, height: ph * 4, beginX: 0, beginY: ph * 3, frameSize: [pw, ph], loop: true }));
            this.player2.addAnimation(new cnGame.SpriteSheet("bottom", pimg, { width: imgw, height: ph, beginX: 0, beginY: 0, frameSize: [pw, ph], loop: true }));
            this.player2.name = name;
            this.player2.identity = 2;
            cnGame.spriteList.add(this.player2);
            return this.player2;
        }
        //-----同步区(每次点击时,提交事件至后台)
        function PostToCS(a, v, CallBack) {
            $.ajax({
                type: "Post",
                url: "Server.ashx",
                data: { action: a, value: v },
                success: function (data) {
                    CallBack(data);
                },
                error: function (data) {

                }
            });
        }
        /*登录*/
        function BeginLogin(name) {
            LoginFunc = function (data) {
                //初始化时必须将资源载入，否则无法生成角色
                cnGame.loader.start(gameObj, { srcArray: [srcObj.player, srcObj.wall, srcObj.grass, srcObj.ground, srcObj.path, "images/E15.png"] });
                setInterval(function () { SyncSpirt(); }, 1000);
                setTimeout(function () {
                    boss = Cenemy(200, 320, "老板娘", "images/E15.png");
                    GetPath(80, 260, boss);
                }, 500);
            }
            PostToCS('Login', name, LoginFunc);
        }
        //同步游戏对象,并且更新位置
        function SyncSpirt() {
            SyncOPFunc = function (data) {
                if (!data && data == "") return;
                data = eval(data);
                for (var i = 0; i < data.length; i++) {
                    if (!GetEnemyByName(data[i].name)) Cenemy(80, 320, data[i].name);
                    else {
                        this.obj = GetEnemyByName(data[i].name);
                        if (this.obj.desX != data[i].X || this.obj.desY != data[i].Y) {
                            //console.log("SyncSpirt2:" + this.obj.name + ":" + this.obj.desX + ":" + data[i].X);
                            this.obj.desX = parseInt(data[i].X);
                            this.obj.desY = parseInt(data[i].Y);
                            GetPath(this.obj.desX, this.obj.desY, this.obj);
                        }//if
                    }
                }
            }
            PostToCS('SyncSpirt', name, SyncOPFunc);
        }

        //发送位置与聊天信息
        function SendPos(x, y) {
            this.CallBack = function (data) { }
            this.name = cnGame.spriteList[0].name;
            PostToCS('SendPos', this.name + "," + x + "," + y, this.CallBack);
        }
        //-----功能方法
        /*根据角色名获取到角色对象*/
        function GetEnemyByName(name) {
            var obj;
            for (var i = 0; i < cnGame.spriteList.length ; i++) {
                if (cnGame.spriteList[i].name == name) { obj = cnGame.spriteList[i]; break; }
            }
            return obj;
        }
</script>
</asp:Content>


