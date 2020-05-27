<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="ZoomLaCMS.Design.mobile.pop.list"  MasterPageFile="~/Design/Master/MB_POP.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>列表修改</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="page-group" ng-app="app">
    <div id="page_list" class="page page-current" ng-controller="APPCtrl">
        <header class="bar bar-nav">
            <a class="button button-link button-nav pull-left back" href="javascript:;" ng-click="save();">
                <span class="icon icon-left"></span>
                返回
            </a>
             <a href="#page_content" class="button button-fill button-nav pull-right" ng-click="new()">添加数据</a>
            <h1 class="title">内容管理</h1>
        </header>
        <div class="content native-scroll">
        <div class="list-block cards-list">
          <ul>
            <li class="card list_item" ng-repeat="item in list track by $index">
              <div class="card-header">
                  <span ng-bind="item.title"></span>
                  <a href="javascript:;" ng-click="del(item.id);" class="button button-fill button-danger"><i class="fa fa-trash-o"></i> 删除</a>
              </div>
              <div class="card-content">
                <div class="card-content-inner">
                    <div class="img_wrap">
                    <span ng-bind-html="getimg(item.wxico)|html"></span>
                </div>
                    <div class="con_wrap">
                        <div><span class="name">背景色：</span><span class="bkval" ng-style="getbk(item.wxbk)"></span></div>
                        <div><span class="name">尺寸：</span><span class="val" ng-bind="item.wxsize"></span></div>
                    </div>
                    <div class="op_wrap" ng-click="edit(item);">
                    <i class="fa fa-pencil fa-2x"></i>
                    <div>
                        <span>编辑</span>
                    </div>
                </div>
                </div>
              </div>
         <%--     <div class="card-footer">卡脚</div>--%>
            </li>
          </ul>
        </div>
      </div>
    </div>
    <div id="page_content" class="page" ng-controller="ConCtrl">

        <header class="bar bar-nav">
            <a class="button button-link button-nav pull-left back" href="javascript:;" ng-click="cancel();">
                <span class="icon icon-left"></span>
                返回
            </a>
            <h1 class="title">内容管理</h1>
        </header>
         <div class="content">
            <div class="list-block">
                <ul>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">标题</div>
                                <div class="item-input">
                                    <input type="text" placeholder="标题" ng-model="model.title" />
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">链接</div>
                                <div class="item-input">
                                    <input type="text" placeholder="为空则自动生成" ng-model="model.wxlink" />
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">尺寸</div>
                                <div class="item-input">
                                    <select ng-model="model.wxsize">
                                        <option>1</option>
                                        <option>2</option>
                                        <option>3</option>
                                        <option>4</option>
                                        <option>5</option>
                                        <option>6</option>
                                        <option>7</option>
                                        <option>8</option>
                                        <option>9</option>
                                        <option>10</option>
                                        <option>11</option>
                                        <option>12</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">图标</div>
                                <div class="item-input" style="position:relative;">
                                    <i class="{{model.wxico}}" style="position:absolute;left:-30px;top:12px;"></i>
                                    <input type="text" placeholder="图标" ng-model="model.wxico" />
                                </div>
                                <a href="#page_icons" class="button button-fill" style="min-width:60px;">选择</a>
                            </div>
                        </div>
                    </li>
                    <li>
                         <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">背景色</div>
                                <div class="item-input">
                                    <input type="text" placeholder="背景色" ng-model="model.wxbk"/>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div ng-repeat="item in colorArr" ng-style="getcolor(item)" ng-click="setcolor(item);" class="colorItem"></div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">数据类型</div>
                                <div class="item-input">
                                    <select ng-model="model.dbtype">
                                        <option value="footbar">底部栏</option>
                                        <option value="image">单图</option>
                                        <option value="list" selected="selected">列表</option>
                                        <option value="nav">轮播图</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </li>
                <%--    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">是否标识</div>
                                <div class="item-input">
                                    <label class="label-switch">
                                        <input type="checkbox" />
                                        <div class="checkbox"></div>
                                    </label>
                                </div>
                            </div>
                        </div>
                    </li>--%>
                    <li onclick="openEditor();">
                         <div class="item-content">
                            <div class="item-inner">
                                 <div class="item-input" id="content_div"></div>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="content-block">
                <div class="row">
                    <div class="col-50"><a href="#page_list" ng-click="cancel();" class="button button-big button-fill button-danger back">取消</a></div>
                    <div class="col-50"><a href="#page_list" ng-click="save();" class="button button-big button-fill button-success back">提交</a></div>
                </div>
            </div>
        </div>
    </div>
    <div id="page_icons" class="page">
        <header class="bar bar-nav">
            <a href="#page_content" class="button button-link button-nav pull-left back"><span class="icon icon-left"></span>返回</a>
            <h1 class="title">图标选择</h1>
            <a href="#page_content" class="button button-fill button-success button-nav pull-right back" onclick="icon.set();">确定</a>
        </header>
        <div class="content">
             <div class="row fontawesome-icon-list">
                    <div class="fa-hover"><a><i class="fa fa-adjust"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-anchor"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-archive"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-area-chart"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-arrows"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-arrows-h"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-arrows-v"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-asterisk"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-at"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-automobile"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-balance-scale"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-ban"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bank"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bar-chart"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bar-chart-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-barcode"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bars"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-battery-0"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-battery-1"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-battery-2"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-battery-3"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-battery-4"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-battery-empty"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-battery-full"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-battery-half"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-battery-quarter"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-battery-three-quarters"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bed"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-beer"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bell"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bell-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bell-slash"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bell-slash-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bicycle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-binoculars"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-birthday-cake"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bluetooth"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bluetooth-b"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bolt"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bomb"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-book"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bookmark"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bookmark-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-briefcase"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bug"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-building"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-building-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bullhorn"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bullseye"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-bus"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-cab"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-calculator"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-calendar"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-calendar-check-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-calendar-minus-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-calendar-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-calendar-plus-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-calendar-times-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-camera"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-camera-retro"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-car"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-caret-square-o-down"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-caret-square-o-left"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-caret-square-o-right"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-caret-square-o-up"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-cart-arrow-down"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-cart-plus"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-cc"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-certificate"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-check"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-check-circle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-check-circle-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-check-square"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-check-square-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-child"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-circle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-circle-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-circle-o-notch"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-circle-thin"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-clock-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-clone"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-close"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-cloud"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-cloud-download"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-cloud-upload"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-code"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-code-fork"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-coffee"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-cog"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-cogs"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-comment"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-comment-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-commenting"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-commenting-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-comments"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-comments-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-compass"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-copyright"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-creative-commons"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-credit-card"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-credit-card-alt"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-crop"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-crosshairs"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-cube"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-cubes"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-cutlery"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-dashboard"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-database"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-desktop"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-diamond"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-dot-circle-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-download"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-edit"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-ellipsis-h"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-ellipsis-v"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-envelope"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-envelope-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-envelope-square"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-eraser"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-exchange"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-exclamation"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-exclamation-circle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-exclamation-triangle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-external-link"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-external-link-square"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-eye"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-eye-slash"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-eyedropper"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-fax"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-feed"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-female"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-fighter-jet"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-archive-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-audio-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-code-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-excel-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-image-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-movie-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-pdf-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-photo-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-picture-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-powerpoint-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-sound-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-video-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-word-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-file-zip-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-film"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-filter"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-fire"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-fire-extinguisher"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-flag"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-flag-checkered"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-flag-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-flash"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-flask"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-folder"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-folder-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-folder-open"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-folder-open-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-frown-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-futbol-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-gamepad"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-gavel"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-gear"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-gears"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-gift"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-glass"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-globe"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-graduation-cap"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-group"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hand-grab-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hand-lizard-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hand-paper-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hand-peace-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hand-pointer-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hand-rock-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hand-scissors-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hand-spock-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hand-stop-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hashtag"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hdd-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-headphones"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-heart"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-heart-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-heartbeat"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-history"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-home"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hotel"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hourglass"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hourglass-1"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hourglass-2"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hourglass-3"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hourglass-end"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hourglass-half"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hourglass-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-hourglass-start"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-i-cursor"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-image"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-inbox"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-industry"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-info"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-info-circle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-institution"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-key"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-keyboard-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-language"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-laptop"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-leaf"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-legal"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-lemon-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-level-down"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-level-up"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-life-bouy"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-life-buoy"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-life-ring"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-life-saver"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-lightbulb-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-line-chart"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-location-arrow"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-lock"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-magic"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-magnet"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-mail-forward"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-mail-reply"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-mail-reply-all"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-male"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-map"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-map-marker"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-map-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-map-pin"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-map-signs"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-meh-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-microphone"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-microphone-slash"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-minus"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-minus-circle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-minus-square"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-minus-square-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-mobile"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-mobile-phone"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-money"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-moon-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-mortar-board"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-motorcycle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-mouse-pointer"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-music"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-navicon"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-newspaper-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-object-group"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-object-ungroup"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-paint-brush"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-paper-plane"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-paper-plane-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-paw"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-pencil"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-pencil-square"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-pencil-square-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-percent"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-phone"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-phone-square"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-photo"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-picture-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-pie-chart"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-plane"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-plug"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-plus"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-plus-circle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-plus-square"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-plus-square-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-power-off"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-print"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-puzzle-piece"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-qrcode"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-question"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-question-circle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-quote-left"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-quote-right"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-random"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-recycle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-refresh"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-registered"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-remove"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-reorder"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-reply"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-reply-all"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-retweet"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-road"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-rocket"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-rss"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-rss-square"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-search"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-search-minus"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-search-plus"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-send"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-send-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-server"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-share"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-share-alt"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-share-alt-square"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-share-square"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-share-square-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-shield"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-ship"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-shopping-bag"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-shopping-basket"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-shopping-cart"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sign-in"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sign-out"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-signal"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sitemap"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sliders"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-smile-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-soccer-ball-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sort"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sort-alpha-asc"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sort-alpha-desc"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sort-amount-asc"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sort-amount-desc"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sort-asc"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sort-desc"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sort-down"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sort-numeric-asc"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sort-numeric-desc"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sort-up"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-space-shuttle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-spinner"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-spoon"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-square"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-square-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-star"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-star-half"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-star-half-empty"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-star-half-full"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-star-half-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-star-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sticky-note"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sticky-note-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-street-view"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-suitcase"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-sun-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-support"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-tablet"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-tachometer"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-tag"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-tags"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-tasks"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-taxi"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-television"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-terminal"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-thumb-tack"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-thumbs-down"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-thumbs-o-down"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-thumbs-o-up"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-thumbs-up"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-ticket"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-times"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-times-circle"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-times-circle-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-tint"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-toggle-down"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-toggle-left"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-toggle-off"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-toggle-on"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-toggle-right"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-toggle-up"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-trademark"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-trash"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-trash-o"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-tree"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-trophy"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-truck"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-tty"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-tv"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-umbrella"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-university"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-unlock"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-unlock-alt"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-unsorted"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-upload"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-user"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-user-plus"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-user-secret"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-user-times"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-users"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-video-camera"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-volume-down"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-volume-off"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-volume-up"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-warning"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-wheelchair"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-wifi"></i></a></div>
                    <div class="fa-hover"><a><i class="fa fa-wrench"></i></a></div>
            </div>
        </div>
    </div>
