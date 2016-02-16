<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="frmMatchResult.ascx.cs" Inherits="DotNetNuke.Modules.ThSport.MatchResult" %>

<script type="text/javascript">

    function OpenPopupA(cardType, id) {
        document.getElementById("<%= hdnCardName.ClientID %>").value = cardType;
      
        var tmp = '#dialogA' + id;
        //alert(tmp);
        $(tmp).show();
        //$("#dialog").dialog();
    }

    function OpenPopupB(cardType, id) {
        //alert(cardType);
        //alert(id);
        document.getElementById("<%= hdnCardName.ClientID %>").value = cardType;
        var tmp = '#dialogB' + id;
        $(tmp).show();
        //$("#dialog").dialog();
    }

    function HidePopUpA(id) {
        var tmp = '#dialogA' + id;
        $(tmp).hide();
        return false;
    }

    function HidePopUpB(id) {
        var tmp = '#dialogB' + id;
        $(tmp).hide();
        falreturnse;
    }

    function ClosePopUp(obj) {
        $('#dialog').dialog('close');
        // console.log($(obj).siblings('#lblPlayerID').val());
        return true;
    }

    function validateAndConfirm(btn_clientid) {
        //var validated = Page_ClientValidate('Sports');
        //if (validated) {

        if (btn_clientid == "btnMatchResultUpdateData") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Update Match Result Details ?";
        }

        if (btn_clientid == "btnMatchResultClose") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Close Match Result Form ?";
        }

        if (btn_clientid == "btnReset") {
            document.getElementById("msgConfirm").innerHTML = "Are You Sure, You Want to Rest Match Details ?";
        }

        $("#dialogBox").dialog({

            modal: true,
            resizable: true,
            draggable: true,
            closeOnEscape: true,
            position: ['center', 80],
            dialogClass: "dnnFormPopup",

            buttons: {
                Ok: function () {

                    if (btn_clientid == "btnMatchResultSave") {
                        <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnMatchResultSave))%>;
                        }

                        if (btn_clientid == "btnMatchResultUpdateData") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnMatchResultUpdateData))%>;
                        }

                        if (btn_clientid == "btnMatchResultClose") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnMatchResultClose))%>;
                        }

                        if (btn_clientid == "btnReset") {
                            <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnReset))%>;
                        }

                    },
                    Cancel: function () {
                        $(this).dialog('close');
                        return false;
                    }
                }

            });

        //}
            return false;
        }
  </script>

<script type="text/javascript">

    function ConfirmationBox() {
        var result = confirm('Are you sure you want to Substitute this Player ?');
        if (result) {
            return true;
        }
        else {
            return false;
        }
    }

    function disablecheck(flg) {
        var teamBgoal = document.getElementById("<%= txtpanltypoint.ClientID %>").value;
        var teamAgoal = document.getElementById("<%= txtpanltypointA.ClientID %>").value;
        document.getElementById("<%= hdteamABpenalty.ClientID %>").value = teamAgoal;
        document.getElementById("<%= hdteamBpenalty.ClientID %>").value = teamBgoal;

        if (flg == 1) {
            var rbddiv = document.getElementById("<%= chkpanlty.ClientID %>");
            rbddiv.disabled = true;
           
            if (document.getElementById("<%= hdteamApenalty.ClientID %>").value == 0) {
                document.getElementById('tblpanlty').style.display = "none";
                document.getElementById('tblpanltyTeamA').style.display = "none";
            }
            if (document.getElementById("<%= hdteamBpenalty.ClientID %>").value == 0) {
                document.getElementById('tblpanltyTeamB').style.display = "none";
            }
        }
        else {
            
            var tmp = document.getElementById("<%= hdteama.ClientID %>").value;
            $("#txtTeamATotal").val(tmp);
            var tmp2 = document.getElementById("<%= hdteamb.ClientID %>").value;
            $("#txtTeamBTotal").val(tmp2);
            var rbd = document.getElementById("<%= rdbteamA.ClientID %>");
            var radio = rbd.getElementsByTagName("input");
            var rbdB = document.getElementById("<%= rdbteamb.ClientID %>");
            var radioB = rbdB.getElementsByTagName("input");
            if (parseInt($("#txtTeamATotal").val()) > parseInt($("#txtTeamBTotal").val())) {
                radio[0].checked = true;
                radioB[1].checked = false;
            }
            else if (parseInt($("#txtTeamBTotal").val()) > parseInt($("#txtTeamATotal").val())) {
                radioB[0].checked = true;
                radio[1].checked = false;
            }
            //penaltywin();
        }

        if (document.getElementById("<%= hdteamApenalty.ClientID %>").value == 0 || document.getElementById("<%= hdteamBpenalty.ClientID %>").value == 0) {
            var rbddiv = document.getElementById("<%= chkpanlty.ClientID %>");
            rbddiv.disabled = true;
            console
            document.getElementById('tblpanlty').style.display = "none";
            document.getElementById('tblpanltyTeamA').style.display = "none";
            document.getElementById('tblpanltyTeamB').style.display = "none";
        }
        if (document.getElementById("<%= HdnFroshowon.ClientID %>").value == 1) {
            var rbddiv = document.getElementById("<%= pnlforgoalshow.ClientID %>");
            rbddiv.style.display = "none";
            document.getElementById('noShowPenaltyPoint').style.display = "block";
        }
    }

    function hideshowpanle(flg) {
        document.getElementById('tblpanlty').style.display = "none";
        document.getElementById('tblpanltyTeamA').style.display = "none";
        document.getElementById('tblpanltyTeamB').style.display = "none";
    }

    function oncheckcheng() {
        var rbddiv = document.getElementById("<%= chkpanlty.ClientID %>");
        if (rbddiv.checked) {
            document.getElementById('tblpanlty').style.display = "table";
            document.getElementById('tblpanltyTeamA').style.display = "block";
            document.getElementById('tblpanltyTeamB').style.display = "block";
            document.getElementById("<%= hdteamApenalty.ClientID %>").value = 1;
            document.getElementById("<%= hdteamBpenalty.ClientID %>").value = 1;
        }
        else {
            document.getElementById('tblpanlty').style.display = "none";
            document.getElementById('tblpanltyTeamA').style.display = "none";
            document.getElementById('tblpanltyTeamB').style.display = "none";
            document.getElementById("<%= hdteamApenalty.ClientID %>").value = 0;
            document.getElementById("<%= hdteamBpenalty.ClientID %>").value = 0;
        }
    }

    function onShowcheckcheng() {
        var rbd = document.getElementById("<%= rdbteamA.ClientID %>");
        var radio = rbd.getElementsByTagName("input");
        var rbdB = document.getElementById("<%= rdbteamb.ClientID %>");
        var radioB = rbdB.getElementsByTagName("input");
        var rbddiv = document.getElementById("<%= checkNoshow.ClientID %>");
        var panshohide = document.getElementById("<%= pnlforgoalshow.ClientID %>").getElementsByTagName("input");
        var pan = panshohide;
        var panshohide1 = document.getElementById("<%= pnlforgoalshow.ClientID %>");

        if (rbddiv.checked) {
            radio[0].disabled = false;
            rbd.className = "disbledButton";
            rbdB.className = "disbledButton";
            radioB[0].disabled = false;
            radio[1].disabled = false;
            radioB[1].disabled = false;
            document.getElementById('noShowPenaltyPoint').style.display = "block";
            panshohide1.style.display = "none";
        }
        else {
            rbd.className = "";
            rbdB.className = "";
            document.getElementById("<%=  hftmp.ClientID %>").value = 0;

              radio[0].checked = false;
              radioB[0].checked = false;
              radio[1].checked = false;
              radioB[1].checked = false;

              radio[0].disabled = true;
              radioB[0].disabled = true;
              radio[1].disabled = true;
              radioB[1].disabled = true;

              panshohide1.style.display = "block";
              document.getElementById('noShowPenaltyPoint').style.display = "none";
          }
        //console.log(pan);
      }

      function onteamRadiobuttonclickA() {
          var rbd = document.getElementById("<%= rdbteamA.ClientID %>");
        var radio = rbd.getElementsByTagName("input");
        var rbdB = document.getElementById("<%= rdbteamb.ClientID %>");
        var radioB = rbdB.getElementsByTagName("input");

        var rbddiv = document.getElementById("<%= checkNoshow.ClientID %>");
        var hdnNoshowgoal = document.getElementById("<%= hdnNoShowGoal.ClientID %>").value;

        for (var x = 0; x < radio.length; x++) {
            if (radio[x].checked) {
                if (x == 0) {
                    radioB[1].checked = true;

                    if (rbddiv.checked) {
                        $("#txtTeamATotal").val(hdnNoshowgoal);
                        $("#txtTeamBTotal").val(0);
                        document.getElementById("<%= hdteama.ClientID %>").value = hdnNoshowgoal;
                		document.getElementById("<%= hdteamb.ClientID %>").value = 0;
                	}
                }
                else if (x == 1) {
                    radioB[0].checked = true;
                    if (rbddiv.checked) {
                        $("#txtTeamBTotal").val(hdnNoshowgoal);
                        $("#txtTeamATotal").val(0);
                        document.getElementById("<%= hdteamb.ClientID %>").value = hdnNoshowgoal;
                		document.getElementById("<%= hdteama.ClientID %>").value = 0;
                	}
                }

        }
    }
}

