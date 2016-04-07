using System.Collections.Generic;
using DocoptNet;

namespace AppVeyorConfigurer.CommandHandlers
{
    public abstract class BaseCommandHandler : ICommandHandler
    {
        public abstract string CommandKey { get; }
        public abstract string CommandKeyValue { get; }
        public abstract void HandleCommand(IDictionary<string, ValueObject> arguments);
    }
}