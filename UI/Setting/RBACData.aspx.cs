using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_RBACData : HotelLogic.HotelPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //module: module action user role rights
        string module = Request["module"] != "" ? Request["module"] : "";
        //action: add read update delete
        string action = Request["action"] != "" ? Request["action"] : "";

        #region 客户前端传来的值
        //module
        string moduletitle = Request.Form["moduletitle"] != "" ? Request.Form["moduletitle"] : "";
        //role
        string rolename = Request.Form["rolename"] != "" ? Request.Form["rolename"] : "";
        //user
        string username = Request.Form["username"] != "" ? Request.Form["username"] : "";
        string password = Request.Form["password"] != "" ? Request.Form["password"] : "";
        string roleid = Request.Form["roleid"] != "" ? Request.Form["roleid"] : "0";
        //action
        string moduleid = Request.Form["moduleid"] != "" ? Request.Form["moduleid"] : "0";
        string actiontitle = Request.Form["actiontitle"] != "" ? Request.Form["actiontitle"] : "";
        //rights
        //string roleid = Request.Form["roleid"] != "" ? Request.Form["roleid"] : "";
        //string moduleid = Request.Form["roleid"] != "" ? Request.Form["roleid"] : "";
        string actionid = Request.Form["actionid"] != "" ? Request.Form["actionid"] : "0";
        string userid = Request.Form["userid"] != "" ? Request.Form["userid"] : "0";
        string enable = Request.Form["enable"] != "" ? Request.Form["enable"] : "0";

        Int32 ID = Convert.ToInt32(Request["ID"] != "" ? Request["ID"] : "0");
        #endregion
        switch (module)
        {
            #region module
            case "module":
                if (action == "add")
                {
                    RBACModule temp = new RBACModule();
                    temp.ModuleTitle = moduletitle;
                    string rlt = RBACHelper.addModule(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    var rlt = RBACHelper.readModule();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    RBACModule temp = new RBACModule();
                    temp.ID = ID;
                    temp.ModuleTitle = moduletitle;
                    string rlt = RBACHelper.updateModule(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = RBACHelper.deleteModule(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = "{\"Error\":\"" + rlt + "\"}";
                }
                break;
            #endregion
            #region action
            case "action":
                if (action == "add")
                {
                    RBACAction temp = new RBACAction();
                    temp.ActionTitle = actiontitle;
                    temp.ModuleID = Convert.ToInt32(moduleid);
                    string rlt = RBACHelper.addAction(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    var rlt = RBACHelper.readAction();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    RBACAction temp = new RBACAction();
                    temp.ModuleID = Convert.ToInt32(moduleid);
                    temp.ID = ID;
                    temp.ActionTitle = actiontitle;
                    string rlt = RBACHelper.updateAction(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = RBACHelper.deleteAction(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion
            #region user
            case "user":
                if (action == "add")
                {
                    RBACUser temp = new RBACUser();
                    temp.Password = password;
                    temp.UserName = username;
                    temp.RoleID = Convert.ToInt32(roleid);
                    string rlt = RBACHelper.addUser(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    var rlt = RBACHelper.readUser();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    RBACUser temp = new RBACUser();
                    temp.RoleID = Convert.ToInt32(roleid);
                    temp.UserName = username;
                    temp.Password = password;
                    temp.ID = ID;
                    string rlt = RBACHelper.updateUser(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = RBACHelper.deleteUser(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion
            #region role
            case "role":
                if (action == "add")
                {
                    RBACRole temp = new RBACRole();
                    temp.RoleName = rolename;
                    string rlt = RBACHelper.addRole(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    var rlt = RBACHelper.readRole();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    RBACRole temp = new RBACRole();
                    temp.RoleName = rolename;
                    temp.ID = ID;
                    string rlt = RBACHelper.updateRole(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = RBACHelper.deleteRole(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion
            #region rights
            case "rights":
                if (action == "add")
                {
                    RBACRights temp = new RBACRights();
                    temp.RoleID = Convert.ToInt32(roleid);
                    temp.ModuleID = Convert.ToInt32(moduleid);
                    temp.Enable = Convert.ToInt32(enable);
                    temp.ActionID = Convert.ToInt32(actionid);
                    string rlt = RBACHelper.addRights(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    var rlt = RBACHelper.readRights();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    RBACRights temp = new RBACRights();
                    temp.RoleID = Convert.ToInt32(roleid);
                    temp.ModuleID = Convert.ToInt32(moduleid);
                    temp.Enable = Convert.ToInt32(enable);
                    temp.ActionID = Convert.ToInt32(actionid);
                    temp.ID = ID;
                    string rlt = RBACHelper.updateRights(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = RBACHelper.deleteRights(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion
            default:
                break;
        }
        //output json result
        Response.Write(result);
    }
}