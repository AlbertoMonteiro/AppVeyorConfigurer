using System.Collections.Generic;
using DocoptNet;

namespace AppVeyorConfigurer
{
    public interface IConfigurer
    {
        void Configure(IDictionary<string, ValueObject> arguments);
    }
}