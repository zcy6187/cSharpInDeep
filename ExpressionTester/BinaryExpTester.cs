using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionTester
{
    class BinaryExpTester
    {
        public static void Base()
        {
            ParameterExpression a = Expression.Parameter(typeof(int), "a");
            ParameterExpression b = Expression.Parameter(typeof(int), "b");
            ParameterExpression i = Expression.Parameter(typeof(int), "i");
            BinaryExpression add = Expression.Add(a, b);


            ConstantExpression s = Expression.Constant(1);
            BinaryExpression substract = Expression.Subtract(i, s);

            NewArrayExpression arrayint = Expression.NewArrayInit(typeof(int), a, b, add);
            IndexExpression arracc = Expression.ArrayAccess(arrayint, substract);
            Console.WriteLine(arracc.ToString());
        }

        public static void ResolveBinaryExp()
        {
            Expression<Func<int, bool>> filter = n => (n * 3) < 5;
            BinaryExpression lt = (BinaryExpression)filter.Body; 
            BinaryExpression mult = (BinaryExpression)lt.Left; // 
            ParameterExpression en = (ParameterExpression)mult.Left;
            ConstantExpression three = (ConstantExpression)mult.Right;
            ConstantExpression five = (ConstantExpression)lt.Right;
            var One = filter.Compile();
            Console.WriteLine("Result: {0},{1}", One(5), One(1)); // Result: False,True
            Console.WriteLine("({0} ({1} {2} {3}) {4})", lt.NodeType,
            mult.NodeType, en.Name, three.Value, five.Value); // (LessThan (Multiply n 3) 5)
        }
    }
}
