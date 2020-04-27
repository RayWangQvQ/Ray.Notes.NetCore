using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ray.Infrastructure.Extensions.Json
{
    public class SettingOption
    {
        private JsonSerializerSettings _settings;

        public SettingOption(JsonSerializerSettings settings)
        {
            _settings = settings;
        }

        public SettingOption()
        {

        }

        /// <summary>
        /// 忽略或只保留部分属性
        /// </summary>
        public IgnoreOption IgnoreProps { get; set; }

        /// <summary>
        /// 忽略Null值
        /// </summary>
        public bool IgnoreNull { get; set; } = false;

        /// <summary>
        /// 枚举序列化为字符串
        /// </summary>
        public bool EnumToString { get; set; } = false;

        /// <summary>
        /// 构建
        /// </summary>
        /// <returns></returns>
        public JsonSerializerSettings BuildSettings()
        {
            if (_settings == null)
                _settings = new JsonSerializerSettings();

            //忽略null值
            if (IgnoreNull)
                _settings.NullValueHandling = NullValueHandling.Ignore;

            //忽略/只保留部分属性
            if (IgnoreProps != null)
                _settings.ContractResolver = new LimitPropsContractResolver(IgnoreProps);

            //枚举处理
            if (EnumToString)
                _settings.Converters.Add(new StringEnumConverter());

            return _settings;
        }
    }
}
