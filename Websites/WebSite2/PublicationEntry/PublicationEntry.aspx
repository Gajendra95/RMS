<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="PublicationEntry.aspx.cs" Inherits="Publicationentry"  MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
       <%-- <link href="../css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
  
    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
    <link href="../css/StyleSheet.css" rel="stylesheet" />--%>



    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation2" />
   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />

      <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation1" />
     <script type="text/javascript">
         function callthis1() {
             debugger;
             var loc = $(location).attr('href');
             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup';
             }
             else {
                 window.location = loc + '#commentpopup';
             }
         }
         function callthis5() {
             debugger;
             var loc = $(location).attr('href');
             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup5';
             }
             else {
                 window.location = loc + '#commentpopup5';
             }
         }
         function callthis2() {
             debugger;
             var loc = $(location).attr('href');
             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup2';
             }
             else {
                 window.location = loc + '#commentpopup2';
             }
         }

         function callthis3() {
             debugger;
             var loc = $(location).attr('href');
             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup3';
             }
             else {
                 window.location = loc + '#commentpopup3';
             }
         }

         function callthis4() {
             debugger;
             // alert("cxvxcvxc");
             var loc = $(location).attr('href');

             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup4';
             }
             else {
                 window.location = loc + '#commentpopup4';
             }
         }

         function callthis7() {
             debugger;
             // alert("cxvxcvxc");
             var loc = $(location).attr('href');

             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup7';
             }
             else {
                 window.location = loc + '#commentpopup7';
             }
         }

     
         function callthis6() {
             $('#commentpopup6').modal('show');
         }

    </script>
     <script type="text/javascript">

         function ViewPdf() {
             debugger;
             window.open('<%= Page.ResolveUrl("~/PublicationEntry/DisplayPdf.aspx")%>', '_blank');
         }
         function ValidateCheckBoxList(sender, args) {
             var checkBoxList = document.getElementById("<%=CheckboxIndexAgency.ClientID %>");
             var checkboxes = checkBoxList.getElementsByTagName("input");
             var isValid = false;
             for (var i = 0; i < checkboxes.length; i++) {
                 if (checkboxes[i].checked) {
                     isValid = true;
                     break;
                 }
             }
             args.IsValid = isValid;
         }
         function ToggleDisplay() {
             // $("#commentpopup").dialog("close");
             $("#commentpopup .close").click()
             var loc = $(location).attr('href');
             window.location = "#"
         }
         function ToggleDisplay5() {
             // $("#commentpopup").dialog("close");
             $("#commentpopup5 .close").click()
             var loc = $(location).attr('href');
             window.location = "#"
         }
         function ToggleDisplay2() {
             // $("#commentpopup").dialog("close");
             $("#commentpopup2 .close").click()
             var loc = $(location).attr('href');
             window.location = "#"
         }
         function ToggleDisplay3() {
             // $("#commentpopup").dialog("close");
             $("#commentpopup3 .close").click()
             var loc = $(location).attr('href');
             window.location = "#"
         }
         function ToggleDisplay4() {
             // $("#commentpopup").dialog("close");
             $("#commentpopup4 .close").click()
             var loc = $(location).attr('href');
             window.location = "#"
         }
         function ToggleDisplay6() {
             debugger;
             // $("#commentpopup").dialog("close");
             $("#commentpopup6 .close").click()
             var loc = $(location).attr('href');
             window.location = "#"
         }

</script>
   <%-- <script type = "text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }

</script>--%>
     <asp:HiddenField runat="server" ID="hfPosition" Value="" />
<script type="text/javascript">
    $(function () {
        var f = $("#<%=hfPosition.ClientID%>");
        window.onload = function () {
            var position = parseInt(f.val());
            if (!isNaN(position)) {
                $(window).scrollTop(position);
            }
        };
        window.onscroll = function () {
            var position = $(window).scrollTop();
            f.val(position);
        };
    });
</script>
     <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border: 3px solid #CCC;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
            clear: both;
            position: absolute;
            margin-top: 0;
        }

        .overlay {
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            background: rgba(0, 0, 0, 0.7);
            transition: opacity 500ms;
            visibility: hidden;
            opacity: 0;
        }

            .overlay:target {
                visibility: visible;
                opacity: 1;
            }

        .popup {
            margin: 70px auto;
            padding: 20px;
            background: #fff;
            border-radius: 5px;
            width: 30%;
            position: relative;
            transition: all 5s ease-in-out;
        }

            .popup h2 {
                margin-top: 0;
                color: #333;
                font-family: Tahoma, Arial, sans-serif;
            }

            .popup .close {
                position: absolute;
                top: 20px;
                right: 30px;
                transition: all 50ms;
                font-size: 30px;
                font-weight: bold;
                text-decoration: none;
                color: #333;
            }

                .popup .close:hover {
                    color: #06D85F;
                }

            .popup .content {
                max-height: 90%;
                /*max-height: 30%;*/
                overflow: auto;
            }

        .popupp {
            margin: 120px auto;
            padding: 20px;
            background: #fff;
            border-radius: 5px;
            width: 30%;
            position: relative;
            transition: all 5s ease-in-out;
        }

            .popupp h2 {
                margin-top: 0;
                color: #333;
                font-family: Tahoma, Arial, sans-serif;
            }

            .popupp .close {
                position: absolute;
                top: 20px;
                right: 30px;
                transition: all 50ms;
                font-size: 30px;
                font-weight: bold;
                text-decoration: none;
                color: #333;
            }

                .popupp .close:hover {
                    color: #06D85F;
                }

            .popupp .content {
                max-height: 90%;
                /*max-height: 30%;*/
                overflow: auto;
            }

        .close1 {
            /*position: absolute;*/
            /*transition: all 200ms;*/
            font-size: 20px;
            font-weight: bold;
            text-decoration: underline;
            color: #333;
        }

            .close1:hover {
                color: #06D85F;
            }

        .test2 {
            background: White;
        }

        .auto-style27 {
            width: 14.5%;
        }
    </style>
  <style type="text/css">
      
      /*.gridViewHeader {

    padding: 40px 50px 4px 4px;
  
  
    border-collapse: collapse;
}
.modelBackground
{
	background-color:Gray;
	filter:alpha(opacity=70);
	opacity:0.7;
}

.modelPopup
{
	background-color:#EEEEE;
	border-width:3px;
	border-style:solid;
	border-color:Gray;
	font-family:Verdana;
	font-size:small;
	padding:3px;
	width:450px;
	position:absolute;
	overflow:scroll;
	max-height:400px;
}*/

