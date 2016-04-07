using System;
using System.Collections.Generic;
using System.IO;
using DocoptNet;
using static System.Environment;
using static System.IO.Directory;
using static System.IO.Path;

namespace AppVeyorConfigurer.CommandHandlers
{
    public class PrepareCommandHandler : BaseCommandHandler
    {
        public override string CommandKey => "prepare";
        public override string CommandKeyValue => "<sln>";

        const string PACKAGES_CONFIG_CONTENT = @"<?xml version=""1.0"" encoding=""utf-8""?>
<packages>
    <package id = ""coveralls.io"" version=""1.3.4"" />
    <package id = ""NUnit.Runners"" version=""3.2.0"" />
    <package id = ""OpenCover"" version=""4.6.519"" />
    <package id = ""NUnit.ConsoleRunner"" version=""3.2.0""/>
    <package id = ""NUnit.Extension.NUnitProjectLoader"" version=""3.2.0""/>
    <package id = ""NUnit.Extension.VSProjectLoader"" version=""3.2.0""/>
    <package id = ""NUnit.Extension.NUnitV2ResultWriter"" version=""3.2.0""/>
    <package id = ""NUnit.Extension.NUnitV2Driver"" version=""3.2.0""/>
</packages>";

        public override void HandleCommand(IDictionary<string, ValueObject> arguments)
        {
            var nugetPath = Combine(GetDirectoryName(Combine(CurrentDirectory, arguments[CommandKeyValue].ToString())), ".nuget");
            CreateDirectory(nugetPath);

            var packagesConfigPath = Combine(nugetPath, "packages.config");
            File.AppendAllText(packagesConfigPath, PACKAGES_CONFIG_CONTENT);
            Console.WriteLine($"Created packages.config in {packagesConfigPath}");
        }
    }
}