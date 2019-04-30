using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionTester
{
    class LambdaExpTester
    {
        public static void Base()
        {
            ParameterExpression expA = Expression.Parameter(typeof(double), "a"); //参数a
            MethodCallExpression expCall = Expression.Call(null,
                typeof(Math).GetMethod("Sin", BindingFlags.Static | BindingFlags.Public),
                expA); //Math.Sin(a)

            LambdaExpression exp = Expression.Lambda<Func<double,double>>(expCall, expA); // 弱类型表达式  a => Math.Sin(a)
           // LambdaExpression exp = Expression.Lambda<Func<double, int>>(expCall, expA); // 强类型表达式 a => Math.Sin(a)
            var exe=exp.Compile();
            var ret=exe.DynamicInvoke(90.0);
            Console.WriteLine(ret);
        }

        public static void SimpleLambdaExp()
        {
            //通过 Expression 类创建表达式树
            //  lambda：num => num == 0
            ParameterExpression pExpression = Expression.Parameter(typeof(int));    //参数：num
            ConstantExpression cExpression = Expression.Constant(0);    //常量：0
            BinaryExpression bExpression = Expression.MakeBinary(ExpressionType.Equal, pExpression, cExpression);   //表达式：num == 0
            Expression<Func<int, bool>> lambda = Expression.Lambda<Func<int, bool>>(bExpression, pExpression);  //lambda 表达式：num => num == 0
            var ret=lambda.Compile()(0);
            Console.WriteLine(ret); //True
        }

        public static void ResolveExp()
        {
            Expression<Func<double, double, double, double, double>> myExp =
            (a, b, m, n) => m * a * a + n * b * b;

            var calc = new BinaryExpressionCalculator(myExp);
            Console.WriteLine(calc.Calculate(1, 2, 3, 4));
        }
    }

    class BinaryExpressionCalculator
    {
        Dictionary<ParameterExpression, double> m_argDict;
        LambdaExpression m_exp;


        public BinaryExpressionCalculator(LambdaExpression exp)
        {
            m_exp = exp;
        }

        public double Calculate(params double[] args)
        {
            //从ExpressionExpression中提取参数，和传输的实参对应起来。
            //生成的字典可以方便我们在后面查询参数的值
            m_argDict = new Dictionary<ParameterExpression, double>();

            for (int i = 0; i < m_exp.Parameters.Count; i++)
            {
                //就不检查数目和类型了，大家理解哈
                m_argDict[m_exp.Parameters[i]] = args[i]; // 传入参数
            }

            //提取树根
            Expression rootExp = m_exp.Body as Expression;

            string prefixExp = InternalPrefix(rootExp);
            Console.WriteLine(prefixExp);

            //计算！
            return InternalCalc(rootExp);




        }

        /* 递归执行
         * 判断表达式，如果是常量表达式，就返回常量表达式；
         * 如果是参数表达式，则按照参数字典，对表达式赋值；
         * 如果是二元表达式，则计算其值；
         */
        double InternalCalc(Expression exp)
        {
            ConstantExpression cexp = exp as ConstantExpression;
            if (cexp != null) return (double)cexp.Value;

            ParameterExpression pexp = exp as ParameterExpression;
            if (pexp != null)
            {
                return m_argDict[pexp];
            }

            BinaryExpression bexp = exp as BinaryExpression;
            if (bexp == null) throw new ArgumentException("不支持表达式的类型", "exp");

            switch (bexp.NodeType)
            {
                case ExpressionType.Add:
                    return InternalCalc(bexp.Left) + InternalCalc(bexp.Right);
                case ExpressionType.Divide:
                    return InternalCalc(bexp.Left) / InternalCalc(bexp.Right);
                case ExpressionType.Multiply:
                    return InternalCalc(bexp.Left) * InternalCalc(bexp.Right);
                case ExpressionType.Subtract:
                    return InternalCalc(bexp.Left) - InternalCalc(bexp.Right);
                default:
                    throw new ArgumentException("不支持表达式的类型", "exp");
            }
        }

        // 获取四则运算的前置表达式
        string InternalPrefix(Expression exp)
        {
            ConstantExpression cexp = exp as ConstantExpression;
            if (cexp != null) return cexp.Value.ToString();

            ParameterExpression pexp = exp as ParameterExpression;
            if (pexp != null) return pexp.Name;

            BinaryExpression bexp = exp as BinaryExpression;
            if (bexp == null) throw new ArgumentException("不支持表达式的类型", "exp");

            switch (bexp.NodeType)
            {
                case ExpressionType.Add:
                    return "+ " + InternalPrefix(bexp.Left) + " " + InternalPrefix(bexp.Right);
                case ExpressionType.Divide:
                    return "- " + InternalPrefix(bexp.Left) + " " + InternalPrefix(bexp.Right);
                case ExpressionType.Multiply:
                    return "* " + InternalPrefix(bexp.Left) + " " + InternalPrefix(bexp.Right);
                case ExpressionType.Subtract:
                    return "/ " + InternalPrefix(bexp.Left) + " " + InternalPrefix(bexp.Right);
                default:
                    throw new ArgumentException("不支持表达式的类型", "exp");
            }
        }
    }
}
