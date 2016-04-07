using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using DocoptNet;
using static System.Environment;
using static System.IO.Path;

namespace AppVeyorConfigurer.CommandHandlers
{
    public class NuspecProject : BaseCommandHandler
    {
        public override string CommandKey => "--nuspec-project";
        public override string CommandKeyValue => "<nuspec-project>";

        public override void HandleCommand(IDictionary<string, ValueObject> arguments)
        {
            /*
             * Can ask for
             * <licenseUrl>
             * <projectUrl>
             */
            var packageElement = new XElement("package",
                                    new XElement("metadata",
                                        new XElement("id", "$id$"),
                                        new XElement("version", "$version$"),
                                        new XElement("title", "$title$"),
                                        new XElement("authors", "$authors$"),
                                        new XElement("owners", "$owners$"),
                                        new XElement("requireLicenseAcceptance", "false"),
                                        new XElement("description", "$description$"),
                                        new XElement("copyright", "$copyright$"),
                                        new XElement("tags", "")
                                    ));

            var nuspec = new XDocument(packageElement);

            var csprojPath = arguments[CommandKeyValue].ToString();
            var csprojFullPath = Combine(CurrentDirectory, csprojPath);

            if (File.Exists(csprojFullPath))
                SaveNuspec(csprojFullPath, nuspec);
            else
            {
                csprojFullPath = Combine(GetDirectoryName(Combine(CurrentDirectory, arguments["<sln>"].ToString())), csprojPath);
                if (File.Exists(csprojFullPath))
                    SaveNuspec(csprojFullPath, nuspec);
            }
        }

        private static void SaveNuspec(string csprojFullPath, XDocument nuspec)
        {
            var csProjLocation = GetDirectoryName(csprojFullPath);
            var nuspecPath = Combine(csProjLocation, GetFileNameWithoutExtension(csprojFullPath) + ".nuspec");
            nuspec.Save(nuspecPath);
        }
    }
}