</div>
<div id="pop_div" class="popup popup-edit">
    <div class="content" id="pop_content">
         
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.list_item .img_wrap {width:80px;padding:10px;text-align:center;display:table-cell;vertical-align:middle;}
.list_item .img_wrap i {font-size:50px;color:#ccc;}
.list_item .img_wrap img {max-width:50px;}
.list_item .con_wrap {display:table-cell;width:65%;}
.list_item .con_wrap > div {height:25px;}
.list_item .con_wrap .name {min-width:80px;text-align:right;display:inline-block;color:#ccc;}
.list_item .con_wrap .bkval {border:1px solid #ddd;width:60px;height:20px;display:inline-block;}
.list_item .op_wrap {text-align:center;display:table-cell;width:80px;vertical-align:middle;color:#999;}
.colorItem {border-radius:50%;display:inline-block;width:30px;height:30px;border:1px solid #ddd;}
.fontawesome-icon-list {padding-left:25px;padding-top:10px;padding-bottom:10px; background-color:#fff;}
.fontawesome-icon-list .fa-hover {float:left;width:24%;margin-right:3px;margin-bottom:10px;text-align:center;border:1px solid #ddd;border-radius:4px;height:50px;padding-top:5px;}
.fontawesome-icon-list .fa-hover i {display:block;font-size:40px;}
.fontawesome-icon-list .text-muted {display:none;}
.fontawesome-icon-list .fa-hover.active {background-color:#0894ec;}
.fontawesome-icon-list .fa-hover.active i {color:#fff;}
#content_div {height:120px;overflow-y:auto;}
#content_div img {max-width:100%;}
</style>
<script>
    //多controller数据共享,同步数据
    //picup.up_before = function () { weui.toast.wait(); }
    var page = { scope: null, backup: "" };//内容scope
    var holder = "点击编辑内容";
    angular.module("app", [])
    .controller("APPCtrl", function ($scope, Data) {
        cfg.firstScope = $scope;
        $scope.list = getlist(cfg.id);
        $scope.getimg = function (url) {
            if (url.indexOf("fa ") > -1) { return '<i class="' + url + '" class="media-object"></i>'; }
            else { return '<img src="' + url + '" class="media-object" />'; }
        }
        $scope.del = function (id) {
            if (!confirm("确定要删除吗")) { return false; }
            for (var i = 0; i < $scope.list.length; i++) {
                top.tools.updatedata("mb_del", { "id": id }, function (data) { console.log(data); });
                if ($scope.list[i].id == id) {
                    $scope.list.splice(i, 1);
                    break;
                }
            }
        }
        //保存列表
        $scope.save = function () {
            //var list = top.tools.clearlist(angular.toJson($scope.list));
            //$.post("/design/update.ashx?action=mb_list", { "list": list }, function (data) { console.log(data); })
            cfg.close();
        }
        //选定编辑某条内容
        $scope.edit = function (item) {
            page.scope.model = item;
            page.backup = angular.toJson(item);
            if (!item.content || item.content == "") { $("#content_div").html(holder); }
            else { $("#content_div").html(item.content); }
            //1,不能直接赋于JSON值,否则无效,
            //2,修改完成后无法实时通知另一个controller
            Zepto.router.load("#page_content");
        }
        $scope.new = function () {
            page.backup = "";
            page.scope.model = { id: "", title: "", wxico: "", wxbk: "#000", wxsize: "1", flag: "", dbtype: "list" };
            $("#content_div").html(holder);
        }
        $scope.getbk = function (color) { return { "background-color": color }; }
    })
    .controller("ConCtrl", function ($scope, Data) {
        page.scope = $scope;
        //$scope.model = Data.model;
        $scope.colorArr = ["#cccccc", "#ff6a00", "#000000", "#fff", "#f6383a", "#b6ff00", "#4cd964", "#35aae7", "#678ce1"];
        $scope.getcolor = function (color) {
            return { "background-color": color }
        }
        $scope.setcolor = function (color) {
            $scope.model.wxbk = color;
        }
        $scope.seticon = function (icon) {
            $scope.model.wxico = icon;
            $scope.$digest();
        }
        //取消修改,还原数据
        $scope.cancel = function () {
            if (page.backup != "") {
                var item = JSON.parse(page.backup);
                $scope.model.title = item.title;
                $scope.model.wxico = item.wxico;
                $scope.model.wxbk = item.wxbk;
                $scope.model.wxsize = item.wxsize;
                $scope.model.flag = item.flag;
                $scope.model.dbtype = item.dbtype;
                page.backup = "";
            }
        }
        //只有新数据才实时提交给后台,否则只要更新model,在关闭时统一提交即可
        $scope.save = function () {
            $("#pop_content").html("");
            $scope.model.content = $("#content_div").html();
            var list = top.tools.clearlist(angular.toJson($scope.model));
            if ($scope.model.id == "") {
                Zepto.showPreloader();
                //提交至后台,并将其加入数组
                top.tools.updatedata("mb_new", { "list": list, type: "" }, function (data) {
                    $scope.model.id = data;
                    cfg.firstScope.list.push($scope.model);
                    cfg.firstScope.$digest();
                    Zepto.hidePreloader();
                })
            }
            else {
                top.tools.updatedata("mb_list", { "list": list }, function (data) { });
                alert("保存成功");
            }
        }
    })
    .factory("Data", function () {
        return {
            model: { id: "", title: "", wxico: "", wxbk: "", wxsize: "", flag: "", dbtype: "" }
        }
    }).filter(
		'html', ['$sce', function ($sce) {
		    return function (text) {
		        return $sce.trustAsHtml(text);
		    }
		}]);
    $(function () {
        $(".fa-hover").click(function () {
            $(".fa-hover").removeClass("active");
            $(this).addClass("active");
        });
    })
    var icon = {};
    icon.set = function () {
        var icon = $(".fa-hover.active").find("i").attr("class");
        page.scope.seticon(icon);
    }
    function openEditor() {
        Zepto.showIndicator();
        $("#pop_content").html("").append('<iframe id="pop_ifr" class="popifr" style="width:100%;height:100%;border:none;"></iframe>');
        $("#pop_ifr").attr("src", "editor.aspx");
        document.getElementById("pop_ifr").onload = function () {
            Zepto.popup('.popup-edit');
            var html = $("#content_div").html();
            if (html != holder) { $("#pop_ifr")[0].contentWindow.settxt(html); }
            else { $("#pop_ifr")[0].contentWindow.settxt(""); }
            Zepto.hideIndicator();
        }
    }
    function closeEditor(content) {
        Zepto.closeModal();
        $("#pop_ifr").remove();
    }
    function saveEditor(content) {
        $("#content_div").html(content);
        setTimeout(function () { closeEditor(); }, 300);//兼容ios
    }
</script>
</asp:Content>
