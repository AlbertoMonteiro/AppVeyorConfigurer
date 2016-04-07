using System.Collections.Generic;
using DocoptNet;

namespace AppVeyorConfigurer.CommandHandlers
{
    public interface ICommandHandler
    {
        string CommandKey { get; }
        string CommandKeyValue { get; }
        void HandleCommand(IDictionary<string, ValueObject> arguments);
    }
}