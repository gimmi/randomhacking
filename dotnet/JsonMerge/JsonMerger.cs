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

        private static void ExtendArray(JArray destArray, JArray newArray)
        {
            foreach (var newToken in newArray)
            {
                var append = true;
                var newId = GetTokenId(newToken);
                for (var idx = 0; idx < destArray.Count; idx++)
                {
                    var destToken = destArray[idx];
                    var destId = GetTokenId(destToken);
                    if (JToken.DeepEquals(destId, newId))
                    {
                        destArray[idx] = Extend(destToken, newToken);
                        append = false;
                        break;
                    }
                }

                if (append)
                {
                    destArray.Add(newToken);
                }
            }
        }

        private static void ExtendObject(JObject destObject, JObject newObject)
        {
            foreach (var (newId, newToken) in newObject)
            {
                if (destObject.TryGetValue(newId, out var destToken))
                {
                    destObject[newId] = Extend(destToken, newToken);
                }
                else
                {
                    destObject[newId] = newToken;
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
