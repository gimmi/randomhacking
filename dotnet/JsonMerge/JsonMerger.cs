using System;
using Newtonsoft.Json.Linq;

namespace JsonMerge
{
    public static class JsonMerger
    {
        public static string Merge(string first, string second) => Merge(JToken.Parse(first), JToken.Parse(second)).ToString();

        public static JToken Merge(JToken first, JToken second)
        {
            if (first is JArray firstArray && second is JArray secondArray)
            {
                var resultArray = new JArray();
                MergeArray(resultArray, firstArray);
                MergeArray(resultArray, secondArray);
                return resultArray;
            }

            if (first is JObject firstObject && second is JObject secondObject)
            {
                var resultObject = new JObject();
                MergeObject(resultObject, firstObject);
                MergeObject(resultObject, secondObject);
                return resultObject;
            }

            if (first.Type == JTokenType.Null)
            {
                return second;
            }

            return first;
        }

        private static void MergeArray(JArray destArray, JArray newArray)
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
                        destArray[idx] = Merge(destToken, newToken);
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

        private static void MergeObject(JObject destObject, JObject newObject)
        {
            foreach (var (newId, newToken) in newObject)
            {
                if (destObject.TryGetValue(newId, out var destToken))
                {
                    destObject[newId] = Merge(destToken, newToken);
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
