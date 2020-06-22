using System;
using Newtonsoft.Json.Linq;

namespace JsonMerge
{
    public static class JsonMerger
    {
        public static string Extend(string baseline, string extension) => Extend(JToken.Parse(baseline), JToken.Parse(extension)).ToString();

        public static JToken Extend(JToken baseline, JToken extension)
        {
            if (baseline is JArray baselineArray && extension is JArray extensionArray)
            {
                var resultArray = new JArray();
                ExtendArray(resultArray, baselineArray);
                ExtendArray(resultArray, extensionArray);
                return resultArray;
            }

            if (baseline is JObject baselineObject && extension is JObject extensionObject)
            {
                var resultObject = new JObject();
                ExtendObject(resultObject, baselineObject);
                ExtendObject(resultObject, extensionObject);
                return resultObject;
            }

            if (baseline.Type == JTokenType.Null)
            {
                return extension;
            }

            return baseline;
        }

        private static void ExtendArray(JArray baseArray, JArray extArray)
        {
            foreach (var extToken in extArray)
            {
                var append = true;
                var extId = GetTokenId(extToken);
                for (var idx = 0; idx < baseArray.Count; idx++)
                {
                    var baseToken = baseArray[idx];
                    var baseId = GetTokenId(baseToken);
                    if (JToken.DeepEquals(baseId, extId))
                    {
                        baseArray[idx] = Extend(baseToken, extToken);
                        append = false;
                        break;
                    }
                }

                if (append)
                {
                    baseArray.Add(extToken);
                }
            }
        }

        private static void ExtendObject(JObject baseObject, JObject extObject)
        {
            foreach (var (extId, extToken) in extObject)
            {
                if (baseObject.TryGetValue(extId, out var baseToken))
                {
                    baseObject[extId] = Extend(baseToken, extToken);
                }
                else
                {
                    baseObject[extId] = extToken;
                }
            }
        }

        private static JToken GetTokenId(JToken token)
        {
            if (token is JObject obj)
            {
                if (obj.TryGetValue("$id", StringComparison.OrdinalIgnoreCase, out var valueToken))
                {
                    return valueToken;
                }

                if (obj.TryGetValue("id", StringComparison.OrdinalIgnoreCase, out valueToken))
                {
                    return valueToken;
                }
            }

            return token;
        }
    }
}
