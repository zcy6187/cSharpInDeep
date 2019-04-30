using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AppConfigOper
{
    /// <summary>
    /// 继承自System.Configuration.ConfigurationSection的类，可以通过ConfigurationManger.GetSection方法读写
    /// </summary>
    class SimpleConfig : System.Configuration.ConfigurationSection
    {
        [ConfigurationProperty("maxValue", IsRequired = false, DefaultValue = Int32.MaxValue)]
        public int MaxValue
        {
            get
            {
                return (int)base["maxValue"];
            }
            set
            {
                base["maxValue"] = value;
            }
        }

        [ConfigurationProperty("minValue", IsRequired = false, DefaultValue = 1)]
        public int MinValue
        {
            get { return (int)base["minValue"]; }
            set { base["minValue"] = value; }
        }


        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = false)]
        public bool Enable
        {
            get
            {
                return (bool)base["enabled"];
            }
            set
            {
                base["enabled"] = value;
            }
        }
    }
}
