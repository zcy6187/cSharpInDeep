using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReflectionAndAttribute
{
    class Reflections
    {
        public static void VisitPublics()
        {
            Type t = typeof(RefClass);
            Func<MemberTypes, String> getType = (x) =>
            {
                switch (x)
                {
                    case MemberTypes.Field:
                        {
                            return "字段";
                        }
                    case MemberTypes.Method:
                        {
                            return "方法";
                        }
                    case MemberTypes.Property:
                        {
                            return "属性";
                        }
                    default:
                        {
                            return "未知";
                        }
                }
            };
            // MemberInfo[] minfos = t.GetMembers();  //所有public成员（属性、字段、方法）,包括继承得到的
            MemberInfo[] minfos = t.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public); // 获取所有的成员，包括public/nonpublic/
            foreach (MemberInfo minfo in minfos)
            {
                Console.WriteLine("名称：{0},类型：{1}", minfo.Name, getType(minfo.MemberType));
            }
        }

        public static void OperProperty()
        {
            Type t = typeof(RefClass);
            RefClass rc = new RefClass();
            rc.Test3 = 3;
            PropertyInfo[] finfos = t.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (PropertyInfo finfo in finfos)
            {
                MethodInfo getinfo = finfo.GetGetMethod(true);
                Console.WriteLine("get方法的名称{0}  返回值类型:{1}  参数数量:{2}  MSIL代码长度:{3} 局部变量数量:{4}", 
                    getinfo.Name, getinfo.ReturnType.ToString(),
                    getinfo.GetParameters().Count(),
                    getinfo.GetMethodBody().GetILAsByteArray().Length,
                    getinfo.GetMethodBody().LocalVariables.Count);

                MethodInfo setinfo = finfo.GetSetMethod(true);
                Console.WriteLine("get方法的名称{0}  返回值类型:{1}  参数数量:{2}  MSIL代码长度:{3} 局部变量数量:{4}", 
                    setinfo.Name, setinfo.ReturnType.ToString(),
                    setinfo.GetParameters().Count(),
                    setinfo.GetMethodBody().GetILAsByteArray().Length,
                    setinfo.GetMethodBody().LocalVariables.Count);

                setinfo.Invoke(rc, new object[] { 123 });
                object obj = getinfo.Invoke(rc, null);
                Console.WriteLine("属性名:{0}  内部值:{1}", finfo.Name, obj);
                Console.WriteLine("----------------------------------");
            }
            Console.ReadKey();
        }
    }

    public class RefClass
    {
        private int _test3;
        private int _test1 { get; set; }
        protected int Test2 { get; set; }
        public int Test3 { get; set; }

        private static void Show2()
        {
        }

        public static void Show3()
        {
        }

        public void Show()
        {

        }
    }
}
