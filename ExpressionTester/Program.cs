using System;

namespace ExpressionTester
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 基础信息
            // Base.SimpleExp();
            // Base.ResolveExp();
            #endregion

            #region 动态拼接Expression
            // DynamicExp.CompileBlockExp();
            // DynamicExp.JointExp();
            #endregion

            #region BinaryExpression
            // BinaryExpTester.Base();
            // BinaryExpTester.ResolveBinaryExp();
            #endregion

            #region LambdaExpression
            // LambdaExpTester.Base();
            // LambdaExpTester.SimpleLambdaExp();
            LambdaExpTester.ResolveExp();
            #endregion

            Console.ReadKey();
        }
    }
}
