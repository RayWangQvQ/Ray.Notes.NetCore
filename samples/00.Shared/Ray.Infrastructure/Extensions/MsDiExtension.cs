using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Ray.Infrastructure.Extensions
{
    public static class MsDiExtension
    {
        /// <summary>
        /// 容器中可释放的
        /// </summary>
        private static string _disposableFiledName = "_disposables";
        private static string _resolvedServicesPropertyName = "ResolvedServices";

        /// <summary>
        /// 获取容器内的可释放实例池中的实例名称集合
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetDisposableCoponentNamesFromScope(this IServiceProvider serviceProvider)
        {
            var result = ((IEnumerable<object>)serviceProvider.GetFieldValue(_disposableFiledName))
                .Select(x => x.ToString());

            return result ?? new List<string>();
        }

        /// <summary>
        /// 获取容器内的实例池中已持久化的实例名称集合
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetInstanceNamesFromScope(this IServiceProvider serviceProvider)
        {
            var dic = new Dictionary<string, string>();

            var result = serviceProvider.GetPropertyValue(_resolvedServicesPropertyName);

            //var re= result as Dictionary<object,object>;
            //var re = (IEnumerable<KeyValuePair<object, object>>)result;

            Console.WriteLine(JsonConvert.SerializeObject(result));

            return dic;
        }
    }
}
