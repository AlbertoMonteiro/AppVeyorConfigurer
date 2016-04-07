using System;
using System.Collections.Generic;
using System.IO;
using DocoptNet;
using static System.Environment;
using static System.IO.Path;

namespace AppVeyorConfigurer.CommandHandlers
{
    public class CreateRunTestWithCoverageBatBaseCommandHandler : BaseCommandHandler
    {
        public override string CommandKey => "--create-run-tests-bat";
        public override string CommandKeyValue => "<project-name>";

        private const string RUN_TESTS_BAT_CONTENT = @"packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -filter:"" +[*]{0}.* -[*]{0}.Tests.*"" -target:""packages\NUnit.ConsoleRunner.3.2.0\tools\nunit3-console.exe"" -targetargs:""/domain:single src\{0}.Tests\bin\release\{0}.Tests.dll"" -output:coverage.xml

packages\coveralls.io.1.3.4\tools\coveralls.net.exe --opencover coverage.xml";

        public override void HandleCommand(IDictionary<string, ValueObject> arguments)
        {
            var slnLocaltion = GetDirectoryName(Combine(CurrentDirectory, arguments["<sln>"].ToString()));

            var batPath = Combine(slnLocaltion, "run tests with coverage.bat");

            File.WriteAllText(batPath, string.Format(RUN_TESTS_BAT_CONTENT, arguments[CommandKeyValue]));
            Console.WriteLine($"Created bat for run tests in {batPath}");
        }
    }
}