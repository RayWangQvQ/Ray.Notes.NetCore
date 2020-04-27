using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.Extensions.Json
{
    public class IgnoreOption
    {
        public LimitPropsEnum LimitPropsEnum { get; set; } = LimitPropsEnum.Ignore;

        public string[] Props { get; set; } = { };
    }
}
