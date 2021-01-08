using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Labs01.MediatR.WebApp.MediatrConfigurations
{
    public static class StrategyConfigurator
    {
        public static IStrategyConfigurator<T> Chain<T>(this IServiceCollection services) where T : class
        {
            return new StrategyConfiguratorImpl<T>(services);
        }

        public interface IStrategyConfigurator<T>
        {
            IStrategyConfigurator<T> Add<TImplementation>() where TImplementation : T;
            void Configure();
        }

        private class StrategyConfiguratorImpl<T> : IStrategyConfigurator<T> where T : class
        {
            private readonly IServiceCollection _services;
            private List<Type> _strategies;
            private Type _interfaceType;

            public StrategyConfiguratorImpl(IServiceCollection services)
            {
                _services = services;
                _strategies = new List<Type>();
                _interfaceType = typeof(T);
            }

            public IStrategyConfigurator<T> Add<TImplementation>() where TImplementation : T
            {
                var type = typeof(TImplementation);

                if (!_interfaceType.IsAssignableFrom(type))
                    throw new ArgumentException($"{type.Name} type is not an implementation of {_interfaceType.Name}", nameof(type));

                _strategies.Add(type);

                return this;
            }

            public void Configure()
            {
                if (_strategies.Count == 0)
                    throw new InvalidOperationException($"No implementation defined for {_interfaceType.Name}");

                bool first = true;
                foreach (var strategy in _strategies)
                {
                    ConfigureStrategy(strategy, first);
                    first = false;
                }
            }

            private void ConfigureStrategy(Type strategy, bool first)
            {
                var nextStrategy = _strategies.SkipWhile(x => x != strategy).SkipWhile(x => x == strategy).FirstOrDefault();

                var ctor = strategy.GetConstructors().OrderByDescending(x => x.GetParameters().Count()).First();

                var parameter = Expression.Parameter(typeof(IServiceProvider), "x");

                var ctorParameters = ctor.GetParameters().Select(x =>
                {
                    if (_interfaceType.IsAssignableFrom(x.ParameterType))
                    {
                        if (nextStrategy == null)
                            return Expression.Constant(null, _interfaceType);
                        else
                            return Expression.Call(typeof(ServiceProviderServiceExtensions), "GetRequiredService", new Type[] { nextStrategy }, parameter);
                    }

                    return (Expression)Expression.Call(typeof(ServiceProviderServiceExtensions), "GetRequiredService", new Type[] { x.ParameterType }, parameter);
                });

                var body = Expression.New(ctor, ctorParameters.ToArray());

                if (first)
                {
                    var expressionType = Expression.GetFuncType(typeof(IServiceProvider), _interfaceType);
                    var expression = Expression.Lambda(expressionType, body, parameter);
                    Func<IServiceProvider, object> x = (Func<IServiceProvider, object>)expression.Compile();
                    _services.AddTransient(_interfaceType, x);
                }
                else
                {
                    var expressionType = Expression.GetFuncType(typeof(IServiceProvider), strategy);
                    var expression = Expression.Lambda(expressionType, body, parameter);
                    Func<IServiceProvider, object> x = (Func<IServiceProvider, object>)expression.Compile();
                    _services.AddTransient(strategy, x);
                }
            }
        }
    }
}
