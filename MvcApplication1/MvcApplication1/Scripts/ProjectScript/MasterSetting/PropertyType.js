
function getRowVal(rowid) {
    $("#btnInsert").hide();
    $("#btnUpdate").show();
    $("#MsgDiv").removeAttr("class");
    $("#lblError").html("");
    $("#MsgDiv").hide();

    $("#dataTable").find('tr.data-row').each(function (i) {

        if(i==rowid)
        {
            //rowid = rowid + 1;

            var t = document.getElementById('dataTable');
            var val1 = $(t.rows[rowid].cells[0]).text();

            $("#PropertyTypeId").val($(t.rows[rowid].cells[1]).text());
            $("#OrderNo").val($(t.rows[rowid].cells[2]).text());
            $("#PropertyTypeName").val($(t.rows[rowid].cells[3]).text());
        }

    });

    //var tbl = document.getElementById("dataTable");
    //if (tbl != null) {
    //    rowid = rowid + 1;

    //    var t = document.getElementById('dataTable');
    //    var val1 = $(t.rows[rowid].cells[0]).text();

    //    $("#PropertyTypeId").val($(t.rows[rowid].cells[1]).text());
    //    $("#OrderNo").val($(t.rows[rowid].cells[2]).text());
    //    $("#PropertyTypeName").val($(t.rows[rowid].cells[3]).text());

        ////var a = tbl.rows[rowid].cells[1].innerHTML;
        ////var b = tbl.rows[rowid].cells[2].innerHTML;
        ////var c = tbl.rows[rowid].cells[3].innerHTML;
        ////var t = document.getElementById('resultGridTable');
        ////var val1 = $(t.rows[3].cells[0]).text();

        //var $tds = tbl.rows[rowid];
        //$("#PropertyTypeId").val($tds.cells[1].innerText.trim());
        //$("#OrderNo").val($tds.cells[2].innerText.trim());
        //$("#PropertyTypeName").val($tds.cells[3].innerText.trim());

        ////$("#PropertyTypeId").val(tbl.rows[rowid].cells[1].innerHTML);
        ////$("#OrderNo").val(tbl.rows[rowid].cells[2].innerHTML);
        ////$("#PropertyTypeName").val(tbl.rows[rowid].cells[3].innerHTML);

    //}
}

function RowDelete(PropertyTypeId) {
    try {

        $("#btnInsert").show();
        $("#btnUpdate").hide();

        $("#PropertyTypeId").val(PropertyTypeId);

        $("#MsgDiv").removeAttr("class");
        $("#lblError").html("");
        $("#MsgDiv").hide();

        if (!confirm("Do You Want To Delete")) {
            return false;
        }
        var form = $("#idAllFormTag");

        $.post("PropertyTypeDelete", form.serialize(),
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

function PropertyTypeGet() {
    try {
        $("#MsgDiv").removeAttr("class");
        $("#lblError").html("");
        $("#MsgDiv").hide();
        $.post("PropertyTypeGet", function (response) {

            if (response.Flag == 0) {

                $("#data").html("");
                $("#data").html(response.Html);
                //$("#dataTable").dataTable();

                $('#data').append('<div id="nav"></div>');
                var rowsShown = 4;
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

    PropertyTypeGet();

    


    //$('#dataTable tr').click(function () {


    //    var $tds = $(this).find('td');
    //    var val1 = $tds.eq(1).text().trim();

    //    $("#PropertyTypeId").val($tds.eq(1).text().trim());
    //    $("#OrderNo").val($tds.eq(1).text().trim());
    //    $("#PropertyTypeName").val($tds.eq(1).text().trim());

    //});

    $("#txtSearch").keyup(function () {
        _this = this;
        // Show only matching TR, hide rest of them
        $.each($("#dataTable tbody").find("tr"), function () {
            if ($(this).text().toLowerCase().indexOf($(_this).val().toLowerCase()) == -1)
                $(this).hide();
            else
                $(this).show();
        });
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

            PropertyTypeGet();

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

                $.post("PropertyTypeInsert", form.serialize(),
                    function (response) {
                        if (response.Flag == 0) {  //Successfully
                            $("#data").html("");
                            $("#data").html(response.Html);

                            $("#MsgDiv").show();
                            $('#MsgDiv').addClass('alert alert-success');
                            $("#lblError").html("Inserted Successfully !");
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

             $.post("PropertyTypeUpdate", form.serialize(),

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




