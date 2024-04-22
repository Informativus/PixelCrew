using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PixelCrew.Utils
{
    public static class LocalizationHelper
    {
        public static string ToJson(Dictionary<string, string> strings) => 
            JsonUtility.ToJson(new ItemsData {Items = new List<ItemData>(strings.ToList().Select(ItemData.FromPair))});

        public static Dictionary<string, string> FromJson(string json) =>
            new(JsonUtility.FromJson<ItemsData>(json).Items.Select(ItemData.ToPair));

        [Serializable]
        private class ItemsData
        {
            public List<ItemData> Items;
        }

        [Serializable]
        private class ItemData
        {
            public string element_name;
            public string element_text;

            public static KeyValuePair<string, string> ToPair(ItemData data) =>
                new(data.element_name, data.element_text);

            public static ItemData FromPair(KeyValuePair<string, string> pair) =>
                new() { element_name = pair.Key, element_text = pair.Value };
        }
    }
}