.blnkImgCSS
{
	opacity: 0;
	filter: alpha(opacity=0);
}

      .auto-style8 {
          width: 152px;
      }

      .auto-style9 {
          height: 36px;
          width: 131px;
      }
      .auto-style10 {
          height: 36px;
          width: 136px;
      }
      .auto-style11 {
          height: 36px;
          width: 109px;
      }
      .auto-style13 {
          height: 39px;
          width: 129px;
      }

      .auto-style14 {
          height: 36px;
          width: 89px;
      }
      .auto-style15 {
          height: 39px;
      }

      .auto-style19 {
          height: 36px;
          width: 217px;
      }
      .auto-style20 {
          width: 217px;
      }
      .auto-style21 {
          height: 36px;
          width: 122px;
      }
      .auto-style22 {
          height: 36px;
          width: 104px;
      }

      .auto-style26 {
          width: 51px;
      }

      .auto-style27 {
          height: 36px;
          width: 307px;
      }

      .auto-style31 {
          height: 56px;
          width: 91px;
      }

      .auto-style32 {
          height: 56px;
          width: 96px;
      }

      .auto-style34 {
          height: 56px;
          width: 70px;
      }
      .auto-style35 {
          height: 56px;
          width: 72px;
      }
      .auto-style38 {
          width: 38px;
      }
      .auto-style40 {
          width: 46px;
      }
      .auto-style42 {
          height: 56px;
          width: 97px;
      }
      .auto-style44 {
          height: 56px;
          width: 101px;
      }
      .auto-style46 {
          width: 39px;
      }
      .auto-style47 {
          height: 56px;
          width: 90px;
      }
      .auto-style48 {
          height: 56px;
          width: 77px;
      }
      .auto-style49 {
          width: 29px;
      }

      .auto-style50 {
          width: 42px;
      }

  </style>

     <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border: 3px solid #CCC;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
            clear: both;
            position: absolute;
            margin-top: 0;
        }

        .overlay {
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            background: rgba(0, 0, 0, 0.7);
            transition: opacity 500ms;
            visibility: hidden;
            opacity: 0;
        }

            .overlay:target {
                visibility: visible;
                opacity: 1;
            }

        .popup {
            margin: 70px auto;
            padding: 20px;
            background: #fff;
            border-radius: 5px;
            width: 30%;
            position: relative;
            transition: all 5s ease-in-out;
        }

            .popup h2 {
                margin-top: 0;
                color: #333;
                font-family: Tahoma, Arial, sans-serif;
            }

            .popup .close {
                position: absolute;
                top: 20px;
                right: 30px;
                transition: all 50ms;
                font-size: 30px;
                font-weight: bold;
                text-decoration: none;
                color: #333;
            }

                .popup .close:hover {
                    color: #06D85F;
                }

            .popup .content {
                max-height: 90%;
                /*max-height: 30%;*/
                overflow: auto;
            }

        .popupp {
            margin: 120px auto;
            padding: 20px;
            background: #fff;
            border-radius: 5px;
            width: 30%;
            position: relative;
            transition: all 5s ease-in-out;
        }

            .popupp h2 {
                margin-top: 0;
                color: #333;
                font-family: Tahoma, Arial, sans-serif;
            }

            .popupp .close {
                position: absolute;
                top: 20px;
                right: 30px;
                transition: all 50ms;
                font-size: 30px;
                font-weight: bold;
                text-decoration: none;
                color: #333;
            }

                .popupp .close:hover {
                    color: #06D85F;
                }

            .popupp .content {
                max-height: 90%;
                /*max-height: 30%;*/
                overflow: auto;
            }

        .close1 {
            /*position: absolute;*/
            /*transition: all 200ms;*/
            font-size: 20px;
            font-weight: bold;
            text-decoration: underline;
            color: #333;
        }

            .close1:hover {
                color: #06D85F;
            }

        .test2 {
            background: White;
        }

    </style>
    

 <script type = "text/javascript">
     function AddConfirm() {



         var rid = document.getElementById('<%= TextBoxPubId.ClientID %>').value;

         var confirm_value2 = document.createElement("INPUT");
         confirm_value2.type = "hidden";
         confirm_value2.name = "confirm_value2";
         confirm_value2.value = "Yes";

         if (confirm("Any previously entered data Will be lost. Do you want to continue?"))
             confirm_value2.value = "Yes";
         else
             confirm_value2.value = "No";


         document.forms[0].appendChild(confirm_value2);
     }
    </script>

        <script type = "text/javascript">
            function ConfirmEntryType() {
                var PublicationEntry = document.getElementById('<%= DropDownListPublicationEntry.ClientID %>').value;

                //var HiddenEntryConfirm1 = document.createElement("INPUT");
                // HiddenEntryConfirm1.type = "hidden";
                // HiddenEntryConfirm1.name = "confirm_value";
                // HiddenEntryConfirm1.value = "";
                var hiddenStatusFlag = document.getElementById('<%= HiddenEntryConfirm1.ClientID%>');
                hiddenStatusFlag.value = "";
                if (PublicationEntry == "JA" || PublicationEntry == "TS" || PublicationEntry == "PR") {
                    if (confirm("Entry can be done only for Published Articles......Do You Want To Continue?")) {

                        //HiddenEntryConfirm1.value = "Yes";
                        hiddenStatusFlag.value = "Yes";
                        // $("#HiddenEntryConfirm").val("Yes");
                    }
                    else {
                        //HiddenEntryConfirm1.value = "No";
                        hiddenStatusFlag.value = "No";
                        //$("#HiddenEntryConfirm").val("No");
                    }
                }
                // document.forms[0].appendChild(HiddenEntryConfirm1);
            }
    </script>


         <script type = "text/javascript">
             function ConfirmJournalId() {

                 var PubJournal = document.getElementById('<%= TextBoxPubJournal.ClientID %>').value;

                 var Institution;
                 var controlId = document.getElementById("<%=RadioButtonListIndexed.ClientID %>");
                 Institution = controlId.value;

                 var radio = controlId.getElementsByTagName("input");



                 var confirm_value = document.createElement("INPUT");
                 confirm_value.type = "hidden";
                 confirm_value.name = "confirm_value";
                 confirm_value.value = "Yes";
                 if (radio[0].checked) {

                     if (confirm("This paper will not be considered for incentive......Do You Want To Continue?"))
                         confirm_value.value = "Yes";
                     else
                         confirm_value.value = "No";
                 }

                 document.forms[0].appendChild(confirm_value);
             }
    </script>


    
         <script type = "text/javascript">
             function ConfirmButtonJournal() {
                 debugger;
                 var validated = Page_ClientValidate('validation2');
                 if (validated) {
                     var PublicationEntry = document.getElementById('<%= DropDownListPublicationEntry.ClientID %>').value;
                     if (PublicationEntry == "JA" || PublicationEntry == "TS" || PublicationEntry == "PR") {

                         var tbl = $("[id$=Grid_AuthorEntry]");
                         var rows = tbl.find('tr');
                         var countAuthType = 0;
                         var countCorrAuth = 0;
                         for (var index = 0; index < rows.length; index++) {
                             var row = rows[index];

                             var AuthorType = $(row).find("[id*=AuthorType]").val();
                             var isCorrAuth = $(row).find("[id*=isCorrAuth]").val();
                             if (AuthorType == "P") {
                                 countAuthType = countAuthType + 1;
                             }
                             if (isCorrAuth == "Y") {
                                 countCorrAuth = countCorrAuth + 1;
                             }
                         }
                         if (countAuthType == 0) {

                             alert('Select atleast one Author Type as First Author !');
                             $("#confirm_value12").val("No");
                             return false;
                         }
                         if (countAuthType > 1) {

                             alert('First Author cannot be more than one!');
                             $("#confirm_value12").val("No");
                             return false;
                         }
                         if (countCorrAuth > 1) {
                             alert('Corresponding Author cannot be more than one!');
                             $("#confirm_value12").val("No");
                             return false;
                         }
                         if (countCorrAuth == 0) {
                             alert('Select atleast one Corresponding Author!');
                             $("#confirm_value12").val("No");
                             return false;
                         }

                     }



                     if (PublicationEntry == "JA" || PublicationEntry == "TS") {
                         var Institution;
                         var controlId = document.getElementById("<%=RadioButtonListIndexed.ClientID %>");
                         Institution = controlId.value;

                         var radio = controlId.getElementsByTagName("input");
                         if (radio[0].checked) {

                             if (confirm(" The papers to be considered for incentives should be published in the journals indexed in Scopus and/or Web of Science......Do You Want To Save?????"))
                                 // confirm_value12.value = "Yes";
                                 $("#confirm_value12").val("Yes");
                             else
                                 // confirm_value12.value = "No";
                                 $("#confirm_value12").val("No");
                         }
                         else {
                             $("#confirm_value12").val("Yes");

                         }
                     }
                     else {

                         $("#confirm_value12").val("Yes");
                     }
                 }
                 //document.forms[0].appendChild(confirm_value12);
             }
    </script>

   <script type = "text/javascript">

       function setRow(obj) {

           var row = obj.parentNode.parentNode;
           var rowIndex = row.rowIndex - 1;


           var sndID = obj.id;
           var sndrID = document.getElementById('<%= senderID.ClientID %>');
           sndrID.value = sndID;

           var rowNo = document.getElementById('<%= rowVal.ClientID %>');
           rowNo.value = rowIndex;
           var mu = $(row).find("[id*=DropdownMuNonMu]").val();
           if (mu == "M") {
               $('#<%=popupPanelAffil.ClientID %>').show()
               $('#<%=popupstudent.ClientID %>').hide();

           }
           else if (mu == "S") {
               $('#<%=popupstudent.ClientID %>').show();
               $('#<%=popupPanelAffil.ClientID %>').hide();

           }
   }
    </script>
    <script type = "text/javascript">

        function setRow1(obj) {

            var row = obj.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;


            var sndID = obj.id;
            var sndrID = document.getElementById('<%= senderID1.ClientID %>');
            sndrID.value = sndID;

            var rowNo = document.getElementById('<%= rowVal1.ClientID %>');
            rowNo.value = rowIndex;
            var mu = $(row).find("[id*=DropDownListPublicationEntry]").val();
            if (mu == "JA") {
                $('#<%=popupPanelJournal.ClientID %>').show()
                $('#<%=popupPanelProceedingsJournal.ClientID %>').hide();

            }
            else if (mu == "PR") {
                $('#<%=popupPanelJournal.ClientID %>').hide();
               $('#<%=popupPanelProceedingsJournal.ClientID %>').show();

           }
   }
    </script>
     <script type = "text/javascript">

         function setRow2(obj) {
             debugger;
             var row = obj.parentNode.parentNode;
             var rowIndex = row.rowIndex - 1;


             var sndID = obj.id;
             var sndrID = document.getElementById('<%= senderID2.ClientID %>');
             sndrID.value = sndID;

             var rowNo = document.getElementById('<%= rowVal2.ClientID %>');
            rowNo.value = rowIndex;
            {
                $('#<%=popupPanelProject.ClientID %>').show();
             }



         }




         function setRow3(obj) {
             debugger;
             var row = obj.parentNode.parentNode;
             var rowIndex = row.rowIndex - 1;


             var sndID = obj.id;
             var sndrID = document.getElementById('<%= senderID7.ClientID %>');
             sndrID.value = sndID;

             var rowNo = document.getElementById('<%= rowVal7.ClientID %>');
             rowNo.value = rowIndex;
             {
                 $('#<%=panelfeedback.ClientID %>').show();
            }



        }


    </script>
    <%-- <script type = "text/javascript">

         function setRow6(obj) {
             debugger;
             var row = obj.parentNode.parentNode;
             var rowIndex = row.rowIndex - 1;


             var sndID = obj.id;
             var sndrID = document.getElementById('<%= senderID3.ClientID %>');
             sndrID.value = sndID;

             var rowNo = document.getElementById('<%= rowVal3.ClientID %>');
             rowNo.value = rowIndex;
             {
                 $('#<%=PanelProjectDetails.ClientID %>').show();
            }



        }
    </script>--%>
    <script type = "text/javascript">
        function ConfirmButtonStudentPub() {
            var validated = Page_ClientValidate('validation1');
            if (validated) {
                var studentpub = document.getElementById('<%= StudentPub.ClientID %>');
                var hdnPredatoryJournal = document.getElementById('<%= hdnPredatoryJournal.ClientID %>').value;
                var pagefrom = document.getElementById('<%= TextBoxPageFrom.ClientID %>').value;
                var TextBoxPageTo = document.getElementById('<%= TextBoxPageTo.ClientID %>').value;
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";

                confirm_value.value = "Yes";
                var PublicationEntry = document.getElementById('<%= DropDownListPublicationEntry.ClientID %>').value;
                if (PublicationEntry == "JA" || PublicationEntry == "TS" || PublicationEntry == "PR") {
                    var tbl = $("[id$=Grid_AuthorEntry]");
                    var rows = tbl.find('tr');
                    for (var index = 0; index < rows.length; index++) {
                        var row = rows[index];
                        var munonmu = $(row).find("[id*=DropdownMuNonMu]").val();
                        var AuthorType = $(row).find("[id*=AuthorType]").val();
                        if (munonmu == "S" || munonmu == "O") {
                            if (AuthorType == "P") {
                                var month = document.getElementById('<%= DropDownListMonthJA.ClientID %>').value;
                                var year = document.getElementById('<%= TextBoxYearJA.ClientID %>').value;
                                var date = "01/07/2016";
                                var date2 = "01" + "/" + month + "/" + year;
                                if (Date.parse(date2) >= Date.parse(date)) {
                                    //var tbl1 = $("[id$=Grid_AuthorEntry]");
                                    //var rows1 = tbl1.find('tr');
                                    //for (var index1 = 0; index1 < rows1.length; index1++) {
                                    //    var row1 = rows1[index1];
                                    //    var munonmu1 = $(row1).find("[id*=DropdownMuNonMu]").val();
                                    //    var AuthorType1 = $(row1).find("[id*=isCorrAuth]").val();
                                    //    if (munonmu1 == "M" || munonmu1 == "S" || munonmu1 == "O") {
                                    //        if (AuthorType1 == "Y") {
                                    //            studentpub.Value = "Y";
                                    //            index1 = rows1.length + 1;
                                    //            index = rows.length + 1;
                                    //        }
                                    //        else {
                                    //            studentpub.Value = "N";
                                    //        }
                                    //    }
                                    //    else {
                                    //        studentpub.Value = "N";
                                    //    }
                                    //}
                                    studentpub.Value = "Y";
                                }
                                else {
                                    studentpub.Value = "N";
                                }
                                break;
                            }
                            else {
                                studentpub.Value = "N";
                                continue;
                            }
                        }
                        else {
                            studentpub.Value = "N";
                        }
                    }
                }
                else {
                    studentpub.Value = "N";
                }
                if (PublicationEntry == "JA" || PublicationEntry == "TS" || PublicationEntry == "PR") {
                    if (studentpub.Value == "Y") {
                        if (pagefrom != "") {
                            if (hdnPredatoryJournal == "PRD") {
                                if (confirm("The article belongs to predatory journal list.This will not be considered for incentive point entry\nThis will be considered as a Student Publication.Do you want to Continue?"))
                                    confirm_value.value = "Yes";
                                else
                                    confirm_value.value = "No";
                            }
                            else {
                                if (confirm("This will be considered as a Student Publication.Do you want to Continue?"))
                                    confirm_value.value = "Yes";
                                else
                                    confirm_value.value = "No";
                            }
                        }
                        else {
                            if (hdnPredatoryJournal == "PRD") {
                                if (confirm("The article belongs to predatory journal list.This will  not be considered for incentive point entry\nPage Number is not entered.This paper will considered as online student Publication..Do you want to Continue?"))
                                    confirm_value.value = "Yes";
                                else
                                    confirm_value.value = "No";
                            }
                            else {
                                if (confirm("Page Number is not entered.This paper will considered as online student Publication..Do you want to Continue?"))
                                    confirm_value.value = "Yes";
                                else
                                    confirm_value.value = "No";
                            }
                        }

                    }
                    else if (studentpub.Value == "N") {
                        if (pagefrom != "") {
                            if (hdnPredatoryJournal == "PRD") {
                                if (confirm("The article belongs to predatory journal list.This will not be considered for incentive point entry\nThis will be considered as a Faculty Publication.Do you want to Continue?"))
                                    confirm_value.value = "Yes";
                                else
                                    confirm_value.value = "No";
                            }
                            else {
                                if (confirm("This will be considered as a Faculty Publication.Do you want to Continue?"))
                                    confirm_value.value = "Yes";
                                else
                                    confirm_value.value = "No";
                            }
                        }
                        else {
                            if (hdnPredatoryJournal == "PRD") {
                                if (confirm("The article belongs to predatory journal list.This will not be consider for incentive point entry\nPage Number is not entered.This paper will be considered as online Faculty Publication.Do you want to Continue?"))
                                    confirm_value.value = "Yes";
                                else
                                    confirm_value.value = "No";
                            }
                            else {
                                if (confirm("Page Number is not entered.This paper will be considered as online Faculty Publication.Do you want to Continue?"))
                                    confirm_value.value = "Yes";
                                else
                                    confirm_value.value = "No";
                            }
                        }

                    }
                    document.forms[0].appendChild(confirm_value);
                }
            }
        }
    </script>
            <asp:ScriptManager ID="Scriptmanager1" runat="server"/>

           <center> <asp:Label ID="lablPanelTitle" runat="server" Text="Publication Entry/Update" Font-Bold="true"  ></asp:Label></center>
            <br />
           
          
                
              <%--  <asp:Panel ID="panelSearchPub" runat="server"  Style="font-weight:bold; background-color:#F8F8F8;border-style:ridge"> 
         --%>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

        <ContentTemplate> 
            <asp:Panel ID="panelSearchPub" runat="server" ForeColor="Black"  GroupingText="Entries Pending Action"  Font-Bold="true" Style="background-color:#E0EBAD" > 
                 
               <%--   <center>
                  Publication Search
                  </center>--%>
                  <br />
  <center>

               <!-- <asp:Label ID="Label2" runat="server" Text="Search" Font-Bold="true" ></asp:Label>
                      <br />-->
                 <table  style="width: 92%">
                    <tr>
                        <td style="height: 36px; font-weight:normal; color:Black" >
                           Publication Entry Type
                           </td>
                        <td style="height: 36px">             
                            <asp:DropDownList ID="EntryTypesearch" runat="server" Style="border-style:inset none none inset;"
                             DataSourceID="SqlDataSource4" DataTextField="EntryName" DataValueField="TypeEntryId" AppendDataBoundItems="true">

                                  <asp:ListItem Value="A">ALL</asp:ListItem>
                          </asp:DropDownList> 
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                
                SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M" 
                ProviderName="System.Data.SqlClient">
             
            </asp:SqlDataSource>
                       
                         </td>
                      
                        <td style="height: 36px; font-weight:normal; color:Black">
                           Publication ID
                        </td>
                        <td style="height: 36px">
                          <asp:TextBox ID="PubIDSearch"  runat="server" Style="border-style:inset none none inset;" ></asp:TextBox>
                        </td>
                         
                        <td style="height: 36px; font-weight:normal; color:Black">
                           Title
                        </td>
                        <td style="height: 36px">
                          <asp:TextBox ID="TextBoxWorkItemSearch"  runat="server" Style="border-style:inset none none inset;" ></asp:TextBox>
                        </td>
                
                         
                        <td style="height: 36px">  
                              <asp:Button ID="ButtonSearchPub" runat="server" Text="Search" OnClick="ButtonSearchPubOnClick"  />
                        </td>
                      
       </tr>
               
                     </table>  
                     <br />

                              
                        <asp:GridView ID="GridViewSearch" runat="server"  AllowPaging="True" 
        PagerSettings-PageButtonCount="5" PageSize="5" DataSourceID="SqlDataSource1"  EmptyDataText="No Data found"
        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="false" OnPageIndexChanging="GridViewSearchPub_PageIndexChanging" 
        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3"  OnRowDataBound="GridView2_RowDataBound" OnRowEditing="edit" OnRowCommand="GridView2_RowCommand"
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False" 
        BorderColor="#FF6600" BorderStyle="Solid">
        <Columns>

     
   <asp:TemplateField ShowHeader="False" >
   <ItemTemplate>
   <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.gif" ToolTip="Edit"  />
   </ItemTemplate>
   </asp:TemplateField>

    <%--      <asp:TemplateField ShowHeader="False">
   <ItemTemplate>
   <asp:Button ID="BtnEditGridViewView" runat="server" CausesValidation="False" CommandName="View"  Text="Approval"  ToolTip="Approval" />
   </ItemTemplate>
   </asp:TemplateField>--%>
        <asp:BoundField DataField="PublicationID" ReadOnly="true" HeaderText="Publication ID" 
                SortExpression="PublicationID" />
<%--
            <asp:BoundField DataField="TypeOfEntry" ReadOnly="true" HeaderText="Type Of Entry" 
                SortExpression="TypeOfEntry" />--%>
                  
                       <asp:BoundField DataField="EntryName" ReadOnly="true" HeaderText="Type Of Entry" 
                SortExpression="EntryName" />
                  <asp:BoundField DataField="TitleWorkItem" ReadOnly="true" HeaderText="Title Of Work Item" 
                SortExpression="TitleWorkItem" />
                  <asp:BoundField DataField="PubCatName" ReadOnly="true" HeaderText="Category" 
                SortExpression="PubCatName" />