function onteamRadiobuttonclickB() {
    var rbd = document.getElementById("<%= rdbteamA.ClientID %>");
        var radio = rbd.getElementsByTagName("input");
        var rbdB = document.getElementById("<%= rdbteamb.ClientID %>");
        var radioB = rbdB.getElementsByTagName("input");

        var rbddiv = document.getElementById("<%= checkNoshow.ClientID %>");
        var hdnNoshowgoal = document.getElementById("<%= hdnNoShowGoal.ClientID %>").value;

        for (var x = 0; x < radioB.length; x++) {

            if (radioB[x].checked) {
                if (x == 0) {

                    radio[1].checked = true;

                    if (rbddiv.checked) {

                        $("#txtTeamATotal").val(0);

                        $("#txtTeamBTotal").val(hdnNoshowgoal);
                        document.getElementById("<%= hdteamb.ClientID %>").value = hdnNoshowgoal;
                		document.getElementById("<%= hdteama.ClientID %>").value = 0;
                	}


                } else if (x == 1) {
                    radio[0].checked = true;

                    if (rbddiv.checked) {

                        $("#txtTeamBTotal").val(0);

                        $("#txtTeamATotal").val(hdnNoshowgoal);
                        document.getElementById("<%= hdteama.ClientID %>").value = hdnNoshowgoal;
                		document.getElementById("<%= hdteamb.ClientID %>").value = 0;
                	}
                }

        }
    }
}

disableRepeaterRow = function (id) {

    if ($("." + id) != undefined) {
        //alert($("." + id).siblings('.overlap').html());
        $("." + id).prev('.overlap').css("height", $("." + id).height());
        $("." + id).prev('.overlap').css("margin-bottom", ("-") + ($("." + id).height()));
        $("." + id).prev('.overlap').css("backgound", "transparent !important");
    }
}

