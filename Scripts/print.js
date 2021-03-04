function printAck(asArea) {
    if (asArea == "") {
        asArea = "printArea";
    }
    
var divToPrint = document.getElementById(asArea);
newWin = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0'); 

  newWin.document.write(divToPrint.outerHTML);
  newWin.document.close();
  newWin.focus();
  newWin.print();
  newWin.close();
}

function printRouteAck() {

    var divToPrint = document.getElementById('printRouteArea');
    
    newWin = window.open('', 'Print Receipt', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');

    newWin.document.write(divToPrint.outerHTML);
    newWin.document.close();
    newWin.focus();
    newWin.print();
    newWin.close();
}
function colorChanged(sender) {
    sender.get_element().style.color =
       "#" + sender.get_selectedColor();
}

function printDoc(id) {
    id.focus()
    id.print()
//    
//    myIframe.focus()
//    myIframe.contentWindow.print()
//    var iframe = document.frames ? document.frames["docvw"] : document.getElementById("docvw");
//    var ifWin = iframe.contentWindow || iframe;
//    id.focus();
//    id.print(); 
//    return false; 

}



function findDOM(objectId) {
    if (document.getElementById) {
        return (document.getElementById(objectId));
    }
    if (document.all) {
        return (document.all[objectId]);
    }
}
function zoom(type, imgx, sz) {
    imgd = findDOM(imgx);
    //if (type == "+" && imgd.width < 175) {
    if (type == "+") {
        imgd.width += 2; imgd.height += (2 * sz);
    }
    //if (type == "-" && imgd.width > 20) {
    if (type == "-" ) {
        imgd.width -= 2; imgd.height -= (2 * sz);
    }
}



function zoom(imgx) {
    var imgd = findDOM(imgx);
    //if (type == "+" && imgd.width < 175) {
    var e = findDOM("dlZoom");     
    var h = findDOM("hfHeight"); 
    var w = findDOM("hfWidth"); 
    var dval = e.options[e.selectedIndex].value
    
    if (h.value == "") h.value = imgd.height;
    if (w.value == "") w.value = imgd.width;
       
    imgd.width = eval(w.value) * eval(dval)
    imgd.height = eval(h.value) * eval(dval)
        
}
function image_size(type, imgx, ht,wdth) {
    imgd = findDOM(imgx);
    
        imgd.width =wdth; imgd.height = ht

    }

    function Zoomi() {
        zoom('+', 'imgHandler', 4);        
    }

    function ZoomOut() {
    
        Zoomo();
        action_timeout2 = setInterval("Zoomo()", 100);
    }

    function ZoomIn() {
    
        Zoomi();
        action_timeout = setInterval("Zoomi()", 100);
    }

    function Zoomo() {       
        zoom('-', 'imgHandler', 4);        
    }

    function endAction() {
        if (typeof (action_timeout) != "undefined") clearTimeout(action_timeout);
    }

    function endAction2() {
        if (typeof (action_timeout2) != "undefined") clearTimeout(action_timeout2);
    }

    window.onload = function () {
      document.getElementsByTag("body").onclick = endAction;
        
    }


  function statusbar(abtn,aprc) {
      document.getElementById(abtn).style.visibility = 'hidden'; document.getElementById(aprc).style.display = ''; return true;
  }

  function showhelp(asdoc) {
      window.open("help/retention.htm", "helpret", 'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes').focus();
  }

  function m_over (obj) {
  obj.style.textDecoration = 'none';
  obj.style.color='WHITE';
  obj.parentElement.style.backgroundColor='#005AA2'
}

  function m_out(obj) {
      obj.style.textDecoration = 'none';
      obj.style.color = 'gray';
      obj.parentElement.style.backgroundColor = '#FFFFFF' //'#D2EBF9'
  }

  function clickButton(e, buttonid) {
  
      var evt = e ? e : window.event;
      var bt = document.getElementById(buttonid);
      if (bt) {
          if (evt.keyCode == 13) {              
              return false;
          }
      }
  }