<%--
                  <asp:BoundField DataField="PubJournalID" ReadOnly="true" HeaderText="PubJournalID" 
                SortExpression="PubJournalID" />--%>
<%--  
            <asp:BoundField DataField="PublishJAMonth" ReadOnly="true" HeaderText="PublishJAMonth" 
                SortExpression="PublishJAMonth" />

                <asp:BoundField DataField="PublishJAYear" ReadOnly="true" HeaderText="PublishJAYear" 
                SortExpression="PublishJAYear" />--%>
    <%--       <asp:BoundField DataField="ImpactFactor" ReadOnly="true" HeaderText="ImpactFactor" 
                SortExpression="ImpactFactor" />--%>
                   <asp:BoundField DataField="StatusName" ReadOnly="true" HeaderText="Status" 
                SortExpression="StatusName" />
                       <asp:TemplateField ShowHeader="false">
             <ItemTemplate>
                 <asp:HiddenField ID="TypeOfEntry" runat="server" Value='<%# Eval("TypeOfEntry") %>'></asp:HiddenField>
                                  
             
            </ItemTemplate>            
        </asp:TemplateField>
            
        </Columns>

           <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#0B532D" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
        <EditRowStyle BorderColor="#FF6600" BorderStyle="Solid" />

    </asp:GridView>
           <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
         SelectCommand=" " >
        </asp:SqlDataSource>

                         <br />
                             </center>        
                     </asp:Panel>
            </ContentTemplate>
                   <Triggers>
                       <asp:PostBackTrigger ControlID="ButtonSearchPub" /> 
                   </Triggers>
                 </asp:UpdatePanel> 

              <br />
<asp:UpdatePanel runat="server" ID="MainUpdate" UpdateMode="Conditional">
<ContentTemplate>
<asp:Panel ID="panel" runat="server" GroupingText="Add/Edit Publication Entries"  Font-Bold="true"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD" > 

<br />

<asp:Panel ID="panel1" runat="server" GroupingText=""  > 
 <asp:UpdatePanel runat="server" ID="PubEntriesUpdatePanel" UpdateMode="Conditional">
                            <ContentTemplate>
 <table style="width: 96%">
                    <tr>
                        <td style="font-weight:normal" class="auto-style22">
                          <asp:Label ID="TypeEntry" runat="server" Text="Entry Type" ForeColor="Black"></asp:Label>
                               
                               </td>

                 <td class="auto-style19" >
                                <asp:DropDownList ID="DropDownListPublicationEntry" runat="server" onchange="ConfirmEntryType(this.options[this.selectedIndex].value);"
                                    AppendDataBoundItems="true" AutoPostBack="true" 
                                    DataSourceID="SqlDataSourcePublicationEntry" DataTextField="EntryName" 
                                    DataValueField="TypeEntryId" 
                                    OnSelectedIndexChanged="DropDownListPublicationEntryOnSelectedIndexChanged" 
                                    Style="border-style:inset none none inset;" Width="200px">
                                    <asp:ListItem Value=" ">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourcePublicationEntry" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                    ProviderName="System.Data.SqlClient" 
                                    SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M">
                                </asp:SqlDataSource>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="DropDownListPublicationEntry" Display="None" 
                                    ErrorMessage="Please select Type Of Entry" InitialValue=" " 
                                    ValidationGroup="validation"></asp:RequiredFieldValidator>
                                 </td>
                        <td style="font-weight: normal;" class="auto-style21">             
                            <asp:Label ID="lblMUCat" runat="server" Text="MAHE Categorization" ForeColor="Black"></asp:Label>
                       
                         </td>
                                 <td style="height: 36px">
                                     <asp:DropDownList ID="DropDownListMuCategory" runat="server" OnSelectedIndexChanged="DropDownListMuCategoryOnSelectedIndexChanged" AutoPostBack="true"
                                         AppendDataBoundItems="true" DataSourceID="SqlDataSourceMuCategory" 
                                         DataTextField="PubCatName" DataValueField="PubCatId" Style="border-style:inset none none inset;" 
                                         Width="200px">
                                         <asp:ListItem Value=" ">--Select--</asp:ListItem>
                                     </asp:DropDownList>
                                     <asp:SqlDataSource ID="SqlDataSourceMuCategory" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select PubCatId,PubCatName from PubMUCategorization_M">
                                     </asp:SqlDataSource>
                                
                                 </td>
                                 <td style="font-weight:normal">
                                     <asp:Label ID="LabelPubId" runat="server" Text="PublicationId" ForeColor="Black"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:TextBox ID="TextBoxPubId" runat="server" Enabled="false" 
                                         Style="border-style:inset none none inset;" Width="200px"></asp:TextBox>
                                 </td>
                                 </tr>
                                 <tr>
                                     <td style="font-weight:normal" class="auto-style22">
                                         <asp:Label ID="lblTitileWorkItem" runat="server" Text="Title Of the Work Item" ForeColor="Black"></asp:Label>
                                     </td>
                                     <td colspan="6" style="height: 36px">
                                         <asp:TextBox ID="txtboxTitleOfWrkItem" runat="server" MaxLength="200" 
                                             Style="border-style:inset none none inset;" TextMode="MultiLine" Width="850px"></asp:TextBox>
                                                </td>
                                 </tr>
      <tr>
                       
                         
                            <td class="auto-style22" style="font-weight:normal">
                                <asp:Label ID="LabelAbstract" runat="server" ForeColor="Black" Text="Abstract"></asp:Label>
                                <asp:Label ID="Label13" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td colspan="6" style="height: 36px">
                                <asp:TextBox ID="TextBoxAbstract" runat="server" MaxLength="250" Style="border-style:inset none none inset;" TextMode="MultiLine" Width="850px"></asp:TextBox>
                            </td>
                        </tr>

    
   
   <%-- <tr>
      <td class="auto-style22" style="font-weight:normal">
                          <asp:Label ID="LabelDOINum" runat="server" Text="DOI Number" ForeColor="Black" ></asp:Label>
                               
                               </td>
                        <td colspan="6" style="height: 36px">             
                             <asp:TextBox ID="TextBoxDOINum" runat="server"   Width="351px" MaxLength="20" Style="border-style:inset none none inset;"></asp:TextBox>
     
                 
                       
                         </td>
                        
          </tr>
     --%>
                    
                     </table>  
                                <table>

                           <tr>

                                    <td style="height: 36px; font-weight:normal;width: 105px;">
                           <asp:Label ID="LabelkeyWords" runat="server" Text="Keywords" ForeColor="Black" ></asp:Label>
                           <asp:Label ID="Label14" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxKeywords" runat="server"   Width="400px" MaxLength="200" Style="border-style:inset none none inset;"></asp:TextBox> </td>
     
                               </tr>
                                    <tr>
                                 <td style="font-weight:normal">
                                <asp:Label ID="LabelDOINum" runat="server" Text="DOI Number" ForeColor="Black" ></asp:Label></td>
                                 <td class="auto-style27">
                                <asp:TextBox ID="TextBoxDOINum" runat="server"   Width="307px" MaxLength="200" Style="border-style:inset none none inset;"></asp:TextBox>
                               </td>
                                            <td class="auto-style26">&nbsp;</td>
                             <td style="font-weight:normal">
                    <asp:Label ID="LabelisErf" runat="server" Text="ERF Related?" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td class="auto-style27">
                         <asp:DropDownList ID="DropDownListErf" runat="server" Style="border-style:inset none none inset;">
                          <asp:ListItem Value="N">No</asp:ListItem>
                         <asp:ListItem Value="Y">Yes</asp:ListItem>
                          
                         </asp:DropDownList>
                            <asp:Label ID="Label234" runat="server" Text="( Environmental Research Fund )" ForeColor="Black" ></asp:Label>
                       

            </td>
                     <%--         <td style="height: 36px; font-weight:normal">
                             <asp:Label ID="LabelReferences" runat="server" Text="References" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px">
                     <asp:TextBox ID="TextBoxReference" runat="server" MaxLength="50" Style="border-style:inset none none inset;"
        Width="400px" ></asp:TextBox>   
        
                        </td>--%>
           
             </tr>
                                 
                       <tr>
                          
                      <td style="font-weight:normal;width:95px"> <asp:Label ID="LabelProjectReference" runat="server" Text="Project Reference?" ForeColor="Black" Visible="false"  ></asp:Label><asp:Label ID="LabelProjectRef" runat="server" Text="*"  ForeColor="Red" Visible="false"></asp:Label></td> 
     <td  >
       <asp:DropDownList ID="DropDownListhasProjectreference" runat="server" Style="border-style:inset none none inset;" Height="20px" Width="100px" Visible="false" OnSelectedIndexChanged="DropDownListhasProjectreference_SelectedIndexChanged"  AutoPostBack="true" >
         <asp:ListItem Value="0" Selected="True">-Select-</asp:ListItem>
         <asp:ListItem Value="Y">Yes</asp:ListItem>
        <asp:ListItem Value="N" >No</asp:ListItem>
       </asp:DropDownList>
            <asp:Label ID="LabelhasProjectreferenceNote" runat="server" Text="(Reference to Ongoing/Completed Projects)" ForeColor="Black" Visible="false" ></asp:Label>
     </td>
                       </tr>
                                    <tr>

                                    <td style="height: 36px; font-weight:normal;width: 105px;">
                           <asp:Label ID="LabelProjectDetails" runat="server" Text="Project Details" ForeColor="Black" Visible="false" ></asp:Label>
                         <%--  <asp:Label ID="Label16" runat="server" Text="*"  ForeColor="Red"></asp:Label>  --%>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxProjectDetails" runat="server"   Width="400px" MaxLength="100" Style="border-style:inset none none inset;" Visible="false" Enabled="false"></asp:TextBox>     
                            <asp:ImageButton ID="ImageButtonProject" runat="server" ImageUrl="~/Images/srchImg.gif" OnClick="showPop4" Visible="false" OnClientClick="setRow2(this)" /></td>
                                        <%--  <asp:ModalPopupExtender ID="ModalPopupExtender5" runat="server" TargetControlID="ImageButtonProject" PopupControlID="popupPanelProject"
                    BackgroundCssClass="modelBackground"  >
            </asp:ModalPopupExtender> --%>
                                         </tr>



                                   <%-- <tr>

                                    <td style="height: 36px; font-weight:normal;width: 105px;">
                           <asp:Button ID="feedbackbutton" runat="server" Text="Feedback" ForeColor="Black" Visible="false"  OnClick="feedbackbutton_Click"  OnClientClick="setRow2(this)" ></asp:Button>
                        </td>
                                        </tr>--%>


                        <%--<tr>
                            <td class="auto-style22" style="font-weight: normal">
                                <asp:Label ID="LabelUploadPfd" runat="server" ForeColor="Black" Text="Upload"> </asp:Label>
                            </td>
                            <td class="auto-style20">
                                <asp:FileUpload ID="FileUploadPdf" runat="server" BorderColor="#996600" BorderStyle="Inset" ClientIDMode="Static" Enabled="false" />
                                
                                <asp:GridView ID="GVViewFile" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="3" CellSpacing="3" DataSourceID="DSforgridview" HeaderStyle-ForeColor="White" OnSelectedIndexChanged="GVViewFile_SelectedIndexChanged" PagerStyle-ForeColor="White" PagerStyle-Height="4" PagerStyle-Width="4">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="Select" ImageUrl="~/Images/view.gif" ToolTip="View File" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgetid" runat="server" Text='<%# Eval("UploadPDFPath") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#0b532d" Font-Bold="True" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="DSforgridview" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="  "></asp:SqlDataSource>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="FileUploadPdf" ErrorMessage="!Upload only PDF(.pdf,.PDF) file" ForeColor="Red" ValidationExpression="^.*\.((p|P)(d|D)(f|F))$" ValidationGroup="validation"></asp:RegularExpressionValidator>
                            </td>
                           
                      
                        </tr>--%>
                                      <%--<tr>
                                          <td colspan="6"><span style="margin-top: 25px;">    <asp:Label ID="lblupload" runat="server" Text="Note:Please Upload the file once the Publication Entry is Saved" ForeColor="Black"  ></asp:Label></label></span>  <br />
                                          <br /></td>
                                            </tr> 
                                    <caption>
                                       
                                        <tr>
                                            <td colspan="6"><span style="margin-top: 25px;">Upload only PDF files (optional for Books) and the file size has to be less than or equal to 10MB.</span><br /> <span style="margin-top: 25px;">If the file size exceeds 10MB please contact Directorate of Research (help.rms@manipal.edu).</span> </td>
                                        </tr>
                                    </caption>--%>
                   
         </table>
                     </ContentTemplate>
       
                        </asp:UpdatePanel>
                     </asp:Panel>
                     <br />
                       <asp:UpdatePanel ID="EditUpdatePanel" runat="server" UpdateMode="Conditional">
    <Triggers>
<asp:AsyncPostBackTrigger ControlID = "Grid_AuthorEntry" />
       <%-- <asp:AsyncPostBackTrigger ControlID = "GridViewProject" />--%>

</Triggers>
        <ContentTemplate>
                     <asp:Panel ID="panAddAuthor" runat="server" GroupingText="Author Details" Visible="false"  ForeColor="Black"  >
              <%--       <center>
          Author Details
                     </center>--%>
                     <br />
                         <table width="92%">
                  <tr><td> <asp:Label ID="lblmsg2" runat="server" Visible="false" Text="Author Details in the approval stage will not be saved.For update go to search page and modify the respective Publication"></asp:Label></td></tr>
                           <tr>
                           <td>
                           <asp:Button ID="BtnAddMU" runat="server" Text="Add New author" onclick="addRow" CausesValidation="true" ValidationGroup="validation" Height="26px"/> 
                            
                           </td>
                            <td><asp:Label ID="lblnote1" runat="server" Text="Note: Please enter authors  in the order of appearance as in the publication" Visible="false"></asp:Label></td>
                           </tr>
                           </table>

  
  <br />

<asp:SqlDataSource ID="SqlDataSourceAuthorType" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                ProviderName="System.Data.SqlClient"> 
                                     
            </asp:SqlDataSource>

