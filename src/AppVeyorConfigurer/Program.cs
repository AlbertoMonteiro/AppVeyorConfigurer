using System;
using DocoptNet;

namespace AppVeyorConfigurer
{
    class Program
    {
        private const string USAGE = @"AppVeyor Configurer.

    Usage:
      AppVeyorConfigurer.exe prepare <sln> --nuspec-project <nuspec-project> 
      AppVeyorConfigurer.exe prepare <sln>
      AppVeyorConfigurer.exe (-h | --help)
      AppVeyorConfigurer.exe --version

    Options:
      -h --help         Show this screen.
      --version         Show version.
      --nuspec-project  The project to add nuspec.
    ";

        static void Main(string[] args)
        {
            var arguments = new Docopt().Apply(USAGE, args, version: "AppVeyor Configurer", exit: true);
            IConfigurer configurer = new Configurer();
            configurer.Configure(arguments);
            Console.ReadLine();
        }
    }
}
