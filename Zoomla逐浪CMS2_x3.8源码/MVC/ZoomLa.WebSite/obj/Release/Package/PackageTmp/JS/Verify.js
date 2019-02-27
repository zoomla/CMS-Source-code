String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}

//去掉字符串头尾空格   
function trim(str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}
//身份证号码验证
/**  
 * 身份证15位编码规则：dddddd yymmdd xx p   
 * dddddd：地区码   
 * yymmdd: 出生年月日   
 * xx: 顺序类编码，无法确定   
 * p: 性别，奇数为男，偶数为女  
 *
 * 身份证18位编码规则：dddddd yyyymmdd xxx y   
 * dddddd：地区码   
 * yyyymmdd: 出生年月日   
 * xxx:顺序类编码，无法确定，奇数为男，偶数为女   
 * y: 校验码，该位数值可通过前17位计算获得  
 * <p />  
 * 18位号码加权因子为(从右到左) Wi = [ 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2,1 ]  
 * 验证位 Y = [ 1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2 ]   
 * 校验位计算公式：Y_P = mod( ∑(Ai×Wi),11 )   
 * i为身份证号码从右往左数的 2...18 位; Y_P为脚丫校验码所在校验码数组位置  
 */
var Wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1];// 加权因子   
var ValideCode = [1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2];// 身份证验证位值.10代表X   

function CheckIdCard(idCard) {
    idCard = trim(idCard.replace(/ /g, ""));
    if (idCard.length == 15) 
        return isValidityBrithBy15IdCard(idCard);
    if (idCard.length == 18) {
        var a_idCard = idCard.split("");// 得到身份证数组   
        if (isValidityBrithBy18IdCard(idCard) && isTrueValidateCodeBy18IdCard(a_idCard)) {
            return true;
        } else {
            return false;
        }
    }
    return false;
}
/** 判断身份证号码为18位时最后的验证位是否正确  
 * @param a_idCard 身份证号码数组  
 */
function isTrueValidateCodeBy18IdCard(a_idCard) {
    var sum = 0; // 声明加权求和变量   
    if (a_idCard[17].toLowerCase() == 'x') {
        a_idCard[17] = 10;// 将最后位为x的验证码替换为10方便后续操作   
    }
    for (var i = 0; i < 17; i++) {
        sum += Wi[i] * a_idCard[i];// 加权求和   
    }
    valCodePosition = sum % 11;// 得到验证码所位置   
    if (a_idCard[17] == ValideCode[valCodePosition]) {
        return true;
    } else {
        return false;
    }
}
/**  通过身份证判断是男是女 
 * @param idCard 15/18位身份证号码   
 * @return 'female'-女、'male'-男  
 */
function maleOrFemalByIdCard(idCard) {
    idCard = trim(idCard.replace(/ /g, ""));// 对身份证号码做处理。包括字符间有空格。   
    if (idCard.length == 15) {
        if (idCard.substr(14) % 2 == 0) {
            return 'female';
        } else {
            return 'male';
        }
    } else if (idCard.length == 18) {
        if (idCard.substr(16,1) % 2 == 0) {
            return 'female';
        } else {
            return 'male';
        }
    } else {
        return null;
    }
    //  可对传入字符直接当作数组来处理   
    // if(idCard.length==15){   
    // alert(idCard[13]);   
    // if(idCard[13]%2==0){   
    // return 'female';   
    // }else{   
    // return 'male';   
    // }   
    // }else if(idCard.length==18){   
    // alert(idCard[16]);   
    // if(idCard[16]%2==0){   
    // return 'female';   
    // }else{   
    // return 'male';   
    // }   
    // }else{   
    // return null;   
    // }   
}
/** 验证18位数身份证号码中的生日是否是有效生日  
 * @param idCard 18位书身份证字符串  
 */
function isValidityBrithBy18IdCard(idCard18) {
    var year = idCard18.substr(6, 4);
    var month = idCard18.substr(10, 2);
    var day = idCard18.substr(12, 2);
    var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
    // 这里用getFullYear()获取年份，避免千年虫问题, 也避免与 FF 的不兼容  
    if (temp_date.getFullYear() != parseFloat(year)
          || temp_date.getMonth() != parseFloat(month) - 1
          || temp_date.getDate() != parseFloat(day)) {
        return false;
    } else {
        return true;
    }
}
/**  
 * 验证15位数身份证号码中的生日是否是有效生日  
 * @param idCard15 15位身份证字符串
 */
function isValidityBrithBy15IdCard(idCard15) {
    var year = idCard15.substring(6, 8);
    var month = idCard15.substring(8, 10);
    var day = idCard15.substring(10, 12);
    var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
    //-- 15 的年份是两位的
    var tmp_year = temp_date.getFullYear().toString().substr(2, 2);
    if (parseFloat(tmp_year) != parseFloat(year)
            || temp_date.getMonth() != parseFloat(month) - 1
            || temp_date.getDate() != parseFloat(day)) {
        return false;
    } else {
        return true;
    }
}

//固定电话、传真 验证 
function CheckPhone(sPhone) {
   // var reg = new RegExp("/^((0\\d{2,4})-)(\\d{7,8})(-(\\d{3,}))?$/", "ig");
    var reg = /^((0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$/;
    //var reg = /^((\+?[0-9]{2,4}\-[0-9]{3,4}\-)|([0-9]{3,4}\-))?([0-9]{7,8})(\-[0-9]+)?$/;
    return reg.test(sPhone);
}
//手机验证
function CheckMobile(mobile)
{
    return (/^1[3|4|5|8][0-9]\d{8}$/.test(mobile));
}
//准考证验证
function CheckSchCard(schid)
{
    return (/^\d{14}$/.test(schid));
}
//邮箱验证
function CheckEmail(email)
{
    var mailReg = /^[a-zA-Z]([\w]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;//邮箱格式的验证
    //var result = email.match(mailReg);
    //return (result[0].length == email.length);
    return mailReg.test(email);
}