<asp:Panel ID="PanelMU" runat="server" ScrollBars="Both"   Width="1050px" style="margin-right: 0px">

     <asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False"  GridLines="None"
     OnRowDeleting="Grid_AuthorEntry_RowDeleting" OnRowDataBound="OnRowDataBound" CellPadding="4" ForeColor="#333333">

     <AlternatingRowStyle BackColor="White" />

     <Columns>

        <asp:TemplateField HeaderText="MAHE/Non MAHE">
             <ItemTemplate>
                 <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="150" OnSelectedIndexChanged="DropdownMuNonMuOnSelectedIndexChanged" AutoPostBack="true" DataSourceID="SqlDataSourceAuthorType" >
               
                 </asp:DropDownList>
                 
                 <asp:ImageButton ID="MuNonMu" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS"  />

               

             </ItemTemplate>              
        </asp:TemplateField>

        <asp:TemplateField ShowHeader="true" HeaderText="Roll No/Employee Code">
             <ItemTemplate>
       <asp:TextBox ID="EmployeeCode" runat="server" Width="155" Visible="true" MaxLength="9"></asp:TextBox>
        <asp:Image ID="ImageEmpCode" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />       
                
             
             </ItemTemplate>            
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Author Name">
             <ItemTemplate>
                 <asp:TextBox ID="AuthorName" runat="server" Width="200" Enabled="false" OnTextChanged="AuthorName_Changed" AutoPostBack="true"></asp:TextBox>
          
                 <asp:ImageButton ID="EmployeeCodeBtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClick="showpopup" OnClientClick="setRow(this)" />
               

                <%-- <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="EmployeeCodeBtn" PopupControlID="popup"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>--%>
                                  <asp:ImageButton ID="ImageButton1" runat="server" Visible="false" ImageUrl="~/Images/srchImg.gif"  OnClick="showpopup1" OnClientClick="setRow(this)" />

                 <%--<asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="ImageButton1" PopupControlID="popup"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>--%>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator23" Display="None" 
                 ControlToValidate="AuthorName" ValidationGroup="validation2"
                 runat="server" 
                 ErrorMessage="Please enter Author Name" 
                 /> 
             </ItemTemplate>              
        </asp:TemplateField>





        



        <asp:TemplateField HeaderText="Institution Name">
         <ItemTemplate>
             <asp:TextBox ID="InstitutionName" runat="server" Width="150" Enabled="false"></asp:TextBox> 
                <asp:DropDownList ID="DropdownStudentInstitutionName" runat="server" Width="150" Visible="false" AutoPostBack="true" DataSourceID="SqlDataSourceDropdownStudentInstitutionName" DataTextField="Institute_Name"  DataValueField="Institute_Id" >
                 
               
                 </asp:DropDownList>
               <asp:SqlDataSource ID="SqlDataSourceDropdownStudentInstitutionName" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select Institute_Id,Institute_Name from Institute_M">
                                     </asp:SqlDataSource>     
             <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />           
             </ItemTemplate>              
        </asp:TemplateField>

                      
       

        <asp:TemplateField HeaderText="Department Name/Course">
             <ItemTemplate>
                 <asp:TextBox ID="DepartmentName" runat="server" Width="180" Enabled="false"></asp:TextBox>
                     <asp:DropDownList ID="DropdownStudentDepartmentName" runat="server" Width="180" Visible="false"  DataSourceID="SqlDataSourceDropdownStudentDepartmentName" DataTextField="DeptName" DataValueField="DeptId" >
                 
              
                 </asp:DropDownList>

                      <asp:SqlDataSource ID="SqlDataSourceDropdownStudentDepartmentName" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select DeptId,DeptName from Dept_M where Institute_Id=@Institute_Id ">
                                           <SelectParameters>
   <%-- <asp:QueryStringParameter Name="Institute_Id" QueryStringField="Institute_Id" />--%>
     <asp:ControlParameter Name="Institute_Id" 
      ControlID="DropdownStudentInstitutionName"
      PropertyName="SelectedValue"/>
  </SelectParameters>
                                     </asp:SqlDataSource>
                 <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
            </ItemTemplate>            
        </asp:TemplateField>

           <asp:TemplateField HeaderText="MailId">
             <ItemTemplate>
                 <asp:TextBox ID="MailId" runat="server" Width="250" Enabled="false" ></asp:TextBox>
                 
                 <asp:Image ID="ImageMailId" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
     
       <asp:RegularExpressionValidator
        id="regEmail"
        ControlToValidate="MailId" Display="Static" ErrorMessage="Invalid Email Id"
        Text="(Invalid email)" ValidationGroup="validation"
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Runat="server" />    
             </ItemTemplate>  
                     
        </asp:TemplateField>

 <asp:TemplateField HeaderText="isCorrAuth">
             <ItemTemplate>
                 <asp:DropDownList ID="isCorrAuth" runat="server" Width="75" >
                 <asp:ListItem Value="Y">Yes</asp:ListItem>
                        <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                 </asp:DropDownList>

                 <asp:Image ID="ImageisCorrAuth" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                

             </ItemTemplate>              
        </asp:TemplateField>

         <asp:TemplateField HeaderText="AuthorType">
             <ItemTemplate>
                 <asp:DropDownList ID="AuthorType" runat="server" Width="125" >
                      <asp:ListItem Value="0" Selected="True">-Select-</asp:ListItem>
                 <asp:ListItem Value="P">First Author</asp:ListItem>
                        <asp:ListItem Value="C" >CO-Author</asp:ListItem>
                 </asp:DropDownList>

                 <asp:Image ID="ImageisAuthorType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                

             </ItemTemplate>              
        </asp:TemplateField>

          <asp:TemplateField HeaderText="NameAsInJournal">
             <ItemTemplate>
          <asp:TextBox ID="NameInJournal" runat="server" Width="190" ></asp:TextBox>

                 
                 <asp:Image ID="ImageNameInJournal" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

           

                

             </ItemTemplate>              
        </asp:TemplateField>

       <asp:TemplateField HeaderText="IsPresenter">
             <ItemTemplate>
          <asp:DropDownList ID="IsPresenter" runat="server" Width="75" OnSelectedIndexChanged="IsPresenterIsPresenter"  AutoPostBack="true">
                 <asp:ListItem Value="N">No</asp:ListItem>
                 <asp:ListItem Value="Y">Yes</asp:ListItem>
                 
                 </asp:DropDownList>
                 
                 <asp:Image ID="ImageIsPresenter" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

           

                

             </ItemTemplate>              
        </asp:TemplateField>

               <asp:TemplateField HeaderText="HasAttented">
             <ItemTemplate>
          <asp:CheckBox ID="HasAttented" runat="server" Width="70" ></asp:CheckBox>

                 
                 <asp:Image ID="ImageHasAttented" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

           

                

             </ItemTemplate>              
        </asp:TemplateField>

          
         <asp:TemplateField  HeaderText="Colloboration">
             <ItemTemplate>
                 <asp:DropDownList ID="NationalType" runat="server" Width="140" OnSelectedIndexChanged="NationalTypeOnSelectedIndexChanged" AutoPostBack="true"  >
                 <asp:ListItem Value="N">National</asp:ListItem>
                        <asp:ListItem Value="I">International</asp:ListItem>
                 </asp:DropDownList>

                 <asp:Image ID="ImageisNationalType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                

             </ItemTemplate>              
        </asp:TemplateField>
            <asp:TemplateField  HeaderText="Continent">
             <ItemTemplate>
                 <asp:DropDownList ID="ContinentId" runat="server" Width="140"   DataSourceID="SqlDataSourceDropdownContinentId" DataTextField="ContinentName" DataValueField="ContinentId" >
     
                 </asp:DropDownList>
                       <asp:SqlDataSource ID="SqlDataSourceDropdownContinentId" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select ContinentId,ContinentName  from Continent_M ">
      
                                     </asp:SqlDataSource>
                 <asp:Image ID="ImageisContinent" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                

             </ItemTemplate>              
        </asp:TemplateField>

         
                        <asp:TemplateField ShowHeader="false">
         <ItemTemplate>
             <asp:HiddenField ID="Institution" runat="server" ></asp:HiddenField>               
                          
                 <asp:ImageButton ID="InstitutionBtn" runat="server"  ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                  
                     
          </ItemTemplate>              
        </asp:TemplateField>
             <asp:TemplateField ShowHeader="false">
             <ItemTemplate>
                 <asp:HiddenField ID="Department" runat="server" ></asp:HiddenField>
                                  
                 <asp:ImageButton ID="DepartmentBtn" runat="server" Enabled="false" ImageUrl="~/Images/srchImg.gif"  CssClass="blnkImgCSS"  />

             
            </ItemTemplate>            
        </asp:TemplateField>


         <asp:CommandField DeleteText="Remove" ShowDeleteButton="True"   />

        </Columns>
        <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#000000" ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>


</asp:Panel>
</asp:Panel>

             </ContentTemplate>
    </asp:UpdatePanel>

<br />

  <asp:Panel ID="panelJournalArticle" runat="server" GroupingText="Journal Publish Details" Visible="false"  ForeColor="Black"  > 
<%--  <center>
  Journal Article
  </center>--%>
  <br />

  <table>
  
  <tr align="center">
                              
     <td style="height: 36px; font-weight:normal"> <asp:Label ID="LabelIndexed" runat="server" Text="Indexed:" ForeColor="Black"  ></asp:Label></td>
     <td style="font-weight:normal" >   <asp:RadioButtonList ID="RadioButtonListIndexed" runat="server"    RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonListIndexedOnSelectedIndexChanged" AutoPostBack="true" Style="border-style:inset none none inset;">
                                        <asp:ListItem Value="N" >No</asp:ListItem>
                             <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                  
 
                             </asp:RadioButtonList>  
     
     </td> 
     <td></td>
        <td></td>
              <td></td>
                  <td></td>
  
   <td style="height: 36px; font-weight:normal">
    

     <asp:Label ID="LabelIndexedIn" runat="server" Text="Indexed In" ForeColor="Black"  ></asp:Label></td>
     <td  style="font-weight:normal" >
  <asp:CheckBoxList ID="CheckboxIndexAgency" runat="server" ForeColor="Black"  DataSourceID="SqlDataSourceCheckboxIndexAgency" RepeatDirection="Horizontal" DataTextField="agencyname" DataValueField="agencyid" Enabled="false">
                
                </asp:CheckBoxList>

                
<asp:SqlDataSource ID="SqlDataSourceCheckboxIndexAgency" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select agencyid,agencyname from IndexAgency_M where active='Y'">
     
                </asp:SqlDataSource>

     </td>
     <td></td>
         <td></td>
             
         <td>

         <asp:Panel ID="panelIndexDet" runat="server" GroupingText="Note">
         <table>
         <tr>
          <td style="font-weight:normal"><asp:Label ID="LabelIndexeddetails" runat="server" Text="Indexed - Search the Journal or type the ISSN"   ></asp:Label>
          </td>
         
          </tr>
           <tr>
            <td style="font-weight:normal"><asp:Label ID="LabelNonIndexeddetails" runat="server" Text="Non Indexed - Enter the ISSN and title for the journal"   ></asp:Label>
          </td>
         </tr>
         </table>
                        
         </asp:Panel>
         </td>      

  </tr>
  </table>
 <table  style="width: 100%">
 <tr>
<%--  <td colspan="6" align="center">
                          <asp:Label ID="LabelPubDetail" runat="server" Text="Publisher Details"  Font-Size="Medium" ></asp:Label>
                               
                               </td>--%>
 </tr>
                    <tr>
                         <td style="height: 56px; font-weight:normal" >
                             <asp:Label ID="LabelMonthJA" runat="server" Text="Publish Month" ForeColor="Black"   ></asp:Label>
                                  <asp:Label ID="Labeljastr2" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label></td>
                                  <td>
                              <asp:DropDownList ID="DropDownListMonthJA" runat="server" Style="border-style:inset none none inset;" DataSourceID="SqlDataSourcePubJAmonth"
      DataTextField="MonthName" DataValueField="MonthValue"  Width="50px"  OnSelectedIndexChanged="txtboxYear_TextChanged" AutoPostBack="true">
      

       
        </asp:DropDownList>   
          <asp:SqlDataSource ID="SqlDataSourcePubJAmonth" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select MonthValue, MonthName from Publication_MonthM">
     
                </asp:SqlDataSource>
                        </td>
                        <td style="height: 56px; font-weight:normal" >
                     <asp:Label ID="LabelYearJA" runat="server" Text="Publish Year" ForeColor="Black"  ></asp:Label>
                          <asp:Label ID="Labeljastr3" runat="server" Text="*" ForeColor="Red" Visible="false" ></asp:Label></td><td>
                              <asp:DropDownList ID="TextBoxYearJA" runat="server" 
        Width="60px"  Style="border-style:inset none none inset;"  OnSelectedIndexChanged="txtboxYear_TextChanged" AutoPostBack="true">
        


        </asp:DropDownList>  

        
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="TextBoxYearJA" ValidationGroup="validation"
                ErrorMessage="Entered Proper Year" SetFocusOnError="true"  
                ValidationExpression="^[0-9]{4}$">
                </asp:RegularExpressionValidator> 
        
                        </td>
                        <td style="height: 36px; font-weight:normal" >
                          <asp:Label ID="LabelPubJournal" runat="server" Text="ISSN" ForeColor="Black" ></asp:Label>
                                     <asp:Label ID="Labeljastar1" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                               </td>
                        <td style="height: 36px" >             
                             <asp:TextBox ID="TextBoxPubJournal" runat="server"  Width="150px" OnTextChanged="JournalIDTextChanged" 
 AutoPostBack="true" Style="border-style:inset none none inset;"></asp:TextBox>
     
                       <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif"  OnClick="showPop2" OnClientClick="setRow1(this)" />
                      <asp:ImageButton ID="imageBkCbtn1" runat="server" ImageUrl="~/Images/srchImg.gif"  OnClick="showPop3" OnClientClick="setRow1(this)" />
                <%-- <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imageBkCbtn" PopupControlID="popupPanelJournal"
                    BackgroundCssClass="modelBackground"  >
                 </asp:ModalPopupExtender>
                       <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="imageBkCbtn1" PopupControlID="popupPanelProceedingsJournal"
                    BackgroundCssClass="modelBackground"  >
                 </asp:ModalPopupExtender>--%>
                         </td>
                      
                        <td style="height: 36px; font-weight:normal" >
                           <asp:Label ID="LabelNameJournal" runat="server" Text="Name Of Journal" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px" >
                             <asp:TextBox ID="TextBoxNameJournal" runat="server"  Enabled="false" Width="300px" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>

               
                           </tr>
                       </table>



      <table><tr><td><span>Note : Enter ISSN without Hyphen (-).</span></td> </tr> </table>




                       <table >
                           <tr>

                           
                               
              
               <td style="font-weight:normal" class="auto-style42"> <asp:Label ID="LabelImpFact" runat="server" Text="1-Year Impact Factor" ForeColor="Black"  ></asp:Label></td> 
     <td  >
       <asp:TextBox ID="TextBoxImpFact" runat="server"  ReadOnly="true" Enabled="false" Style="border-style:inset none none inset;" Width="100px" ></asp:TextBox>

     </td>

 <td class="auto-style49"></td>

