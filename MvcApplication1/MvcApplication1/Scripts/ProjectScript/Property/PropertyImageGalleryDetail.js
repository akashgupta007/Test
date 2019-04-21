
function getRowVal(rowid) {
    $("#btnInsert").hide();
    $("#btnUpdate").show();
    $("#MsgDiv").removeAttr("class");
    $("#lblError").html("");
    $("#MsgDiv").hide();

    //$("#dataTable").find('tr.data-row').each(function (i) {

    //    if (i == rowid) {

            var t = document.getElementById('dataTable');

            $("#PropertyId").val($(t.rows[rowid].cells[1]).text());
            var a = $(t.rows[rowid].cells[1]).text();
            alert(a);
            //$("#OrderNo").val($(t.rows[rowid].cells[2]).text());
            //$("#PropertyTypeName").val($(t.rows[rowid].cells[3]).text());
    //    }

    //});
}

function RowDelete(PropertyId) {
    try {

        $("#btnInsert").show();
        $("#btnUpdate").hide();

        $("#MsgDiv").removeAttr("class");
        $("#lblError").html("");
        $("#MsgDiv").hide();

        if (!confirm("Do You Want To Delete")) {
            return false;
        }

        $("#PropertyId").val(PropertyId);

        var form = $("#idAllFormTag");

        $.post("PropertyDetailDelete", form.serialize(),
           function (response) {

               if (response.Flag == 0) {

                   $("#data").html("");
                   $("#data").html(response.Html);

                   $("#MsgDiv").show();
                   $('#MsgDiv').addClass('alert alert-success');
                   $("#lblError").html("Deleted Successfully !");

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
        return false;
    }
    catch (Ex) {
        $("#MsgDiv").show();
        $('#MsgDiv').addClass('alert alert-danger');
        $("#lblError").html("Delete Failed !");
        return false;
    }
}



function functionReset() {
    $('#idAllFormTag').each(function () {
        this.reset();
    });
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

function PropertyDetailGet() {
    try {
        $("#MsgDiv").removeAttr("class");
        $("#lblError").html("");
        $("#MsgDiv").hide();

        $.post("PropertyDetailGet", function (response) {

            if (response.Flag == 0) {

                $("#data").html("");
                $("#data").html(response.Html);
            }
            if (response.Flag == 1) {

                $("#MsgDiv").show();
                $('#MsgDiv').addClass('alert alert-danger');
                $("#lblError").html(response.Html);
            }
            if (response.Flag == 2) {

                $("#MsgDiv").show();
                $('#MsgDiv').addClass('alert alert-danger');
                $("#lblError").html(response.Html);
            }
        });
    }
    catch (Ex) {
        $("#MsgDiv").show();
        $('#MsgDiv').addClass('alert alert-danger');
        $("#lblError").html("Fetch data failed !");
    }
}

$(document).ready(function (e) {

    PropertyDetailGet();

    $("#btnUpdate").hide();

    $("#ProperyGalleryUpload").change(function () {

        var iSize = ($("#ProperyGalleryUpload")[0].files[0].size / 1024);
        iSize = (Math.round(iSize * 100) / 100);
        if (iSize <= 110)
        {
            if (this.files && this.files[0]) {
                var FR = new FileReader();
                FR.onload = function (e) {
                    $('#img').attr("src", e.target.result);
                    $('#strbase64Image').val(e.target.result);
                };
                FR.readAsDataURL(this.files[0]);
            }
        }
        else
        {
            $("#MsgDiv").show();
            $('#MsgDiv').addClass('alert alert-danger');
            $("#lblError").html("Image Size is Greater than 100 and its Size is " + iSize);
            $('#strbase64Image').val(null);
            $('#img').attr("src",'');
            return false;
        }
    });


    $("#btnInsert").click(

        function (e) {
            try {

                $("#btnUpdate").hide();
                $("#MsgDiv").removeAttr("class");
                $("#lblError").html("");
                $("#MsgDiv").hide();

                var form = $("#idAllFormTag");

                //if (!(Boolean)(FormValidate())) {
                //    return false;
                //}

                $.post("PropertyDetailInsert", form.serialize(),
                    function (response) {
                        if (response.Flag == 0) { 
                            $("#data").html("");
                            $("#data").html(response.Html);

                            $("#MsgDiv").show();
                            $('#MsgDiv').addClass('alert alert-success');
                            $("#lblError").html("Inserted Successfully !");
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
                $("#lblError").html("Insert Failed !");
                return false;
            }
        });


    $("#btnUpdate").click(

        function (e) {
            try {

                $("#btnUpdate").hide();
                $("#MsgDiv").removeAttr("class");
                $("#lblError").html("");
                $("#MsgDiv").hide();

                var form = $("#idAllFormTag");

                //if (!(Boolean)(FormValidate())) {
                //    return false;
                //}

                $.post("PropertyDetailUpdate", form.serialize(),
                    function (response) {
                        if (response.Flag == 0) {  
                            $("#data").html("");
                            $("#data").html(response.Html);

                            $("#MsgDiv").show();
                            $('#MsgDiv').addClass('alert alert-success');
                            $("#lblError").html("Update Successfully !");
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
                return false;
            }
        });
});




