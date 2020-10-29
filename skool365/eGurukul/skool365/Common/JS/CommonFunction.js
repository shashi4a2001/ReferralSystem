// JScript File
/////////////////////////////////////////////////
///Function Name: fnCommonLowerSideValue
///Written By   : Shashi
///Return Type  : Two Precision Number (1.00) With Lower Side
///Task         : Formattin of Numbers , Down side value only last caharacter
//                Like value is (1.27) then it will return (1.20)
/////////////////////////////////////////////////
function fnCommonUpperSideRoundFigureValue(num) {


    if (num == "") {
        num = 0;
    }

    num = new Number(num);
    num = num.toFixed(2);

    var str = num.toString();
    str = str.substring(str.length - 2, str.length);


    if (eval(str) > 0)
    { return (eval(num) - eval("0." + str) + 1); }
    else { return (eval(num)) }

}
//-----------------------------------------

/////////////////////////////////////////////////
///Function Name: fnCommonLowerSideValue
///Written By   : Shashi
///Return Type  : Two Precision Number (1.00) With Lower Side
///Task         : Formattin of Numbers , Down side value only last caharacter
//                Like value is (1.27) then it will return (1.20)
/////////////////////////////////////////////////
function fnCommonLowerSideValue(num) {
    if (num == "") {
        num = 0;
    }
    num = new Number(num);

    num = num.toFixed(2);
    var str = new String();
    str = num.toString();
    str = str.substring(0, str.length - 1);
    num = eval(str);
    num = num.toFixed(2);

    return num;
}
//-----------------------------------------


/////////////////////////////////////////////////
///Function Name: fnCommonFormatNumbers
///Written By   : Shashi
///Return Type  : Two Precision Number (1.00)
///Task         : Formattin of Numbers
/////////////////////////////////////////////////
function fnCommonFormatNumbers(num) {
    if (num == "") {
        num = 0;
    }
    num = new Number(num);
    return num.toFixed(2);
}
//-----------------------------------------

/////////////////////////////////////////////////
///Function Name: fnCommonConvertStringToNumber
///Written By   : Shashi
///Return Type  : Number
///Task         : Convert String into number
/////////////////////////////////////////////////
function fnCommonConvertStringToNumber(num) {
    if (num == "") {
        num = 0;
    }
    return eval(num);
}
//------------------------------------------

/////////////////////////////////////////////////
///Function Name: fnCommonCheckAlphabets
///Written By   : Shashi
///Return Type  : True or False (If Any Numeric in string then return Talse else True)
///Task         : Check for Numeric character within string
/////////////////////////////////////////////////
function fnCommonCheckAlphabets(ch) {
    ch = new String(ch);
    var c = new Number(-1);

    c = ch.search('0');
    if (c > 0) {
        return false;
    }
    c = ch.search('1');
    if (c > 0) {
        return false;
    }
    c = ch.search('2');
    if (c > 0) {
        return false;
    }

    c = ch.search('3');
    if (c > 0) {
        return false;
    }
    c = ch.search('4');
    if (c > 0) {
        return false;
    }
    c = ch.search('5');
    if (c > 0) {
        return false;
    }
    c = ch.search('6');
    if (c > 0) {
        return false;
    }
    c = ch.search('7');
    if (c > 0) {
        return false;
    }
    c = ch.search('8');
    if (c > 0) {
        return false;
    }
    c = ch.search('9');
    if (c > 0) {
        return false;
    }
    return true;

}

/////////////////////////////////////////////////
///Function Name: fnCommonAcceptCharacterOnly
///Written By   : Shashi
///Return Type  : True or False
///               If Any Character Except (a-z and A-Z and Enter and Back Space 
///               Then Return False Else Return True)                    
///Task         : Accept Character Only Also Accept Back Space and Enter Key
/////////////////////////////////////////////////
//---------------------------------------------
//Method to calling to this function
//onkeypress="return fnCommonAcceptCharacterOnly(event)"
//---------------------------------------------

function fnCommonAcceptCharacterOnly(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    // alert(charCode);
    if (charCode == 32)
        return true;
    if (charCode == 13)
        return true;

    if ((charCode >= 65 && charCode <= 90))
        return true;

    if ((charCode >= 97 && charCode <= 122))
        return true;

    return false;
}

//-----------------------------------------------------

