function printAck() {

var divToPrint = document.getElementById('printArea');
newWin = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0'); 

  newWin.document.write(divToPrint.outerHTML);
  newWin.document.close();
  newWin.focus();
  newWin.print();
  newWin.close();
}

function printDoc(myIframe) {
    
    myIframe.focus()
    myIframe.print()

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

