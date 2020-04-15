using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    public abstract class TestBase
    {
        /// <summary>
        /// 初始化配置树
        /// </summary>
        public abstract void InitConfiguration();

        /// <summary>
        /// 初始化依赖注入容器
        /// </summary>
        public abstract void InitServiceProvider();

        public abstract void Print();

        public virtual void Run()
        {
            InitConfiguration();
            InitServiceProvider();
            Print();
        }
    }
}
