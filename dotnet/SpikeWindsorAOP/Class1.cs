using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.Core.Configuration;
using Castle.DynamicProxy;
using Castle.MicroKernel;
using Castle.MicroKernel.Proxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace SpikeWindsorAOP
{
	[TestFixture]
	public class Class1
	{
		[Test]
		public void Tt()
		{
			var container = new WindsorContainer();
			container.Register(
			                   Component.For<IInterceptor>().ImplementedBy<Interceptor>().LifestyleTransient(),
			                   Component.For<Cmp>()
				);


			container.Resolve<Cmp>().Meth1();
		}
	}

	public class EventSubscriptionFacility : IFacility
	{
		public void Init(IKernel kernel, IConfiguration facilityConfig)
		{
			kernel.ProxyFactory.AddInterceptorSelector(new ModelInterceptorsSelector<InterceptAttribute, Interceptor>());
		}

		public void Terminate()
		{
		}
	}

	public class Cmp
	{
		[Intercept]
		public virtual void Meth1()
		{
			Debug.WriteLine("call");
		}
	}

	public class InterceptAttribute : Attribute
	{
	}

	public class Interceptor : IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			invocation.Proceed();
		}
	}

	public class ModelInterceptorsSelector<TAttribute, TInterceptor> : IModelInterceptorsSelector
	{
		public bool HasInterceptors(ComponentModel model)
		{
			MemberInfo[] members = model.Implementation.GetMembers();
			IEnumerable<Type> attributeTypes = members.SelectMany(x => x.GetCustomAttributes(true)).Select(x => x.GetType());
			bool hasInterceptors = attributeTypes.Contains(typeof(TAttribute));
			Debug.WriteLine(hasInterceptors);
			return hasInterceptors;
		}

		public InterceptorReference[] SelectInterceptors(ComponentModel model, InterceptorReference[] interceptors)
		{
			InterceptorReference interceptorReference = InterceptorReference.ForType<TInterceptor>();
			Debug.WriteLine(interceptorReference);
			return new[] { interceptorReference };
		}
	}

	internal class ReflectionHelpers
	{
		public T FindAttribute<T>(MemberInfo member, T def) where T : Attribute
		{
			return FindAttribute<T>(member) ?? def;
		}

		public T FindAttribute<T>(MemberInfo member) where T : Attribute
		{
			return (T) member.GetCustomAttributes(typeof (T), true).FirstOrDefault();
		}

		public bool HasAttribute<T>(MemberInfo member) where T : Attribute
		{
			return FindAttribute<T>(member) != default(T);
		}
	}
}