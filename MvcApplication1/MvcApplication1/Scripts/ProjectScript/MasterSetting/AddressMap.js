
function getRowVal(rowid) {
    $("#btnInsert").hide();
    $("#btnUpdate").show();
    $("#MsgDiv").removeAttr("class");
    $("#lblError").html("");
    $("#MsgDiv").hide();
    var tbl = document.getElementById("dataTable");
    if (tbl != null) {
        rowid = rowid + 1;
        var $tds = tbl.rows[rowid];
        $("#AddressGoogleMapId").val($tds.cells[1].innerText.trim());
        $("#AddressId").val($tds.cells[2].innerText.trim());
        $("#LocationName").val($tds.cells[3].innerText.trim());
        $("#Latitude").val($tds.cells[4].innerText.trim());
        var a = $tds.cells[4].innerText.trim();
        var b = $tds.cells[5].innerText.trim();
        $("#Longitude").val($tds.cells[5].innerText.trim());
    }
}

function RowDelete(AddressGoogleMapId) {
    try {

        $("#btnInsert").show();
        $("#btnUpdate").hide();

        $("#AddressGoogleMapId").val(AddressGoogleMapId);

        $("#MsgDiv").removeAttr("class");
        $("#lblError").html("");
        $("#MsgDiv").hide();

        if (!confirm("Do You Want To Delete")) {
            return false;
        }
        var form = $("#idAllFormTag");

        $.post("AddressDetailMapDelete", form.serialize(),
           function (response) {

               if (response.Flag == 0) {  //Successfully

                   $("#data").html("");
                   $("#data").html(response.Html);
                   $("#MsgDiv").show();
                   $('#MsgDiv').addClass('alert alert-success');
                   $("#lblError").html("Deleted Successfully !");

                   return false;
               }
               if (response.Flag == 1) {  // Exception
                   $("#MsgDiv").show();
                   $('#MsgDiv').addClass('alert alert-danger');
                   $("#lblError").html(response.Html);
                   return false;
               }
               if (response.Flag == 2) {
                   $("#MsgDiv").show();
                   $('#MsgDiv').addClass('alert alert-danger');
                   $("#lblError").html(response.Html);
                   return false;

               }
           });
        return false;
    }
    catch (Ex) {
        $("#MsgDiv").show();
        $('#MsgDiv').addClass('alert alert-danger');
        $("#lblError").html("Delete Failed !");
    }
}

function functionReset() {
    $('#idAllFormTag').each(function () {
        this.reset();
    });
}

function AddressDetailMapGet() {
    try {
        $("#MsgDiv").removeAttr("class");
        $("#lblError").html("");
        $("#MsgDiv").hide();
        $.post("AddressDetailMapGet", function (response) {

            if (response.Flag == 0) {

                $("#data").html("");
                $("#data").html(response.Html);
                return false;
            }
            if (response.Flag == 1) {

                $("#MsgDiv").show();
                $('#MsgDiv').addClass('alert alert-danger');
                $("#lblError").html(response.Html);
                return false;
            }
            if (response.Flag == 2) {

                $("#MsgDiv").show();
                $('#MsgDiv').addClass('alert alert-danger');
                $("#lblError").html(response.Html);
                return false;
            }


        });
    }
    catch (Ex) {
        $("#MsgDiv").show();
        $('#MsgDiv').addClass('alert alert-danger');
        $("#lblError").html("Fetch data failed !");
        return false;
    }
}

function FormValidate() {
    var validator = $("#idAllFormTag").validate(); // obtain validator
    var anyError = false;
    $("#idAllFormTag").find(".form-control").each(function () {
        try {
            if (!validator.element(this)) { // validate every input element inside this step
                anyError = true;
            }
        }
        catch (ex) {
            return false;
        }
    });
    if (anyError) {
        return false;
    }
    return true;
}

$(document).ready(function (e) {

    AddressDetailMapGet();

    $("#btnClear").click(
        function (e) {

            $("#MsgDiv").removeAttr("class");
            $("#lblError").html("");
            $("#MsgDiv").hide();

            functionReset();

            $("#btnInsert").show();
            $("#btnUpdate").hide();
            e.stopImmediatePropagation();
            e.preventDefault();
        });

    $("#btnRefresh").click(
        function (e) {

            AddressDetailMapGet();

        });

    $("#btnInsert").click(
        function (e) {
            try {
                $("#btnUpdate").hide();
                $("#MsgDiv").removeAttr("class");
                $("#lblError").html("");
                $("#MsgDiv").hide();

                var form = $("#idAllFormTag");

                if (!(Boolean)(FormValidate())) {
                    return false;
                }

                $.post("AddressDetailMapInsert", form.serialize(),
                    function (response) {
                        if (response.Flag == 0) {  //Successfully
                            $("#data").html("");
                            $("#data").html(response.Html);
                            $("#MsgDiv").show();
                            $('#MsgDiv').addClass('alert alert-success');
                            $("#lblError").html("Inserted Successfully !");

                            functionReset();

                            return false;
                        }
                        if (response.Flag == 1) {  // Exception
                            $("#MsgDiv").show();
                            $('#MsgDiv').addClass('alert alert-danger');
                            $("#lblError").html(response.Html);
                            return false;
                        }
                        if (response.Flag == 2) { //Record Exists
                            $("#MsgDiv").show();
                            $('#MsgDiv').addClass('alert alert-danger');
                            $("#lblError").html(response.Html);
                            return false;
                        }
                    });
                e.stopImmediatePropagation();
                e.preventDefault();
            }
            catch (Ex) {
                $("#MsgDiv").show();
                $('#MsgDiv').addClass('alert alert-danger');
                $("#lblError").html("Insert Failed !");
            }
        });

    //$("#btnClose").click(
    //    function (e) {
    //        $("#MsgDiv").removeAttr("class");
    //        $("#lblError").html("");
    //        $("#MsgDiv").hide();
    //        e.stopImmediatePropagation();
    //        e.preventDefault();
    //    });

    $("#btnUpdate").click(
     function (e) {
         try {
             $("#btnInsert").hide();
             $("#MsgDiv").removeAttr("class");
             $("#lblError").html("");
             $("#MsgDiv").hide();

             var form = $("#idAllFormTag");

             if (!(Boolean)(FormValidate())) {
                 return false;
             }

             $.post("AddressDetailMapUpdate", form.serialize(),

                function (response) {

                    if (response.Flag == 0) {  //Successfully

                        $("#data").html("");
                        $("#data").html(response.Html);

                        functionReset();

                        $("#MsgDiv").show();
                        $('#MsgDiv').addClass('alert alert-success');
                        $("#lblError").html("Update Successfully !");

                        $("#btnUpdate").hide();
                        $("#btnInsert").show();

                        return false;
                    }
                    if (response.Flag == 1) {

                        $("#MsgDiv").show();
                        $('#MsgDiv').addClass('alert alert-danger');
                        $("#lblError").html(response.Html);
                        return false;
                    }
                    if (response.Flag == 2) {

                        $("#MsgDiv").show();
                        $('#MsgDiv').addClass('alert alert-danger');
                        $("#lblError").html(response.Html);
                        return false;
                    }
                });
             e.stopImmediatePropagation();
             e.preventDefault();
         }
         catch (Ex) {

             $("#MsgDiv").show();
             $('#MsgDiv').addClass('alert alert-danger');
             $("#lblError").html("Update Failed !");
         }
     });

});




