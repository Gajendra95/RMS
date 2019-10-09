function onchangebasepoint(obj) {
    debugger;

    
    var row = obj.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var GridView = document.getElementById('<%=Grid_AuthorEntry.ClientID %>');
    var basepoint = $(row).find("[id*=txtBasePoint]").val();
    var snip = $(row).find("[id*=txtSNIPSJRPoint]").val();
    //var threshold = $(row).find("[id*=txtThresholdPoint]").val();

    //decallowed = 2;
    //if (isNaN(basepoint) || basepoint == "") {
    //    alert("Not a valid number.try again.");
    //}
    //else {
    //    if (basepoint.indexOf('.') == -1) basepoint += ".";
    //    dectext = basepoint.substring(basepoint.indexOf('.') + 1, basepoint.length);
    //    if (dectext.length > decallowed) {
    //        alert("Enter a number with up to " + decallowed + "decimal places. try again.");
          
    //    }
    //    else {
           
    //    }
    //}
    //if (isNaN(snip) || snip == "") {
    //    alert("Not a valid number.try again.");
    //}
    //else {
    //    if (snip.indexOf('.') == -1) snip += ".";
    //    dectext = snip.substring(snip.indexOf('.') + 1, snip.length);
    //    if (dectext.length > decallowed) {
    //        alert("Enter a number with up to " + decallowed + "decimal places. try again.");

    //    }
    //    else {

    //    }
    //}

    //if (isNaN(threshold) || threshold == "") {
    //    alert("Not a valid number.try again.");
    //}
    //else {
    //    if (threshold.indexOf('.') == -1) threshold += ".";
    //    dectext = threshold.substring(threshold.indexOf('.') + 1, threshold.length);
    //    if (dectext.length > decallowed) {
    //        alert("Enter a number with up to " + decallowed + "decimal places. try again.");

    //    }
    //    else {

    //    }
    //}
    //if (Quartile != "") {
    //    var totval = parseFloat(basepoint);
    //}
    //else
    if (basepoint == "") {
        basepoint = "0.0";
    }
    if (snip == "") {
        snip = "0.0";
    }
    //if (threshold == "") {
    //    threshold = "0.0";
    //}
    //var totval = parseFloat(basepoint) + parseFloat(snip) + parseFloat(threshold);
    var totval = parseFloat(basepoint) + parseFloat(snip);
   
   
  
    if (totval != 0) {
        $(row).find("[id*=txtTotalPoint]").val(totval.toFixed(2));
    }
    else {
        $(row).find("[id*=txtTotalPoint]").val("0");
    }

}
function onchangebasepointTotal(obj) {
    debugger;


    var row = obj.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var GridView = document.getElementById('<%=Grid_AuthorEntry.ClientID %>');
    var basepoint = $(row).find("[id*=txtBasePoint]").val();
    //var snip = $(row).find("[id*=txtSNIPSJRPoint]").val();
    if (basepoint == "") {
        basepoint = "0.0";
    }
    //if (snip == "") {
    //    snip = "0.0";
    //}

    var totval = parseFloat(basepoint);



    if (totval != 0) {
        $(row).find("[id*=txtTotalPoint]").val(totval.toFixed(2));
    }
    else {
        $(row).find("[id*=txtTotalPoint]").val("0");
    }

}

function onchangeSJRpoint(obj)
{
    debugger;

    
    var row = obj.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var GridView = document.getElementById('<%=Grid_AuthorEntry.ClientID %>');
    var snip = $(row).find("[id*=txtSNIPSJRPoint]").val();
    //var threshold = $(row).find("[id*=txtThresholdPoint]").val();

    //decallowed = 2;
    //if (isNaN(basepoint) || basepoint == "") {
    //    alert("Not a valid number.try again.");
    //}
    //else {
    //    if (basepoint.indexOf('.') == -1) basepoint += ".";
    //    dectext = basepoint.substring(basepoint.indexOf('.') + 1, basepoint.length);
    //    if (dectext.length > decallowed) {
    //        alert("Enter a number with up to " + decallowed + "decimal places. try again.");
          
    //    }
    //    else {
           
    //    }
    //}
    //if (isNaN(snip) || snip == "") {
    //    alert("Not a valid number.try again.");
    //}
    //else {
    //    if (snip.indexOf('.') == -1) snip += ".";
    //    dectext = snip.substring(snip.indexOf('.') + 1, snip.length);
    //    if (dectext.length > decallowed) {
    //        alert("Enter a number with up to " + decallowed + "decimal places. try again.");

    //    }
    //    else {

    //    }
    //}

    //if (isNaN(threshold) || threshold == "") {
    //    alert("Not a valid number.try again.");
    //}
    //else {
    //    if (threshold.indexOf('.') == -1) threshold += ".";
    //    dectext = threshold.substring(threshold.indexOf('.') + 1, threshold.length);
    //    if (dectext.length > decallowed) {
    //        alert("Enter a number with up to " + decallowed + "decimal places. try again.");

    //    }
    //    else {

    //    }
    //}
   
    if (snip == "") {
        snip = "0.0";
    }
    //if (threshold == "") {
    //    threshold = "0.0";
    //}
    //var totval = parseFloat(basepoint) + parseFloat(snip) + parseFloat(threshold);
    var totval =  parseFloat(snip);
    if (totval != 0) {
        $(row).find("[id*=txtTotalPoint]").val(totval.toFixed(2));
    }
    else {
        $(row).find("[id*=txtTotalPoint]").val("0");
    }

}

function onchangeARIpoint(obj)
{
    debugger;


    var row = obj.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var GridView = document.getElementById('<%=Grid_AuthorEntry.ClientID %>');
    var ARI = $(row).find("[id*=txtARIPoint]").val();
   
    if (ARI == "") {
        ARI = "0.0";
    }
    var totval = parseFloat(ARI);
    if (totval != 0) {
        $(row).find("[id*=txtTotalPoint]").val(totval.toFixed(2));
    }
    else {
        $(row).find("[id*=txtTotalPoint]").val("0");
    }
}

function SelectIncentiveBalance(txtVendor,txt) {
    //This function call on text change.     

    debugger;
    var txtvalue = $("[id$=" + txtVendor + "]").val();
    jQuery.ajax({

        type: "POST",
        url: "IncentivePointUtilization.aspx/SelectMember", // this for calling the web method function in cs code.  
        data: '{Vid: "'+txtvalue+'" }', // terms and condition value  
      
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {
            if (data.d != 0.00) {

                $("[id$=" + txt + "]").val(data.d);
            }
            else if (data.d == 0.00) {

                $("[id$=" + txt + "]").val("");
                alert("Invalid MemberId")
            }
        },

        error: function (response) {

        }
    });

}