//----------------------------
// http://webabcd.cnblogs.com/
//----------------------------

/*Helper 开始*/
function yy_sgv_get(id)
{
/// <summary>$get()</summary>

    return document.getElementById(id);
}

function yy_sgv_changeCssClass(param, cssClass)
{
/// <summary>根据ID或元素设置其样式 css 类名</summary>

    if (typeof(param) == 'object')
        param.className = cssClass;
    else if (typeof(param) == 'string')
        yy_sgv_get(param).className = cssClass;
}

String.prototype.yy_sgv_endsWith = function(s)
{   
/// <summary>EndsWith()</summary>

    var reg = new RegExp(s + "$");   
    return reg.test(this);
}  

String.prototype.yy_sgv_startsWith = function(s)
{
/// <summary>StartsWith()</summary>

    var reg = new RegExp("^" + s);   
    return reg.test(this);
}

function yy_sgv_addEvent(obj, evtType, fn) 
{
/// <summary>绑定事件</summary>

    // FF
    if (obj.addEventListener)
    {
        obj.addEventListener(evtType, fn, true);
        return true;
    }
    // IE
    else if (obj.attachEvent)
    {
        var r = obj.attachEvent("on" + evtType, fn);
        return r;
    }
    else
    {
        return false;
    }    
}
/*Helper 结束*/

/*鼠标经过行时改变行的样式 开始*/
var yy_sgv_originalCssClassName = ''; // 初始样式

function yy_sgv_changeMouseOverCssClass(obj, cssClassName)
{
/// <summary>鼠标经过时改变样式</summary>

    if (arguments.length == 1)
    {
        yy_sgv_changeCssClass(obj, yy_sgv_originalCssClassName);
        yy_sgv_originalCssClassName = '';
    }
    else
    {
        yy_sgv_originalCssClassName = obj.className;
        yy_sgv_changeCssClass(obj, cssClassName);
    }
}
/*鼠标经过行时改变行的样式 结束*/


/*联动复选框 开始*/
var yy_sgv_ccGridView_pre = new Array(); // cs中动态向其灌数据（GridView内控件ID的前缀数组）
var yy_sgv_ccAll_post = new Array(); // cs中动态向其灌数据（全选复选框ID的后缀数组）
var yy_sgv_ccItem_post = new Array(); // cs中动态向其灌数据（项复选框ID的后缀数组）

function yy_sgv_ccCheck(e) 
{
/// <summary>单击复选框时</summary>

    var evt = e || window.event; // FF || IE
    var obj = evt.target || evt.srcElement  // FF || IE

    var ccIndex = -1;
    for (var i=0; i<yy_sgv_ccGridView_pre.length; i++)
    {   
        if (obj.id.yy_sgv_startsWith(yy_sgv_ccGridView_pre[i]))
        {
            ccIndex = i;
            break;
        }
    }
    
    if (ccIndex != -1)
    {
        if (obj.id.yy_sgv_endsWith(yy_sgv_ccAll_post[i]))
        {
            yy_sgv_ccCheckAll(ccIndex, obj.checked);
        }
        else if (obj.id.yy_sgv_endsWith(yy_sgv_ccItem_post[i]))
        {
            yy_sgv_ccCheckItem(ccIndex);
        }
    }
}

function yy_sgv_ccCheckAll(ccIndex, isCheckAll)
{
/// <summary>设置全选复选框的状态</summary>

    var elements =  document.getElementsByTagName("INPUT");
    
    for (i=0; i< elements.length; i++) 
    {       
        if (elements[i].type == 'checkbox' 
            && elements[i].id.yy_sgv_startsWith(yy_sgv_ccGridView_pre[ccIndex]) 
            && elements[i].id.yy_sgv_endsWith(yy_sgv_ccItem_post[ccIndex])) 
        {
            elements[i].checked = isCheckAll;
            
            if (yy_sgv_crClassName != '')
            {
                yy_sgv_changeCheckedRowCssClass(elements[i], yy_sgv_crClassName, false);
            }
        }
    }    
}

function yy_sgv_ccCheckItem(ccIndex)
{
/// <summary>单击项复选框时</summary>

    var elements =  document.getElementsByTagName("INPUT");
    
    var checkedNum = 0;
    var uncheckedNum = 0;
    
    for (i=0; i< elements.length; i++) 
    {       
        if (elements[i].type == 'checkbox' 
            && elements[i].id.yy_sgv_startsWith(yy_sgv_ccGridView_pre[ccIndex]) 
            && elements[i].id.yy_sgv_endsWith(yy_sgv_ccItem_post[ccIndex])) 
        {
            if (elements[i].checked)
            {
                checkedNum++;
            }
            else
            {
                uncheckedNum++;
            }
        }
    }
    
    if (uncheckedNum == 0)
    {
        yy_sgv_ccCheckCheckbox(yy_sgv_ccGridView_pre[ccIndex], yy_sgv_ccAll_post[ccIndex], true)
    }
    else
    {
        yy_sgv_ccCheckCheckbox(yy_sgv_ccGridView_pre[ccIndex], yy_sgv_ccAll_post[ccIndex], false)
    }
}

