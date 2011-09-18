using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;
using NUnit.Framework;

namespace SpikeDynamicProxy
{
	[TestFixture]
	public class Class1
	{
		public class Class
		{
			public virtual string StringProperty { get; set; }

			[AllowedRole("admin")]
			public virtual string StringMethod(string parameter)
			{
				return "";
			}
		}

		public class AllowedRoleAttribute : Attribute
		{
			public readonly string Role;

			public AllowedRoleAttribute(string role)
			{
				Role = role;
			}
		}

		public class ClassInterceptor : IInterceptor
		{
			public StringBuilder Sb = new StringBuilder();

			public void Intercept(IInvocation invocation)
			{
				Sb.AppendFormat("Invoking {0}", invocation.Method.Name);
				var attribute = FindAttribute<AllowedRoleAttribute>(invocation.MethodInvocationTarget);
				if (attribute != null)
				{
					Sb.Append(" checking role");
				}
				Sb.AppendLine();
			}
			
			public T FindAttribute<T>(MemberInfo member) where T : Attribute
			{
				return (T)member.GetCustomAttributes(typeof(T), false).FirstOrDefault();
			}
		}

		[Test]
		public void tt()
		{
			var target = new Class();
			var interceptor = new ClassInterceptor();
			var targetProxy = new ProxyGenerator().CreateClassProxyWithTarget(target, interceptor);

			targetProxy.StringProperty = "value";
			string value = targetProxy.StringProperty;
			value = targetProxy.StringMethod(value);

			string expected = @"
Invoking set_StringProperty
Invoking get_StringProperty
Invoking StringMethod checking role
".TrimStart('\n', '\r');

			string actual = interceptor.Sb.ToString();
			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}