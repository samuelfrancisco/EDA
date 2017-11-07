using System;
using MassTransit;

namespace EDA.Poc.Worker.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var containerBootstrapper = ContainerBootstrapper.Bootstrap();

            var commandBusControl = containerBootstrapper.Container.Resolve<IBusControl>("commandBusControl");

            var eventBusControl = containerBootstrapper.Container.Resolve<IBusControl>("eventBusControl");
            
            commandBusControl.Start();
            eventBusControl.Start();

            Console.ReadLine();
            
            commandBusControl.Stop();
            eventBusControl.Stop();
            containerBootstrapper.Dispose();
        }
    }
}
