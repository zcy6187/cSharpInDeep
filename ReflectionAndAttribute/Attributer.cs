using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace ReflectionAndAttribute
{
   class Attributer
    {
        public static void Tester()
        {
            AnyClass anyer=new AnyClass();
            var tt = anyer.GetType();

            var customArray11=tt.GetCustomAttributes(false); // 如果为true，则会按照继承路线获取所有的Attributes。
            foreach (var item in customArray11)
            {
                if (item.GetType() == typeof(HelpAttribute))
                {
                    Console.WriteLine(((HelpAttribute)item).Description); // AnyClass
                }
                Console.WriteLine(item.ToString()); // ReflectionAndAttribute.HelpAttribute
            }

            // 与GetCustomAttributes作用类似，但GetCustomAttributes可以通过参数获得更细微的数据
            var customArray22 = tt.GetCustomAttributesData();
            foreach (var item in customArray22)
            {
                Console.WriteLine(item.AttributeType.FullName); // ReflectionAndAttribute.HelpAttribute
            }
        }

        // 遍历一个类的所有特性，并使用特性的值
        public static void TraversalAttributes()
        {
            // 以类名获取所有特性
            var attrArray=System.Attribute.GetCustomAttributes(typeof(AnyClass2));
            foreach (var item in attrArray)
            {
                if (item is HelpAttribute)
                {
                    HelpAttribute a = (HelpAttribute)item;
                    Console.WriteLine("{0}",a.Version); // 2
                }

            }

            // 特性可以加在方法、属性、字段上，可以按需在这些信息上获取特性信息。
            PropertyInfo[] propertys = typeof(AnyClass).GetProperties();//返回AnyClass的所有公共属性
            if (propertys != null && propertys.Length > 0)
            {
                foreach (PropertyInfo p in propertys)
                {
                    object[] objAttrs = p.GetCustomAttributes(typeof(ColumnAttribute), true);//获取自定义特性
                    //GetCustomAttributes(要搜索的特性类型，是否搜索该成员的继承链以查找这些特性)
                    if (objAttrs != null && objAttrs.Length > 0)
                    {
                        ColumnAttribute attr = objAttrs[0] as ColumnAttribute;
                        Console.WriteLine("自定义特性Name：" + p.Name + ", 元数据：" + attr);
                    }
                };
            }
        }
    }

    public class HelpAttribute : Attribute
    {
        public string Description { get; }
        public double Version { get; set; }

        public HelpAttribute(string desption_in)
        {
            this.Description = desption_in;
            this.Version = 1.0;
        }
    }


    [Help("AnyClass")]
    public class AnyClass
    {
        public string ClassName = "FirstClass";
    }

    [Help("Help2", Version = 2.0)]
    public class AnyClass2
    {
        public string ClassName = "SecondClass";
    }
}
