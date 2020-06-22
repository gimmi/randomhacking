using System;
using Newtonsoft.Json.Linq;

namespace JsonMerge
{
    public static class JsonMerger
    {
        public static string Extend(string baseline, string extension) => Extend(JToken.Parse(baseline), JToken.Parse(extension)).ToString();

        public static JToken Extend(JToken baseline, JToken extension)
        {
            if (baseline is JArray basArray && extension is JArray extArray)
            {
                var resArray = new JArray();
                ExtendArray(resArray, basArray);
                ExtendArray(resArray, extArray);
                return resArray;
            }

            if (baseline is JObject basObject && extension is JObject extObject)
            {
                var resObject = new JObject();
                ExtendObject(resObject, basObject);
                ExtendObject(resObject, extObject);
                return resObject;
            }

            if (baseline.Type == JTokenType.Null)
            {
                return extension;
            }

            return baseline;
        }

        private static void ExtendArray(JArray basArray, JArray extArray)
        {
            foreach (var extToken in extArray)
            {
                var append = true;
                var extId = GetTokenId(extToken);
                for (var idx = 0; idx < basArray.Count; idx++)
                {
                    var basToken = basArray[idx];
                    var basId = GetTokenId(basToken);
                    if (JToken.DeepEquals(basId, extId))
                    {
                        basArray[idx] = Extend(basToken, extToken);
                        append = false;
                        break;
                    }
                }

                if (append)
                {
                    basArray.Add(extToken);
                }
            }
        }

        private static void ExtendObject(JObject basObject, JObject extObject)
        {
            foreach (var (extId, extToken) in extObject)
            {
                if (basObject.TryGetValue(extId, out var basToken))
                {
                    basObject[extId] = Extend(basToken, extToken);
                }
                else
                {
                    basObject[extId] = extToken;
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
