
var fieldObj;
var bCaps = false;
var focus_count = 0;
var sHTML = "";
var maincontent = "ContentPlaceHolder1_";

function showPopup1() {

    var p = window.createPopup();
    var pbody = p.document.body;
    pbody.style.backgroundColor = "FE5200";
    pbody.style.border = "solid black 1px";
    pbody.style.padding = "10px";
    pbody.innerHTML = "<Center><font size=4><font color=#FBFBFB>Please wait, your request is being processed .......<br><center><br>Do not click anywhere.....</br>";
    p.show(-200, -80, 250, 150, event.srcElement);
}
function getArr() {
    var keyArr = [[['~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '.']],
			  		['`', ['{', '}', '|', '[', ']', '\\', ':', '\'', '"', '?', ','], ['-', '=']],
			  		[['q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p'], [' '], ['1', '2', '3']],
			  		[['a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'x'], [' '], ['4', '5', '6']],
			  		[[' '], ['z', 'c', 'v', 'b', 'n', 'm'], ['/', ';'], [' ', ' '], ['7', '8', '9']],
			  		[' <', '> ', '0']];

    return (keyArr);
}

function getFocus(x) {

    //fieldObj = document.getElementById(maincontent + 'txtPassword');
    fieldObj = document.getElementById(x);
}
function disableautocompletion(id1) {
    var passwordControl = document.getElementById(id1);
    passwordControl.setAttribute("autocomplete", "off");
}
function constructKeyboard(bool) {
    
    var check;
    if (typeof (bool) == 'undefined')
        check = document.getElementById(maincontent + 'chkbox').checked;
    else
        check = bool;
    var keyArr = getArr();

    var str_trco = "</tr><tr>";
    sHTML = "<table	border='0' class='keyboardtbl' cellspacing='3px' id='keypad' width='100%'><tr>";
    for (var i = 0; i < keyArr.length - 1; i++) {

        for (var j = 0; j < keyArr[i].length; j++) {
            var code;
            if (typeof (keyArr[i][j]) == 'object') {
                while (keyArr[i][j].length > 0) {
                    var ix = Math.floor(Math.random() * keyArr[i][j].length);
                    var ch = keyArr[i][j].splice(ix, 1);
                    code = ch[0].charCodeAt(0);
                    if (!check) {
                        sHTML += "<td class='keyboardtbldis' style='font-size:14px;'>" + ch + "</td>";
                    }
                    else {
                        if (ch != " ")
                            sHTML += "<td onClick='putChar(" + code + "),init()' style='font-size:14px;'  class='keyboardtblenb' onMouseOut='this.className=\"keyboardtblenb\";' onMouseOver=' this.className=\"dpTDHover\";'>" + ch + "</td>";
                        else
                            sHTML += "<td  style='font-size:12px;'  class='keyboardtbldis' >" + ch + "</td>";
                    }
                }
            } else {
                code = keyArr[i][j].charCodeAt(0);
                if (check)
                    sHTML += "<td onClick='putChar(" + code + "),init()' style='font-size:14px;' class='keyboardtblenb' onMouseOut='this.className=\"keyboardtblenb\";' onMouseOver=' this.className=\"dpTDHover\";'>" + keyArr[i][j] + "</td>";
                else
                    sHTML += "<td class='keyboardtbldis' style='font-size:12px;'>" + keyArr[i][j] + "</td>";
            }
        }
        sHTML += str_trco;
    }
    if (check) {
        sHTML += "<td colspan='1' id='cap'  class='keyboardtbldis'></td>";
        sHTML += "<td colspan='3' id='cap' onClick='setCaps(this)'  class='keyboardtblenb' onMouseOut='this.className=\"keyboardtblenb\";' onMouseOver=' this.className=\"dpTDHover\";' >CAPSLOCK</td>";
        sHTML += "<td colspan='2' id='clr' onClick='setClearAll()'  class='keyboardtblenb' onMouseOut='this.className=\"keyboardtblenb\";' onMouseOver=' this.className=\"dpTDHover\";' >CLEAR</td>";
        sHTML += "<td colspan='3' id='back' onClick='backSpace1()'  class='keyboardtblenb' onMouseOut='this.className=\"keyboardtblenb\";' onMouseOver=' this.className=\"dpTDHover\";' >BACKSPACE</td>";
        sHTML += "<td colspan='2' id='5'  class='keyboardtbldis'    ></td>";
    }
    else {
        sHTML += "<td colspan='1' id='cap'   onClick='setCaps(this)' class='keyboardtbldis'></td>";
        sHTML += "<td colspan='3' id='cap'   class='keyboardtbldis' >CAPSLOCK</td>";
        sHTML += "<td colspan='2' id='clr'   class='keyboardtbldis' >CLEAR</td>";
        sHTML += "<td colspan='3' id='back'  class='keyboardtbldis' >BACKSPACE</td>";
        sHTML += "<td colspan='2' id='back'  class='keyboardtbldis' ></td>";
    }
    var codeArray = new Array();
    for (var i = 0; i < 3; i++) {
        codeArray[i] = keyArr[5][i];
    }
    shuffle(codeArray);
    for (var i = 0; i < 3; i++) {
        var code = codeArray[i].charCodeAt(0);
        if (check)
            sHTML += "<td onClick='putChar(" + code + "),init()' style='font-size:14px;' class='keyboardtblenb' onMouseOut='this.className=\"keyboardtblenb\";' onMouseOver=' this.className=\"dpTDHover\";' >" + codeArray[i] + "</td>";
        else
            sHTML += "<td  class='keyboardtbldis' style='font-size:14px;'>" + codeArray[i] + "</td>";
    }
    sHTML += "</tr></table>";
    document.getElementById('kbplaceholder').innerHTML = sHTML;
    if (check) {
        document.getElementById(maincontent + "txtPassword").readOnly = true;
        document.getElementById(maincontent + 'txtPassword').focus();
    } else {
        document.getElementById(maincontent + "txtPassword").readOnly = false;
        document.getElementById(maincontent + 'txtPassword').focus();
    }

}
shuffle = function (v) {
    for (var j, x, i = v.length; i; j = parseInt(Math.random() * i), x = v[--i], v[i] = v[j], v[j] = x);
    return v;

};
function putChar(code) {
    if (fieldObj.value.length < 32) {
        var keycode = (code > 96 && code < 123 && bCaps) ? code - 32 : code;
        var text = String.fromCharCode(keycode);
        if (fieldObj.type == "text") {
            fieldObj.value = fieldObj.value;
        }
        else {
            fieldObj.value += text;
        }
        setCaretTo();
    }
}
function setCaretTo() {
    var pos = fieldObj.value.length;
    if (fieldObj.createTextRange) {
        var range = fieldObj.createTextRange();
        range.move('character', pos);
        range.select();
    } else if (fieldObj.selectionStart) {
        fieldObj.focus();
        fieldObj.setSelectionRange(pos, pos);
    }
}
function changeCase() {
    var alphabets = document.getElementById('keypad').getElementsByTagName('TD');
    for (var i = 0; i < alphabets.length; i++) {
        var ch = alphabets[i].innerHTML;
        if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') && ch.length == 1) {
            alphabets[i].innerHTML = bCaps ? ch.toUpperCase() : ch.toLowerCase();
        }
    }
}
function setCaps(obj) {
    bCaps = !(bCaps);
    toggleCap(obj);
    changeCase();
}
function toggleCap(obj) {
    var str = bCaps ? "CAPS LOCK" : "CAPS LOCK";
    obj.innerHTML = str;
}
function setClearAll() {
    fieldObj.value = "";
    fieldObj.focus();
}
function backSpace1() {
    var j = fieldObj.value;
    fieldObj.value = j.substring(0, j.length - 1);

}
function init() {
    constructKeyboard();
    if (document.getElementById(maincontent + "chkbox").checked == false) {
       // document.getElementById("Vkeyboard").style.display = "block";
        changeCase();
    }
    else {
       // document.getElementById("Vkeyboard").style.display = "block";
        changeCase();

    }

}
