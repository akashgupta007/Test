
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

        $(".LoadingDiv").show();
        

        $.post("PropertyDetailGet", function (response) {

            if (response.Flag == 0) {

                $("#data").html("");
                $("#data").html(response.Html);
                $("#TotalRecordInDb").val(response.TotalRecord);

                var TotalRecordInDb = $("#TotalRecordInDb").val().trim();
                $("#divPaging").smartpaginator({
                    totalrecords: TotalRecordInDb, recordsperpage: 10, initval: 0, next: 'Next', prev: 'Prev', first: 'First', last: 'Last', theme: 'black', onchange: GetDetailWithPaging
                });

                $(".LoadingDiv").hide();

                //$("#divPaging").html(response.TotalRecord);
            }
            if (response.Flag == 1) {

                $("#MsgDiv").show();
                $('#MsgDiv').addClass('alert alert-danger');
                $("#lblError").html(response.Html);

                $(".LoadingDiv").hide();
            }
            if (response.Flag == 2) {

                $("#MsgDiv").show();
                $('#MsgDiv').addClass('alert alert-danger');
                $("#lblError").html(response.Html);

                $(".LoadingDiv").hide();
            }
        });
    }
    catch (Ex) {
        $("#MsgDiv").show();
        $('#MsgDiv').addClass('alert alert-danger');
        $("#lblError").html("Fetch data failed !");

        $(".LoadingDiv").hide();
    }
}

function GetDetailWithPaging(PageNo) {

    try {
        $("#PageNo").val(PageNo);
        var form = $("#idAllFormTag");

        $(".LoadingDiv").show();

        $.post("GetDetailWithPaging",form.serialize(), function (response) {

            if (response.Flag == 0) {
                $("#data").html("");
                $("#data").html(response.Html);

                $(".LoadingDiv").hide();
            }
            if (response.Flag == 1) {

                $("#MsgDiv").show();
                $('#MsgDiv').addClass('alert alert-danger');
                $("#lblError").html(response.Html);

                $(".LoadingDiv").hide();
            }
            if (response.Flag == 2) {

                $("#MsgDiv").show();
                $('#MsgDiv').addClass('alert alert-danger');
                $("#lblError").html(response.Html);

                $(".LoadingDiv").hide();
            }
        });
    }
    catch (Ex) {
        $("#MsgDiv").show();
        $('#MsgDiv').addClass('alert alert-danger');
        $("#lblError").html("Fetch data failed !");

        $(".LoadingDiv").hide();
    }  
}


$(document).ready(function (e) {

    PropertyDetailGet();

    

    $("#ProperyImageUpload").change(function () {

        var fileName = this.files[0].name;
        var fileSize = this.files[0].size;
        var fileType = this.files[0].type;

        switch (fileType) {
            case 'image/png':
            case 'image/gif':
            case 'image/jpeg':
            case 'image/pjpeg':
               
                var fname = FileNameGet(fileName);
                $("#ImageName").val(fname.trim());
                $("#ImageType").val(fileType.trim());

                var iSize = ($("#ProperyImageUpload")[0].files[0].size / 1024);
                iSize = (Math.round(iSize * 100) / 100);
                if (iSize <= 110) {
                    if (this.files && this.files[0]) {
                        var FR = new FileReader();
                        FR.onload = function (e) {
                            $('#img').attr("src", e.target.result);
                            //$('#base').text(e.target.result);
                            $('#strbase64Image').val(e.target.result);
                        };
                        FR.readAsDataURL(this.files[0]);
                    }
                }
                else {
                    $("#MsgDiv").show();
                    $('#MsgDiv').addClass('alert alert-danger');
                    $("#lblError").html("Image Size is Greater than 100 and its Size is " + iSize);
                    $('#strbase64Image').val(null);
                    $('#img').attr("src", '');
                    return false;
                }

                break;
            default:
                $("#MsgDiv").show();
                $('#MsgDiv').addClass('alert alert-danger');
                $("#lblError").html("Unsupported File Format " + fileType);
                return false;
        }                       
    });



    function FileNameGet(fileName) {

        var fileExtension;
        fileExtension = fileName.substr((fileName.lastIndexOf('.') + 1));

        var text = "";
        var text1 = "";
        var text2 = "";

        var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var possibleChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (var i = 0; i < 5; i++) {
            text += possible.charAt(Math.floor(Math.random() * possible.length));
            text1 += possible.charAt(Math.floor(Math.random() * possible.length));
            text2 += possibleChar.charAt(Math.floor(Math.random() * possibleChar.length));
        }

        var fname = text1 + '-' + Math.floor((Math.random() * 99999))
        + '-' + text2
        + '-' + Math.floor((Math.random() * 99999999))
        + '-' + text
        + '.' + fileExtension

        return fname;
        
    }













    //jQuery.fn.center = function (parent) {
    //    if (parent) {
    //        parent = this.parent();
    //    } else {
    //        parent = window;
    //    }
    //    this.css({
    //        "position": "absolute",
    //        "top": ((($(parent).height() - this.outerHeight()) / 2) + $(parent).scrollTop() + "px"),
    //        "left": ((($(parent).width() - this.outerWidth()) / 2) + $(parent).scrollLeft() + "px")
    //    });
    //    return this;
    //}


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

                $.post("PropertyDetailInsert", enctype="multipart/form-data", form.serialize(),
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




