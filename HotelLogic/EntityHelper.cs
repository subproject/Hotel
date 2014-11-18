using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;

namespace HotelLogic
{
    public class EntityHelper
    {
        /// <summary>
        /// 从DataRow获取对象实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="dr">DataRow数据</param>
        /// <returns>实例</returns>
        public static T GetEntityFromDataRow<T>(DataRow dr) where T : new()
        {
            T entity = new T();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo item in properties)
            {
                try
                {
                    if (!dr.IsNull(item.Name) && dr[item.Name] != System.DBNull.Value)
                    {
                        item.SetValue(entity, dr[item.Name], null);
                    }
                }
                catch
                {
                    // Do nothing.
                }
            }

            return entity;
        }

        /// <summary>
        /// 从DataRow填充对象实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="entity">对象实例</param>
        /// <param name="dr">数据</param>
        /// <returns>实例</returns>
        public static T FillEntityFromDataRow<T>(T entity, DataRow dr) where T : new()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo item in properties)
            {
                try
                {
                    if (dr[item.Name] != null && dr[item.Name] != System.DBNull.Value)
                    {
                        item.SetValue(entity, dr[item.Name], null);
                    }
                }
                catch
                {
                    // Do nothing.
                }
            }

            return entity;
        }

        /// <summary>
        /// 从对象实例中得到sql参数。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="entity">实例</param>
        /// <returns>sql 参数。</returns>
        public static SqlParameter[] GetSqlParameterByEntity<T>(T entity)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            int count = properties.Count();

            SqlParameter[] parameters = new SqlParameter[count];

            for (int i = 0; i < count; i++)
            {
                parameters[i] = new SqlParameter(("@" + properties[i].Name), properties[i].GetValue(entity, null));
            }

            return parameters;
        }
        /// <summary>
        /// 根据实体对象生成SQL参数变量列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SqlParameter[] GetSqlParameterByEntity_B<T>(T entity)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            int count = properties.Count();

            SqlParameter[] parameters = new SqlParameter[count];

            for (int i = 0; i < count; i++)
            {
                if (properties[i].GetValue(entity, null) != null)
                {
                    if (properties[i].GetValue(entity, null).ToString() == "0001/1/1 0:00:00")
                    {
                        if (properties[i].Name == "CreateTime")
                        {
                            parameters[i] = new SqlParameter(("@" + properties[i].Name), "GetDate()");
                        }
                        else
                        {
                            parameters[i] = new SqlParameter(("@" + properties[i].Name), null);
                        }
                    }
                    else
                    {
                        parameters[i] = new SqlParameter(("@" + properties[i].Name), properties[i].GetValue(entity, null));
                    }
                }
                else
                {
                    parameters[i] = new SqlParameter(("@" + properties[i].Name), properties[i].GetValue(entity, null));
                }
            }

            return parameters;
        }
        /// <summary>
        /// 判断赋值
        /// </summary>
        /// <param name="objecttype"></param>
        /// <param name="typename"></param>
        /// <param name="fieldname"></param>
        /// <param name="fieldvalue"></param>
        /// <param name="DataObject"></param>
        private static void SetValueToEntityValue(Type objecttype, string fieldname, string fieldvalue, object DataObject)
        {
            string typename = objecttype.GetProperty(fieldname).PropertyType.ToString();
            if (fieldvalue != "undefined")
            {
                switch (typename)
                {
                    case "System.String":
                        {
                            objecttype.GetProperty(fieldname).SetValue(DataObject, fieldvalue, null);

                        }; break;
                    case "System.DateTime":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, DateTime.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, null, null);
                            }

                        }; break;
                    case "System.Nullable`1[System.DateTime]":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, DateTime.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, null, null);
                            }

                        }; break;
                    case "System.Int32":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, int.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, int.Parse("0"), null);
                            }
                        }; break;
                    case "System.Nullable`1[System.Int32]":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, int.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, int.Parse("0"), null);
                            }
                        }; break;
                    case "System.Int":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, int.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, int.Parse("0"), null);
                            }
                        }; break;
                    case "System.Nullable`1[System.Int]":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, int.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, int.Parse("0"), null);
                            }
                        }; break;
                    case "System.Double":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, Double.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, Double.Parse("0"), null);
                            }
                        }; break;
                    case "System.Nullable`1[System.Double]":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, Double.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, Double.Parse("0"), null);
                            }
                        }; break;
                    case "System.Decimal":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, decimal.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, decimal.Parse("0.00"), null);
                            }
                        }; break;
                    case "System.Nullable`1[System.Decimal]":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, decimal.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, decimal.Parse("0.00"), null);
                            }
                        }; break;
                    case "System.Boolean":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, bool.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, false, null);
                            }
                        }; break;
                    case "System.Nullable`1[System.Boolean]":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, bool.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, false, null);
                            }
                        }; break;
                    case "System.Guid":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, Guid.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, Guid.Empty, null);
                            }
                        };
                        break;
                    case "System.Nullable`1[System.Guid]":
                        {
                            try
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, Guid.Parse(fieldvalue), null);
                            }
                            catch
                            {
                                objecttype.GetProperty(fieldname).SetValue(DataObject, Guid.Empty, null);
                            }
                        };
                        break;
                }
            }
            else
            {
                objecttype.GetProperty(fieldname).SetValue(DataObject, null, null);
            }
        }
        // <summary>
        /// 将Form数据转为相应的实体对象
        /// </summary>
        /// <param name="DataObject"></param>
        /// <param name="DataForm"></param>
        public static void FormDataToDataObject(object DataObject, NameValueCollection DataForm)
        {
            Type objecttype = DataObject.GetType();
            foreach (string fieldname in DataForm)
            {
                if (objecttype.GetProperty(fieldname) != null)
                {

                    // var fieldvalueq = DataForm[fieldname];
                    SetValueToEntityValue(objecttype, fieldname, DataForm[fieldname], DataObject);
                    //objecttype.GetProperty(fieldname).SetValue(DataObject, DataForm[fieldname], null);

                }
            }
        }
        /// <summary>
        /// 将一个实体类的值根据相同的属性名赋于另一个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="NEntiny"></param>
        /// <param name="SEntiny"></param>
        public static void ConverToEntity<T, T1>(T NEntiny, T1 SEntiny)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            Type stype = typeof(T1);
            PropertyInfo[] sproperties = stype.GetProperties();
            foreach (PropertyInfo item in properties)
            {
                try
                {
                    item.SetValue(NEntiny, stype.GetProperty(item.Name).GetValue(SEntiny, null), null);
                }
                catch
                {
                    // Do nothing.
                }
            }
        }

        /// <summary>
        /// 实体函数从JSON字符串反序列化
        /// </summary>
        /// <param name="jsonPageParams"></param>
        /// <returns></returns>
        public static List<T> GetEntityInfoFromJsonPage<T>(string jsonPageParams) where T : new()
        {
            var TempEntity = new System.Web.Script.Serialization.JavaScriptSerializer().DeserializeObject(jsonPageParams);

            string typename = TempEntity.ToString();
            List<T> EntityList = new List<T>();
            if (typename == "System.Object[]")
            {
                foreach (var itemarray in (object[])TempEntity)
                {
                    T NewEntity = new T();
                    Type objecttype = typeof(T);
                    foreach (var item in (Dictionary<string, object>)itemarray)
                    {
                        SetValueToEntityValue(objecttype, item.Key, (item.Value == null ? null : item.Value.ToString()), NewEntity);
                        // objecttype.GetProperty(item.Key).SetValue(NewEntity, item.Value, null);
                    }
                    EntityList.Add(NewEntity);
                }
            }
            else
            {
                foreach (var itemarray in (Dictionary<string, object>)TempEntity)
                {
                    T NewEntity = new T();
                    Type objecttype = typeof(T);
                    foreach (var item in (Dictionary<string, object>)itemarray.Value)
                    {
                        SetValueToEntityValue(objecttype, item.Key, (item.Value == null ? null : item.Value.ToString()), NewEntity);
                        // objecttype.GetProperty(item.Key).SetValue(NewEntity, item.Value, null);
                    }
                    EntityList.Add(NewEntity);
                }
            }
            return EntityList;

        }
    }
}
