using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionTester
{
    class DynamicExp
    {
        public static void CompileBlockExp()
        {
            ParameterExpression i = Expression.Parameter(typeof(int), "i");
            BlockExpression block = Expression.Block(
                new[] { i },
                //赋初值 i=5
                Expression.Assign(i, Expression.Constant(5, typeof(int))),
                //i+=5 10
                Expression.AddAssign(i, Expression.Constant(5, typeof(int))),
                //i-=5 5
                Expression.SubtractAssign(i, Expression.Constant(5, typeof(int))),
               //i*=15 75
               Expression.MultiplyAssign(i, Expression.Constant(15, typeof(int))),
               //i/=5 15
               Expression.DivideAssign(i, Expression.Constant(5, typeof(int)))
               );
            Console.WriteLine(Expression.Lambda<Func<int>>(block).Compile()()); // 15

            //变量i
            ParameterExpression ic = Expression.Parameter(typeof(int), "i");
            //跳出循环
            LabelTarget label = Expression.Label();
            BlockExpression block2 = Expression.Block(
                new[] { i },
                //为i赋初值
                Expression.Assign(i, Expression.Constant(1, typeof(int))),
                Expression.Loop(
                    Expression.IfThenElse(
                      //如果i<=100
                      Expression.LessThanOrEqual(ic, Expression.Constant(100, typeof(int))),
                        //如果为true.进入循环体
                        Expression.Block(
                             Expression.IfThen(
                                    //条件i%2==0;
                                    Expression.Equal(Expression.Modulo(i, Expression.Constant(2, typeof(int))),
                                    Expression.Constant(0, typeof(int))),
                                    Expression.Call(typeof(Console).GetMethod("WriteLine",
                                    new Type[] { typeof(int) }), new[] { i })),
                             //i++
                             Expression.PostIncrementAssign(i)
                ),
                //如果i>100
                Expression.Break(label)),
                label
                ));
            Expression.Lambda<Action>(block).Compile()();
            Console.Read();


            //ParameterExpression i = Expression.Parameter(typeof(int), "i");
            //ConstantExpression constExp = Expression.Constant(5, typeof(int));
            //BinaryExpression binaryExp = Expression.Assign(i, constExp);
            //Expression.Lambda<Func<int>>(binaryExp).Compile()();


            //ParameterExpression expA = Expression.Parameter(typeof(double), "a"); //参数a
            //MethodCallExpression expCall = Expression.Call(null,
            //    typeof(Math).GetMethod("Sin", BindingFlags.Static | BindingFlags.Public),
            //    expA); //Math.Sin(a)构建方法调用表达式（表示一次方法调用）

            //Console.WriteLine(expCall);
        }

        public static void JointExp()
        {
            string ss = "abc";
            Expression<Func<string, bool>> exp1 = s => s.Contains("a");
            Expression<Func<string, bool>> exp2 = s => s.Contains("c");


            var la = Expression.Lambda<Func<string, bool>>(Expression.And(exp1.Body, exp2.Body), exp1.Parameters[0]);

            var ret = la.Compile()(ss);
            Console.WriteLine(ret);
        }

        public void BuildExp()
        {
            //假如我们要拼接x=>x.Username.Contains("aaa")，假如x的类型为User
            var parameterExp = Expression.Parameter(typeof(User), "x");
            var propertyExp = Expression.Property(parameterExp, "Username");
            //上面两句不再介绍
            var containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
            //因为我们要拼接的表达式中调用了string类型的Username的Contains方法，所以反射获取string类型的Contains方法
            var constExp = Expression.Constant("aaa");
            //不再解释
            MethodCallExpression containsExp = Expression.Call(propertyExp, containsMethod, constExp);
            //结果是：x=>x.Username.Contains("aaa")，第一个参数，是要调用哪个实例的方法，这里是propertyExp，第二个是调用哪个方法，第三个是参数，理解了上一个示例，这个应该不难理解
            var lambda = Expression.Lambda<Func<User, bool>>(containsExp, parameterExp);
            //不再解释
        }
    }

    public class SqlFilter
    {
        public static SqlFilter Create(string propertyName, Operation operation, object value)
        {
            return new SqlFilter()
            {
                Name = propertyName,
                Operation = operation,
                Value = value
            };
        }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 搜索操作，大于小于等于
        /// </summary>
        public Operation Operation { get; set; }

        /// <summary>
        /// 搜索参数值
        /// </summary>
        public object Value { get; set; }
    }

    public enum Operation
    {
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        NotEqual,
        Equal,
        Like,
        In
    }

    public class User
    {
        public string UserName { get; set; }
    }
}