/////////////////////////////////////////////////
///Function Name: fnCommonAcceptNumberOnly
///Written By   : Shashi
///Return Type  : True or False
///               If Any Character Except (0-9 and Enter and Back Space 
///               Then Return False Else Return True)                    
///Task         : Accept Number Only Also Accept Back Space and Enter Key
/////////////////////////////////////////////////
//---------------------------------------------
//Method to calling to this procedure
//onkeypress="return fnCommonAcceptNumberOnly(event)"
//---------------------------------------------
function fnCommonAcceptNumberOnly(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode == 32)
        return true;
    if (charCode == 13)
        return true;

    if ((charCode >= 48 && charCode <= 57))
        return true;


    return false;
}



//-----------------------------------------------------

/////////////////////////////////////////////////
///Function Name: fnCommonAcceptNumberWithDecimalOnly
///Written By   : Shashi
///Return Type  : True or False
///               If Any Character Except (0-9 and Enter and Back Space 
///               Then Return False Else Return True)                    
///Task         : Accept Number With Decimal Only Also Accept Back Space and Enter Key
/////////////////////////////////////////////////
//---------------------------------------------
//Method to calling to this procedure
//onkeypress="return fnCommonAcceptNumberWithDecimalOnly(event)"
//---------------------------------------------

function fnCommonAcceptNumberWithDecimalOnly(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
        return false;

    return true;
}



//-----------------------------------------------------


//-----------------------------------------------------

/////////////////////////////////////////////////
///Function Name: fnCommonCheckNumberOnOnFocusOutAndMakeBlank
///Written By   : Shashi
///Return Type  : alert message
///               If Any Character Except (0-9 and Enter and Back Space 
///               Then Return False Else Return True)                    
///Task         : check Number Only
/////////////////////////////////////////////////
//---------------------------------------------
//Method to calling to this procedure
//onfocusout="fnCommonCheckNumberOnOnFocusOutAndMakeBlank(this)"
//---------------------------------------------
function fnCommonCheckNumberOnOnFocusOutAndMakeBlank(t) {

    var numbers = /^[0-9]+$/;
    if (t.value.match(numbers)) {
        var i = '';
    }
    else {
        alert('Please input numeric characters only');
        t.value = '';

    }

}
//-----------------------------------------------------

/////////////////////////////////////////////////
///Function Name: fnCommonCheckNumberWithDecimalOnly
///Written By   : Shashi
///Return Type  : True or False
///               If Any Character Except (0-9 and Enter and Back Space 
///               Then Return False Else Return True)                    
///Task         : Allow Number With Decimal Only Also Accept Back Space and Enter Key
/////////////////////////////////////////////////
//---------------------------------------------
//Method to calling to this procedure
// return onblur="fnCommonCheckNumberWithDecimalOnly(this,2,false);" 
//---------------------------------------------

function fnCommonCheckNumberWithDecimalOnly(obj, decimalPlaces, allowNegative) {
    var temp = obj.value;

    // avoid changing things if already formatted correctly
    var reg0Str = '[0-9]*';
    if (decimalPlaces > 0) {
        reg0Str += '\[\,\.]?[0-9]{0,' + decimalPlaces + '}';
    } else if (decimalPlaces < 0) {
        reg0Str += '\[\,\.]?[0-9]*';
    }
    reg0Str = allowNegative ? '^-?' + reg0Str : '^' + reg0Str;
    reg0Str = reg0Str + '$';
    var reg0 = new RegExp(reg0Str);
    if (reg0.test(temp)) return true;

    // first replace all non numbers
    var reg1Str = '[^0-9' + (decimalPlaces != 0 ? '.' : '') + (decimalPlaces != 0 ? ',' : '') + (allowNegative ? '-' : '') + ']';
    var reg1 = new RegExp(reg1Str, 'g');
    temp = temp.replace(reg1, '');

    if (allowNegative) {
        // replace extra negative
        var hasNegative = temp.length > 0 && temp.charAt(0) == '-';
        var reg2 = /-/g;
        temp = temp.replace(reg2, '');
        if (hasNegative) temp = '-' + temp;
    }

    if (decimalPlaces != 0) {
        var reg3 = /[\,\.]/g;
        var reg3Array = reg3.exec(temp);
        if (reg3Array != null) {
            // keep only first occurrence of .
            //  and the number of places specified by decimalPlaces or the entire string if decimalPlaces < 0
            var reg3Right = temp.substring(reg3Array.index + reg3Array[0].length);

            reg3Right = reg3Right.replace(reg3, '');
            reg3Right = decimalPlaces > 0 ? reg3Right.substring(0, decimalPlaces) : reg3Right;
            if (reg3Right != '') {
                alert('Invalid Number');
                obj.value = '';
                obj.focus();
                return false;

            }
            // temp = temp.substring(0, reg3Array.index) + '.' + reg3Right;
        }
    }

    obj.value = temp;
}




