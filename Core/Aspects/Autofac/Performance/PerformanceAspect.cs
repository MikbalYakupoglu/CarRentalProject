using System.Diagnostics;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance;

public class PerformanceAspect : MethodInterceptions
{
    private int _interval;
    Stopwatch _stopwatch;

    public PerformanceAspect(int interval, Stopwatch stopwatch)
    {
        _interval = interval;
        _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
    }

    protected override void OnBefore(IInvocation invocation)
    {
        _stopwatch.Start();
    }

    protected override void OnAfter(IInvocation invocation)
    {
        var time = _stopwatch.Elapsed.TotalSeconds;

        if (time > _interval)
        {
            Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}." +
                            $"{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
        }

        _stopwatch.Reset();
    }
}