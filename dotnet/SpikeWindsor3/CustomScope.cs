using System;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace SpikeWindsor3
{
	public class CustomScope : IScopeAccessor
	{
		[ThreadStatic]
		private static ILifetimeScope _lifetimeScope;

		public static void BeginScope()
		{
			if (_lifetimeScope != null)
			{
				throw new InvalidOperationException("already in scope");
			}
			_lifetimeScope = new DefaultLifetimeScope();
		}

		public static void EndScope()
		{
			if (_lifetimeScope == null)
			{
				throw new InvalidOperationException("not in scope");
			}
			_lifetimeScope.Dispose();
			_lifetimeScope = null;
		}

		public void Dispose()
		{
			if(_lifetimeScope != null)
			{
				_lifetimeScope.Dispose();
				_lifetimeScope = null;
			}
		}

		public ILifetimeScope GetScope(CreationContext context)
		{
			if(_lifetimeScope == null)
			{
				throw new InvalidOperationException("Scope was not available. Did you forget to call CustomScope.BeginScope()?");
			}
			return _lifetimeScope;
		}
	}
}