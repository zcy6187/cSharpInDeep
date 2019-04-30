using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ReflectionAndAttribute.Typer
{
    class BaseClass
    {
        public int BaseFiled = 0;

        public string BaseProperty { get; set; }

        public void BaseMethod()
        {
            Console.WriteLine("BaseMehtod");
        }

        private void PrivateBaseMethod()
        {
            Console.WriteLine("PrivateBaseMehtod");
        }
    }

    interface ISoftDelete
    {
        void Delete();
    }

    class DerivedClass : BaseClass,ISoftDelete
    {
        public int DerivedFiled = 2;

        public string DerivedProperty { get; set; }

        public void DerivedMethod()
        {
            Console.WriteLine("DerivedMehtod");
        }

        private void PrivateDerivedMethod()
        {
            Console.WriteLine("PrivateDerivedMethod");
        }
        public void Delete()
        {
            Console.WriteLine("SoftDeleted");
        }
    }

    class Typer
    {
        public static void BaseTester()
        {
            Type tt1 = typeof(string);
            Console.WriteLine("{0}", tt1.Name); // String 
            Console.WriteLine("{0}", tt1.FullName); // System.String 
            Type tt2 = typeof(System.String);
            Console.WriteLine("{0}", tt2.Name); // String
            Console.WriteLine("{0}", tt1 == tt2); // True

            /* GetType typeof */
            Type tt3 = typeof(BaseClass);
            Console.WriteLine("{0}", tt3.Name); // BaseClass
            var bc = new BaseClass();
            Console.WriteLine("{0}", bc.GetType().Name); // BaseClass
            Console.WriteLine("{0}", bc.GetType().FullName); // ReflectionAndAttribute.BaseClass

            Type tt4 = typeof(int);
            Console.WriteLine("{0}", tt4.Name); // Int32
            Type type1 = System.Type.GetType("System.Int32");
            Console.WriteLine(type1.Name); // Int32

        }

        public static void InheritedTester()
        {
            DerivedClass deriver = new DerivedClass();
            var isInterface=deriver.GetType().IsSubclassOf(typeof(ISoftDelete)); // false
            Console.WriteLine("isSubclassOf: {0}",isInterface);
            var isAssignableFrom = typeof(ISoftDelete).IsAssignableFrom(deriver.GetType()); // true
            Console.WriteLine("IsAssignableFrom: {0}", isAssignableFrom);

            var isClass = deriver.GetType().IsSubclassOf(typeof(BaseClass)); // true
            Console.WriteLine("isClass: {0}", isClass);
            isAssignableFrom = typeof(BaseClass).IsAssignableFrom(deriver.GetType()); // true
            Console.WriteLine("IsAssignableFrom: {0}", isAssignableFrom);


        }

        public static void ClassTester()
        {
            var bc = new BaseClass()
            {
                BaseProperty = "bc"
            };

            var dc = new DerivedClass()
            {
                BaseProperty = "dc",
                DerivedProperty = "derivedDc"
            };


            BaseClass[] bca = new BaseClass[] { bc, dc };
            foreach (var item in bca)
            {
                Type t = item.GetType();
                Console.WriteLine("Object Type: {0}", t.Name);

                FieldInfo[] fi = t.GetFields();
                foreach (var f in fi)
                {
                    Console.WriteLine("     Field: {0}", f.Name);
                }

                MethodInfo[] mi = t.GetMethods();
                foreach (var m in mi)
                {
                    Console.WriteLine("     Method: {0}", m.ToString());
                }

                PropertyInfo[] pi = t.GetProperties();
                Console.WriteLine(pi.Length);
                foreach (var p in pi)
                {
                    Console.WriteLine("      PropertyName: {0} ,PropertyValue: {1}", p.Name, p.GetValue(item));
                }
            }

            /* 结果：
             Object Type: BaseClass
            Field: BaseFiled
            Method: System.String get_BaseProperty()
            Method: Void set_BaseProperty(System.String)
            Method: Void BaseMethod()
            Method: System.String ToString()
            Method: Boolean Equals(System.Object)
            Method: Int32 GetHashCode()
            Method: System.Type GetType()
    1
            PropertyName: BaseProperty ,PropertyValue: bc
    Object Type: DerivedClass
            Field: DerivedFiled
            Field: BaseFiled
            Method: System.String get_DerivedProperty()
            Method: Void set_DerivedProperty(System.String)
            Method: Void DerivedMethod()
            Method: System.String get_BaseProperty()
            Method: Void set_BaseProperty(System.String)
            Method: Void BaseMethod()
            Method: System.String ToString()
            Method: Boolean Equals(System.Object)
            Method: Int32 GetHashCode()
            Method: System.Type GetType()
    2
            PropertyName: DerivedProperty ,PropertyValue: derivedDc
            PropertyName: BaseProperty ,PropertyValue: dc


               说明：
               只能获得public类型的字段和方法，不能获取private型的。
            */
        }

        public static void GetGenericTypeDefinition()
        {     
            Dictionary<string, Typer> d = new Dictionary<string, Typer>();

            Type constructed = d.GetType(); // 普通的Type反射
            DisplayTypeInfo(constructed);

            /*
System.Collections.Generic.Dictionary`2[System.String,ReflectionAndAttribute.Typer]
         IsGenericTypeDefinition: False
         IsGenericType：True
         ListOfTypeArguments (2):
                System.String
                ReflectionAndAttribute.Typer
           */

            Type generic = constructed.GetGenericTypeDefinition(); // 获取该类的泛型定义类型
            DisplayTypeInfo(generic);

            /*
System.Collections.Generic.Dictionary`2[TKey,TValue]
        IsGenericTypeDefinition: True
        IsGenericType：True
        ListOfTypeArguments (2):
             TKey
             TValue
         */
        }

        private static void DisplayTypeInfo(Type t)
        {
            Console.WriteLine("\r\n{0}", t);
            Console.WriteLine("\t IsGenericTypeDefinition: {0}",
                t.IsGenericTypeDefinition); // 是不是泛型定义类型
            Console.WriteLine("\t IsGenericType：{0}",
                t.IsGenericType);
            Type[] typeArguments = t.GetGenericArguments(); 
            Console.WriteLine("\t ListOfTypeArguments ({0}):", typeArguments.Length);
            foreach (Type tParam in typeArguments)
            {
                Console.WriteLine("\t\t{0}", tParam);
            }
        }
    }
}
