using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        BusinessLayer obj = new BusinessLayer();
        public ActionResult Dashboard2()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Dashboard1()
        {
            try
            {
                string optionName = string.Empty;
                string cssClass = string.Empty;
                StringBuilder htmlTable = new StringBuilder();
                Session["MenuString"] = null;
                DataTable _mInfo = obj.FunDataTable("exec M_SP_GetMenuInformation");
                var _parentItem = (from _mMenu in _mInfo.AsEnumerable()
                                    where _mMenu.Field<string>("ParentID").Equals(string.Empty)
                                    select _mMenu).ToList();

                for (int i = 0; i < _parentItem.Count; i++)
                {
                    cssClass = _parentItem[i]["cssClass"].ToString();
                    optionName = _parentItem[i]["OptionName"].ToString();
                    var liParentActive = i == 0 ? "active treeview" : "treeview";
                    htmlTable.Append("<li class=" + liParentActive + "><a href='#'><i class='" + cssClass + "'></i><span>" + optionName + "</span> <i class='fa fa-angle-left pull-right'></i></a>");
                    htmlTable.Append("<ul class='treeview-menu'>");
                    var MenuID = _parentItem[i]["MenuID"];
                    var _childMenu = (from sItem in _mInfo.AsEnumerable() where sItem.Field<string>("ParentID").Equals(MenuID) select sItem).ToList();

                    if (_childMenu.Count > 0)
                    {
                        string childCssClass = string.Empty;
                        string childOptionName = string.Empty;
                        for (int j = 0; j < _childMenu.Count; j++)
                        {
                            childCssClass = _childMenu[j]["cssClass"].ToString();
                            childOptionName = _childMenu[j]["OptionName"].ToString();
                            var liActive = j == 0 && i == 0 ? "active" : "";
                            htmlTable.Append("<li class=" + liActive + "><a href=''><i class='" + childCssClass + "'></i>" + childOptionName + "</a></li>");
                        }
                        htmlTable.Append("</ul>");
                    }
                }
                htmlTable.Append("</li>");
                Session["MenuString"] = Convert.ToString(htmlTable);
                return View();
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

    }
}
