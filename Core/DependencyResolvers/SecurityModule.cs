using Core.Utilities.IoC;
using Core.Utilities.Security.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers;

public class SecurityModule : ICoreModule
{
    public void Load(IServiceCollection collection)
    {
        collection.AddSingleton<ITokenHelper, JwtHelper> ();

    }
}