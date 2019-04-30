using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionTester
{
    class Base
    {
        public static void SimpleExp()
        {
            Expression<Func<int,int,int>> baseExp= (a,b) => a + b;
            BinaryExpression body = (BinaryExpression)baseExp.Body; 
            ParameterExpression left = (ParameterExpression)body.Left; // a
            ParameterExpression right = (ParameterExpression)body.Right; // b
            int c = baseExp.Compile()(5, 4);
            Console.WriteLine(baseExp.Body); // (a+b) ，表达式会自动用()包裹
            Console.WriteLine($"Left Name:{left.Name}"); // a
            Console.WriteLine($"Right Name:{right.Name}"); // b
            Console.WriteLine($"Body Type:{body.Type}"); // System.Int32
            Console.WriteLine($"NodeType:{body.NodeType}"); // Add

            // Console.WriteLine(" 表达式左边部分: " + "{0}{4} 节点类型: {1}{4} 表达式右边部分: {2}{4} 类型: {3}{4}", left.Name, body.NodeType, right.Name, body.Type, Environment.NewLine);
        }

        // 解析表达式
        public static void ResolveExp()
        {
            Expression<Func<int, bool>> expTree = num => num >= 5;
            ParameterExpression param = expTree.Parameters[0];
            BinaryExpression body = (BinaryExpression)expTree.Body;
            ConstantExpression right = (ConstantExpression)body.Right; // 转换为常量表达式
            ParameterExpression left = (ParameterExpression)body.Left; // 转换为变量表达式
            
            ExpressionType type = body.NodeType; 
            Console.WriteLine($"解析后：{left}   {type}   {right}"); // 解析后：num   GreaterThanOrEqual   5

            Console.WriteLine(expTree.Compile()(10)); // True，调用表达式

            // Expression expLeft = body.Left; // 默认为普通表达式
            // Console.WriteLine(expLeft); //   num

        }
    }

}
