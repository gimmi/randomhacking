using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;

namespace AspNetCoreSelfHost
{
    public class JsonEqualConstraint : Constraint
    {
        private readonly JToken _expectedJson;

        public JsonEqualConstraint(string expectedJson)
        {
            _expectedJson = JToken.Parse(expectedJson);
        }

        public override string Description
        {
            get => string.Concat("equal to ", '"', _expectedJson.ToString(Formatting.Indented), '"');
            protected set { }
        }

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            var actualJson = JToken.Parse(actual?.ToString());
            var isSuccess = JToken.DeepEquals(_expectedJson, actualJson);
            return new ConstraintResult(this, actualJson.ToString(Formatting.Indented), isSuccess);
        }
    }
}