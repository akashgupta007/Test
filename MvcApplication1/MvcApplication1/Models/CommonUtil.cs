using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;

namespace MvcApplication1.Models
{
    public class CommonUtil
    {
        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        public static StringBuilder htmlTableEditMode(DataTable datatable)
        {
            try
            {

                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                html.Append("<thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    html.Append("<th>" + col.Caption + "</th>");

                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        html.Append("<tr class='data-row'  onclick='getRowVal(" + rowId + ")' >");
                        foreach (var cell in row.ItemArray)
                        {
                            html.Append("<td>" + cell.ToString() + "</td>");
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody>");

                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        public static StringBuilder htmlTablePaging(Int32 TotalPage, Int32 PageSize)
        {
            try
            {
                int numberofpage = Convert.ToInt32(Math.Ceiling((double)TotalPage / PageSize));

                StringBuilder html = new StringBuilder();

                html.Append("<table>");
                html.Append("<tr>");
                html.Append("<td>");
                for (int i = 1; i <= numberofpage; i++)
                {
                    //html.Append("<a href='Property/Property_Detail_Get/" + i + ">" + i + "</a>");
                    html.Append("<button type='button' title='Get Record' onclick='GetDetailWithPaging(" + i + ")' class='btn btn-default btn-xs'><span class='badge'>" + i + "</span></button></td>");


                }
                html.Append("</td >");
                html.Append("</tr>");
                html.Append("</table>");
                return html;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //public static StringBuilder htmlTableEditMode(DataTable datatable, int[] columnHide)
        //{
        //    try
        //    {
        //        string Id = null;
        //        StringBuilder html = new StringBuilder();
        //        int rowId = 0;
        //        int i = 0;

        //        html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
        //        foreach (System.Data.DataColumn col in datatable.Columns)
        //        {
        //            if (!(columnHide.Contains(i)))
        //            {
        //                html.Append("<th>" + col.Caption + "</th>");
        //            }
        //            else
        //            {
        //                html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
        //            }
        //            i = i + 1;
        //        }
        //        html.Append("</tr></thead><tbody>");
        //        if (datatable.Rows.Count > 0)
        //        {
        //            foreach (System.Data.DataRow row in datatable.Rows)
        //            {
        //                i = 0;

        //                html.Append("<tr class='data-row'> <td style='width:80px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + rowId + ")' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");

        //                foreach (var cell in row.ItemArray)
        //                {
        //                    if (i == 0)
        //                    {
        //                        Id = cell.ToString();
        //                        html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
        //                    }
        //                    if (!(columnHide.Contains(i)))
        //                    {
        //                        html.Append("<td>" + cell.ToString() + "</td>");
        //                    }
        //                    else
        //                    {
        //                        html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
        //                    }
        //                    i = i + 1;
        //                }
        //                html.Append("</tr>");
        //                rowId = rowId + 1;
        //            }
        //            html.Append("</tbody></table>");
        //            return html;
        //        }
        //        return html;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public static StringBuilder htmlTableEditModeWithImage(DataTable datatable, int[] columnHide)
        //{
        //    try
        //    {
        //        string Id = null;
        //        StringBuilder html = new StringBuilder();
        //        int rowId = 0;
        //        int i = 0;

        //        html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
        //        foreach (System.Data.DataColumn col in datatable.Columns)
        //        {
        //            if (!(columnHide.Contains(i)))
        //            {
        //                html.Append("<th>" + col.Caption + "</th>");
        //            }
        //            else
        //            {
        //                html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
        //            }
        //            i = i + 1;
        //        }
        //        html.Append("</tr></thead><tbody>");
        //        if (datatable.Rows.Count > 0)
        //        {
        //            foreach (System.Data.DataRow row in datatable.Rows)
        //            {
        //                i = 0;

        //                html.Append("<tr class='data-row'> <td style='width:80px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + rowId + ")' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");

        //                foreach (var cell in row.ItemArray)
        //                {
        //                    if (i == 0)
        //                    {
        //                        Id = cell.ToString();
        //                        html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
        //                    }
        //                    if (!(columnHide.Contains(i)))
        //                    {                               
        //                        if (i == 15)
        //                        {
        //                            html.Append("<td><img src='" + cell.ToString() + "' style='width:100px;height:100px;' class='img-thumbnail img-responsive'/></td>");
        //                        }
        //                        else
        //                        {
        //                            html.Append("<td>" + cell.ToString() + "</td>");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
        //                    }
        //                    i = i + 1;
        //                }
        //                html.Append("</tr>");
        //                rowId = rowId + 1;
        //            }
        //            html.Append("</tbody></table>");
        //            return html;
        //        }
        //        return html;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public static StringBuilder htmlTableEditMode(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'> <td style='width:80px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + rowId + ")' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }
                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td><a href='#' data-html='true' data-toggle='tooltip' title='<em>Tooltip</em> <u>with</u> <b>"+ cell.ToString() +"</b>'>" + cell.ToString() + "</a></td>");
                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static StringBuilder htmlTableEditModeWithImage(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'> <td style='width:80px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + rowId + ")' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }
                            if (!(columnHide.Contains(i)))
                            {
                                if (i == 15)
                                {
                                    html.Append("<td><img src='" + cell.ToString() + "' style='width:100px;height:100px;' class='img-thumbnail img-responsive'/></td>");
                                }
                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }
                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static StringBuilder htmlTableImage(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'> <td style='width:80px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + rowId + ")' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }
                            if (i == 2)
                            {
                                html.Append("<td><img src='" + cell.ToString() + "' style='width:100px;height:100px;' class='img-thumbnail img-responsive'/></td>");
                            }
                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static StringBuilder htmlTableImageSub(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'> <td style='width:80px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + rowId + ")' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }
                            if (i == 4)
                            {
                                html.Append("<td><img src='" + cell.ToString() + "' style='width:100px;height:100px;' class='img-thumbnail img-responsive'/></td>");
                            }
                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static StringBuilder htmlTableEditModeHideDelete(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'> <td style='width:60px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + rowId + ")' class='btn btn-success btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");

                        foreach (var cell in row.ItemArray)
                        {



                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        public static StringBuilder htmlTableEditMode(int[] columnShow, DataTable datatable)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnShow.Contains(i)))
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }

                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'> <td style='width:60px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + rowId + ")' class='btn btn-success btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }


                            if (!(columnShow.Contains(i)))
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        ////------------------------Hirarichal table------------------

        public static StringBuilder htmlChildTableEditMode(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='childDataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'> <td style='width:60px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowValChild(" + rowId + ")' class='btn btn-success btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDeleteChild(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        public static StringBuilder htmlNestedTableEditMode(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th></th><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr class='data-row'><td> <a class='glyphicon glyphicon-circle-arrow-right' data-toggle='collapse' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'  data-parent='#x'   href='" + "#" + childPanelId + rowId + "' </a></td> <td style='width:60px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + rowId + ")' class='btn btn-success btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");


                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        childPanelDivId = "childPanelDiv" + rowId;
                        html.Append("<tr id='" + childPanelId + rowId + "' class='panel-collapse collapse'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' data-toggle='collapse'> Add new reord</div></tr>");

                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static StringBuilder htmlVegitableDetails(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'> <td style='width:80px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + rowId + ")' class='btn btn-success btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }

                            if (i == 1)
                            {
                                html.Append("<td><img src='/VegitableDirectory/jpg/" + cell.ToString() + "'</img></td>");
                            }
                            if (!(columnHide.Contains(i)) && i != 1)
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            //if (i == 3)
                            //{
                            //    //html.Append("<td><img src='data:image/jpeg;base64," +(byte)cell + "'</img></td>");
                            //    html.Append("<td><img src='string.Format('data:image/jpg;base64,{0}', " + (byte[])cell + ")'</img></td>");
                            //    html.Append("<td><img src='/VegitableDirectory/jpg/', " + cell.ToString() + ")'</img></td>");
                            //}
                            //else
                            //{
                            //    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            //}
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static StringBuilder htmlVegitableDetailsDiv(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<div id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'> <td style='width:80px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + rowId + ")' class='btn btn-success btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }

                            if (i == 1)
                            {
                                html.Append("<td><img src='/VegitableDirectory/jpg/" + cell.ToString() + "'</img></td>");
                            }
                            if (!(columnHide.Contains(i)) && i != 1)
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</div>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}