
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
        $("#TextFlashId").val($tds.cells[1].innerText.trim());
        $("#TextMessage").val($tds.cells[2].innerText.trim());
    }
}

function RowDelete(TextFlashId) {
    try {

        $("#btnInsert").show();
        $("#btnUpdate").hide();

        $("#TextFlashId").val(TextFlashId);

        $("#MsgDiv").removeAttr("class");
        $("#lblError").html("");
        $("#MsgDiv").hide();

        if (!confirm("Do You Want To Delete")) {
            return false;
        }
        var form = $("#idAllFormTag");

        $.post("TextFlashNewsDelete", form.serialize(),
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

function TextFlashNewsGet() {
    try {
        $("#MsgDiv").removeAttr("class");
        $("#lblError").html("");
        $("#MsgDiv").hide();
        $.post("TextFlashNewsGet", function (response) {

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

    TextFlashNewsGet();

    $('#dataTable').after('<div id="nav"></div>');
    var rowsShown = 2;
    var rowsTotal = $('#dataTable tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#nav').append('<a href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#dataTable tbody tr').hide();
    $('#dataTable tbody tr').slice(0, rowsShown).show();
    $('#nav a:first').addClass('active');
    $('#nav a').bind('click', function () {

        $('#nav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#dataTable tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
                css('display', 'table-row').animate({ opacity: 1 }, 300);
    });



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

            TextFlashNewsGet();

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

                $.post("TextFlashNewsInsert", form.serialize(),
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

             $.post("TextFlashNewsUpdate", form.serialize(),

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




