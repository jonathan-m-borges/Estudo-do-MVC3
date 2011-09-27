using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AdventureWorks_MVC.Base.Binder;

namespace AdventureWorks_MVC.Base.Model
{
    [ModelBinder(typeof(EntidadeModelBinder))]
    public class Entidade : ICloneable
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }


        //public PropertyInfo[] GetPropertyForeignKeys()
        //{
        //    var types = new List<PropertyInfo>();
        //    foreach (PropertyInfo prop in GetType().GetProperties())
        //    {
        //        if (prop.PropertyType == typeof(Entidade))
        //        {
        //            types.Add(prop);
        //        }
        //    }
        //    return types.ToArray();
        //}

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}