function yy_sgv_ccCheckCheckbox(pre, post, isCheckAll)
{
/// <summary>设置项复选框的状态</summary>

    var elements =  document.getElementsByTagName("INPUT");
    
    for (i=0; i< elements.length; i++) 
    {       
        if (elements[i].type == 'checkbox'
            && elements[i].id.yy_sgv_startsWith(pre) 
            && elements[i].id.yy_sgv_endsWith(post)) 
        {
            elements[i].checked = isCheckAll;
            break;
        }
    }    
}

function yy_sgv_ccListener()
{
/// <summary>监听所有联动复选框的单击事件</summary>

    var elements =  document.getElementsByTagName("INPUT");
    
    for (i=0; i< elements.length; i++) 
    {       
        if (elements[i].type == 'checkbox') 
        {
            for (j=0; j<yy_sgv_ccGridView_pre.length; j++)
            {
                if (elements[i].id.yy_sgv_startsWith(yy_sgv_ccGridView_pre[j]) 
                    && (elements[i].id.yy_sgv_endsWith(yy_sgv_ccAll_post[j]) || elements[i].id.yy_sgv_endsWith(yy_sgv_ccItem_post[j])))
                {
                    yy_sgv_addEvent(elements[i], 'click', yy_sgv_ccCheck); 
                    break;
                }
            }
        }
    }    
}
   
if (document.all)
{
    window.attachEvent('onload', yy_sgv_ccListener)
}
else
{
    window.addEventListener('load', yy_sgv_ccListener, false);
}
/*联动复选框 结束*/


/*行的指定复选框选中时改变行的样式 开始*/
var yy_sgv_crGridView_pre = new Array(); // cs中动态向其灌数据（GridView内控件ID的前缀数组）
var yy_sgv_crCheckbox_post = new Array(); // cs中动态向其灌数据（数据行的复选框ID的后缀数组）
var yy_sgv_crClassName = ''; // cs中动态向其灌数据（css 类名）

var yy_sgv_crCheckbox = new Array(); // Checkbox数组
var yy_sgv_crCssClass = new Array(); // css 类名数组

function yy_sgv_changeCheckedRowCssClass(obj, cssClass, enableChangeMouseOverCssClass)
{
/// <summary>数据行的指定复选框选中行时改变行的样式</summary>

    if (yy_sgv_crClassName == '') return;
    
    var objChk = obj;
    var objTr = obj;
    
    do
    {
        objTr = objTr.parentNode;
    } 
    while (objTr.tagName != "TR")
    
    if (objChk.checked)
    {
		yy_sgv_crCheckbox.push(objChk.id);
		if (yy_sgv_originalCssClassName != '' && enableChangeMouseOverCssClass)
		{
		    yy_sgv_crCssClass.push(yy_sgv_originalCssClassName);
		}
		else
		{
		    yy_sgv_crCssClass.push(objTr.className);
		}
		    
		objTr.className = cssClass;
		
		if (enableChangeMouseOverCssClass)
            yy_sgv_originalCssClassName = cssClass;
    }
	else
	{
	    for (var i=0; i<yy_sgv_crCheckbox.length; i++)
        {
            if (yy_sgv_crCheckbox[i] == objChk.id)
            {
                objTr.className = yy_sgv_originalCssClassName = yy_sgv_crCssClass[i];
                yy_sgv_crCheckbox.splice(i, 1);
                yy_sgv_crCssClass.splice(i, 1);
                
                break;
            }
        }
	}
}

function yy_sgv_crCheck(e) 
{
/// <summary>单击数据行的复选框时</summary>

    var evt = e || window.event; // FF || IE
    var obj = evt.target || evt.srcElement  // FF || IE

    yy_sgv_changeCheckedRowCssClass(obj, yy_sgv_crClassName, true)
}

function yy_sgv_crListener()
{
/// <summary>监听所有数据行的复选框的单击事件</summary>

    var elements =  document.getElementsByTagName("INPUT");
    
    for (i=0; i< elements.length; i++) 
    {       
        if (elements[i].type == 'checkbox') 
        {
            for (j=0; j<yy_sgv_crGridView_pre.length; j++)
            {
                if (elements[i].id.yy_sgv_startsWith(yy_sgv_crGridView_pre[j]) 
                    && elements[i].id.yy_sgv_endsWith(yy_sgv_crCheckbox_post[j]))
                {
                    yy_sgv_addEvent(elements[i], 'click', yy_sgv_crCheck); 
                    break;
                }
            }
        }
    }    
}
   
