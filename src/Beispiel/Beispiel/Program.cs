using System;
using System.IO.Abstractions;
using Autofac;
using Beispiel.AutofacModules;
using Beispiel.Implementations;
using Beispiel.Interfaces;
using Beispiel.Models;

namespace Beispiel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Komponentenregistrierung
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<LogRequestsModule>();
            containerBuilder
                .RegisterType<KundenInteraktor>()
                .As<IKundenInteraktor>();
            containerBuilder
                .RegisterType<JsonKundenDatenspeicher>()
                .As<IKundenDatenspeicher>();
            containerBuilder
                .RegisterType<BunterKonsolenProtokollierer>()
                .As<IProtokollierer>();
            containerBuilder
                .RegisterType<FileSystem>()
                .As<IFileSystem>();

            IKundenInteraktor kundenInteractor = containerBuilder
                .Build()
                .Resolve<IKundenInteraktor>();

            foreach (Kunde kunde in kundenInteractor.SucheKunden(filter: ""))
            {
                Console.WriteLine(value: $"{kunde.Id} - {kunde.Name}");
            }

            Console.WriteLine(value: "Fertsch");
            Console.ReadKey();
        }
    }
}
