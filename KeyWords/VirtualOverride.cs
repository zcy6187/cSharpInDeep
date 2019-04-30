using System;
using System.Collections.Generic;
using System.Text;

namespace KeyWords
{
    public class VirtualOverride
    {
        public static void Tester()
        {
            BaseClass baseClass= new BaseClass();
            baseClass.displayName(); // BaseClass
            BaseClass virtualBaseClass = new VirtualDerivedClass();
            virtualBaseClass.displayName(); // BaseClass
            BaseClass newBaseClass = new NewDerivedClass();
            newBaseClass.displayName(); // BaseClass
            NewDerivedClass newDerivedClass = new NewDerivedClass();
            newDerivedClass.displayName(); // New DerivedClass
            BaseClass overrideBaseClass = new OverrideDerivedClass();
            overrideBaseClass.displayName();
        }
    }
    public class BaseClass
    {
        public virtual void displayName()
        {
            Console.WriteLine("BaseClass");
        }
    }

    public class VirtualDerivedClass : BaseClass
    {
        public virtual void displayName()
        {
            Console.WriteLine("Virtual BaseClass");
        }
    }
    public class NewDerivedClass : BaseClass
    {
        public new void displayName()
        {
            Console.WriteLine("New DerivedClass");
        }
    }
    public class DerivedClass : BaseClass
    {
        public void displayName()
        {
            Console.WriteLine("New DerivedClass");
        }
    }
    public class OverrideDerivedClass : BaseClass
    {
        public override void displayName()
        {
            Console.WriteLine("Override DerivedClass");
        }
    }


}
