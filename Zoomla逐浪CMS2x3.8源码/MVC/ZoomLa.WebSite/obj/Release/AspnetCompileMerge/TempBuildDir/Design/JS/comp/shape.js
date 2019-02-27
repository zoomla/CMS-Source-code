define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    _self.prototype.htmlTlp = '';
    _self.prototype.Init_Pre = function (model, extend) {
        //------不可改变颜色
        //this.htmlTlp = '<img src="' + model.config.url + '" width="100%" height="100%"></img>';

        //------不可改变颜色，右键无法打开菜单
        //this.htmlTlp = '<object data="' + model.config.url + '" width="100%" height="100%" type="image/svg+xml" codebase="http://www.adobe.com/svg/viewer/imstall/" />'
        //this.htmlTlp = '<embed src="' + model.config.url + '" width="100%" height="100%" type="image/svg+xml" pluginspage="http://www.adobe.com/svg/viewer/install/" />';

        //------可通过更改外层div的css（fill）来改变其颜色
        var tlp = '';
        switch (model.config.shape) {
            case "prismatic_y":
                tlp = '<svg version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"'
                    + 'width="100%" height="100%" viewBox="0 0 64 64" enable-background="new 0 0 64 64" xml:space="preserve">'
                    + '<polygon fill="' + model.config.color + '" points="64,32 32,64 0,32 32,0 "/>'
                    + '</svg>';
                break;
            case "prismatic_x":
                tlp = '<svg version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"'
                    + 'width="100%" height="100%" viewBox="0 0 64 64" enable-background="new 0 0 64 64" xml:space="preserve">'
                    + '<rect fill="' + model.config.color + '"/>'
                    + '</svg>';
                break;
            case "circular":
                tlp = '<svg version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"'
                    + 'width="100%" height="100%" viewBox="0 0 64 64" enable-background="new 0 0 64 64" xml:space="preserve">'
                    + '<ellipse fill="' + model.config.color + '" cx="32" cy="32" rx="32" ry="32"/>'
                    + '</svg>';
                break;
            case "hexagon_x":
                tlp = '<svg version="1.1" xmlns:ev="http://www.w3.org/2001/xml-events"'
                    + 'xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="100%" height="100%"'
                    + 'viewBox="0 0 312 270" enable-background="new 0 0 312 270" xml:space="preserve">'
                    + '<path fill="' + model.config.color + '" d="M311.352,134.625l-77.67,134.528H78.342L0.672,134.625L78.342,0.097h155.34L311.352,134.625z"/>'
                    + '</svg>';
                break;
            case "hexagon_y":
                tlp = '<svg version="1.1" xmlns:ev="http://www.w3.org/2001/xml-events"'
                    + 'xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="100%" height="100%"'
                    + 'viewBox="0 0.766 234 270.234" enable-background="new 0 0.766 234 270.234" xml:space="preserve">'
                    + '<path fill="' + model.config.color + '" d="M117.141,270.617L0.291,203.154V68.229l116.85-67.463L233.99,68.229v134.925L117.141,270.617z"/>'
                    + '</svg>';
                break;
            case "octagon":
                tlp = '<svg version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"'
                    + 'width="100%" height="100%" viewBox="0 0 64 64" enable-background="new 0 0 64 64" xml:space="preserve">'
                    + '<polygon fill="' + model.config.color + '" points="64,32 54.6,54.6 32,64 9.4,54.6 0,32 9.4,9.4 32,0 54.6,9.4 "/>'
                    + '</svg>';
                break;
            case "ring":
                tlp = '<svg version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"'
                    + 'width="100%" height="100%" viewBox="0 0 26.053 26.053" enable-background="new 0 0 26.053 26.053" xml:space="preserve">'
                    + '<path fill="none" stroke="' + model.config.color + '" stroke-width="8" stroke-miterlimit="10" d="M13.026,4c4.985,0,9.026,4.041,9.026,9.026'
                    + 'c0,1.37-0.306,2.668-0.852,3.832c-1.441,3.069-4.56,5.194-8.175,5.194C8.041,22.052,4,18.011,4,13.026S8.041,4,13.026,4z"/>'
                    + '</svg>';
                break;
            case "target":
                tlp = '<svg version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"'
                    + 'width="100%" height="100%" viewBox="0 0 500 499.992" enable-background="new 0 0 500 499.992" xml:space="preserve">'
                    + '<path id="ry" fill="' + model.config.color + '" d="M249.996,128.157c67.291,0,121.841,54.55,121.841,121.841s-54.55,121.841-121.841,121.841'
                    + 's-121.841-54.55-121.841-121.841S182.705,128.157,249.996,128.157z M423.392,249.998c0,95.606-77.784,173.389-173.388,173.389'
                    + 'c-95.607,0-173.394-77.785-173.394-173.389c0-95.606,77.789-173.386,173.394-173.386'
                    + 'C345.608,76.612,423.391,154.392,423.392,249.998z M396.443,249.998c0-80.749-65.694-146.444-146.439-146.444'
                    + 'c-80.753,0-146.448,65.695-146.448,146.444c0,80.748,65.695,146.443,146.448,146.443'
                    + 'C330.753,396.441,396.444,330.748,396.443,249.998z M463.237,250.004c0,117.576-95.658,213.228-213.241,213.228'
                    + 'c-117.579,0-213.234-95.654-213.234-213.228c0-117.579,95.655-213.239,213.234-213.239'
                    + 'C367.579,36.765,463.238,132.425,463.237,250.004z M449.763,250.007c0-110.156-89.621-199.771-199.767-199.771'
                    + 'c-110.148,0-199.767,89.617-199.767,199.771c0,110.152,89.62,199.756,199.767,199.756S449.766,360.156,449.763,250.007z'
                    + 'M500,250.004c0,137.845-112.156,249.988-249.996,249.988C112.154,499.992,0,387.849,0,250.004C0,112.156,112.155,0,250.004,0'
                    + 'C387.844,0,500,112.156,500,250.004z M491.015,250.004c0-132.907-108.115-241.026-241.011-241.026'
                    + 'C117.1,8.978,8.982,117.097,8.982,250.004c0,132.893,108.118,241.008,241.022,241.008'
                    + 'C382.9,491.012,491.014,382.897,491.015,250.004z"/>';
                + '</svg>';
                break;
        }

        this.htmlTlp = tlp;
    };
    module.exports = function () { return _self; }
});