if (document.all)
{
    window.attachEvent('onload', yy_sgv_crListener)
}
else
{
    window.addEventListener('load', yy_sgv_crListener, false);
}
/*行的指定复选框选中时改变行的样式 结束*/


/*右键菜单 开始*/
yy_sgv_rightMenu = function ()
{
/// <summary>构造函数</summary>

    this._menu = null;
    this._menuItem = null;
    this._handle = null;
    this._target = null;
    this._cssClass = null
};

yy_sgv_rightMenu.prototype = 
{
/// <summary>相关属性和相关方法</summary>

    get_menu: function() 
    {
        return this._menu;
    },
    set_menu: function(value)
    {
        this._menu = value;
    },
    
    get_handle: function() 
    {
        return this._handle;
    },
    set_handle: function(value)
    {
        this._handle = value;
    },
    
    get_target: function() 
    {
        return this._target;
    },
    set_target: function(value)
    {
        this._target = value;
    },
    
    get_cssClass: function() 
    {
        return this._cssClass;
    },
    set_cssClass: function(value)
    {
        this._cssClass = value;
    },
    
    get_menuItem: function() 
    {
        return this._menuItem;
    },
    set_menuItem: function(value)
    {
        this._menuItem = value;
    },


    show:function(e)
    {
        if (this.get_menuItem() == null) 
        {
            this.hidden();
            return true;
        }
    
        var rightMenu = this.get_menu();
        if (rightMenu == null)
        {
            rightMenu = document.createElement("div");
        }

        var menuInnerHTML = ""; // 菜单容器里的HTML内容
        var $items = this.get_menuItem();
        var $handle = this.get_handle();
        var $target = this.get_target();
        
        rightMenu.className = "yy_sgv_rightMenuBase"; 
        if (this.get_cssClass() == null || this.get_cssClass() == "")
            rightMenu.className += " " + "yy_sgv_rightMenu";
        else
            rightMenu.className += " " + this.get_cssClass();            
        
        menuInnerHTML += "<ul>";
        
        for (var i in $items)
        {
	        if ($items[i].indexOf("<hr") != -1)
	        {
		        menuInnerHTML += $items[i];
		    }
	        else
	        {
	            if ($target[i] == "")
	            {
	                $target[i] = "_self";
	            }
	        
		        menuInnerHTML += "<li><a href=\"" + $handle[i] + "\" target=\"" + $target[i] + "\">";
		        menuInnerHTML += $items[i];
		        menuInnerHTML += "</a></li>";   
		    }
        }
        
        menuInnerHTML += "</ul>";
        // alert(menuInnerHTML);
        
        rightMenu.innerHTML = menuInnerHTML;
        
        rightMenu.style.visibility = "visible";
        
        rightMenu.onmousedown = function(e)
        {
	        e=e||window.event;
	        document.all ? e.cancelBubble = true : e.stopPropagation();
        }
        
        rightMenu.onselectstart = function()
        {
	        return false;
        }
        
        document.body.appendChild(rightMenu);
        this.set_menu(rightMenu); // 方便别的方法引用

	    e = e || window.event;
	    
	    var root = document.documentElement;
	    var x = root.scrollLeft + e.clientX; 
	    var y = root.scrollTop + e.clientY;
	    
	    if (this.get_menu().clientWidth+e.clientX > root.clientWidth)
	    {
		    x = x - this.get_menu().clientWidth;
	    }
	    if (this.get_menu().clientHeight+e.clientY > root.clientHeight)
	    {
		    y = y - this.get_menu().clientHeight;
	    }
	    
	    this.get_menu().style.left = x + "px"; 
	    this.get_menu().style.top = y + "px"; 
	    this.get_menu().style.visibility = "visible";
	    
	    this.set_handle(null);
	    this.set_menuItem(null);
	    this.set_target(null);
	    
	    return false;
    },

    hidden:function() 
    {
        if (this.get_menu() != null)
        {
	        this.get_menu().style.visibility = "hidden";
	    }
    }
}

if (document.all)
{
    window.attachEvent('onload', yy_sgv_rightMenu = new yy_sgv_rightMenu())
}
else
{
    window.addEventListener('load', yy_sgv_rightMenu = new yy_sgv_rightMenu(), false);
}

function yy_sgv_setRightMenu(handle, menuItem, target, cssClass)
{
/// <summary>设置需要显示的右键菜单</summary>

    yy_sgv_rightMenu.set_handle(handle);
    yy_sgv_rightMenu.set_menuItem(menuItem);
    yy_sgv_rightMenu.set_target(target);
    yy_sgv_rightMenu.set_cssClass(cssClass);
}
/*右键菜单 结束*/