$(document).ready(function () {

    penaltywin = function () {
        var rbd = document.getElementById("<%= rdbteamA.ClientID %>");
            var radio = rbd.getElementsByTagName("input");
            var rbdB = document.getElementById("<%= rdbteamb.ClientID %>");
            var radioB = rbdB.getElementsByTagName("input");
            var teamBgoal = document.getElementById("<%= txtpanltypoint.ClientID %>").value;
            var teamAgoal = document.getElementById("<%= txtpanltypointA.ClientID %>").value;

            // var teamAgoal = flage.text;

            if (teamAgoal > teamBgoal) {
                radio[0].checked = true;
                radioB[1].checked = true;
            }
            else if (teamAgoal < teamBgoal) {
                radio[1].checked = true;
                radioB[0].checked = true;
            }
            else {
                radio[2].checked = true;
                radioB[2].checked = true;
                radio[2].disabled = false;
                radioB[2].disabled = false;
                radio[1].disabled = false;
                radio[0].disabled = false;
                radioB[1].disabled = false;
                radioB[0].disabled = false;
            }
            document.getElementById("<%= hdteamABpenalty.ClientID %>").value = teamAgoal;
            document.getElementById("<%= hdteamBpenalty.ClientID %>").value = teamBgoal;
    }


        //quantity
        $(".quantityUp").click(function () {
            $('.smallMessage').fadeOut();
            if (isNaN(parseInt($(this).siblings('.productQuantity').val()))) {
                $(this).siblings('.productQuantity').val(1);
            }
            else {
                var qty = parseInt($(this).siblings('.productQuantity').val()) + 1;
                $(this).siblings('.productQuantity').val(qty);
            }
            finalCalculationA();
        });

        $(".quantityDown").click(function () {
            $('.smallMessage').fadeOut();
            if (isNaN(parseInt($(this).siblings('.productQuantity').val()))) {
                $(this).siblings('.productQuantity').val(1);
            } else {
                var qty = parseInt($(this).siblings('.productQuantity').val()) - 1;
                if (qty <= 0) { qty = 0; }
                $(this).siblings('.productQuantity').val(qty);
            }
            finalCalculationA();
        });


        $('.productQuantity').change(function () {
            if (isNaN(parseInt($(this).val()))) {
                $(this).val(0);
            }
            finalCalculationA();
        });

        $('.productQuantityongoal').change(function () {
            event.preventDefault();
            if (isNaN(parseInt($(this).val()))) {
                $(this).val(0);
            }
            finalCalculationA();
        });

        calculateTeamASum = function () {
            var sum = 0;
            $('.productQuantity').each(function () {
                if (!(isNaN(parseInt($(this).val())))) {
                    sum += parseInt($(this).val());
                }
            });
            sum = sum + parseInt(document.getElementById("<%= hdntotalGoal.ClientID %>").value);
            document.getElementById("<%= hdnfinalA.ClientID %>").value = sum;
        }

        finalCalculationA = function () {

            calculateTeamASum();
            calculateTeamBSuongoal();
            //            alert("temaB"+(document.getElementById("<%= HdnfinalgoalB.ClientID %>").value));
            //              alert("temaA"+(document.getElementById("<%= HiddenField1.ClientID %>").value));
            var sum = parseInt(document.getElementById("<%= hdnfinalA.ClientID %>").value) + parseInt(document.getElementById("<%= HdnfinalAongol.ClientID %>").value);
            $("#txtTeamATotal").val(sum);
            document.getElementById("<%= hdteama.ClientID %>").value = sum;
            result();

        }

        result = function () {
            var teamAgoal = parseInt(document.getElementById("<%= txtTeamATotal.ClientID %>").value);
            var teamBgoal = parseInt(document.getElementById("<%= txtTeamBTotal.ClientID %>").value);
            var rbd = document.getElementById("<%= rdbteamA.ClientID %>");
            var radio = rbd.getElementsByTagName("input");
            var rbdB = document.getElementById("<%= rdbteamb.ClientID %>");
            var radioB = rbdB.getElementsByTagName("input");
            var rbddiv = document.getElementById("<%= chkpanlty.ClientID %>");
            var txtpanlA = document.getElementById("<%= txtpanltypointA.ClientID %>");
            var txtpanB = document.getElementById("<%= txtpanltypoint.ClientID %>");
            var divpenlty = rbddiv.getElementsByTagName("input");

            var TxtpanlA = document.getElementById("<%= txtpanltypointA.ClientID %>");
            var TxtpanlB = document.getElementById("<%= txtpanltypoint.ClientID %>");

            //console.log(rbd);
            //alert(teamAgoal);
            //alert(teamBgoal);
            //alert(radio[0].checked);

            if (teamAgoal > teamBgoal) {
                radio[0].checked = true;
                radioB[1].checked = true;
            }
            else if (teamAgoal < teamBgoal) {
                radio[1].checked = true;
                radioB[0].checked = true;
            }
            else {
                radio[2].checked = true;
                radioB[2].checked = true;
                radio[2].disabled = false;
                radioB[2].disabled = false;
                radio[1].disabled = false;
                radio[0].disabled = false;
                radioB[1].disabled = false;
                radioB[0].disabled = false;
            }

            if (radio[2].checked == true) {
                if ((TxtpanlA.value == "" || TxtpanlA.value == "0") && (TxtpanlB.value == "" || TxtpanlB.value == "0")) {
                    rbddiv.disabled = false;
                }
                else {
                    rbddiv.disabled = true;
                }
                TxtpanlB.disabled = false;
                TxtpanlA.disabled = false;
            }
            else {
                rbddiv.disabled = true;
                TxtpanlB.disabled = true;
                TxtpanlA.disabled = true;
            }
        }

        calculateTeamASuongoal = function () {
            var sum = 0;
            $('.productQuantityongoal').each(function () {
                if (!(isNaN(parseInt($(this).val())))) {
                    sum += parseInt($(this).val());
                }
            });
            sum = sum + parseInt(document.getElementById("<%= hdongolaA.ClientID %>").value);
            $("#txtTeamBTotal").val(sum);
            document.getElementById("<%= HiddenField1.ClientID %>").value = sum;
        }

        validate = function (evt) {
            var theEvent = evt || window.event;
            var key = theEvent.keyCode || theEvent.which;
            key = String.fromCharCode(key);
            var regex = /[0-9]|\./;
            if (!regex.test(key)) {
                theEvent.returnValue = false;
                if (theEvent.preventDefault) theEvent.preventDefault();
            }
        }

        //for assist

    
        //quantity
        $(".quantityUpassist").click(function () {
            $('.smallMessage').fadeOut();
            if (isNaN(parseInt($(this).siblings('.productQuantityassist').val()))) {
                $(this).siblings('.productQuantityassist').val(1);
            }
            else {
                var qty = parseInt($(this).siblings('.productQuantityassist').val()) + 1;
                $(this).siblings('.productQuantityassist').val(qty);
            }
        });

        $(".quantityDownassist").click(function () {
            $('.smallMessage').fadeOut();
            if (isNaN(parseInt($(this).siblings('.productQuantityassist').val()))) {
                $(this).siblings('.productQuantityassist').val(1);
            }
            else {
                var qty = parseInt($(this).siblings('.productQuantityassist').val()) - 1;
                if (qty <= 0) { qty = 0; }
                $(this).siblings('.productQuantityassist').val(qty);
            }
        });

        //for on goal

        //quantity
        $(".quantityUpOngoal").click(function () {
            $('.smallMessage').fadeOut();

            if (isNaN(parseInt($(this).siblings('.productQuantityongoal').val()))) {
                $(this).siblings('.productQuantityongoal').val(1);

            }
            else {
                var qty = parseInt($(this).siblings('.productQuantityongoal').val()) + 1;
                $(this).siblings('.productQuantityongoal').val(qty);
            }
            finalcaculationB();
        });

        $(".quantityDownongoal").click(function () {
            $('.smallMessage').fadeOut();
            if (isNaN(parseInt($(this).siblings('.productQuantityongoal').val()))) {
                $(this).siblings('.productQuantityongoal').val(1);
            }
            else {
                var qty = parseInt($(this).siblings('.productQuantityongoal').val()) - 1;
                if (qty <= 0) { qty = 0; }
                $(this).siblings('.productQuantityongoal').val(qty);
            }
            finalcaculationB();
        });
    });

    //team b

    $(document).ready(function () {

        //quantity
        $(".quantityUpB").click(function () {
            $('.smallMessage').fadeOut();

            if (isNaN(parseInt($(this).siblings('.productQuantityB').val()))) {
                $(this).siblings('.productQuantityB').val(1);
            }
            else {
                var qty = parseInt($(this).siblings('.productQuantityB').val()) + 1;
                $(this).siblings('.productQuantityB').val(qty);
            }
            finalcaculationB();
        });

        $(".quantityDownB").click(function () {
            $('.smallMessage').fadeOut();
            if (isNaN(parseInt($(this).siblings('.productQuantityB').val()))) {
                $(this).siblings('.productQuantityB').val(1);
            } else {
                var qty = parseInt($(this).siblings('.productQuantityB').val()) - 1;
                if (qty <= 0) { qty = 0; }
                $(this).siblings('.productQuantityB').val(qty);
            }
            finalcaculationB();
        });


        $('.productQuantityB').change(function () {

            if (isNaN(parseInt($(this).val()))) {
                $(this).val(0);
            }

            finalcaculationB();
        });
        $('.productQuantityGoalonB').change(function () {

            if (isNaN(parseInt($(this).val()))) {
                $(this).val(1);
            }

            finalcaculationB();
        });

        calculateTeamBSum = function () {
            var sum = 0;
            $('.productQuantityB').each(function () {
                if (!(isNaN(parseInt($(this).val())))) {
                    sum += parseInt($(this).val());
                }
            });
            sum = sum + parseInt(document.getElementById("<%= hdntotalGoalForB.ClientID %>").value);
            document.getElementById("<%= HdnfinalgoalB.ClientID %>").value = sum;
        }

        finalcaculationB = function () {
            calculateTeamBSum();
            calculateTeamASuongoal();
            var sum = parseInt(document.getElementById("<%= HdnfinalgoalB.ClientID %>").value) + parseInt(document.getElementById("<%= HiddenField1.ClientID %>").value);
            $("#txtTeamBTotal").val(sum);
            document.getElementById("<%= hdteamb.ClientID %>").value = sum;
            result();
        }

        var flag = 0;
        var i = 2;
        calculateTeamBSuongoal = function () {
            var sumofnogoal = 0;
            $('.productQuantityGoalonB').each(function () {
                if (!(isNaN(parseInt($(this).val())))) {
                    sumofnogoal += parseInt($(this).val());
                }
            });
            sumofnogoal = sumofnogoal + parseInt(document.getElementById("<%= hdnTptablongol.ClientID %>").value);
            document.getElementById("<%= HdnfinalAongol.ClientID %>").value = sumofnogoal;
        }
        //for assist

        //quantity
        $(".quantityUpassistB").click(function () {
            $('.smallMessage').fadeOut();
            if (isNaN(parseInt($(this).siblings('.productQuantityassistB').val()))) {
                $(this).siblings('.productQuantityassistB').val(1);
            }
            else {
                var qty = parseInt($(this).siblings('.productQuantityassistB').val()) + 1;
                $(this).siblings('.productQuantityassistB').val(qty);
            }
        });

        $(".quantityDownassistB").click(function () {
            $('.smallMessage').fadeOut();
            if (isNaN(parseInt($(this).siblings('.productQuantityassistB').val()))) {
                $(this).siblings('.productQuantityassistB').val(1);
            }
            else {
                var qty = parseInt($(this).siblings('.productQuantityassistB').val()) - 1;
                if (qty <= 0) { qty = 0; }
                $(this).siblings('.productQuantityassistB').val(qty);
            }
        });

        //On Goal

        $(".quantityUpGoalonB").click(function () {
            $('.smallMessage').fadeOut();
            if (isNaN(parseInt($(this).siblings('.productQuantityGoalonB').val()))) {
                $(this).siblings('.productQuantityGoalonB').val(1);
            }
            else {
                var qty = parseInt($(this).siblings('.productQuantityGoalonB').val()) + 1;
                $(this).siblings('.productQuantityGoalonB').val(qty);
            }
            finalCalculationA();
        });

        $(".quantityDownGoalonB").click(function () {
            $('.smallMessage').fadeOut();
            if (isNaN(parseInt($(this).siblings('.productQuantityGoalonB').val()))) {
                $(this).siblings('.productQuantityGoalonB').val(1);
            }
            else {
                var qty = parseInt($(this).siblings('.productQuantityGoalonB').val()) - 1;
                if (qty <= 0) { qty = 0; }
                $(this).siblings('.productQuantityGoalonB').val(qty);
            }
            finalCalculationA();
        });

        validate = function (evt) {
            var theEvent = evt || window.event;
            var key = theEvent.keyCode || theEvent.which;
            key = String.fromCharCode(key);
            var regex = /[0-9]|\./;
            if (!regex.test(key)) {
                theEvent.returnValue = false;
                if (theEvent.preventDefault) theEvent.preventDefault();
            }
        }

        function getQuerystring(key, default_) {
            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("([/])" + key + "/.*?(/|$)");
            var qs = regex.exec(window.location.href);
            //alert("key" + key + "\nqs = " + qs);
            var vals = qs[0].replace("/" + key + "/", "").replace("/", "");
            //alert("Val:" + vals);
            if (vals == null)
                return default_;
            else
                return vals;
        }

        //var pageURL = "//" + window.location.host + "/";
       // var sf = $.ServicesFramework($("#hdnModuleId").val());
        //var matchId = getQuerystring('MatchID');

       // var competitionId = parseInt(document.getElementById("<%= hdnCompetitionID.ClientID %>").value);

        // Get All Match List With Stage Wise

       // for (var i = 0; i <= 16; i++) {
        //    if (i == 0 || i == 2 || i == 4 || i == 8 || i == 16) {
        //        $.ajax(pageURL + "DesktopModules/SportSiteServer/API/SportsService/GetAllMatcheslist", {
        //            //data: {},
        //            data: 'competitionId=' + competitionId + '&matchType=' + i,
        //            type: "get", contentType: "application/json",
        //            beforeSend: sf.setModuleHeaders
        //        }).done(function (result) {
        //            if (result == 'null') {
        //                $.dnnAlert({
        //                    okText: 'OK',
        //                    text: 'No data fetched.'
        //                });
        //            }
        //            else {
        //                //console.log("Match List result: " + result);

        //            }
        //        }).fail(function (xhr, result, status) {
        //            $.dnnAlert({
        //                okText: 'OK',
        //                text: 'Error.'
        //            });
        //        });
        //    }
        //}


        //Get Data For Match Result


        //$.ajax(pageURL + "DesktopModules/SportSiteServer/API/SportsService/GetMatchResult", {
        //    //data: {},
        //    data: 'matchId=' + matchId,
        //    type: "get", contentType: "application/json",
        //    beforeSend: sf.setModuleHeaders
        //}).done(function (result) {
        //    if (result == 'null') {
        //        $.dnnAlert({
        //            okText: 'OK',
        //            text: 'No data fetched.'
        //        });
        //    }
        //    else {
        //        //console.log("All Result Data : " + result);
        //    }
        //}).fail(function (xhr, result, status) {
        //    $.dnnAlert({
        //        okText: 'OK',
        //        text: 'Error.'
        //    });
        //});


        //Get Data For Repeater A

        //alert("matchId : " + matchId);
        //$.ajax(pageURL + "DesktopModules/SportSiteServer/API/SportsService/GetTeamAPlayer", {
        //    //data: {},
        //    data: 'matchId=' + matchId,
        //    type: "get", contentType: "application/json",
        //    beforeSend: sf.setModuleHeaders
        //}).done(function (result) {
        //    if (result == 'null') {
        //        $.dnnAlert({
        //            okText: 'OK',
        //            text: 'No data fetched.'
        //        });
        //    }
        //    else {
        //        //console.log("RepeaterA : " + result);
        //    }
        //}).fail(function (xhr, result, status) {
        //    $.dnnAlert({
        //        okText: 'OK',
        //        text: 'Error.'
        //    });
        //});


        //Get Data For Repeater A1


        //$.ajax(pageURL + "DesktopModules/SportSiteServer/API/SportsService/GetTeamBPlayer", {
        //    //data: {},
        //    data: 'matchId=' + matchId,
        //    type: "get", contentType: "application/json",
        //    beforeSend: sf.setModuleHeaders
        //}).done(function (result) {
        //    if (result == 'null') {
        //        $.dnnAlert({
        //            okText: 'OK',
        //            text: 'No data fetched.'
        //        });
        //    }
        //    else {
        //        //console.log("RepeaterB : " + result);
        //    }
        //}).fail(function (xhr, result, status) {
        //    $.dnnAlert({
        //        okText: 'OK',
        //        text: 'Error.'
        //    });
        //});



        //Get Data For RepeaterB

        //alert("matchId : " + matchId);
        //$.ajax(pageURL + "DesktopModules/SportSiteServer/API/SportsService/GetTeamA1Player", {
        //    //data: {},
        //    data: 'matchId=' + matchId,
        //    type: "get", contentType: "application/json",
        //    beforeSend: sf.setModuleHeaders
        //}).done(function (result) {
        //    if (result == 'null') {
        //        $.dnnAlert({
        //            okText: 'OK',
        //            text: 'No data fetched.'
        //        });
        //    }
        //    else {
        //        //console.log("RepeaterA1 : " + result);
        //    }
        //}).fail(function (xhr, result, status) {
        //    $.dnnAlert({
        //        okText: 'OK',
        //        text: 'Error.'
        //    });
        //});



        //Get Data For Repeater B1

    //    $.ajax(pageURL + "DesktopModules/SportSiteServer/API/SportsService/GetTeamB1Player", {
    //        //data: {},
    //        data: 'matchId=' + matchId,
    //        type: "get", contentType: "application/json",
    //        beforeSend: sf.setModuleHeaders
    //    }).done(function (result) {
    //        if (result == 'null') {
    //            $.dnnAlert({
    //                okText: 'OK',
    //                text: 'No data fetched.'
    //            });
    //        }
    //        else {
    //            //console.log("RepeaterB1 : " + result);
    //        }
    //    }).fail(function (xhr, result, status) {
    //        $.dnnAlert({
    //            okText: 'OK',
    //            text: 'Error.'
    //        });
    //    });
    });

    $(document).ready(function () {

        var rbd = document.getElementById("<%= rdbteamA.ClientID %>");
                 var radio = rbd.getElementsByTagName("input");
                 var rbdB = document.getElementById("<%= rdbteamb.ClientID %>");
             	var radioB = rbdB.getElementsByTagName("input");

             	if (parseInt($("#txtTeamATotal").val()) > parseInt($("#txtTeamBTotal").val())) {
             	    radio[0].checked = true;
             	    radioB[1].checked = true;
             	}
             	else if (parseInt($("#txtTeamBTotal").val()) > parseInt($("#txtTeamATotal").val())) {
             	    radioB[0].checked = true;
             	    radio[1].checked = true;
             	}


                 /* No Show Penalty Section*/

             	if (document.getElementById("<%= HdnFroshowon.ClientID %>").value == 1) {
             	    var rbddiv = document.getElementById("<%= pnlforgoalshow.ClientID %>");
             	    rbddiv.style.display = "none";
             	    document.getElementById('noShowPenaltyPoint').style.display = "block";
             	}
             	else {
             	    document.getElementById('noShowPenaltyPoint').style.display = "none";
             	}

                 $(".pointUp").click(function () {
                     $('.smallMessage').fadeOut();

                     if (isNaN(parseInt($(this).siblings('.noShowPenaltyText').val()))) {
                         $(this).siblings('.productQuantity').val(1);
                     }
                     else {
                         var qty = parseInt($(this).siblings('.noShowPenaltyText').val()) + 1;
                         $(this).siblings('.noShowPenaltyText').val(qty);
                     }
                 });

                 $(".pointDown").click(function () {
                     $('.smallMessage').fadeOut();
                     if (isNaN(parseInt($(this).siblings('.noShowPenaltyText').val()))) {
                         $(this).siblings('.noShowPenaltyText').val(1);
                     }
                     else {
                         var qty = parseInt($(this).siblings('.noShowPenaltyText').val()) - 1;
                         if (qty <= 0) { qty = 0; }
                         $(this).siblings('.noShowPenaltyText').val(qty);
                     }

                 });
             });
