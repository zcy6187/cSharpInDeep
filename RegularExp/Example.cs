using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegularExp
{
    class Example
    {
        #region 正则表达式
        public static bool CheckExpressionValid(string input)
        {
            string pattern = @"^(((?<o>\()[-+]?([0-9]+[-+*/])*)+[0-9]+((?<-o>\))([-+*/][0-9]+)*)+($|[-+*/]))*(?(o)(?!))$";
            //去掉空格，且添加括号便于进行匹配    
            return Regex.IsMatch("(" + input.Replace(" ", "") + ")", pattern);
        }

        public static void ExeFourSpecis()
        {
            string[] inputs = new string[]
            {
                "(",
                "(() ",
                "1 / ",
                ")1 ",
                "+3 ",
                "((1) ",
                "(1)) ",
                "(1) ",
                "1 + 23",
                "1+2*3 ",
                "1*(2+3) ",
                "((1+2)*3+4)/5 ",
                "1+(2*) ",
                "1*(2+3/(-4)) ",
            };

            foreach (string input in inputs)
            {
                Console.WriteLine("{0}:{1} ", input, CheckExpressionValid(input));
            }
            Console.ReadKey();
        }

        public static void IsValid()
        {
            string formulaStr = "(m1+m2)*v/m3";
            // 简单的匹配
            string mPattern = "m[0-9]*";
            string vPattern = "v[0-9]*";
            string cPattern = "c[0-9]*";

            // 参数替换成常数99，判断是否是正常的四则表达式
            string tempFormula = Regex.Replace(formulaStr, mPattern, "99");
            tempFormula = Regex.Replace(formulaStr, vPattern, "99");
            tempFormula = Regex.Replace(formulaStr, cPattern, "99");
        }

        #endregion
    }
}