<td style="font-weight:normal" class="auto-style44"> <asp:Label ID="LabelImpFact5" runat="server" Text="5-Year Impact Factor" ForeColor="Black"  ></asp:Label></td> 
     <td  >
       <asp:TextBox ID="TextBoxImpFact5" runat="server"  ReadOnly="true" Enabled="false" Style="border-style:inset none none inset;" Width="100px" ></asp:TextBox>

     </td>
                               <td class="auto-style46"></td>
        <td style="font-weight:normal" class="auto-style31"> <asp:Label ID="LblIFAY" runat="server" Text="IF-ApplicableYear" ForeColor="Black"  ></asp:Label></td> 
     <td  >
      <asp:TextBox ID="txtIFApplicableYear" runat="server"  ReadOnly="true" Enabled="false" Style="border-style:inset none none inset;" Width="100px" ></asp:TextBox>
     </td>
                                <td class="auto-style46"></td>
 <td style="font-weight:normal" class="auto-style35">
                             <asp:Label ID="lblQuartile" runat="server" Text="Quartile" ForeColor="Black" Visible="true" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtquartile" runat="server" ReadOnly="true" Width="100px" Style="border-style:inset none none inset;" Enabled="false"  Visible="true" ></asp:TextBox>
                        </td>
                        <td  style="font-weight:normal" class="auto-style35">
                             <asp:Label ID="lblQuartileid" runat="server" Text="QuartileID" ForeColor="Black" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtquartileid" runat="server" ReadOnly="true" Width="100px" Visible="false" Style="border-style:inset none none inset;"></asp:TextBox>
                        </td>
     </tr>
     </table>

     <table>
     <tr>
     
     
  <td style="font-weight:normal" class="auto-style35">  
                  <asp:Label ID="LabelPageFrom" runat="server" Text="Page From" ForeColor="Black"  ></asp:Label>
                       <%--<asp:Label ID="Labeljastr4" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                        </td>
                         
         <td>
      <asp:TextBox ID="TextBoxPageFrom" runat="server"  Width="100px" MaxLength="10" AutoPostBack="true"
                 Style="border-style:inset none none inset; margin-left: 23px;" 
                 ontextchanged="TextBoxPageFrom_TextChanged"></asp:TextBox> 
   </td>
     <td class="auto-style38"></td>
                                <td style="font-weight:normal" class="auto-style32">
                    <asp:Label ID="LabelPageTo" runat="server" Text="Page To" ForeColor="Black"  ></asp:Label>
                        <%-- <asp:Label ID="Labeljastr5" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                        </td>
                        <td style="height: 36px" >
                         <asp:TextBox ID="TextBoxPageTo" runat="server"   Width="100px" MaxLength="10" Enabled="false" Style="border-style:inset none none inset;"></asp:TextBox>
      
            </td>
         <td class="auto-style50"></td>
            <td style="font-weight:normal" class="auto-style47">
                                <asp:Label ID="LabelVolume" runat="server" Text="Volume" ForeColor="Black"  ></asp:Label>
                                
                                  <asp:Label ID="Labelvolstar8" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                </td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxVolume" runat="server"   Width="100px" Style="border-style:inset none none inset;"></asp:TextBox>
     
                       
                         </td>
         <td class="auto-style40"></td>
                           <td style="font-weight:normal" class="auto-style34">  <asp:Label ID="Labelissue" runat="server" Text="Issue" ForeColor="Black"  ></asp:Label>
<%--          <asp:Label ID="Labeljastr7" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
      </td>

    <td  style="font-weight:normal" >  <asp:TextBox ID="TextBoxIssue" runat="server"   Width="181px" Style="border-style:inset none none inset;"></asp:TextBox></td>
                         </tr>
         </table>
      <table>
                    <tr>
                      
        <td style="font-weight:normal;width:95px"> <asp:Label ID="LabelPubicationType" runat="server" Text="Publication Type" ForeColor="Black"  ></asp:Label></td> 
     <td  >
       <asp:DropDownList ID="DropDownListPubType" runat="server" Style="border-style:inset none none inset;" Height="20px" Width="100px" >
       <asp:ListItem Value="N">National</asp:ListItem>
        <asp:ListItem Value="I">International</asp:ListItem>
       </asp:DropDownList>

     </td>
                         
                    </tr>   
    </table>
   

  <table>
    <tr>
  <td >
<%--  <asp:Label ID="Label277" runat="server" Text="----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"></asp:Label>
--%>  </td>
  </tr>
  <tr>
  <td>
  <asp:Label ID="lblnote" runat="server" Text="Note: Please contact Directorate of Research (help.rms@manipal.edu) if the desired Journal is not found for the Indexed Category."></asp:Label>
  </td>
  </tr>
                 
               
                     </table>  
         
</asp:Panel>


  <asp:Panel ID="panelConfPaper" runat="server" GroupingText=" Conference Paper" Visible="false" > 

<%--  <center>
  Conference Paper
  </center>--%>
  <br />
 <table width="100%" >
                    <tr>
                        <td style="height: 36px; font-weight:normal">
                          <asp:Label ID="LabelEventTitle" runat="server" Text="Conference Title" ></asp:Label>
                                               <asp:Label ID="Label11" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                               </td>
                        <td style="height: 36px">             
                             <asp:TextBox ID="TextBoxEventTitle" runat="server"   Width="450px" MaxLength="200" Style="border-style:inset none none inset;"></asp:TextBox>
     
                 
                       
                         </td>
                      
   
                 <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="LabelDate" runat="server" Text="Conference From Date" ></asp:Label>
                                                <asp:Label ID="Label2confdate" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                                
                                </td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxDate" runat="server"   Width="130px" Style="border-style:inset none none inset;"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                TargetControlID="TextBoxDate" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>

                 <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None"  ControlToValidate="TextBoxDate" ValidationGroup="validation"
                ErrorMessage="Presentation Date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>
          <asp:CompareValidator ID="CompareValidatorTextBoxDate" runat="server" ErrorMessage=" Date of Recruitment must be less than or equal to the current date." Display="None" ControlToValidate="TextBoxDate" ValidationGroup="validation"

Operator="LessThanEqual" Type="Date" SetFocusOnError="true" ></asp:CompareValidator>     </td>

                        <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="LabelDate1" runat="server" Text="Conference To Date" ></asp:Label>
                                                <asp:Label ID="Label2confdate2" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                                
                                </td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxDate1" runat="server"   Width="130px" Style="border-style:inset none none inset;"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender3" runat="server"
                TargetControlID="TextBoxDate1" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>

                 <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="None"  ControlToValidate="TextBoxDate1" ValidationGroup="validation"
                ErrorMessage="Presentation Date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>
       <%--   <asp:CompareValidator ID="CompareValidatorTextBoxDate1" runat="server" ErrorMessage=" Date of Recruitment must be less than or equal to the current date." Display="None" ControlToValidate="TextBoxDate1" ValidationGroup="validation"

Operator="LessThanEqual" Type="Date" SetFocusOnError="true" ></asp:CompareValidator> --%>  </td></tr>

                           </table>

                            <table width="100%" >
                       
                <tr>
                                 <td style="height: 36px; font-weight:normal;width: 96px;">
                           <asp:Label ID="LabelISBN" runat="server" Text="ISBN" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxIsbn" runat="server"  Width="400px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
                
                                     <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="LabelPlace" runat="server" Text="Conference Venue" ></asp:Label>
                            <asp:Label ID="Label12" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                   
                        <td style="height: 36px" >
                             <asp:TextBox ID="TextBoxPlace" runat="server"  Width="400px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>

                  

                </tr>       
                </table>
        <table width="100%">
          <tr>
              
                                 <td style="height: 36px; font-weight:normal;width: 480px;">
                           <asp:Label ID="lblCity" runat="server" Text="City" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="txtcity" runat="server"  Width="250px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
              <td class="auto-style38"></td>
                                 <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="lblState" runat="server" Text="State/Country" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="txtState" runat="server"  Width="250px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
              <td class="auto-style38"></td>
               <td style="height: 36px; font-weight:normal">
                  <asp:Label ID="LabelConferencetype" runat="server" Text="Type Of Conference" ForeColor="Black"  ></asp:Label>
              </td>
              <td style="height: 36px; font-weight:normal">
                   <asp:DropDownList ID="DropDownListConferencetype" runat="server" Width="250px" DataTextField="Type" DataValueField="Id" Style="border-style:inset none none inset;" >
<asp:ListItem Value="0">--Select--</asp:ListItem>
        <asp:ListItem Value="N">National</asp:ListItem>
        <asp:ListItem Value="I">International</asp:ListItem>
     </asp:DropDownList>
     
       <%-- <asp:SqlDataSource ID="SqlDataSourceDropDownListConferencetype" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select Id,Type from Conference_Type_M">
     
                </asp:SqlDataSource>     --%>                   
                  </td>
          </tr>
      </table>
             <table width="100%">
             <tr>
                   <td style="height: 56px; font-weight:normal;    width: 100px;" >
                             <asp:Label ID="Label18" runat="server" Text="Publish Month" ForeColor="Black"   ></asp:Label>
                                  <asp:Label ID="Label19" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label></td>
                                  <td>
                              <asp:DropDownList ID="DropDownListMonthCP" runat="server" Style="border-style:inset none none inset;" DataSourceID="SqlDataSourceCPPubJAmonth"
      DataTextField="MonthName" DataValueField="MonthValue"  Width="100px" AutoPostBack="true" AppendDataBoundItems="true">
      <asp:ListItem Value="0">-select-</asp:ListItem>

       
        </asp:DropDownList>   
          <asp:SqlDataSource ID="SqlDataSourceCPPubJAmonth" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select MonthValue, MonthName from Publication_MonthM">
     
                </asp:SqlDataSource>
                        </td>
                   <td class="auto-style38"></td>
                        <td style="height: 56px; font-weight:normal" >
                     <asp:Label ID="Label20" runat="server" Text="Publish Year" ForeColor="Black"  ></asp:Label>
                          <asp:Label ID="Label21" runat="server" Text="*" ForeColor="Red" Visible="false" ></asp:Label></td><td>
                          <%--    <asp:DropDownList ID="TextBoxYearJA" runat="server" 
        Width="60px"  Style="border-style:inset none none inset;" AutoPostBack="true">
        </asp:DropDownList>  
                           --%>
           <asp:TextBox ID="TextBoxYearCP" runat="server"  Width="100px" MaxLength="10" AutoPostBack="true"
                 Style="border-style:inset none none inset;" ></asp:TextBox> 
           <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="None"  ControlToValidate="TextBoxYearCP" ValidationGroup="validation"
                ErrorMessage="Entered Proper Year" SetFocusOnError="true"  
                ValidationExpression="^[0-9]{4}$">
                </asp:RegularExpressionValidator> 
        </td>
                  <td class="auto-style38"></td>
                  <td style="font-weight:normal" class="auto-style32">  
                  <asp:Label ID="Label22" runat="server" Text="Page From" ForeColor="Black"  ></asp:Label>
                       <%--<asp:Label ID="Labeljastr4" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                        </td>
                         
         <td>
      <asp:TextBox ID="TextBoxPageFromCP" runat="server"  Width="100px" MaxLength="10" AutoPostBack="true"
                 Style="border-style:inset none none inset;" 
                 ontextchanged="TextBoxPageFromCP_TextChanged"></asp:TextBox> 
   </td>
     <td class="auto-style38"></td>
                                <td style="font-weight:normal" class="auto-style32">
                    <asp:Label ID="Label23" runat="server" Text="Page To" ForeColor="Black"  ></asp:Label>
                        <%-- <asp:Label ID="Labeljastr5" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                        </td>
                        <td style="height: 36px" >
                         <asp:TextBox ID="TextBoxPageToCP" runat="server"   Width="100px" MaxLength="10" Enabled="false" Style="border-style:inset none none inset;"></asp:TextBox>
      
            </td>
                 <td>
                  <%--       <asp:DropDownList ID="TextBoxYear" runat="server" Style="border-style:inset none none inset;"
                             Width="100px" >

                          </asp:DropDownList> --%>
                 </td>
             </tr>
         </table>
      <table width="100%">
          <tr>
             
        <td style="height: 36px; font-weight:normal;width: 100px;"> <asp:Label ID="LblCPIndexed" runat="server" Text="Indexed:" ForeColor="Black"  ></asp:Label></td>
     <td style="font-weight:normal" >   <asp:RadioButtonList ID="RadioButtonListCPIndexed" runat="server"    RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonListCPIndexedOnSelectedIndexChanged" AutoPostBack="true" Style="border-style:inset none none inset;">
                                        <asp:ListItem Value="N" >No</asp:ListItem>
                             <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                  
 
                             </asp:RadioButtonList>  
     
     </td> 
     <td class="auto-style22"></td>
  <td class="auto-style38"></td>
  
   <td style="height: 36px; font-weight:normal">
    

     <asp:Label ID="Label15" runat="server" Text="Indexed In:" ForeColor="Black"  ></asp:Label></td>
     <td  style="font-weight:normal" >
  <asp:CheckBoxList ID="CheckBoxListCPIndexedIn" runat="server" ForeColor="Black"  DataSourceID="SqlDataSourceCheckboxCPIndexAgency" RepeatDirection="Horizontal" DataTextField="agencyname" DataValueField="agencyid" Enabled="false">
                
                </asp:CheckBoxList>

                
