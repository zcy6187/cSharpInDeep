using System;

namespace ReflectionAndAttribute
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Typer
            //Typer.ClassTester();
            //Typer.BaseTester();
            //Typer.InheritedTester();
            // Typer.Typer.GetGenericTypeDefinition();
            Typer.GenericExample.BaseTester();
            #endregion

            #region Serializer序列化
            // Serializer.TesterReflection();
            #endregion

            #region 特性
            // Attributer.Tester();
            // Attributer.TraversalAttributes();
            #endregion

            #region Reflector
            // Reflections.VisitPublics();
            // Reflections.OperProperty();
            #endregion

            Console.ReadKey();
        }
    }
}
