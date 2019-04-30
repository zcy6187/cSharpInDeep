using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionAndAttribute
{
    public class Serializer
    {
        public static TOut TransReflection<TIn, TOut>(TIn tIn)
        {
            TOut tOut = Activator.CreateInstance<TOut>();
            var tInType = tIn.GetType();
            foreach (var itemOut in tOut.GetType().GetProperties())
            {
                var itemIn = tInType.GetProperty(itemOut.Name); ;
                if (itemIn != null)
                {
                    itemOut.SetValue(tOut, itemIn.GetValue(tIn));
                }
            }
            return tOut;
        }

        public static void TesterReflection()
        {
            Student tempStu = new Student()
            {
                Name = "zcy",
                Age = 10
            };
            StudentDto refStudent=TransReflection<Student, StudentDto>(tempStu);
            Console.WriteLine(refStudent.Name);
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class StudentDto
    {
        public string Name { get; set; }
    }
}
