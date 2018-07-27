using System;

namespace Acme.Application.DependencyInjection
{
    public interface IDependencyResolver
    {
        TType Resolve<TType>() where TType : class;

        TType[] ResolveAll<TType>() where TType : class;

        object[] ResolveAll(Type t);


    }
}