////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
///Function Name: fnCommon_isValidDate
///Written By   : Shashi
///Return Type  : True or False
///Task         : Check Date Format In DD/MM/YYYY
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

//<script language = "Javascript">
/**
* DHTML date validation script for dd/mm/yyyy. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
*/

function fnCommon_isValidDate_CustomValidator(source, arguments) {
    var dtStr = arguments.Value.toString();
    var daysInMonth = DaysArray(12)
    var pos1 = dtStr.indexOf(dtCh)
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
    var strDay = dtStr.substring(0, pos1)
    var strMonth = dtStr.substring(pos1 + 1, pos2)
    var strYear = dtStr.substring(pos2 + 1)
    strYr = strYear
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
    }
    month = parseInt(strMonth)
    day = parseInt(strDay)
    year = parseInt(strYr)
    if (pos1 == -1 || pos2 == -1) {
        //alert("The date format should be : dd/mm/yyyy");
        alert("Invalid Date");
        arguments.IsValid = false;
        return;
    }
    if (strMonth.length < 1 || month < 1 || month > 12) {
        //alert("Please enter a valid month");
        alert("Invalid Date");
        arguments.IsValid = false;
        return;
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        //alert("Please enter a valid day");
        alert("Invalid Date");
        arguments.IsValid = false;
        return;
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
        //alert("Please enter a valid 4 digit year between " + minYear + " and " + maxYear);
        alert("Invalid Date");
        arguments.IsValid = false;
        return;
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
        //alert("Please enter a valid date")
        alert("Invalid Date");
        arguments.IsValid = false;
        return;
    }
    arguments.IsValid = true;
}
/////////////////////////////////




////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
///Function Name: fnCommon_isValidDate
///Written By   : Shashi
///Return Type  : True or False
///Task         : Check Date Format In DD/MM/YYYY
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

//<script language = "Javascript">
/**
* DHTML date validation script for dd/mm/yyyy. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
*/

function fnCommon_isValidDate(dtStr) {
    var daysInMonth = DaysArray(12)
    var pos1 = dtStr.indexOf(dtCh)
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
    var strDay = dtStr.substring(0, pos1)
    var strMonth = dtStr.substring(pos1 + 1, pos2)
    var strYear = dtStr.substring(pos2 + 1)
    strYr = strYear
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
    }
    month = parseInt(strMonth)
    day = parseInt(strDay)
    year = parseInt(strYr)
    if (pos1 == -1 || pos2 == -1) {
        alert("The date format should be : dd/mm/yyyy")
        return false
    }
    if (strMonth.length < 1 || month < 1 || month > 12) {
        alert("Please enter a valid month")
        return false
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        alert("Please enter a valid day")
        return false
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
        alert("Please enter a valid 4 digit year between " + minYear + " and " + maxYear)
        return false
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
        alert("Please enter a valid date")
        return false
    }
    return true
}
/////////////////////////////////
// Declaring valid date character, minimum year and maximum year
var dtCh = "/";
var minYear = 1900;
var maxYear = 2100;

