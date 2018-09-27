using System;
using Autofac;
using Autofac.Core;

namespace Beispiel.AutofacModules
{
    public class LogRequestsModule : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            registration.Activated += Registration_Activated;
            registration.Activating += Registration_Activating;
            registration.Preparing += Registration_Preparing;
        }

        private void Registration_Preparing(object sender, PreparingEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Preparing:  {e.Component.Activator.LimitType}");
            Console.ResetColor();
        }

        private void Registration_Activating(object sender, ActivatingEventArgs<object> e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Activating: {e.Component.Activator.LimitType}");
            Console.ResetColor();
        }

        private void Registration_Activated(object sender, ActivatedEventArgs<object> e)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Activated:  {e.Component.Activator.LimitType}");
            Console.ResetColor();
        }
    }
}
