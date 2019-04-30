using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ReflectionAndAttribute
{
    class Dynamicer
    {
        //var stradsa = "asdasdas";   //报错
        static void MainTester()
        {
            #region var 接收一种类型后 在接受其他类型会报错  编译时解析   编译器会在编译过程中帮我们创建一个具有相关属性和方法的类型。 类型由赋于的值来决定
            //还有一点 var 仅能声明方法内的局部变量(只能存在方法体里面 外面就报错)
            var a123 = "123";
            Console.WriteLine(a123);
            //a123 = 456;    //报错  错误    1    无法将类型“int”隐式转换为“string”    
            //var 声明的变量在被赋值后类型即确定下了，后续程序中不能在赋其他类型的值
            var xwyy = new object();//没有意义 不要写这样的代码
            #endregion


            #region var 匿名类型集合  通过反射读取object 对应的属性名
            //var xss = new { name = "a", Id = 1 };
            //var list = (new int[] { 1 }).Select(x => new { Name = "张三", Num = 123 }).ToList();

            var listss = new List<object>();
            listss.Add(new { BranchName = "A01", Address = "人民路108号", Tel = "66666666" });
            listss.Add(new { BranchName = "A02", Address = "天津路888号", Tel = "88888888" });
            bool ishave = false;
            foreach (var item in listss)
            {
                PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(item);
                PropertyDescriptor pdID = pdc.Find("Address", true);
                var x = pdID.GetValue(item);
                Console.WriteLine("xxxx  " + x);
                PropertyDescriptor pdName = pdc.Find("Tel", true);
                var z = pdName.GetValue(item);
                Console.WriteLine("zzzz  " + z);
            }
            #endregion



            #region dynamic  接收一种类型后 接受其他类型不会报错  运行时解析
            //随用随定义，用完就消失
            dynamic b123 = "123";
            Console.WriteLine(b123);
            b123 = 123;
            Console.WriteLine(b123);
            b123 = new { name = "123", Id = "123" };  // 正常运行
            Console.WriteLine(b123.Id);
            #endregion

            #region object 接收任意类型 但取值 要转换为 对应类型 object对象无法 解析 定义成员
            object c123 = "123";
            c123 = 123;
            c123 = new { name = "obj", Id = "123" }; // 可以赋值，但无法取出数值
            //Console.WriteLine(c123.name);  //报错  错误    1    “object”不包含“name”的定义，并且找不到可接受类型为“object”的第一个参数的扩展方法“name”(是否缺少 using 指令或程序集引用?)    
            //从object 取出 Id name  就要用  dynamic
            dynamic dyc123 = c123;
            Console.WriteLine(dyc123.name);
            #endregion

            #region var 可以解析 详情 可看  .NET中那些所谓的新语法之二：匿名类、匿名方法与扩展方法
            // 反编译为 泛型类  类似  <>f__AnonymousType0<<ID>j__TPar, <Name>j__TPar, <Age>j__TPar>
            // 属性只读  重写了3个基类方法 Equals GetHashCode Tostring
            //匿名类共享 符合一定条件则可以共享一个泛型类 
            //属性类型和顺序都一致，那么默认共享前一个泛型类
            //属性名称和顺序一致，但属性类型不同，那么还是共同使用一个泛型类，只是泛型参数改变了而已，所以在运行时会生成不同的对象：
            //数据型名称和类型相同，但顺序不同，那么编译器会重新创建一个匿名类
            var annoyCla1 = new
            {
                ID = 10010,
                Name = "EdisonChou",
                Age = 25
            };
            Console.WriteLine(annoyCla1.Name);
            #endregion

            #region dynamic 可读可写
            dynamic expando = new System.Dynamic.ExpandoObject(); //动态类型字段 可读可写
            expando.Id = 1;
            expando.Name = "Test";
            string json = JsonConvert.SerializeObject(expando);  //输出{Id:1,Name:'Test'}
            #endregion

            #region 动态添加字段
            Console.WriteLine("动态添加字段");
            List<string> fieldList = new List<string>() { "Name", "Age", "Sex" }; //From config or db
            dynamic dobj = new System.Dynamic.ExpandoObject();
            var dic = (IDictionary<string, object>)dobj;
            foreach (var fieldItem in fieldList)
            {
                dic[fieldItem] = "set " + fieldItem + " value";  /*实现类似js里的 动态添加属性的功能
                                                                        var obj={};
                                                                        var field="Id";
                                                                        eval("obj."+field+"=1");
                                                                        alert(obj.Id); //1  */
            }
            var val = dobj.Name; //“set Name value”
            var val1 = dobj.Age;//”set Age value“
            #endregion

            #region 枚举该对象所有成员
            Console.WriteLine("枚举该对象所有成员");
            foreach (var fieldItem in (IDictionary<String, Object>)dobj)
            {
                var v = (fieldItem.Key + ": " + fieldItem.Value);
                Console.WriteLine(v);
            }
            #endregion

            #region 匿名类
            Console.WriteLine("匿名类");
            //var aaaa = new {Id=1,Name="Test"}; //匿名类字段只读不可写
            // 切记 就算 用运行解析装起来 也不行 还是表示 匿名类型
            dynamic aaaa = new { Id = 1, Name = "Test" }; //匿名类字段只读不可写
            //aaaa.Id = 2; //不可写 报错无法为属性或索引器“<>f__AnonymousType1<int>.Id”赋值 - 它是只读的
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject("{Name:'aa',Sex:'Male',Age:11}");
            var name = obj.Name.Value; //aa
            Console.WriteLine(name);
            #endregion

            #region 返回一个匿名对象 方法返回值 类型无法确定 方法返回的对象不确定怎么办 返回object ? 返回dynamic
            var xs1 = GetObject();
            //xs1.Name;  //object点不出来 要用反射
            //此时方法不会出现语法错误，程序可以成功编译并执行
            //dynamic 表示动态类型，动态类型的含义就是 程序编写、编译阶段 类型不确定，在Runtime时再通过反射机制确定相关对象的属性或方法。因此编写阶段 不会进行语法检测。
            //dynamic 可用来声明 字段、属性、方法参数、方法返回值
            //dynamic 不支持智能提示，因为你写代码时 dynamic  是什么没法知晓（反射）
            //dynamic 声明的变量，可理解为 object 类型变量。所以给dynamic变量赋任何类型值都正确，但在使用变量来取得某个属性值或调用某方法时（此时程序肯定处于Runtime状态），CLR会检查（反射）所调用的属性或方法是否存在，不存在报运行时异常。
            //dynamic在 Asp.net Mvc web开发中处处使用，虽然看上去很复杂，本质就上面所说内容。
            //                                  var    dynamic
            //          声明字段             ×         √
            //          局部变量            √       √
            //          方法参数类型    ×         √
            //          方法返回值类型     ×        √
            Console.WriteLine(xs1.Name);  // dynamic 就很方便
            #endregion

            #region var关键字 静态类型语言也被称为强类型语言 就是在编译过程中执行类型检查的语言
            //间接 定义数据类型的方式
            string str001 = "Test";// 这种写法被称为显式类型的声明，也被称为直接声明。
            var str = "Test";//这被称为隐式类型声明或间接类型声明。
            //编译器会在编译过程中验证数据，并在编译过程中创建适当的类型。在此实例中，编译器将检查Test，并在生成IL代码时将var关键字替换为字符串类型。
            //var关键字在编译时静态的定义数据类型，而不是在运行时，即：一旦定义了数据类型，它将不会在运行时更改。
            var str002 = "Test";
            //str002 = 123;  //编译错误     隐式转换抛出编译错误
            //对于像int、double、string等简单数据类型，我们最好使用特定的数据类型来声明变量，以免让事情变得复杂。但当你创建了一个很大的类，那么为了方便创建对象，你最好使用var关键字。还有一种约定俗成的习惯，当我们使用LINQ或Anonymous类型时，一般使用var关键字。
            #endregion

            #region 动态类型语言  运行时执行类型检查的语言。如果您不知道您将获得或需要分配的值的类型，则在此情况下，类型是在运行时定义的
            dynamic str0002 = "Test";
            //在为 str0002 分配值之后，如果执行一些数学运算，它不会给出任何错误信息。
            str0002++;   // 编译通过 生成也成功 但是，如果你运行这个应用程序，对不起，VS会给你如下所示的运行时错误：
            //dynamic关键字内部使用反射，感兴趣的童鞋可以自行研究一下。


            //            var和dynamic关键字之间的主要区别在于绑定时间不一样：var是早期绑定，dynamic绑定则会在运行时进行。
            //var实际上是编译器抛给我们的语法糖，一旦被编译，编译器就会自动匹配var变量的实际类型，并用实际类型来替换该变量的声明，等同于我们在编码时使用了实际类型声明。而dynamic被编译后是一个Object类型，编译器编译时不会对dynamic进行类型检查。
            //.Net 4.0之前的运行时的动态调用一般是通过反射来实现，但是反射的代码的可读性不高。.Net 4.0之后使用dynamic就好得多，因为dynamic是一种静态类型，完全可以像其它类型一样的声明和调用，而不用写反射相关的代码。
            //合理的运用dynamic可以让你的代码更加的简洁，而且比直接使用反射性能更好（反射没有优化处理的前提），因为dynamic是基于DLR，第一次运行后会缓存起来。其实有心的同学会发现.net的类库里面很多地方都用了dynamic这个东西，例如：mvc中的ViewBag就是一个很好的例子。一般情况下，如果开发者不知道方法和方法的返回类型是否公开，请使用dynamic关键字。
            //补充：C# var和dynamic的用法和理解
            //var和dynamic的本质区别是类型判断的时间不同，前者是编译时，后者是运行时。
            //1.var在声明变量方面简化语法（只能是局部变量），在编译时交给编译器推断。
            //2.dynamic也是为简化语法而生的，它的类型推断是交给系统来执行的（运行时推断类型）。
            //3.var不能用于字段、参数等，而dynamic则可以。
            //4.var在初始化的时候就确定了类型。
            //5.dynamic可以用于方法字段、参数、返回值以及泛型参数，把动态发挥的淋漓尽致。
            //6.dynamic在反射方面做的可以，只是我自己没有尝试过。
            //7.var是C# 3.0的产物，dynamic是C# 4.0的产物。
            //最后还得关心一下效率问题：
            // 越底层的效率越高
            //可以说是 传统强类型 >= var > dynamic,所以用dynamic的时候还得考虑性能和效率！
            #endregion






            Func<int> hui = () => 5;
            //var hui2 = () => 5;    //错误    1    无法将“lambda 表达式”赋予隐式类型的局部变量    
            // var  不是一个匿名函数，同时初始化表达式也不能是 null；




        }
        //private static object GetObject()
        //private static var GetObject()  不支持 报错
        private static dynamic GetObject()
        {
            return new { Name = "张三", Age = 20, Like = "LOL" };
        }
    }
}