function isInteger(s) {
    var i;
    for (i = 0; i < s.length; i++) {
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function stripCharsInBag(s, bag) {
    var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary(year) {
    // February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
}
function DaysArray(n) {
    for (var i = 1; i <= n; i++) {
        this[i] = 31
        if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
        if (i == 2) { this[i] = 29 }
    }
    return this
}




////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
//End Of Is Valid Date Function ValidateForm()
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////




function fnCommon_GetValueFromControls(Ctrl) {
    return document.getElementById("ctl00_ContentPlaceHolder2_" + Ctrl).value.toString();
}

function fnCommon_SetValueToControls(Ctrl, V) {
    document.getElementById("ctl00_ContentPlaceHolder2_" + Ctrl).value = V;
}


function fnCommon_SetFocusOnControls(Ctrl) {
    return document.getElementById("ctl00_ContentPlaceHolder2_" + Ctrl).focus();
}


function fnCommon_Integer_IsBlankOrZero(i) {
    if (i == '' || i == '0' || isNaN(i))
    { return true; }
    else { return false; }
}

function fnCommon_String_IsBlankOrZero(i) {
    if (i == '' || i == '0')
    { return false; }
    else { return true; }
}

function fnCommonCompanyId() {
    return document.getElementById('ctl00$hidMasterPageCompanyId').value;
}

function fnCommonCallWebServiceConnectionString() {
    return document.getElementById('ctl00$hidMasterCallWebServiceConnectionString').value;
}

function fnCommonReportConnectionString() {
    return document.getElementById('ctl00$hidMasaterPageReportConnectionString').value;
}

function fnCommonCscId() {
    return document.getElementById('ctl00$hidMasterPageCscId').value;
}

function fnCommonCscCode() {
    return document.getElementById('ctl00$hidMasterPageCSCCode').value;
}

function fnCommonCscName() {
    return document.getElementById('ctl00$hidMsterPageCscName').value;
}


function fnDateDifferenceInDays(Dt1, Dt2) {
    var date1 = new Date();
    var date2 = new Date();
    var diff = new Date();
    var date1temp = new Date();
    var date2temp = new Date();

    //if (isValidDate(dateform.firstdate.value) && isValidTime(dateform.firsttime.value))
    // { // Validates first date 

    date1temp = Convert_DD_MM_YYYY_To_Date(Dt1)
    date1.setTime(date1temp.getTime());

    //}

    //else return false; // otherwise exits

    //if (isValidDate(dateform.seconddate.value) && isValidTime(dateform.secondtime.value)) 

    //{ // Validates second date 

    date2temp = Convert_DD_MM_YYYY_To_Date(Dt2)
    date2.setTime(date2temp.getTime());

    //}

    //else return false; // otherwise exits

    // sets difference date to difference of first date and second date

    diff.setTime(Math.abs(date1.getTime() - date2.getTime()));

    timediff = diff.getTime();

    weeks = Math.floor(timediff / (1000 * 60 * 60 * 24 * 7));

    timediff -= weeks * (1000 * 60 * 60 * 24 * 7);

    days = Math.floor(timediff / (1000 * 60 * 60 * 24));
    timediff -= days * (1000 * 60 * 60 * 24);

    //hours = Math.floor(timediff / (1000 * 60 * 60)); 
    //timediff -= hours * (1000 * 60 * 60);

    //mins = Math.floor(timediff / (1000 * 60)); 
    //timediff -= mins * (1000 * 60);

    //secs = Math.floor(timediff / 1000); 
    //timediff -= secs * 1000;

    //dateform.difference.value = weeks + " weeks, " + days + " days, " + hours + " hours, " + mins + " minutes, and " + secs + " seconds";
    weeks = weeks * 7;
    weeks = weeks + days;

    return weeks; // form should never submit, returns false
}


function fnIsFullAndFinalDone(RegnNo) {


    var sAgreeementddCol = 'REGN_NO';
    //var sCurrentDate = window.document.forms(0).ctl00_ContentPlaceHolder2_hidCurrentDate.value;
    //var sAgreementddjoin = 'inner join cheque_bussiness_t as cb on cb.cb_cheque_no = rec.RECEIPT_DD_CHEQUE_NO inner join registration_t as reg on rec.receipt_regn_id = regn_id';
    var sWhereClause = "REGN_NO='" + RegnNo + "' AND  REGN_FULL_AND_FINAL_DONE_YN='Y' AND REGN_ACTIVE_YN='Y'";
    var sOrderBy = '';
    CallWebService(false, 'CommonFunction', '', 'fnIsFullAndFinalDone', 'SimpleQuery', 'REGISTRATION_T', sAgreeementddCol, '', sWhereClause, '', '', sOrderBy, false, 2);

    var result = g_objXML.getElementsByTagName("DataSet/diffgr:diffgram").item(0).text;
    if (result != '') {
        return true;
    }
    else {
        return false;
    }

}

 //Alert Message
        function Confirm(Msg) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(Msg)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            while (confirm_value.lastChild) {
                confirm_value.removeChild(confirm_value.lastChild);
            }
            document.forms[0].appendChild(confirm_value);
        }

//Used to check Checkbox inside the grid.
        function SelectAllCheckboxes(spanChk) {
            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {

                    //elm[i].click();
                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;
                }
        }

        //Show Image in Separate Window
        function showImg(imgSrc, H, W, Caption) {
            var newImg = window.open("", "myImg", config = "height=" + H + ",width=" + W + "")
            newImg.document.write("<title>" + Caption + "</title>")
            newImg.document.write("<img src='" + imgSrc + "' height='" + H + "' width='" + W + "' onclick='window.close()' style='position:absolute;left:0;top:0'>")
            newImg.document.write("<script type='text/javascript'> document.oncontextmenu = new Function(\"return false\") </sc" + "ript>")
            newImg.document.close()
        }


        //Confirmation Message
        function Confirm(MSG) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(MSG)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }


        $(function () {
            blinkeffect('#txtblnk');
        })
        function blinkeffect(selector) {
            $(selector).fadeOut('slow', function () {
                $(this).fadeIn('slow', function () {
                    blinkeffect(this);
                });
            });
        } //MEssage blink 