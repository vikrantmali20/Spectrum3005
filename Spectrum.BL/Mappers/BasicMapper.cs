using System;
using System.Collections.Generic;
using System.Reflection;
using Spectrum.Models;
using System.ComponentModel;
using System.Data;

namespace Spectrum.BL.Mappers
{
    public static class BasicMapper
    {
        public static object ToAddOrModifyEntity(this object obj, bool isNew)
        {
            if (isNew)
            {
                obj.GetType().GetProperty("CreatedAt", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, CommonModel.SiteCode, null);
                obj.GetType().GetProperty("CreatedBy", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, CommonModel.UserID, null);
                obj.GetType().GetProperty("CreatedOn", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, CommonModel.CurrentDate, null);

                if (obj.GetType().GetProperty("Status", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).Name.ToString().ToUpper().Equals("Status".ToString().ToUpper()))
                {
                    obj.GetType().GetProperty("Status", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, true, null);
                }

            }

            obj.GetType().GetProperty("UpdatedAt", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, CommonModel.SiteCode, null);
            obj.GetType().GetProperty("UpdatedBy", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, CommonModel.UserID, null);
            obj.GetType().GetProperty("UpdatedOn", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, CommonModel.CurrentDate, null);

            return obj;
        }

        public static object ToModel<T>(this object obj, T type)
        {
            string modelName = type.ToString();
            Type modelType = Type.GetType(modelName);

            var tmp = Activator.CreateInstance(modelType);

            //var tmp = Activator.CreateInstance(Type.GetType(type.ToString()));

            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                try
                {
                    tmp.GetType().GetProperty(pi.Name).SetValue(tmp,
                                              pi.GetValue(obj, null), null);
                }
                catch { }
            }

            return tmp;
        }

        public static object ToModels<T>(this IList<T> list, Type t)
        {
            try
            {
                var genericType = typeof(List<>).MakeGenericType(t);

                var l = Activator.CreateInstance(genericType);

                MethodInfo addMethod = l.GetType().GetMethod("Add");

                foreach (T item in list)
                {
                    addMethod.Invoke(l, new object[] { item.ToModel(t) });
                }

                return l;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);
            }
            return table;
        }

        public static object ToRetaibEntityCreateDetails(this object obj, object entity)
        {
            string siteCode =
                (string) entity.GetType()
                    .GetProperty("CreatedAt", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                    .GetValue(entity,null);
            
            string userID =
               (string)entity.GetType()
                   .GetProperty("CreatedBy", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                   .GetValue(entity, null);

            DateTime currentDate = (DateTime)entity.GetType()
                   .GetProperty("CreatedOn", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                   .GetValue(entity, null);

            obj.GetType().GetProperty("CreatedAt", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, siteCode, null);
            obj.GetType().GetProperty("CreatedBy", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, userID, null);
            obj.GetType().GetProperty("CreatedOn", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, currentDate, null);

            return obj;
        }

    }
}
