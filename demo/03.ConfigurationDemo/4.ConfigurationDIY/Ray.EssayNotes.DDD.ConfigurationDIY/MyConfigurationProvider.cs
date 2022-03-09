using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Microsoft.Extensions.Configuration;

namespace Ray.EssayNotes.DDD.ConfigurationDIY
{
    public class MyConfigurationProvider : ConfigurationProvider
    {
        /// <summary>
        /// 计时器
        /// </summary>
        private readonly Timer _timer;

        public MyConfigurationProvider()
        {
            _timer = new Timer();
            _timer.Elapsed += Timer_Elapsed;//执行事件
            _timer.Interval = 3000;//每3s执行一次
            _timer.Start();//开始计时
        }

        /// <summary>
        /// 首次加载数据集
        /// </summary>
        public override void Load()
        {
            //加载数据
            Load(false);
        }

        /// <summary>
        /// 加载数据集到DataDic
        /// </summary>
        /// <param name="reload">是否需要触发重新加载</param>
        private void Load(bool reload)
        {
            this.Data["lastTime"] = DateTime.Now.ToString();
            if (reload)
            {
                base.OnReload();
            }
        }

        /// <summary>
        /// 定时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Load(true);
        }
    }
}