<asp:SqlDataSource ID="SqlDataSourceCheckboxCPIndexAgency" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select agencyid,agencyname from IndexAgency_M where active='Y'">
     
                </asp:SqlDataSource>

     </td>
      <td class="auto-style38"></td>
          <td style="height: 36px; font-weight:normal">
                          <asp:Label ID="Label2Presentation" runat="server" Text="Type Of Presentation:" ></asp:Label>
                               
                               </td>
                        <td style="height: 36px; font-weight:normal">             
                             <asp:RadioButtonList ID="RadioButtonListTypePresentaion" runat="server"  RepeatDirection="Horizontal"  Style="border-style:inset none none inset;">
                             <asp:ListItem Value="P">Poster</asp:ListItem>
                                     <asp:ListItem Value="O">Oral  </asp:ListItem>
                                     <asp:ListItem  Value="B">Both</asp:ListItem >
                             </asp:RadioButtonList>
     
                 
                       
                         </td>

             </tr>
             </table>
  
                 <table width="100%">
     
        <tr>
                       
                        
                                      
                      
                        <td style="height: 36px; font-weight:normal;width: 100px;">
                           <asp:Label ID="Label3" runat="server" Text="Credit Point" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxCreditPoint" runat="server" Width="100px" Text="0" Style="border-style:inset none none inset;" OnTextChanged="TextBoxCreditPointOnTextChanged" AutoPostBack="true"></asp:TextBox>
         </td>

                 <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="Label4" runat="server" Text="Awarded By" ></asp:Label></td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxAwardedBy" runat="server" Enabled="false"   Width="400px" Style="border-style:inset none none inset;"></asp:TextBox>
     
                         </td>
              <td style="height: 36px; font-weight:normal;width: 125px;">
                           <asp:Label ID="LabelFunds" runat="server" Text="Funds Utilized" ></asp:Label>
                        </td>
                        <td style="font-weight:normal" class="auto-style53">
                             <asp:TextBox ID="TextBoxFunds" runat="server"  Width="150px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
                           </tr>
                       


               
                     </table>  
  

</asp:Panel>



  <asp:Panel ID="panelBookPublish" runat="server" GroupingText="Book Publish" Visible="false"> 
<%--  <center>
  Book Publish
  </center>--%>
  <br />
 <table  style="width: 100%" cellspacing="5">
                    <tr>
                        <td style="height: 36px; font-weight:normal">
                          <asp:Label ID="LabelTitileBook" runat="server" Text="Title Of The Book" ></asp:Label>
                             <asp:Label ID="Labelstr" runat="server" Text="*"  ForeColor="Red"></asp:Label>   
                               </td>
                        <td style="height: 36px">             
                             <asp:TextBox ID="TextBoxTitleBook" runat="server"  Width="880px" MaxLength="100"  Rows="2" TextMode="MultiLine" Style="border-style:inset none none inset;"></asp:TextBox>
     
                 
                       
                         </td>
                      
                    
                           </tr>
                     
                        <tr>
                      <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="LabelTitleChapterContributed" runat="server" Text="Title Of the Chapter Contributed" ></asp:Label>
                                    <asp:Label ID="Label2str" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxChapterContributed" runat="server"   Width="880px" MaxLength="100" Rows="2" TextMode="MultiLine" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
         </tr>
         </table>
     
         <table style="width: 100%" >
         <tr>
         

                 <td style="font-weight:normal" class="auto-style8"  >
                                <asp:Label ID="LabelEdition" runat="server" Text="Edition" ></asp:Label>
                                         <asp:Label ID="Label5" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                                </td>
                        <td colspan="3">             
                       <asp:TextBox ID="TextBoxEdition" runat="server"   Width="304px" MaxLength="50"  Style="border-style:inset none none inset;"></asp:TextBox>
     
                       
                         </td>
              
                       <td style="width:20px"></td>
                   

                           
                                <td style="font-weight:normal">
                             <asp:Label ID="LabelPublisher" runat="server" Text="Publisher" ></asp:Label>
                                      <asp:Label ID="Label6" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                        <td   colspan="3">
                     <asp:TextBox ID="TextBoxPublisher" MaxLength="50" Width="308px"  runat="server" Style="border-style:inset none none inset;"
     ></asp:TextBox>   
        
                        </td>
             </tr>
       </table>
      <table style="width: 100%" >
          <tr>
               <td style="font-weight:normal" class="auto-style8">
                           <asp:Label ID="lblbISBN" runat="server" Text="ISBN" ></asp:Label>
                        </td>
                        <td colspan="3">
                             <asp:TextBox ID="txtbISBN" runat="server"  Width="400px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
              <td style="font-weight:normal">
                           <asp:Label ID="lblbSection" runat="server" Text="Section" ></asp:Label>
                        </td>
                        <td colspan="3">
                             <asp:TextBox ID="txtbSection" runat="server"  Width="350px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
          </tr>
          </table>
      <table style="width: 100%" >
          <tr>
                <td style="font-weight:normal" class="auto-style8">
                     <asp:Label ID="lblbChapter" runat="server" Text="Chapter" ></asp:Label>
                        </td>
                        <td >
                             <asp:TextBox ID="txtChapter" runat="server"  Width="250px"  Style="border-style:inset none none inset;"></asp:TextBox>
                         
         </td>
              <td>
              </td>
              <td style="font-weight:normal">
                           <asp:Label ID="lblbCountry" runat="server" Text="Country" ></asp:Label>
                        </td>
                        <td >
                             <asp:TextBox ID="txtCountry" runat="server"  Width="250px" Style="border-style:inset none none inset;"></asp:TextBox> 
         </td>
               <td>
              </td>
             <td style="font-weight:normal;width: 130px;">
                           <asp:Label ID="lblbPublicationType" runat="server" Text="Type of Publication" ></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListBookPublicationType" runat="server" Width="150px" DataTextField="Type" DataValueField="Id" Style="border-style:inset none none inset;" >
<asp:ListItem Value="0">--Select--</asp:ListItem>
        <asp:ListItem Value="C">Chapter in a book</asp:ListItem>
        <asp:ListItem Value="F">Full book</asp:ListItem>
     </asp:DropDownList>
         </td>
          </tr>
          </table>
      <table style="width: 100%">
             <tr>
     
      <td style="font-weight:normal" class="auto-style8"  >  
                  <asp:Label ID="LabelYear" runat="server" Text="Year" ></asp:Label>
                         <span>  <asp:Label ID="Label7" runat="server" Text="*"   ForeColor="Red"></asp:Label> </span> 
                        </td>
                         
         <td>

         
                      <asp:DropDownList ID="TextBoxYear" runat="server" Style="border-style:inset none none inset;"
                             Width="100px" >

                          </asp:DropDownList> 

 

   </td>
   <td style="font-weight:normal;width: 60px;">  
                  <asp:Label ID="LabelMonth" runat="server" Text="Month" ></asp:Label>
                           <asp:Label ID="Label8" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                         
         <td>


                      <asp:DropDownList ID="DropDownListBookMonth" runat="server" Style="border-style:inset none none inset;"
                             DataSourceID="SqlDataSourcePubBookMonth" DataTextField="MonthName" DataValueField="MonthValue" Width="100px"  >

                          </asp:DropDownList> 
            <asp:SqlDataSource ID="SqlDataSourcePubBookMonth" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                
                SelectCommand="select MonthValue, MonthName from Publication_MonthM" 
                ProviderName="System.Data.SqlClient">
             
            </asp:SqlDataSource>
   </td>

     <td></td>
                                <td style="font-weight:normal;width: 100px;">
                    <asp:Label ID="Labelpagenum" runat="server" Text="Page Number" ></asp:Label>
                             <asp:Label ID="Label9" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                        <td>
                         <asp:TextBox ID="TextBoxPageNum" runat="server"   Width="100px" MaxLength="10" Style="border-style:inset none none inset;"></asp:TextBox>
      
            </td>
  <td style="font-weight:normal;width: 60px;"> <asp:Label ID="LabelVolume1" runat="server" Text="Volume"  ></asp:Label>
           <asp:Label ID="Label10" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
  </td> 
     <td  >
  <asp:TextBox ID="TextBoxVolume1" runat="server"  MaxLength="10"  Width="100px" Style="border-style:inset none none inset;"></asp:TextBox>
       

     </td>

                       </tr>
</table>

   

</asp:Panel>


 <asp:Panel ID="panelOthes" runat="server" GroupingText="NewsPaper/Magazine" Visible="false" BorderColor="Gray" BorderWidth="1px"> 
<%-- <center>
 NewsPaper/Magazine
 </center>--%>
 <br />
 <table  >
                    <tr>
                        <td style="height: 36px; font-weight:normal">
                          <asp:Label ID="LabelPublisherNewsPaper" runat="server" Text="Publisher" ></asp:Label>
                                <asp:Label ID="Labelstrnm" runat="server" Text="*"  ForeColor="Red"></asp:Label> 
                               
                               </td>
                        <td style="height: 36px">             
                             <asp:TextBox ID="TextBoxNewsPublish" runat="server"   Width="580px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
     
                 
                       
                         </td>
                      
                        <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="LabelDateOfPublish" runat="server" Text="Date Of Publish" ></asp:Label>
                            <asp:Label ID="Label2nmstr" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxDateOfNewsPublish" runat="server"  Width="100px" Style="border-style:inset none none inset;"></asp:TextBox>

                                   <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                TargetControlID="TextBoxDateOfNewsPublish" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="None"  ControlToValidate="TextBoxDateOfNewsPublish" ValidationGroup="validation"
                ErrorMessage="Date Of Publish must be in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>
                <asp:CompareValidator ID="CompareValidatorTextBoxDateOfNewsPublish" runat="server" ErrorMessage=" Date Of Publish must be less than or equal to the current date." Display="None" ControlToValidate="TextBoxDateOfNewsPublish" ValidationGroup="validation" 

Operator="LessThanEqual" Type="Date" SetFocusOnError="true" ></asp:CompareValidator>


         </td>

                 <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="LabelPageNumNewsPaper" runat="server" Text="PageNum" ></asp:Label></td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxPageNumNewsPaper" runat="server"   Width="90px" Style="border-style:inset none none inset;"></asp:TextBox>
     
                       
                         </td>
                           </tr>
                       
                       
     
    


               
                     </table>  

  

</asp:Panel>
<br />


  <asp:Panel ID="panelTechReport" runat="server" Visible="false" GroupingText="Generic Details" ForeColor="Black"  > 


<%--   <center>
Generic Details
 </center>--%>
  <center>
 <table style="width: 92%">
                   <%-- <tr>
                        <td style="height: 36px; font-weight:normal">
                             <asp:Label ID="LabelURL" runat="server" Text="Official URL" ForeColor="Black"  ></asp:Label>
                             <asp:Label ID="LabelURL1" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                        <td style="height: 36px" colspan="3">
                     <asp:TextBox ID="TextBoxURL" runat="server" MaxLength="200" Style="border-style:inset none none inset;"
        Width="900px" ></asp:TextBox>   
        
                        </td>
                     
                     

               
                           </tr>--%>
                      
       <tr><td>&nbsp;</td></tr>
          <tr align="center" style="display:none;">
     
     <td colspan="6" style="font-weight:normal">
     <asp:Label ID="Eprint" runat="server" Text="EPrint" Font-Bold="true" ForeColor="Black" ></asp:Label>
     
     </td>
     
     </tr>
                       <tr style="display:none;">
                     
                   <td style="height: 36px; font-weight:normal">
                             <asp:Label ID="LabeluploadEPrint" runat="server" Width="100px" Text="Upload To EPrint" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px">
                     <asp:DropDownList ID="DropDownListuploadEPrint" runat="server" Enabled="false" Style="border-style:inset none none inset;"
        Width="200px" >
           <asp:ListItem Value="N">No</asp:ListItem>
           <asp:ListItem Value="Y">Yes</asp:ListItem>
                        
        
        </asp:DropDownList>   
        
                        </td>
                        </tr>
                        <tr style="display:none;">
                         <td style="height: 36px; font-weight:normal" >
                             <asp:Label ID="Label1" runat="server" Text="EprintURL"  ForeColor="Black" ></asp:Label>
                        </td>
                        <td style="height: 36px" colspan="3">
                     <asp:TextBox ID="TextBoxEprintURL" runat="server" ReadOnly="true" Style="border-style:inset none none inset;"
        Width="900px" ></asp:TextBox>   
        
                        </td>
                         
                       
                       </tr>
     
               
                     </table>  
  </center>

</asp:Panel>
       <asp:Panel ID="panelFileUpload" runat="server" Visible="false" GroupingText="File Upload"  > 

         <table>
              <tr>
                            <td class="auto-style22" style="font-weight: normal">
                                <asp:Label ID="LabelUploadPfd" runat="server" ForeColor="Black" Text="Upload"> </asp:Label>
                            </td>
                            <td class="auto-style20">
                                <asp:FileUpload ID="FileUploadPdf" runat="server" BorderColor="#996600" BorderStyle="Inset" ClientIDMode="Static" Enabled="false" />
                                
                               
                            </td>
                           <td style="width: 100px;">
                                <asp:Button ID="Buttonupload" runat="server" Text="Upload" OnClick="Buttonupload_Click" Enabled="false" ></asp:Button>
