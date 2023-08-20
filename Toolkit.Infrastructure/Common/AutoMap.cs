using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Infrastructure.Common
{
    public static class AutoMap
    {
        public static Target MemberMap<Source, Target>(Source src) where Target : new()
        {
            Type Ttype = typeof(Target);
            Type Stype = typeof(Source);

            Target result = (Target)Activator.CreateInstance(Ttype);

            var properties = Stype.GetRuntimeProperties();

            foreach (var property in properties)
            {
                string peopertyName = property.Name;
                PropertyInfo propertyInfo = Ttype.GetRuntimeProperty(peopertyName);
                if (propertyInfo != null && property.CanRead && property.CanWrite)
                {
                    propertyInfo.SetValue(result, property.GetValue(src, null), null);
                }
            }

            return result;
        }

        public static Target MemberMap<Source, Target, Inherit>(Source src) where Source : Inherit where Target : Inherit ,new() where Inherit : class
        {
            var a = new Target();
            a = (Target)(src as Inherit);
            return a;
            Type Ttype = typeof(Target);
            Type Itype = typeof(Inherit);

            Target result = (Target)Activator.CreateInstance(Ttype);

            PropertyInfo[] properties = (PropertyInfo[])Itype.GetProperties();//(PropertyInfo[])Itype.GetRuntimeProperties();

            foreach (var property in properties)
            {
                string peopertyName = property.Name;
                PropertyInfo propertyInfo = Ttype.GetRuntimeProperty(peopertyName);
                if (propertyInfo != null && property.CanRead && property.CanWrite)
                {
                    propertyInfo.SetValue(result, property.GetValue(src, null), null);
                }
            }

            return result;
        }
    }
}