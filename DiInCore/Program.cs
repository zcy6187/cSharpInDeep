using Microsoft.Extensions.DependencyInjection;
using System;


namespace DiInCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // SingletonTester();
            // ScopedTester();
            BaseTester();
            Console.ReadKey();
        }

        static void SingletonTester()
        {
            var services = new ServiceCollection();

            // 默认构造
            services.AddSingleton<IOperationSingleton, Operation>();
            // 自定义传入Guid空值
            services.AddSingleton<IOperationSingleton>(
              new Operation(Guid.Empty));
            Guid tempG = Guid.NewGuid();
            // 自定义传入一个New的Guid
            services.AddSingleton<IOperationSingleton>(
              new Operation(tempG));
            Console.WriteLine($"{tempG}");
            Console.WriteLine($"{Guid.Empty}"); // 00000000 - 0000 - 0000 - 0000 - 000000000000


            var provider = services.BuildServiceProvider();

            // 输出singletone1的Guid
            var singletone1 = provider.GetService<IOperationSingleton>();
            Console.WriteLine($"signletone1: {singletone1.OperationId}");

            // 输出singletone2的Guid
            var singletone2 = provider.GetService<IOperationSingleton>();
            Console.WriteLine($"signletone2: {singletone2.OperationId}");
            Console.WriteLine($"singletone1 == singletone2  : { singletone1 == singletone2 }");
        }

        static void ScopedTester()
        {
            var services = new ServiceCollection();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddTransient<IOperationTransient, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();

            var provider = services.BuildServiceProvider();
            using (var scope1 = provider.CreateScope())
            {
                var p = scope1.ServiceProvider;

                var scopeobj1 = p.GetService<IOperationScoped>();
                var transient1 = p.GetService<IOperationTransient>();
                var singleton1 = p.GetService<IOperationSingleton>();

                var scopeobj2 = p.GetService<IOperationScoped>();
                var transient2 = p.GetService<IOperationTransient>();
                var singleton2 = p.GetService<IOperationSingleton>();

                Console.WriteLine(
                    $"scope1: { scopeobj1.OperationId }," +
                    $"transient1: {transient1.OperationId}, " +
                    $"singleton1: {singleton1.OperationId}");

                Console.WriteLine($"scope2: { scopeobj2.OperationId }, " +
                    $"transient2: {transient2.OperationId}, " +
                    $"singleton2: {singleton2.OperationId}");
            }

            Console.WriteLine("**********************************");
            Console.WriteLine("**********************************");

            using (var scope1 = provider.CreateScope())
            {
                var p = scope1.ServiceProvider;

                var scopeobj1 = p.GetService<IOperationScoped>();
                var transient1 = p.GetService<IOperationTransient>();
                var singleton1 = p.GetService<IOperationSingleton>();

                var scopeobj2 = p.GetService<IOperationScoped>();
                var transient2 = p.GetService<IOperationTransient>();
                var singleton2 = p.GetService<IOperationSingleton>();

                Console.WriteLine(
                    $"scope1: { scopeobj1.OperationId }," +
                    $"transient1: {transient1.OperationId}, " +
                    $"singleton1: {singleton1.OperationId}");

                Console.WriteLine($"scope2: { scopeobj2.OperationId }, " +
                    $"transient2: {transient2.OperationId}, " +
                    $"singleton2: {singleton2.OperationId}");
            }

        }

        static void BaseTester()
        {
            var services = new ServiceCollection();
            services.AddScoped<IOperation, Operation>();
            services.AddTransient<IOperation, Operation>();
            services.AddSingleton<IOperation, Operation>();

            var p = services.BuildServiceProvider();
            var op1 = p.GetService<IOperation>();
            var op2 = p.GetService<IOperation>();
            var op3 = p.GetService<IOperation>();

            Console.WriteLine($"op1:    {op1.OperationId}");
            Console.WriteLine($"op2:    {op2.OperationId}");
            Console.WriteLine($"op3:    {op3.OperationId}");
        }
    }
}