<%--                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="FileUploadPdf" Display="None" Enabled="false"
 ErrorMessage="Please choose the file"  ValidationGroup="validation1"></asp:RequiredFieldValidator>--%>
                           </td>
                      
                        </tr>
             <tr>
                 <td class="auto-style22" style="font-weight: normal">
                     </td>
                 <td>
                      <asp:GridView ID="GVViewFile" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="3" CellSpacing="3" DataSourceID="DSforgridview" HeaderStyle-ForeColor="White" OnSelectedIndexChanged="GVViewFile_SelectedIndexChanged" PagerStyle-ForeColor="White" PagerStyle-Height="4" PagerStyle-Width="4">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="Select" ImageUrl="~/Images/view.gif" ToolTip="View File" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgetid" runat="server" Text='<%# Eval("UploadPDFPath") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#0b532d" Font-Bold="True" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="DSforgridview" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="  "></asp:SqlDataSource>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="FileUploadPdf" ErrorMessage="!Upload only PDF(.pdf,.PDF) file" ForeColor="Red" ValidationExpression="^.*\.((p|P)(d|D)(f|F))$" ValidationGroup="validation1"></asp:RegularExpressionValidator>
                 </td>
             </tr>
              <tr>
                            <td colspan="6"><span style="margin-top: 25px;">Note:Please Upload the file once the Publication Entry is Saved</span><br /><br /> </td>
                        </tr>
              <caption>
                  <br />
                  <tr>
                      <td colspan="6"><span style="margin-top: 25px;">Upload only PDF files (optional for Books) and the file size has to be less than or equal to 10MB.</span><br /> <span style="margin-top: 25px;">If the file size exceeds 10MB please contact Directorate of Research (help.rms@manipal.edu).</span> </td>
                  </tr>
              </caption>
         </table>
         </asp:Panel>
  <asp:Panel ID="panelRemarks" runat="server" Visible="false" GroupingText="Remarks"  > 
 <table style="width: 92%">
                    <tr>
                        <td style="height:36px;font-weight:normal">
                             <asp:Label ID="Labelrem" runat="server" Text="Remarks"  ></asp:Label>
                        </td>
                        <td style="height: 36px">
                     <asp:TextBox ID="TextBoxRemarks" runat="server" TextMode="MultiLine" Style="border-style:inset none none inset;" Enabled="false"
        Width="900px" ></asp:TextBox>   
        
                        </td>
                      
                     

               
                           </tr>
                           </table>
</asp:Panel>

 


<br />
 <asp:Label ID="lblmsg" runat="server" Visible="true" Text="Note: Any changes to the Publication data will not be saved once it has been submitted/approved"></asp:Label>
 
    <br />

   <%-- <asp:Panel ID="PanelFELFEEDBACK" runat="server" Visible="false" GroupingText="FeedBack Section" ForeColor="Black"  > 
        <table>
             <tr><td>
                                        <asp:Label ID="LabelFeedback" runat="server" Text="FeedBack " ForeColor="Black" ></asp:Label>
                                         
                                <td>
                                     <asp:ImageButton ID="ImageButtonFeedback" runat="server" ImageUrl="~/Images/srchImg.gif" OnClick="feedbackbutton_Click" OnClientClick="setRow2(this)" /></td>
                                </td>
                          </tr>
        </table>
  </asp:Panel>--%>

    <br />
<table width="100%">
    <tr>
        <td align="center">
          
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="BtnSave_Click" Enabled="false" OnClientClick="ConfirmButtonJournal();" CausesValidation="true" ></asp:Button>
         <%--  <asp:Button ID="ButtonSub" runat="server" Text="Submit for Approval" Enabled="false" OnClick="BtnSubmit_Click" OnClientClick="ConfirmButtonStudentPub()" CausesValidation="true" Visible="false" ></asp:Button>--%>
             <asp:Button ID="BtnFeedback" runat="server" Text="Submit for Approval" OnClick="feedbackbutton_Click" Visible="false" OnClientClick="ConfirmButtonStudentPub()" CausesValidation="true"></asp:Button>

    
 </td>
 </tr>
</table>
<%--<table width="100%">
    <tr>
        <td align="center">
<asp:Label ID="lblmsgpubnonondexed" runat="server" Visible="false" ForeColor="Red" Text="Note: The papers to be considered for incentives should be published in the journals indexed in Scopus and/or Web of Science"></asp:Label>

</td>
</tr>
</table>--%>

</asp:Panel>

<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtboxTitleOfWrkItem" Display="None" 
 ErrorMessage="Please Enter Title Of Work Item"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="TextBoxURL" Display="None" 
 ErrorMessage="Please Enter a Official URL"  ValidationGroup="validation1"></asp:RequiredFieldValidator>--%>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxKeywords" Display="None" 
 ErrorMessage="Please Enter a Keyword"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxAbstract" Display="None" 
 ErrorMessage="Please Enter a Abstract"  ValidationGroup="validation1"></asp:RequiredFieldValidator>


   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxPubJournal" Display="None"  Enabled="false"
 ErrorMessage="Please enter the ISSN"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBoxVolume" Display="None" Enabled="false"
 ErrorMessage="Please enter the Volume"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
  <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBoxIssue" Display="None" Enabled="false"
 ErrorMessage="Please enter the Issue"  ValidationGroup="validation1"></asp:RequiredFieldValidator>--%>

  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBoxTitleBook" Display="None"  Enabled="false"
 ErrorMessage="Please enter title of the book"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBoxChapterContributed" Display="None" Enabled="false"
 ErrorMessage="Please enter title of the chapter"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TextBoxEdition" Display="None" Enabled="false"
 ErrorMessage="Please enter the edition"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TextBoxPublisher" Display="None" Enabled="false"
 ErrorMessage="Please enter the publisher"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TextBoxPageNum" Display="None" Enabled="false"
 ErrorMessage="Please enter the Page num"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="TextBoxVolume1" Display="None" Enabled="false"
 ErrorMessage="Please enter the volume"  ValidationGroup="validation1"></asp:RequiredFieldValidator>


  <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="TextBoxDate" Display="None" Enabled="false"
 ErrorMessage="Please enter the Conference Date"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="TextBoxDate1" Display="None" Enabled="false"
 ErrorMessage="Please enter the Conference To Date"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="TextBoxEventTitle" Display="None" Enabled="false"
 ErrorMessage="Please enter the Conference Title"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="TextBoxPlace" Display="None" Enabled="false"
 ErrorMessage="Please enter the Conference Venue"  ValidationGroup="validation1"></asp:RequiredFieldValidator>


 <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="TextBoxDateOfNewsPublish" Display="None" Enabled="false"
 ErrorMessage="Please enter the Date of Publish"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="TextBoxNewsPublish" Display="None" Enabled="false"
 ErrorMessage="Please enter the Publisher"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxPageTo" Display="None" Enabled="false"
 ErrorMessage="Please enter Page to"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
<%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="CheckboxIndexAgency" Display="None" Enabled="false"
 ErrorMessage="Please select the index agency"  ValidationGroup="validation1"></asp:RequiredFieldValidator>--%>

 <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select the index agency" Display="None" Enabled="false" ValidationGroup="validation1"
    ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                                    ControlToValidate="DropDownListMuCategory" Display="None" 
                                    ErrorMessage="Please select MU Category" InitialValue=" " 
                                    ValidationGroup="validation2"></asp:RequiredFieldValidator>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" 
                                    ControlToValidate="DropDownListMuCategory" Display="None" 
                                    ErrorMessage="Please select MU Category" InitialValue=" " 
                                    ValidationGroup="validation1"></asp:RequiredFieldValidator>
<asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />
    <asp:HiddenField ID="rowVal1" runat="server" />
    <asp:HiddenField ID="senderID1" runat="server" />
     <asp:HiddenField ID="senderID2" runat="server" />
     <asp:HiddenField ID="senderID3" runat="server" />
      <asp:HiddenField ID="rowVal2" runat="server" />
       <asp:HiddenField ID="rowVal3" runat="server" />

      <asp:HiddenField ID="senderID7" runat="server" />
      <asp:HiddenField ID="rowVal7" runat="server" />

       <asp:HiddenField ID="StudentPub" runat="server" />
     <asp:HiddenField ID="confirm_value12" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenAddConfirm" runat="server" ClientIDMode="Static" />

  <asp:HiddenField ID="HiddenEntryConfirm" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HiddenEntryConfirm1" runat="server" />
<asp:HiddenField ID="HiddenSaveConfirm" runat="server" ClientIDMode="Static" />

<asp:HiddenField ID="HiddenSubmitConfirm" runat="server" ClientIDMode="Static" />
     <asp:HiddenField ID="hdnPredatoryJournal" runat="server" ClientIDMode="Static"  />
       </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
               <%-- <asp:PostBackTrigger ControlID="ButtonSub" />--%>
             <asp:PostBackTrigger ControlID="Buttonupload" />
             <asp:PostBackTrigger ControlID="GridViewProject" />
        </Triggers>

                                </asp:UpdatePanel>
    <div id="commentpopup5" class="overlay">
                <div class="popupp" style="width: 900px; height: 575px;margin-top: 20px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: 570px; width: 900px;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
<asp:Panel ID="popupstudent"   runat="server" >

<center>
<table align="center">
<tr>
<th > Student Details </th>
</tr>
</table>
<br />
<table>
<tr>
<td>Name:</td><td> <asp:TextBox ID="txtSrchStudentName" runat="server"   ></asp:TextBox></td> 
<td></td>
<td>Roll No:</td><td> <asp:TextBox ID="txtSrchStudentRollNo" runat="server"></asp:TextBox></td> 
<td>Institution:</td><td><asp:DropDownList ID="StudentIntddl" DataSourceID="sqlstudentds" DataTextField="InstName" DataValueField="InstID" runat="server" AppendDataBoundItems="true">
<asp:ListItem Value="">--Select--</asp:ListItem>
</asp:DropDownList></td>
</tr>
</table>
<br />
<table align="center">
<tr>
<td><asp:Button ID="Button1" runat="server" Text="Search" OnClick="SearchStudentData" /></td>
<td><asp:Button ID="Button2" runat="server" Text="EXIT" OnClick="exit" /> </td>
</tr>
</table>
<br />
<table>
<tr>
<td>
<asp:GridView ID="popupStudentGrid" runat="server"  AutoGenerateSelectButton="true" EmptyDataText="No Records Found" GridLines="Both"
OnSelectedIndexChanged="StudentDataSelect" AllowSorting="true"  AutoGenerateColumns="false" CellPadding="5"  CellSpacing="5">
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510"  />
        <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
         <Columns>
         <asp:BoundField DataField="RollNo" HeaderText="Roll No" ReadOnly="True" 
            SortExpression="RollNo"  ItemStyle-Width="100px"/>
        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" 
            SortExpression="Name"  ItemStyle-Width="500px" />
            <asp:BoundField DataField="InstName" HeaderText="Institution" ReadOnly="True" 
            SortExpression="InstName"  ItemStyle-Width="500px" />
             <asp:BoundField DataField="ClassName" HeaderText="Class" ReadOnly="True" 
            SortExpression="ClassName"  ItemStyle-Width="500px" />
             <asp:BoundField DataField="EmailID1" HeaderText="Email" ReadOnly="True" 
            SortExpression="EmailID1"  ItemStyle-Width="500px" />
          
           
            <asp:TemplateField ShowHeader="false" >
            <ItemTemplate>
            <asp:HiddenField ID="lblClassCode" runat="server" Value='<%# Eval("ClassCode") %>'></asp:HiddenField>
            </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField ShowHeader="false">
            <ItemTemplate>
             <asp:HiddenField ID="lblInstn" runat="server" Value='<%# Eval("InstID") %>'></asp:HiddenField>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
</asp:GridView>
<asp:SqlDataSource ID="StudentSQLDS" runat="server" ConnectionString="<%$ ConnectionStrings:SISConStr %>" 
SelectCommand="Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 
,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where 
SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID
and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID" ProviderName="System.Data.SqlClient">
<SelectParameters>
<asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
</SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlstudentds" runat="server" ConnectionString="<%$ ConnectionStrings:SISConStr %>" 
SelectCommand="Select InstName,InstID from SISInstitution" ProviderName="System.Data.SqlClient">
</asp:SqlDataSource>
</td>
</tr>
</table>
</center>
</asp:Panel>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <center><a class="close1" id="a3" href="#">Close</a></center>

                </div>

            </div>
            <br />
           


 <div id="commentpopup" class="overlay">
                <div class="popupp" style="width: 700px; height:500px;margin-top: 20px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: auto; width: 700px;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
<asp:Panel ID="popupPanelAffil" runat="server" >
    <br />
    <center>
<table style="background:white">
<tr>
<th align="center" colspan="3"> Author </th>
</tr>
    <tr><td>&nbsp;</td></tr>
<tr>
<td>Search By Name: <asp:TextBox ID="affiliateSrch" runat="server"  ></asp:TextBox>

 Search By Id: <asp:TextBox ID="SrchId" runat="server"   ></asp:TextBox>
  <asp:Button ID="BtnEmployeeSearch" runat="server" Text="Search" OnClick="branchNameChanged" />

<asp:Button ID="Buttonexit4" runat="server" Text="EXIT" OnClick="exit" />

  </td> 
  
  </tr>
    <tr><td>&nbsp;</td></tr>
<tr>
<td colspan="3">
<asp:GridView ID="popGridAffil" runat="server"  AutoGenerateSelectButton="true" EmptyDataText="No User Found" GridLines="Both"
OnSelectedIndexChanged="popSelected1" AllowSorting="true"  AutoGenerateColumns="false" Height="215px" CellPadding="5"  CellSpacing="5">
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510"  />
        <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
         <Columns>
         <asp:BoundField DataField="User_Id" HeaderText="User Id" ReadOnly="True" 
            SortExpression="User_Id"  ItemStyle-Width="200px"/>
        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" 
            SortExpression="Name"  ItemStyle-Width="500px" />
        </Columns>
</asp:GridView>



<asp:SqlDataSource ID="SqlDataSourceAffil" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
SelectCommand="SELECT User_Id,prefix+' '+firstname+' '+middlename+' '+lastname  as Name,1 flag from User_M where User_Id=@UserId  UNION SELECT top 10  User_Id, prefix+' '+firstname+' '+middlename+' '+lastname  as Name, 2 flag from User_M  where Active='Y' order by flag" ProviderName="System.Data.SqlClient">
<SelectParameters>
<asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
</SelectParameters>
</asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
SelectCommand="" ProviderName="System.Data.SqlClient">
</asp:SqlDataSource>
</td>
</tr>
</table>
        </center>
    <br />