</script>


<script type="text/javascript">
    function SaveSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { savevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function savevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divsavemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divsavemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<script type="text/javascript">
    function UpdateSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { updatevalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function updatevalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divupdatemassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divupdatemassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<script type="text/javascript">
    function ResetSuccessfully() {
        $(document).ready(function () {
            $.blockUI();
            setTimeout(function () {
                $.unblockUI({
                    onUnblock: function () { cancelvalidateAndConfirmClose(); }
                });
            }, 2000);
        });
    }
</script>

<script type="text/javascript">
    function cancelvalidateAndConfirmClose() {
        $(document).ready(function () {
            $("#divcancelmassage").dialog({
                modal: true,
                resizable: true,
                draggable: true,
                closeOnEscape: true,
                position: ['center', 80],
                dialogClass: "dnnFormPopup",
            });
        });
        setTimeout(function () {
            $("#divcancelmassage").delay(2000).fadeOut(0);
            $(".dnnFormPopup").delay(2000).fadeOut(0);
            $(".ui-widget-overlay").delay(2000).fadeOut(0);
            return false;
        }, 2000);
    }
</script>

<style type="text/css">
        .ui-dialog , .ui-dialog-buttonpane 
        {
            margin:0 !important;
            padding:0 !important;
        }

        .ui-widget-content 
        {
            overflow: hidden;
            display: table;
            position: relative;
            width: 100%;
            background-color: rgba(255,255,255,.98) !important;
            font-size: 16px;
            height:17% !important;
        }
</style>

<div id="divsavemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/SportSite/Images/icons/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label2" ClientIDMode="Static" runat="server" Text=" Match Result detail are save successfully. ">
     </asp:Label>
</div>

<div id="divupdatemassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
    <img src="<%= Page.ResolveUrl("~/DesktopModules/SportSite/Images/icons/Ok.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label4" ClientIDMode="Static" runat="server" Text=" Match Result are update successfully. ">
     </asp:Label>
</div>

<div id="divcancelmassage" runat="server" clientidmode="static" style="display: none;position:inherit !important;">
     <img src="<%= Page.ResolveUrl("~/DesktopModules/SportSite/Images/icons/Cancel.png")%>" />
     <asp:Label CssClass="lobibox-body-text" ID="Label5" ClientIDMode="Static" runat="server" Text=" Match Result are reset successfully. ">
     </asp:Label>
</div>

<div id="dialogBox" runat="server" clientidmode="static"  style="display:none;">
    <div class="lobibox-body-text-wrapper">
        <asp:Label CssClass="lobibox-body-text" ID="msgConfirm" ClientIDMode="Static" runat="server" Text="Are You Sure, You Want to Save Match Result Details ?"></asp:Label>
    </div>
</div>

<asp:HiddenField ID="hdnModuleId" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnMatchID" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnCompetitionID" ClientIDMode="Static" runat="server" />

<asp:HiddenField ID="hdnCardName" ClientIDMode="Static" runat="server" />

<div class="row-fluid">
	<div class="span12">


<asp:Panel ID="PnlGrid" runat="server" Visible="false">

    <table class="addbuttoncss">
        <tr>
            <td>
                <asp:Button ID="btnAddMatchResult" runat="server" Width="200px" Height="25px" Text="New Match Result" />
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    
        <asp:GridView ID="gvMatchResult" runat="server" CssClass="grid" AutoGenerateColumns="false"
                            ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="10" EmptyDataText="No Records Found"
                            EmptyDataRowStyle-ForeColor="Red">
            <Columns>

                <asp:TemplateField HeaderText="No." ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblMatchResultID" runat="server" Text='<%#Eval("MatchResultID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="No." ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblMatchID" runat="server" Text='<%#Eval("MatchID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Team A Name " ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfTeam_ID" runat="server" Value='<%#Eval("TeamID") %>'></asp:HiddenField>
                        <asp:Label ID="lblTeamAID" runat="server" Text='<%#Eval("TeamName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Goal" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblTeamATotal" runat="server" Text='<%#Eval("TeamATotal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Team B Name" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfTeam_ID1" runat="server" Value='<%#Eval("TeamID1") %>'></asp:HiddenField>
                        <asp:Label ID="lblTeamBID" runat="server" Text='<%#Eval("TeamName1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Goal" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblTeamBTotal" runat="server" Text='<%#Eval("TeamBTotal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Descr") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" HeaderText="Edit">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" /> <%-- OnClick="btnEdit_Click"--%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Delete" Visible="false">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="Button" Visible="false" />
                        <%--OnClientClick="return confirm('Are You Sure? Want To Delete.');"
                                        OnClick="btnDelete_Click" --%>
                        <asp:Label ID="lblComp_RegID" runat="server" Text='<%#Eval("MatchResultID") %>' Visible="false">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <PagerStyle CssClass="gridviewPagerStyle" HorizontalAlign="Center" />
        </asp:GridView>

        <input type="hidden" runat="server" id="hidRegID" />
    
</asp:Panel>

<asp:HiddenField ID="hdteama" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdteamb" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdteamApenalty" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdteamBpenalty" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdteamABpenalty" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdnNoShowGoal" runat="server"  ClientIDMode="Static" />
<asp:HiddenField ID="HdnfinalAongol" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdnfinalA" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="HdnFroshowon" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="HdnfinalgoalB" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdnTptablongol" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />

<div id="maildiv" runat="server" style="margin-top:20px;">
    <asp:Panel ID="mainContent" runat="server">
            
        <div class="portlet box blue tabbable">

			<div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480">Match Result</span>
				</div>
			</div>


<div class="portlet-body form">
<div class="tabbable portlet-tabs">

<div class="tab-content" style="margin-top:10px !important;">
<div class="tab-pane active" id="portlet_tab1">

<div class="form-horizontal" style="margin-top:20px;">
            
      <asp:HiddenField ID="hdnmatchresultid" runat="server" />
      <asp:HiddenField ID="hdongolaA" runat="server" />
      <asp:HiddenField ID="hdntotalGoal" runat="server" />
      <asp:HiddenField ID="hdntotalGoalForB" runat="server" />

    <center>
   
    <table id="Table1" runat="server" clientidmode="Static" style="margin-left:25%;width:100%">
        <tr>
            <td>
                 <div class="control-group">
      	               <label class="control-label" style="padding:0px;"> 
                            Toss Result:
                       </label>

                       <asp:RadioButtonList ID="rdbtoss" ClientIDMode="Static" runat="server" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="rdbteamA_OnSelectedIndexChanged" 
                                                    onclick="onteamRadiobuttonclickA();" style="text-align:center;width:32%">
                              <asp:ListItem Value="1"></asp:ListItem>
                              <asp:ListItem Value="2"></asp:ListItem>
                       </asp:RadioButtonList>
                 </div>
           </td>
        </tr>
   </table>
   
    </center>


     <div class="matchup matchResult_fieldset">
          <fieldset>
              <legend>
                       <asp:Label ID="lblTeamAName" runat="server"></asp:Label>
                        
            </legend>

              <asp:HiddenField runat="server" ID="hftmp" Value="1" />
                <asp:RadioButtonList ClientIDMode="Static" ID="rdbteamA" runat="server" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="rdbteamA_OnSelectedIndexChanged" onclick="onteamRadiobuttonclickA();" 
                    style="text-align:center;width:100%">
                    <asp:ListItem Value="1" class="t" >Won</asp:ListItem>
                    <asp:ListItem Value="2" class="t" >Lose</asp:ListItem>
                    <asp:ListItem Value="3" class="t" >Draw</asp:ListItem>
                </asp:RadioButtonList>

            <div class="matchResult_toss">
              <span>Total Goal :</span>
            <asp:TextBox ID="txtTeamATotal" ClientIDMode="Static" runat="server" Width="50px"
                Height="35px" Enabled="false" ForeColor="Black" CssClass="fontcentersize" BorderStyle="Solid"
                BorderWidth="1"> </asp:TextBox>
            </div>

            
            <div id="tblpanltyTeamA" runat="server" clientidmode="Static" class="matchResult_panelty">
                   Penalty Point:
                          
                    <asp:TextBox ID="txtpanltypointA" ClientIDMode="Static" runat="server" Width="50px"
                                       onchange='javascript:penaltywin(this);' Height="35px" ForeColor="Black" CssClass="fontcentersize"
                                       BorderStyle="Solid" BorderWidth="1">
                    </asp:TextBox>
            </div>

    </fieldset>
  </div>


<div class="left_div_css matchResult_noshow">
        <center>
     
        <div id="checkdivpenlty" runat="server" clientidmode="Static">
             <label class="checkbox">
                 <asp:CheckBox ID="chkpanlty" runat="server" ClientIDMode="Static" onclick="oncheckcheng();"/>
                 <asp:Label ID="Label3" AssociatedControlID="chkpanlty" runat="server" Text="Is Penalty" style="display:inline;">
                 </asp:Label>
             </label>
        </div>

    <div id="Div1" runat="server"  clientidmode="Static">
        <label class="checkbox">
              <asp:CheckBox ID="checkNoshow" runat="server" ClientIDMode="Static" onclick="onShowcheckcheng();" />
              <asp:Label ID="Label1" AssociatedControlID="checkNoshow" style="display:inline;" runat="server" Text="No Show">
              </asp:Label>
        </label>                
    </div>

    <div id="noShowPenaltyPoint" runat="server"  clientidmode="Static" style="width:65%;">
        Penalty :
        <div class="pointDown matchResult_plusminus">-</div>
                <asp:TextBox ID="txtNoShowPenalty" runat="server" Width="30px" 
                                   CssClass="fontcentersize quantityInput-text noShowPenaltyText"
                                   onkeypress='validate(event)' 
                                   Style="display: inline; float: left; line-height: 30px;height:30px !important;
                                   margin-right: 10px;">
                </asp:TextBox>
        <div class="pointUp" style="margin-top:4px;">+</div>
    </div>

  </center>
</div>

<div class="matchup matchResult_fieldset">
          <fieldset>
              <legend>
                  <asp:Label ID="lblTeamBName" runat="server"></asp:Label>
              </legend>

              
              <asp:RadioButtonList ID="rdbteamb" ClientIDMode="Static" runat="server" RepeatDirection="Horizontal" 
                  OnSelectedIndexChanged="rdbteamB_OnSelectedIndexChanged" 
                onclick="onteamRadiobuttonclickB();" style="text-align:center;width:100%">
                <asp:ListItem Value="1">Won</asp:ListItem>
                <asp:ListItem Value="2">Lose</asp:ListItem>
                <asp:ListItem Value="3">Draw</asp:ListItem>
                </asp:RadioButtonList>
            
               <div class="matchResult_toss">
                   <span>Total Goal :</span>
              <asp:TextBox ID="txtTeamBTotal" ClientIDMode="Static" runat="server" Width="50px"
                        Height="35px" Enabled="false" ForeColor="Black" CssClass="fontcentersize" BorderStyle="Solid"
                        BorderWidth="1"></asp:TextBox>
                    
                   </div>
                    <div id="tblpanltyTeamB" runat="server" clientidmode="Static" class="matchResult_panelty">
                    Penalty Point:
                          
                    <asp:TextBox ID="txtpanltypoint" ClientIDMode="Static" runat="server" Width="50px"
                        Height="35px" ForeColor="Black" CssClass="fontcentersize" BorderStyle="Solid"
                        onchange='javascript:penaltywin();' BorderWidth="1"></asp:TextBox>

                    </div>
                      
             

            </fieldset>
</div>

<table id="tblpanlty" runat="server" style="width: 100%;" clientidmode="Static">
                
</table>

                         
<asp:Panel ID="pnlforgoalshow" runat="server" ClientIDMode="Static">

        

<div style="float: left; width: 450px; border: 1px; margin: 10px;">
    <div style="border: 1px solid #35aa47; width: 470px;">

        <asp:Repeater ID="rptleftTeamA" runat="server"  OnItemCommand="rptleftTeamA_OnItemCommand"
            OnItemDataBound="rptleftTeamA_OnItemDataBound">
            <HeaderTemplate>
                <table style="width: 100%" cellpadding="0">
                                            
                    <tr style="background-color: #35aa47; color: White; height: 35px;">
                        <th style="width: 100px;">
                            Player List
                        </th>
                        <th style="width: 82px;">
                            Goal
                        </th>
                        <th style="width: 82px;">
                            Assist
                        </th>
                            <th style="width: 82px;">
                            Own Goal
                        </th>
                        <th style="width: 82px;">
                            Yellow
                        </th>
                        <th>
                            Red
                        </th>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate >
            <div class="overlap" style="width:100%;opacity:0.2;position:relative;top:0px;left:0px;z-index:120;" ></div>

            <table id="Table2" runat="server" clientidmode="Static"  class='<%# "teamAtable" + Container.ItemIndex  %>'>
                <tr align="center">
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                                            
                                    <asp:HiddenField ID="hfrptleftTeamA" runat="server" Value='<%#Eval("TeamID") %>' />
                                    <div>

                                        <asp:HiddenField ID="hfId" runat="server" Value='<%#Eval("MatchPlayerPerfomanceID") %>' />

                                        <asp:LinkButton ID="downSwap" runat="server" CommandName="SwapDownPlayer" CommandArgument='<%#Eval("MatchPlayerPerfomanceID") %>' OnClientClick="javascript:return confirm('Are You Sure, You Want to Swap Down Player?');">
                                                <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Down.png")%>" alt="" width="30px" height="30px" style="max-width:30px;"/>
                                        </asp:LinkButton>
                                    </div>
                                </td>
                                <td style="text-align: left; padding-top: 20px; width: 30%;">

                                    <div class="control-group">
		                                <label class="control-label"> 
                                                <asp:Label ID="lblPlayerName" runat="server" Text='<%#"" +" "+ Eval("PlayerName").ToString()+" "+ ":"%>' />
                                        </label>
                                        </div>
                                                            
                                </td>
                                <td style="padding-top: 5px; width: 112px">
                                    <div class="quantityDown matchResult_plusminus">
                                        -</div>
                                    <asp:TextBox ID="txtPlayerGoalA" runat="server" Width="30px" CssClass="fontcentersize quantityInput-text productQuantity"
                                        onkeypress='validate(event)' Style="display: inline; float: left; line-height: 30px;height:30px !important;
                                        margin-right: 10px;">

                                    </asp:TextBox>
                                    <div class="quantityUp" style="margin-top:4px;">
                                        +</div>
                                </td>
                                <td style="padding-top: 5px; width: 120px">
                                    <div class="quantityDownassist matchResult_plusminus">
                                        -</div>
                                    <asp:TextBox ID="txtteamAassist" runat="server" Width="30px" CssClass="fontcentersize quantityInput-text productQuantityassist"
                                        onkeypress='validate(event)' Style="display: inline; float: left; line-height: 30px;height:30px !important;
                                        margin-right: 10px;">

                                    </asp:TextBox>
                                    <div class="quantityUpassist" style="margin-top:4px;">
                                        +</div>
                                </td>
                                    <td style="padding-top: 5px; width: 120px">
                                    <div class="quantityDownongoal matchResult_plusminus">
                                        -</div>
                                    <asp:TextBox ID="txtAongoal" runat="server" Width="30px" CssClass="fontcentersize quantityInput-text productQuantityongoal"
                                        onkeypress='validate(event)' Style="display: inline; float: left; line-height: 30px;height:30px !important;
                                        margin-right: 10px;">

                                    </asp:TextBox>
                                    <div class="quantityUpOngoal" style="margin-top:4px;">
                                        +</div>
                                </td>
                                <td style="width: 64px">
                                    <%--<asp:TextBox ID="txtYellowA" runat="server" Width="30px" Height="25px" CssClass="fontcentersize">
                                    </asp:TextBox>--%>
                                    <asp:DropDownList ID="dlYellowA" CssClass="m-wrap xsmall" onchange=<%# "javascript:OpenPopupA('Yellow','" + Eval("PlayerID") + "')" %>  runat="server">
                                        <asp:ListItem Text="--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="padding-top: 5px; width: 84px; text-align: center; ">
                                    <label class="checkbox">
                                        <asp:CheckBox ID="chkIsRedA" runat="server"  onchange=<%# "javascript:OpenPopupA('Red','" + Eval("PlayerID") + "')" %>/>
                                        </label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <div id="dialogA" runat="server" clientidmode="Static" title="Basic dialog" class="suspendReasonBox" style="display:none;">
                <center style="padding-top: 10px;">
                <%--<div>Remark for Player: <asp:Literal id="playerrmk" runat="server" Text='<%#Eval("PlayerName").ToString()%>' /></div>--%>
                <asp:TextBox ID="txtNoteA" runat="server" TextMode="MultiLine"  Height="85px" Width="370px"></asp:TextBox>
                </br>
                 <div style="padding-top: 10px;">
                <asp:LinkButton ID="btnSubmitNote" runat="server" Text="Save"  CommandName="updatecards" CommandArgument='<%#Eval("PlayerID") %>'  CssClass="form-button form-button-submit"></asp:LinkButton>
                <asp:Button ID="btnHidePopUp" runat="server" Text="Close"  CssClass="form-button" OnClientClick=<%# "javascript:return HidePopUpA('" + Eval("PlayerID") + "')" %> />
                <%--OnClientClick="return ClosePopUp(this)--%>
                 </div>
                <asp:HiddenField ID="lblPlayerID" runat="server" Value='<%#Eval("PlayerID") %>' ClientIDMode="Static"/>
                </center>
            </div>

            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
                            
                           
        <div id="statusForTeamA" runat="server" class="smallMessage">
            <asp:Label ID="lblForTeamA" runat="server" CssClass="prodetail-lable" Text="" Visible="false" />
        </div>
    </div>
                           
    <div style="border: 1px solid #35aa47; width: 470px;">
        <asp:Repeater ID="rptrTeamA1" runat="server" OnItemCommand="rptrTeamA1_OnItemCommand"
            OnItemDataBound="rptrTeamA1_OnItemDataBound">
            <HeaderTemplate>
                                        
                <table style="width: 100%" cellpadding="0">
                    <tr style="background-color: #35aa47; color: White; height: 35px;">
                        <th style="width: 150px;">
                            Player List
                        </th>
                        <th style="width: 82px;">
                            Goal
                        </th>
                        <th style="width: 82px;">
                            Assist
                        </th>
                        <th style="width: 82px;">
                            Yellow
                        </th>
                        <th>
                            Red
                        </th>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr align="center">
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <div>
                                        <asp:LinkButton ID="upSwap" runat="server" CommandName="SwapUpPlayer" CommandArgument='<%#Eval("PlayerID") %>' OnClientClick="javascript:return confirm('Are You Sure, You Want to Swap Up Player?')">
                                                        
                                                <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/up.png")%>" alt="" width="30px" height="30px" style="max-width:30px;"/>
                                                                    
                                        </asp:LinkButton>
                                    </div>
                                </td>
                                <td style="text-align: left; padding-top: 20px; width: 30%;">
                                    <div class="control-group">
		                                <label class="control-label"> 
                                            <asp:Label ID="lblPlayerName" runat="server" Text='<%#"" +" "+ Eval("PlayerName").ToString()+" "+ ":"%>' />
                                        </label>
                                    </div>
                                    <asp:HiddenField ID="lblPlayerID" runat="server" Value='<%#Eval("PlayerID") %>' />
                                    <asp:HiddenField ID="hdnFlag" runat="server" Value='<%#Eval("Flag") %>' />
                                </td>
                                <td style="padding-top: 5px; width: 112px">
                                    <%--<div class="quantityDown" style="display: inline; float: left; margin-right: 10px;">-</div>--%>
                                    <%--<asp:TextBox ID="txtPlayerGoalA" runat="server" Width="30px" Height="25px" CssClass="fontcentersize quantityInput-text productQuantity" onkeypress='validate(event)' Style="display: inline; float: left; line-height: 30px; margin-right: 10px;">

                                    </asp:TextBox>--%>
                                    <asp:Label ID="labelGoalForA1" runat="server" Text='<%#Eval("Goal") %>'></asp:Label>
                                    <%--<div class="quantityUp">+</div>--%>
                                </td>
                                <td style="padding-top: 5px; width: 120px">
                                    <asp:Label ID="labelAssist" runat="server" Text='<%#Eval("Assist") %>'></asp:Label>
                                    <%--<div class="quantityUpassist">+</div>--%>
                                </td>
                                <td style="width: 120px">
                                    <%--<asp:TextBox ID="txtYellowA" runat="server" Width="30px" Height="25px" CssClass="fontcentersize">
                                    </asp:TextBox>--%>
                                    <asp:Label ID="lblYellowA" runat="server" Text='<%#Eval("Yellow") %>'></asp:Label>
                                </td>
                                <td style="padding-top: 5px; width: 84px; text-align: center;">
                                    <div id="checkdiv" runat="server" class="col-left">

                                        <label class="checkbox">
                                            <asp:CheckBox ID="chkIsRedA" runat="server" />
                                        </label>
                                                                
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <div id="statusForTeamA1" runat="server" class="smallMessage">
            <asp:Label ID="lblForTeamA1" runat="server" CssClass="prodetail-lable" Text="" Visible="false" />
        </div>
    </div>
</div>

<div style="float: left; width: 450px; border: 1px; margin:10px 0 0 84px;">
    <div style="border: 1px solid #35aa47; width: 470px;">

    <div id="myDiv">
        <asp:Repeater ID="rptrightTeamB" runat="server" OnItemCommand="rptrightTeamB_OnItemCommand"
            OnItemDataBound="rptrightTeamB_OnItemDataBound">
            <HeaderTemplate>
                                        
                <table style="width: 100%" cellpadding="0">
                    <tr style="background-color: #35aa47; color: White; height: 35px;">
                        <th style="width: 100px;">
                            Player List
                        </th>
                        <th style="width: 82px;">
                            Goal
                        </th>
                        <th style="width: 82px;">
                            Assist
                        </th>
                            <th style="width: 82px;">
                            Own Goal
                        </th>
                        <th style="width: 82px;">
                            Yellow
                        </th>
                        <th>
                            Red
                        </th>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
            <div class="overlap" style="width:100%;opacity:0.2;position:relative;top:0px;left:0px;z-index:120;" ></div>
            <table id="Table3"  runat="server" clientidmode="Static"  class='<%# "teamBtable" + Container.ItemIndex  %>' >
                                    
                <tr style="color: Black;" align="center">
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <div>
                                        <asp:HiddenField ID="hfrptrightTeamB" runat="server" Value='<%#Eval("TeamID") %>' />
                                        <asp:LinkButton ID="downSwap" runat="server" CommandName="SwapDownPlayer" CommandArgument='<%#Eval("MatchPlayerPerfomanceID") %>' OnClientClick="javascript:return confirm('Are You Sure, You Want to Swap Down Player?')">
                                            <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/Down.png")%>" alt=""
                                                width="30px" height="30px"  style="max-width:30px;"/>
                                            <asp:HiddenField ClientIDMode="Static" ID="rptrBhfId" runat="server" Value='<%#Eval("MatchPlayerPerfomanceID") %>' />
                                        </asp:LinkButton>
                                    </div>
                                </td>
                                <td style="text-align: left; padding-top: 20px; width: 30%">

                                    <div class="control-group">
		                                <label class="control-label"> 
                                            <asp:Label ID="lblPlayerName" runat="server" Text='<%#"" +" "+ Eval("PlayerName").ToString()+" "+ ":"%>' />
                                        </label>
                                    </div>
                                    <%--<asp:HiddenField ID="lblPlayerID" runat="server" Value='<%#Eval("User_RegID") %>' />--%>
                                </td>
                                <td style="padding-top: 5px; width: 112px">
                                    <div class="quantityDownB matchResult_plusminus">
                                        -</div>
                                    <asp:TextBox ID="txtPlayerGoalB" runat="server" Width="30px" CssClass="fontcentersize quantityInput-text productQuantityB"
                                        onkeypress='validate(event)' Style="display: inline; float: left; line-height: 30px;height:30px !important;
                                        margin-right: 10px;"></asp:TextBox>
                                    <div class="quantityUpB" style="margin-top:4px;">
                                        +</div>
                                </td>
                                <td style="padding-top: 5px; width: 120px">
                                    <div class="quantityDownassistB matchResult_plusminus">
                                        -</div>
                                    <asp:TextBox ID="txtteamBassist" runat="server" Width="30px" CssClass="fontcentersize quantityInput-text productQuantityassistB"
                                        onkeypress='validate(event)' Style="display: inline; float: left; line-height: 30px;height:30px !important;
                                        margin-right: 10px;">

                                    </asp:TextBox>
                                    <div class="quantityUpassistB" style="margin-top:4px;">
                                        +</div>
                                </td>
                                    <td style="padding-top: 5px; width: 120px">
                                    <div class="quantityDownGoalonB matchResult_plusminus">
                                        -</div>
                                    <asp:TextBox ID="txtBongoal" runat="server" Width="30px" CssClass="fontcentersize quantityInput-text productQuantityGoalonB"
                                        onkeypress='validate(event)' Style="display: inline; float: left; line-height: 30px;height:30px !important;
                                        margin-right: 10px;"> </asp:TextBox>
                                    <div class="quantityUpGoalonB" style="margin-top:4px;">
                                        +</div>
                                </td>
                                <td style="width: 64px">
                                    <asp:DropDownList ID="dlYellowB" CssClass="m-wrap xsmall" onchange=<%# "javascript:OpenPopupB('Yellow','" + Eval("PlayerID") + "')" %> runat="server">
                                        <asp:ListItem Text="--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                                            
                                </td>
                                                        
                                <td style="padding-top: 5px; width: 84px; text-align: center; ">
                                    <label class="checkbox">
                                        <asp:CheckBox ID="chkIsRedB" onchange=<%# "javascript:OpenPopupB('Red','" + Eval("PlayerID") + "')" %>  runat="server" />
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <div id="dialogB" runat="server" clientidmode="Static" title="Basic dialog" class="suspendReasonBox" style="display:none;">
                 <center style="padding-top: 10px;">
                <%--<div>Remark for Player: <asp:Literal id="playerrmk" runat="server" Text='<%#Eval("PlayerName").ToString()%>' /></div>--%>
                <asp:TextBox ID="txtNoteB" runat="server" TextMode="MultiLine"  Height="85px" Width="370px"/>
                <br /> <div style="padding-top: 10px;">
                <asp:LinkButton ID="btnSubmitNote" runat="server" Text="Save"  CommandName="updatecards" CommandArgument='<%#Eval("PlayerID") %>'  CssClass="form-button form-button-submit"></asp:LinkButton>
                <asp:Button ID="btnHidePopUp" runat="server" Text="Close"  CssClass="form-button" OnClientClick=<%# "javascript:return HidePopUpB('" + Eval("PlayerID") + "')" %> />
                <%--OnClientClick="return ClosePopUp(this)--%></div>
                <asp:HiddenField ID="lblPlayerID" runat="server" Value='<%#Eval("PlayerID") %>' ClientIDMode="Static" />
                </center>
            </div>

            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </div>
        <div id="statusForTeamB" runat="server" class="smallMessage">
            <asp:Label ID="lblForTeamB" runat="server" CssClass="prodetail-lable" Text="" Visible="false" />
        </div>
    </div>
                            
    <div style="border: 1px solid #35aa47; width: 470px;">
        <asp:Repeater ID="rptrTeamB1" runat="server" OnItemCommand="rptrTeamB1_OnItemCommand"
            OnItemDataBound="rptrTeamB1_OnItemDataBound">
            <HeaderTemplate>
                <table style="width: 100%" cellpadding="0">
                    <tr style="background-color: #35aa47; color: White; height: 35px;">
                        <th style="width: 150px;">
                            Player List
                        </th>
                        <th style="width: 82px;">
                            Goal
                        </th>
                        <th style="width: 82px;">
                            Assist
                        </th>
                        <th style="width: 82px;">
                            Yellow
                        </th>
                        <th>
                            Red
                        </th>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="color: Black;" align="center">
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <div class="icons icon-5_8">
                                        <asp:LinkButton ID="upSwap" runat="server" CommandName="SwapUpPlayer" CommandArgument='<%#Eval("PlayerID") %>' OnClientClick="javascript:return confirm('Are You Sure, You Want to Swap Up Player?')">
                                <img src="<%= Page.ResolveUrl("~/DesktopModules/ThSport/Images/OtherImages/up.png")%>" alt="" width="30px" height="30px" style="max-width:30px;"/>

                                        </asp:LinkButton>
                                    </div>
                                </td>
                                <td style="text-align: left; padding-top: 20px; width: 30%">

                                    <div class="control-group">
		                                <label class="control-label"> 
                                            <asp:Label ID="lblPlayerName" runat="server" Text='<%#"" +" "+ Eval("PlayerName").ToString()+" "+ ":"%>' />
                                        </label>
                                    </div>
                                    <asp:HiddenField ID="lblPlayerID" runat="server" Value='<%#Eval("PlayerID") %>' />
                                    <asp:HiddenField ID="hdnFlag" runat="server" Value='<%#Eval("Flag") %>' />
                                </td>
                                <td style="padding-top: 5px; width: 135px">
                                    <%--<div class="quantityDownB" style="display: inline; float: left; margin-right: 10px;">-</div>--%>
                                    <%--<asp:TextBox ID="txtPlayerGoalB" runat="server" Width="30px" Height="25px" CssClass="fontcentersize quantityInput-text productQuantityB" onkeypress='validate(event)' Style="display: inline; float: left; line-height: 30px; margin-right: 10px;"></asp:TextBox>--%>
                                    <asp:Label ID="labelGoalForB1" runat="server" Text='<%#Eval("Goal") %>'></asp:Label>
                                    <%--<div class="quantityUpB">+</div>--%>
                                </td>
                                <td style="padding-top: 5px; width: 120px">
                                    <%--<div class="quantityDownassistB" style="display: inline; float: left; margin-right: 10px;">-</div>--%>
                                    <%--<asp:TextBox ID="txtteamBassist" runat="server" Width="30px" Height="25px" CssClass="fontcentersize quantityInput-text productQuantityassistB" onkeypress='validate(event)' Style="display: inline; float: left; line-height: 30px; margin-right: 10px;">
                                    </asp:TextBox>--%>
                                    <asp:Label ID="labelAssist" runat="server" Text='<%#Eval("Assist") %>'></asp:Label>
                                    <%--<div class="quantityUpassistB">+</div>--%>
                                </td>
                                <td style="width: 64px">
                                    <%--<asp:TextBox ID="txtYellowB" runat="server" Width="30px" Height="25px" CssClass="fontcentersize">
                                    </asp:TextBox>--%>
                                    <%--<asp:DropDownList ID="dlYellowB" runat="server">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    </asp:DropDownList>--%>
                                    <asp:Label ID="lblYellowB" runat="server" Text='<%#Eval("Yellow") %>'></asp:Label>
                                </td>
                                <td style="padding-top: 5px; width: 84px; text-align: center; ">
                                    <div id="checkdiv1" runat="server" class="col-left">
                                        <label class="checkbox">
                                            <asp:CheckBox ID="chkIsRedB" runat="server" />
                                        </label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <div id="statusForTeamB1" runat="server" class="smallMessage">
            <asp:Label ID="lblForTeamB1" runat="server" CssClass="prodetail-lable" Text="" Visible="false" />
        </div>
    </div>
</div>

           </asp:Panel>


        <table style="margin:10px;">
            <tr>
                <td>
                    <asp:Label ID="lblDescription" runat="server" Width="105px" Height="25px" Text="Description :-" />
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" Width="813px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
                
            
            <div class="form-actions">

                <div class="right_div_css">

                    <input type="button" id="buttontnAddNews" value="Update Result" class="btn blue" style="display:none;"/>


                        <asp:Button ID="btnGoToCompetition" runat="server"   Text="Competition" 
                                     OnClick="btnGoToCompetition_Click" CssClass="btn blue back_btn_Position" />
                                    

                        <asp:Button ID="btnMatchResultSave" runat="server"  Text="Save" ClientIDMode="Static"
                                    OnClick="btnMatchResultSave_Click" CssClass="btn blue" Width="100px"
                                    OnClientClick="return validateAndConfirm(this.id);" />

                        <asp:Button ID="btnMatchResultUpdateData" runat="server"  ClientIDMode="Static"
                                    Text="Update" Visible="false" OnClick="btnMatchResultUpdateData_Click" 
                                    CssClass="btn red" Width="100px"
                                    OnClientClick="return validateAndConfirm(this.id);"/> 

                        <asp:Button ID="btnReset" runat="server"  Text="Reset"  
                                    OnClick="btnReset_Click" CssClass="btn red" ClientIDMode="Static"
                                    OnClientClick="return validateAndConfirm(this.id);" Width="100px"/>

                        <asp:Button ID="btnMatchResultClose" runat="server"  Text="Cancel"
                                    OnClick="btnMatchResultClose_Click" CssClass="btn" 
                                    ClientIDMode="Static" Width="100px"
                                    OnClientClick="return validateAndConfirm(this.id);"/>
                    </div>
            </div>


    </div>

    </div>

</div>

</div>

</div>

    </div>
              
    </asp:Panel>
</div>



</div>

</div>

