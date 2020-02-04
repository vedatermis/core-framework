﻿using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors.Autofac;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IoC;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect: MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = $"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}";
            var arguments = invocation.Arguments.ToList();

            var key = $"{methodName}({string.Join(",", arguments.Select(c => c?.ToString() ?? "<Null>"))})";

            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
            }

            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}