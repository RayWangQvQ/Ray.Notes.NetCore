using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ray.Infrastructure.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 利用反射获取实例的某个字段值
        /// （包括私有变量）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fielName"></param>
        /// <returns></returns>
        public static T GetFieldValue<T>(this object obj, string fielName)
        {
            try
            {
                Type type = obj.GetType();
                FieldInfo fieldInfo = type.GetFields(BindingFlags.NonPublic
                    | BindingFlags.Public
                    | BindingFlags.Instance
                    | BindingFlags.DeclaredOnly
                    | BindingFlags.Static)
                    .FirstOrDefault(x => x.Name == fielName);
                T value = (T)fieldInfo?.GetValue(obj);
                return value;
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 利用反射获取实例的某个属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static dynamic GetPropertyValue(this object obj, string fieldName)
        {
            try
            {
                Type Ts = obj.GetType();
                var pi = Ts.GetProperty(fieldName, BindingFlags.NonPublic
                    | BindingFlags.Public
                    | BindingFlags.Instance
                    | BindingFlags.DeclaredOnly
                    | BindingFlags.Static);
                dynamic o = pi.GetValue(obj, null);
                return o;
            }
            catch
            {
                return default;
            }
        }
    }
}
