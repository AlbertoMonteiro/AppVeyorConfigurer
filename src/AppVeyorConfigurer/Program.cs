using System;
using DocoptNet;

namespace AppVeyorConfigurer
{
    class Program
    {
        private const string USAGE = @"AppVeyor Configurer.

    Usage:
      AppVeyorConfigurer.exe prepare <sln> [--nuspec-project <nuspec-project>] [--create-run-tests-bat <project-name>]
      AppVeyorConfigurer.exe (-h | --help)
      AppVeyorConfigurer.exe --version

    Options:
      -h --help                 Show this screen.
      --version                 Show version.
      --nuspec-project          The project to add nuspec.
      --create-run-tests-bat    The project name that will run tests in bat.
    ";

        static void Main(string[] args)
        {
            try
            {
                var arguments = new Docopt().Apply(USAGE, args, version: "AppVeyor Configurer", exit: true);
                IConfigurer configurer = new Configurer();
                configurer.Configure(arguments);

            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
