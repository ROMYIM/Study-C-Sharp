﻿using DynamicProxy.Attributes;
using DynamicProxySample.Interceptors;

namespace DynamicProxySample.Interfaces;

public interface IServiceA
{
    [Aspect(typeof(LogInterceptor))]
    public int Test(ref int number);
    
    [Aspect(typeof(LogInterceptor), typeof(TransactionalInterceptor))]
    public ValueTask<string> TestDbAsync(int id, string name);
}