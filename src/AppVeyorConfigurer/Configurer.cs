using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AppVeyorConfigurer.CommandHandlers;
using DocoptNet;

namespace AppVeyorConfigurer
{
    public class Configurer : IConfigurer
    {
        public void Configure(IDictionary<string, ValueObject> arguments)
        {
            var type = typeof(ICommandHandler);
            var handlers = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract && type.IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<ICommandHandler>()
                .ToList();

            handlers = arguments.Join(handlers, x => x.Key, h => h.CommandKey, Tuple.Create)
                .Where(t => arguments[t.Item1.Key].IsTrue)
                .Select(t => t.Item2)
                .ToList();

            foreach (var handler in handlers)
                handler.HandleCommand(arguments);
        }
    }
}
