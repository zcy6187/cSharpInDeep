using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsharpConsole
{
    class AsAndIs
    {
        public static void BaseTester()
        {
            int i = 1;
            double d = 2.0;
            int? b = d as int?;

            Dog dog = new Dog();
            Animal anm = new Animal();
            Animal anDog = dog as Animal;
            Dog anAnimal = anm as Dog;
            Console.WriteLine(anDog.GetType()); // Dog
            Console.WriteLine(anAnimal is null); // True

            Console.WriteLine(anm is Dog); // False
            Console.WriteLine(anm is Animal); // True
            Console.WriteLine(anDog is Animal);  // True
            Console.WriteLine(anDog is Dog);  // True

        }
    }

    class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Dog : Animal
    {
        public string Crawler { get; set; }
        
    }
}
