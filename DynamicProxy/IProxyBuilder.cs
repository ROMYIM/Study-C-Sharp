using System;

namespace DynamicProxy;

public interface IProxyBuilder
{
    object? BuildProxy(Type serviceType);
}