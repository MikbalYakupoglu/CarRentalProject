﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching;

public class CacheAspect : MethodInterceptions
{
    private int _duration;
    private ICacheService _cacheService;

    public CacheAspect(int duration = 60)
    {
        _duration = duration;
        _cacheService = ServiceTool.ServiceProvider.GetService<ICacheService>();
    }


    public override void Intercept(IInvocation invocation)
    {
        var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
        var arguments = invocation.Arguments.ToList();
        var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";

        if (_cacheService.IsAdd(key))
        {
            invocation.ReturnValue = _cacheService.Get(key);
            return;
        }

        invocation.Proceed();
        _cacheService.Add(key,invocation.ReturnValue,_duration);
    }
}