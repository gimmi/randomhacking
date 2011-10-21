using EmitMapper;
using NUnit.Framework;

namespace SpikeEmitMapper
{
	[TestFixture]
	public class Class1
	{
		private ObjectMapperManager _target;

		public class ClassA
		{
			public string StringValue;
			public int IntValue;
		}

		public class ClassB
		{
			public string StringValue;
			public int IntValue;
		}

		[Test]
		public void Tt()
		{
			_target = new ObjectMapperManager();

			var classA = new ClassA {StringValue = "a string", IntValue = 123};
			ClassB classB = _target.GetMapper<ClassA, ClassB>().Map(classA);

			Assert.That(classB.StringValue, Is.EqualTo(classA.StringValue));
			Assert.That(classB.IntValue, Is.EqualTo(classA.IntValue));
		}
	}
}