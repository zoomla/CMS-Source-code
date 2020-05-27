﻿define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    _self.prototype.Init_Pre = function (model, extend) {
        var ref = this;
        ref.htmlTlp = '<canvas id="progress" style="width:500px;height:300px;"></canvas>';
    }
    _self.prototype.Init_After = function () {
        var ref = this;
        setTimeout(function () {
            var c = $(ref.instance).find("canvas")[0];
            var cl = new lightLoader(c, c.width, c.height);
            setupRAF();
            cl.init();
        }, 500);
    }
    var lightLoader = function (c, cw, ch) {

        var _this = this;
        this.c = c;
        this.ctx = c.getContext('2d');
        this.cw = cw;
        this.ch = ch;

        this.loaded = 0;
        this.loaderSpeed = .6;
        this.loaderHeight = 10;
        this.loaderWidth = 310;
        this.loader = {
            x: (this.cw / 2) - (this.loaderWidth / 2),
            y: (this.ch / 2) - (this.loaderHeight / 2)
        };
        this.particles = [];
        this.particleLift = 180;
        this.hueStart = 0
        this.hueEnd = 120;
        this.hue = 0;
        this.gravity = .15;
        this.particleRate = 4;

        /*========================================================*/
        /* Initialize
        /*========================================================*/
        this.init = function () {
            this.loop();
        };
        /*========================================================*/
        /* Utility Functions
        /*========================================================*/
        this.rand = function (rMi, rMa) { return ~~((Math.random() * (rMa - rMi + 1)) + rMi); };
        this.hitTest = function (x1, y1, w1, h1, x2, y2, w2, h2) { return !(x1 + w1 < x2 || x2 + w2 < x1 || y1 + h1 < y2 || y2 + h2 < y1); };
        /*========================================================*/
        /* Update Loader
        /*========================================================*/
        this.updateLoader = function () {
            if (this.loaded < 100) {
                this.loaded += this.loaderSpeed;
            } else {
                this.loaded = 0;
            }
        };
        /*========================================================*/
        /* Render Loader
        /*========================================================*/
        this.renderLoader = function () {
            this.ctx.fillStyle = '#000';
            this.ctx.fillRect(this.loader.x, this.loader.y, this.loaderWidth, this.loaderHeight);

            this.hue = this.hueStart + (this.loaded / 100) * (this.hueEnd - this.hueStart);

            var newWidth = (this.loaded / 100) * this.loaderWidth;
            this.ctx.fillStyle = 'hsla(' + this.hue + ', 100%, 40%, 1)';
            this.ctx.fillRect(this.loader.x, this.loader.y, newWidth, this.loaderHeight);

            this.ctx.fillStyle = '#222';
            this.ctx.fillRect(this.loader.x, this.loader.y, newWidth, this.loaderHeight / 2);
        };
        /*========================================================*/
        /* Particles
        /*========================================================*/
        this.Particle = function () {
            this.x = _this.loader.x + ((_this.loaded / 100) * _this.loaderWidth) - _this.rand(0, 1);
            this.y = _this.ch / 2 + _this.rand(0, _this.loaderHeight) - _this.loaderHeight / 2;
            this.vx = (_this.rand(0, 4) - 2) / 100;
            this.vy = (_this.rand(0, _this.particleLift) - _this.particleLift * 2) / 100;
            this.width = _this.rand(1, 4) / 2;
            this.height = _this.rand(1, 4) / 2;
            this.hue = _this.hue;
        };
        this.Particle.prototype.update = function (i) {
            this.vx += (_this.rand(0, 6) - 3) / 100;
            this.vy += _this.gravity;
            this.x += this.vx;
            this.y += this.vy;

            if (this.y > _this.ch) {
                _this.particles.splice(i, 1);
            }
        };
        this.Particle.prototype.render = function () {
            _this.ctx.fillStyle = 'hsla(' + this.hue + ', 100%, ' + _this.rand(50, 70) + '%, ' + _this.rand(20, 100) / 100 + ')';
            _this.ctx.fillRect(this.x, this.y, this.width, this.height);
        };
        this.createParticles = function () {
            var i = this.particleRate;
            while (i--) {
                this.particles.push(new this.Particle());
            };
        };
        this.updateParticles = function () {
            var i = this.particles.length;
            while (i--) {
                var p = this.particles[i];
                p.update(i);
            };
        };
        this.renderParticles = function () {
            var i = this.particles.length;
            while (i--) {
                var p = this.particles[i];
                p.render();
            };
        };
        this.clearCanvas = function () {
            this.ctx.globalCompositeOperation = 'source-over';
            this.ctx.clearRect(0, 0, this.cw, this.ch);
            this.ctx.globalCompositeOperation = 'lighter';
        };
        this.loop = function () {
            var loopIt = function () {
                requestAnimationFrame(loopIt, _this.c);
                _this.clearCanvas();

                _this.createParticles();

                _this.updateLoader();
                _this.updateParticles();

                _this.renderLoader();
                _this.renderParticles();

            };
            loopIt();
        };
    };
    var setupRAF = function () {
        var lastTime = 0;
        var vendors = ['ms', 'moz', 'webkit', 'o'];
        for (var x = 0; x < vendors.length && !window.requestAnimationFrame; ++x) {
            window.requestAnimationFrame = window[vendors[x] + 'RequestAnimationFrame'];
            window.cancelAnimationFrame = window[vendors[x] + 'CancelAnimationFrame'] || window[vendors[x] + 'CancelRequestAnimationFrame'];
        };

        if (!window.requestAnimationFrame) {
            window.requestAnimationFrame = function (callback, element) {
                var currTime = new Date().getTime();
                var timeToCall = Math.max(0, 16 - (currTime - lastTime));
                var id = window.setTimeout(function () { callback(currTime + timeToCall); }, timeToCall);
                lastTime = currTime + timeToCall;
                return id;
            };
        };

        if (!window.cancelAnimationFrame) {
            window.cancelAnimationFrame = function (id) {
                clearTimeout(id);
            };
        };
    };
    module.exports = function () { return _self; }
});