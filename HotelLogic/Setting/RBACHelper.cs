using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Setting
{
    public static class RBACHelper
    {
        //Module
        public static string addModule(RBACModule m)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_Module temp = new RBAC_Module();
                temp.ModuleTitle = m.ModuleTitle;
                _db.RBAC_Module.AddObject(temp);
                _db.SaveChanges();
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static string deleteModule(Int32 ID)
        {  
            try {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_Module temp = _db.RBAC_Module.FirstOrDefault(a => a.ID == ID);
                if (temp != null)
                {
                    _db.RBAC_Module.DeleteObject(temp);
                    _db.SaveChanges();
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static string updateModule(RBACModule m)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_Module temp = _db.RBAC_Module.FirstOrDefault(a => a.ID == m.ID);
                if (temp != null)
                {
                    temp.ModuleTitle = m.ModuleTitle;
                    _db.SaveChanges();
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static List<RBACModule> readModule()
        {
            List<RBACModule> result = new List<RBACModule>();
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                var temp = _db.RBAC_Module.ToList();
                foreach (var t in temp)
                {
                    RBACModule m = new RBACModule();
                    m.ModuleTitle = t.ModuleTitle;
                    m.ID = t.ID;
                    result.Add(m);
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return result;
        }
        //Action
        public static string addAction(RBACAction ac)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_Action temp = new RBAC_Action();
                temp.ActionTitle = ac.ActionTitle;
                temp.ModuleID = ac.ModuleID;
                _db.RBAC_Action.AddObject(temp);
                _db.SaveChanges();
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static string deleteAction(Int32 ID)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_Action temp = _db.RBAC_Action.FirstOrDefault(a => a.ID == ID);
                if (temp != null)
                {
                    _db.RBAC_Action.DeleteObject(temp);
                    _db.SaveChanges();
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static string updateAction(RBACAction ac)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_Action temp = _db.RBAC_Action.FirstOrDefault(a => a.ID == ac.ID);
                if (temp != null)
                {
                    temp.ActionTitle = ac.ActionTitle;
                    temp.ModuleID = ac.ModuleID;
                    _db.SaveChanges();
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static List<RBACAction> readAction()
        {
            List<RBACAction> result = new List<RBACAction>();
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                var temp = _db.RBAC_Action.ToList();
                foreach (var t in temp)
                {
                    RBACAction ac = new RBACAction();
                    ac.ActionTitle = t.ActionTitle;
                    ac.ID = t.ID;
                    ac.ModuleID = t.ModuleID;
                    result.Add(ac);
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return result;
        }
        //Role
        public static string addRole(RBACRole r)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_Role temp = new RBAC_Role();
                temp.RoleName = r.RoleName;
                _db.RBAC_Role.AddObject(temp);
                _db.SaveChanges();
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static string deleteRole(Int32 ID)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_Role temp = _db.RBAC_Role.FirstOrDefault(a => a.ID == ID);
                if (temp != null)
                {
                    _db.RBAC_Role.DeleteObject(temp);
                    _db.SaveChanges();
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static string updateRole(RBACRole r)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_Role temp = _db.RBAC_Role.FirstOrDefault(a => a.ID == r.ID);
                if (temp != null)
                {
                    temp.RoleName = r.RoleName;
                    _db.SaveChanges();
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static List<RBACRole> readRole()
        {
            List<RBACRole> result = new List<RBACRole>();
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                var temp = _db.RBAC_Role.ToList();
                foreach (var t in temp)
                {
                    RBACRole r = new RBACRole();
                    r.RoleName = t.RoleName;
                    r.ID = t.ID;
                    result.Add(r);
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return result;
        }
        //User
        public static string addUser(RBACUser u)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_User temp = new RBAC_User();
                temp.UserName = u.UserName;
                temp.Password = u.Password;
                temp.RoleID = u.RoleID;
                _db.RBAC_User.AddObject(temp);
                _db.SaveChanges();
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static string deleteUser(Int32 ID)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_User temp = _db.RBAC_User.FirstOrDefault(a => a.ID == ID);
                if (temp != null)
                {
                    _db.RBAC_User.DeleteObject(temp);
                    _db.SaveChanges();
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static string updateUser(RBACUser u)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_User temp = _db.RBAC_User.FirstOrDefault(a => a.ID == u.ID);
                if (temp != null)
                {
                    temp.UserName = u.UserName;
                    temp.Password = u.Password;
                    temp.RoleID = u.RoleID;
                    _db.SaveChanges();
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static List<RBACUser> readUser()
        {
            List<RBACUser> result = new List<RBACUser>();
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                var temp = _db.RBAC_User.ToList();
                foreach (var t in temp)
                {
                    RBACUser u = new RBACUser();
                    u.UserName = t.UserName;
                    u.ID = t.ID;
                    u.Password = t.Password;
                    u.RoleID = t.RoleID;
                    result.Add(u);
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return result;
        }
        //Rights
        public static string addRights(RBACRights r)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_Rights temp = new RBAC_Rights();
                temp.ActionID = r.ActionID;
                temp.Enable = r.Enable;
                temp.ModuleID = r.ModuleID;
                temp.RoleID = r.RoleID;
                _db.RBAC_Rights.AddObject(temp);
                _db.SaveChanges();
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static string deleteRights(Int32 ID)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_Rights temp = _db.RBAC_Rights.FirstOrDefault(a => a.ID == ID);
                if (temp != null)
                {
                    _db.RBAC_Rights.DeleteObject(temp);
                    _db.SaveChanges();
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static string updateRights(RBACRights r)
        {
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                RBAC_Rights temp = _db.RBAC_Rights.FirstOrDefault(a => a.ID == r.ID);
                if (temp != null)
                {
                    temp.ActionID = r.ActionID;
                    temp.Enable = r.Enable;
                    temp.ModuleID = r.ModuleID;
                    temp.RoleID = r.RoleID;
                    _db.SaveChanges();
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        public static List<RBACRights> readRights()
        {
            List<RBACRights> result = new List<RBACRights>();
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                var temp = _db.RBAC_Rights.ToList();
                foreach (var t in temp)
                {
                    RBACRights r = new RBACRights();
                    r.ActionID = t.ActionID;
                    r.Enable = t.Enable;
                    r.ModuleID = t.ModuleID;
                    r.RoleID = t.RoleID;
                    r.ID = t.ID;
                    result.Add(r);
                }
                _db.Dispose();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return result;
        }
    }

    public class RBACModule
    {
        public Int32 ID { get; set; }
        public string ModuleTitle { get; set; }
    }
    public class RBACRole
    {
        public Int32 ID { get; set; }
        public string RoleName { get; set; }
    }
    public class RBACUser
    {
        public Int32 ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Int32? RoleID { get; set; }
    }
    public class RBACAction
    {
        public Int32 ID { get; set; }
        public Int32? ModuleID { get; set; }
        public string ActionTitle { get; set; }
    }
    public class RBACRights
    {
        public Int32 ID { get; set; }
        public Int32? RoleID { get; set; }
        public Int32? ModuleID { get; set; }
        public Int32? ActionID { get; set; }
        public Int32? Enable { get; set; }
        public string role { get; set; }
        public string module { get; set; }
        public string action { get; set; }
    }
}