</asp:Panel>
 </ContentTemplate>

                        </asp:UpdatePanel>

                    </div>
                    <center><a class="close1" id="anch" href="#">Close</a></center>

                </div>

            </div>
            <br />

    <div id="commentpopup2" class="overlay">
                <div class="popupp" style="width: 700px; height:500px;margin-top: 20px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: auto; width: 700px;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
            <asp:Panel ID="popupPanelJournal" runat="server">
<br />
       <center>
<table style="background:white">
<tr>
<th align="center" colspan="3"> Search Journal</th>
</tr>
<tr>
<td align="center">
<b>ISSN: </b>
<asp:TextBox ID="journalcodeISSNSrch" runat="server" ></asp:TextBox> 
<b>Journal Name: </b>
<asp:TextBox ID="journalcodeSrch" runat="server" ></asp:TextBox> 


<asp:Button ID="btnPopSearch" runat="server" Text="Search" OnClick="JournalCodePopChanged"  />
<asp:Button ID="Buttonexit" runat="server" Text="EXIT" OnClick="exit1" />
</td>
</tr>
   <tr><td>&nbsp;</td></tr>  
<tr>
<td colspan="3">
<asp:GridView ID="popGridJournal" runat="server"  AutoGenerateSelectButton="true" CellPadding="5"  CellSpacing="5" GridLines="Both"
OnSelectedIndexChanged="popSelected" AllowSorting="true" EmptyDataText="No Records Found" Height="236px"  >
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510"  />
        <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
</asp:GridView>


<asp:SqlDataSource ID="SqlDataSourceJournal" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="SELECT top 10 j.Id,j.Title,j.AbbreviatedTitle FROM [Journal_M] j,[Journal_Year_Map] m where j.Id=m.Id and m.Year=@Year" SelectCommandType="Text">
     <SelectParameters>
  <asp:ControlParameter Name="Year"  ControlID="TextBoxYearJA" PropertyName="SelectedValue"/>
  </SelectParameters>
                </asp:SqlDataSource>

</td>
</tr>
</table>
           </center>
<br /><br />
</asp:Panel>
</ContentTemplate>

                        </asp:UpdatePanel>

                    </div>
                    <center><a class="close1" id="a1" href="#">Close</a></center>

                </div>

            </div>
            <br />

                                <div id="commentpopup3" class="overlay">
                <div class="popupp" style="width: 700px; height:500px;margin-top: 20px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: auto; width: 700px;">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

         <asp:Panel ID="popupPanelProceedingsJournal" runat="server">
<br />
       <center>
<table style="background:white">
<tr>
<th align="center" colspan="3"> Search Journal</th>
</tr>
<tr>
<td align="center">
<b>ISSN: </b>
<asp:TextBox ID="journalcodeISSNSrchproceeding" runat="server" ></asp:TextBox> 
<b>Journal Name: </b>
<asp:TextBox ID="journalcodeSrchproceeding" runat="server" ></asp:TextBox> 


<asp:Button ID="Button3" runat="server" Text="Search" OnClick="ProceedingCodePopChanged"  />
<asp:Button ID="Button4" runat="server" Text="EXIT" OnClick="exit2" />
</td>
</tr>
   <tr><td>&nbsp;</td></tr>  
<tr>
<td colspan="3">
<asp:GridView ID="popGridJournalProceedings" runat="server"  AutoGenerateSelectButton="true" CellPadding="5"  CellSpacing="5" GridLines="Both"
OnSelectedIndexChanged="popSelectedProceeding" AllowSorting="true" EmptyDataText="No Records Found" Height="236px"  >
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510"  />
        <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
</asp:GridView>


<asp:SqlDataSource ID="SqlDataSourceProceedings" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="SELECT top 10 j.ID,j.Title,j.AbbreviatedTitle FROM [Proceedings_M] j,[Proceedings_Year_Map] m where j.ID=m.ISSN and m.Year=@Year" SelectCommandType="Text">
     <SelectParameters>
  <asp:ControlParameter Name="Year"  ControlID="TextBoxYearJA" PropertyName="SelectedValue"/>
  </SelectParameters>
                </asp:SqlDataSource>

</td>
</tr>
</table>
           </center>
<br /><br />
</asp:Panel>
                                </ContentTemplate>

                        </asp:UpdatePanel>

                    </div>
                    <center><a class="close1" id="a2" href="#">Close</a></center>

                </div>

            </div>
            <br />

<div id="commentpopup4" class="overlay">
                <div class="popupp" style="width: 700px; height:500px;margin-top: 20px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: auto; width: 700px;">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
         <asp:Panel ID="popupPanelProject" runat="server"  >
       
<table style="background:white">
<tr>
<th align="center" colspan="3"> Project Details</th>
</tr>

   <tr><td> <center> 
       <asp:Button ID="Button5" runat="server" Text="Select" OnClick="Button7_Click" />
           <asp:Button ID="Button8" runat="server" Text="EXIT" OnClick="exit1" /></td>
     
   </tr>  

<tr>
<td colspan="3">
    <asp:GridView ID="GridViewProject" runat="server" AllowSorting="true" AutoGenerateColumns="false" EmptyDataText="No Data found"
        OnRowDataBound="GridViewProject_RowDataBound" DataSourceID="SqlDataSourceProject" >
        <%--<asp:GridView ID="GridView1" runat="server" AllowSorting="true" AutoGenerateColumns="false"
         AllowPaging="True" 
        PagerSettings-PageButtonCount="5" PageSize="5" DataSourceID="SqlDataSourceProject"  EmptyDataText="No Data found"
        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" OnPageIndexChanging="GridViewProject_PageIndexChanging" 
        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3"  OnRowDataBound="GridViewProject_RowDataBound"
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" 
        BorderColor="#FF6600" BorderStyle="Solid">--%>
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="csSelect" runat="server" Checked="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Project ID">
                                    <ItemTemplate>
                                         <%--<asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                                     <%--    <asp:LinkButton ID="lnkPubId" runat="server" OnClick="Redirect" Text='<%# Eval("ID ") %>'></asp:LinkButton>--%>
                                          <%-- <asp:Button ID="lnkPubId" runat="server" OnClick="Redirect" Text='<%# Eval("ID ") %>' />--%>
                                        <asp:Label ID="TextBoxProjectId" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                    <%-- </ContentTemplate>--%>
                                  <%-- <Triggers> 
                                      <%-- <asp:PostBackTrigger ControlID="lnkPubId" />--%>
                                     <%--  <asp:AsyncPostBackTrigger ControlID="lnkPubId" EventName="OnClick" />--%>
                                   <%--</Triggers>
                                    </asp:UpdatePanel>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="ProjectUnit">
                                    <ItemTemplate>                                       
                                        <asp:Label ID="TextBoxProjectUnit" runat="server" Text='<%# Eval("ProjectUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>                                       
                                        <asp:Label ID="TextBoxProjectTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FundingAgency">
                                    <ItemTemplate>                                       
                                        <asp:Label ID="TextBoxFundingAgency" runat="server" Text='<%# Eval("FundingAgency") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="InvestigatorName">
                                    <ItemTemplate>                                       
                                        <asp:Label ID="TextBoxInvestigatorName" runat="server" Text='<%# Eval("InvestigatorName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                            </Columns>
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="#0B532D" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                        </asp:GridView>
<asp:SqlDataSource ID="SqlDataSourceProject" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="ProjectreferenceProjectList" SelectCommandType="StoredProcedure">  
    <SelectParameters>
<asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
</SelectParameters>
                </asp:SqlDataSource>

</td>
</tr>
</table>
              <asp:Button ID="Button7" runat="server" Text="Select" OnClick="Button7_Click" />
           <asp:Button ID="Button6" runat="server" Text="EXIT" OnClick="exit1" />            
           </center>
</asp:Panel>
</ContentTemplate>
                           <%-- <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="GridViewProject" />
                                <%-- <asp:as ControlID="GridViewProject" />
                            </Triggers>--%>

                        </asp:UpdatePanel>

                    </div>
                    <center><a class="close1" id="a4" href="#">Close</a></center>

                </div>

            </div>
            <br />

     <div id="commentpopup7"  class="overlay">
                <div class="popupp" style="width: 700px;background-color:#CCC; height:500px;margin-top: 20px;">

                   <%-- <a class="close" href="#">&times;</a>--%>
<%--                    <div class="content" style="overflow-y: scroll; height: auto; width: 700px;">--%>
                     <div class="content" style="height: auto; width: 700px;">

                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
         <asp:Panel ID="panelfeedback" Visible="false" runat="server"  >
       




    <table style="width: 80%; border-color:black">

        <tr>
<th align="center" colspan="3" style="font-size:19px; color:green">Please Enter Your Feedback</th>
</tr>
                    <tr>
                        <td style="font-weight:normal;color:Black" class="auto-style22">
                             <asp:Label ID="qustar" runat="server" ForeColor="Black" Text="*"></asp:Label>
                          <asp:Label ID="Q1" runat="server" Text="Please Rate your Ease of  Publication Entry" ForeColor="Black"></asp:Label>
                             
                               </td>
                          </tr>
                            <tr>
                                         <td >
                                             <asp:TextBox ID="txtq1" runat="server"
                                         Style="border-style:inset none none inset;" Width="100%"></asp:TextBox>
                             </td>
                        </tr>
                     
                        <tr>
                        <td style="font-weight:normal" class="auto-style22">
                                                         <asp:Label ID="qustar2" runat="server" ForeColor="Black" Text="*"></asp:Label>

                          <asp:Label ID="lblq2" runat="server" Text="Please indicate the ease of navigation to required Journal" ForeColor="Black"></asp:Label>
                             
                               </td>
                          </tr>
                            <tr>
                                         <td >
                                             <asp:TextBox ID="txtq2" runat="server"
                                         Style="border-style:inset none none inset;" Width="100%"></asp:TextBox>
                             </td>
                        </tr>




                   <tr>
                        <td style="font-weight:normal" class="auto-style22">
                                                                                     <asp:Label ID="qustar3" runat="server" ForeColor="Black" Text="*"></asp:Label>

                          <asp:Label ID="lblq3" runat="server" Text="How do you rate the overall experience" ForeColor="Black"></asp:Label>
                             
                               </td>
                          </tr>
                            <tr>
                                         <td >
                                         <asp:RadioButtonList ID="txtq3" runat="server"    RepeatDirection="Horizontal"   Style="border-style:inset none none inset;">
                                        <asp:ListItem Value="Excellent" >Excellent</asp:ListItem>
                                       <asp:ListItem Value="Good">Good</asp:ListItem>
                                       <asp:ListItem Value="Improve">Can Improve</asp:ListItem>

 
                             </asp:RadioButtonList>     
                             </td>
                        </tr>




                           <tr>
                        <td style="font-weight:normal" class="auto-style22">
                                                                                     <asp:Label ID="qustar4" runat="server" ForeColor="Black" Text="*"></asp:Label>

                          <asp:Label ID="lblq4" runat="server" Text="Please provide your comment on overall area to improve" ForeColor="Black"></asp:Label>
                             
                               </td>
                          </tr>
                            <tr>
                                         <td >
                                             <asp:TextBox ID="txtq4" runat="server"
                                         Style="border-style:inset none none inset;" Width="100%" TextMode="MultiLine"></asp:TextBox>
                             </td>
                        </tr>




                        <%-- <tr>
                        <td style="font-weight:normal" class="auto-style22">
                          <asp:Label ID="lblq5" runat="server" Text="What is an institutional strategy for research data management" ForeColor="Black"></asp:Label>
                             
                               </td>
                          </tr>
                            <tr>
                                         <td >
                                             <asp:TextBox ID="txtq5" runat="server"
                                         Style="border-style:inset none none inset;" Width="100%"></asp:TextBox>
                             </td>
                        </tr>




                        <tr>
                        <td style="font-weight:normal" class="auto-style22">
                          <asp:Label ID="lblq6" runat="server" Text="Why are the agencies developing a data management policy" ForeColor="Black"></asp:Label>
                             
                               </td>
                          </tr>
                            <tr>
                                         <td >
                                             <asp:TextBox ID="txtq6" runat="server"
                                         Style="border-style:inset none none inset;" Width="100%"></asp:TextBox>
                             </td>
                        </tr>




                                  <tr>
                        <td style="font-weight:normal" class="auto-style22">
                          <asp:Label ID="lblq7" runat="server" Text="How are research materials related to research data" ForeColor="Black"></asp:Label>
                             
                               </td>
                          </tr>
                            <tr>
                                         <td >
                                             <asp:TextBox ID="txtq7" runat="server"
                                         Style="border-style:inset none none inset;" Width="100%"></asp:TextBox>
                             </td>
                        </tr>

--%>

    </table>

<br />

             
            <asp:Button ID="BtnSubmitFeedback" runat="server"  Text="Submit" OnClick="BtnSubmitFeedback_Click" />
           <asp:Button ID="btnfedbackexit" runat="server" Text="EXIT" OnClick="exitfeedback" />    
                   
</asp:Panel>


                                  <asp:Panel ID="panel3" Visible="false" runat="server"  >
                                      <table>
                                          <tr>
                                              <td>
                                                  <asp:Label ID="lablfeedback" runat="server" Text="Feedback Details Already Entered."></asp:Label>
                                              </td>
                                          </tr>
                                      </table>
                                      </asp:Panel>
</ContentTemplate>
                           <%-- <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="GridViewProject" />
                                <%-- <asp:as ControlID="GridViewProject" />
                            </Triggers>--%>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="BtnSubmitFeedback" />
                                 <asp:PostBackTrigger ControlID="btnfedbackexit" />

                            </Triggers>
                        </asp:UpdatePanel>
                         

                    </div>
                 <%--   <center><a class="close1" id="a5" href="#">Close</a></center>--%>

                </div>

            </div>


            <br />

</asp:Content>