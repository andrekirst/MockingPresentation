using System;
using System.IO.Abstractions;
using Autofac;
using Beispiel.Implementations;
using Beispiel.Interfaces;

namespace Beispiel;

public static class Program
{
    public static void Main()
    {
        // Komponentenregistrierung
        var containerBuilder = new ContainerBuilder();
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

        var kundenInteractor = containerBuilder
            .Build()
            .Resolve<IKundenInteraktor>();

        foreach (var kunde in kundenInteractor.SucheKunden(""))
        {
            Console.WriteLine($"{kunde.Id} - {kunde.Name}");
        }

        Console.WriteLine("Fertsch");
        Console.ReadKey();
    }
}
