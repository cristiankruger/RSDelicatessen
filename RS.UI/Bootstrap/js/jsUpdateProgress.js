Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

function beginReq(sender, args) {
    // shows the Popup 
    waitingDialog.show('Aguarde...')
    //$find(ModalProgress).show();
}

function endReq(sender, args) {
    //  shows the Popup 
    waitingDialog.hide('Aguarde...');
    //$find(ModalProgress).hide();
}
