
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ConfigurationDemo.Infranstructures.Extensions
{
    public static class ConfigurationExtension
    {
        public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs(this JsonElement jsonElement)
        {
            var data = new Dictionary<string, string>();
            
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Object:
                {
                    var enumerate = jsonElement.EnumerateObject();
                    while (enumerate.MoveNext())
                    {
                        var element = enumerate.Current;
                        var value = element.Value;

                        IEnumerable<KeyValuePair<string, string>> childrenData;
                        StringBuilder keyBuilder = new StringBuilder();

                        switch (value.ValueKind)
                        {
                            case JsonValueKind.Array:
                                for (int i = 0; i < value.GetArrayLength(); i++)
                                {
                                    childrenData = value[i].ToKeyValuePairs();
                                    keyBuilder = new StringBuilder();
                                    foreach (var childData in childrenData)
                                    {
                                        keyBuilder.Clear();
                                        var key = keyBuilder.Append(element.Name).Append(':').Append(i).Append(':').Append(childData.Key).ToString();
                                        key = key.AsSpan().TrimEnd(':').ToString();
                                        data.Add(key, childData.Value);
                                    }
                                }
                                break;
                            case JsonValueKind.Object:
                                childrenData = value.ToKeyValuePairs();
                                keyBuilder = new StringBuilder();
                                foreach (var childData in childrenData)
                                {
                                    keyBuilder.Clear();
                                    var key = keyBuilder.Append(element.Name).Append(':').Append(childData.Key).ToString();
                                    data.Add(key, childData.Value);
                                }
                                break;
                            default:
                                data.Add(element.Name, value.GetString());
                                break;
                        }
                    }
                    break;
                }

                case JsonValueKind.Array:
                {
                    for (int i = 0; i < jsonElement.GetArrayLength(); i++)
                    {
                        var keyBuilder = new StringBuilder();
                        var childrenData = jsonElement[i].ToKeyValuePairs();

                        foreach (var childData in childrenData)
                        {
                            keyBuilder.Clear();
                            var key = keyBuilder.Append(i).Append(':').Append(childData.Key).ToString();
                            key = key.AsSpan().TrimEnd(':').ToString();
                            data.Add(key, childData.Value);
                        }
                    }
                    break;
                }
                default:
                    data.Add(string.Empty, jsonElement.GetString());
                    break;
            }
            
            return data;
        }
    }
}