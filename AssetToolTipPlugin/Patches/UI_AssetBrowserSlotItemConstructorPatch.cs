using Bounce.Unmanaged;
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace AssetToolTip.Patches
{
    [HarmonyPatch(typeof(UI_AssetBrowserSlotItem),"Setup")]
    class UI_AssetBrowserSlotItemConstructorPatch
    {
        internal static Dictionary<NGuid, string> TextLookup = new Dictionary<NGuid, string>();
        internal static Dictionary<NGuid,MouseTextOnHover> items = new Dictionary<NGuid, MouseTextOnHover>();
        
        internal static void refreshText(object o)
        {
            var keys = new List<NGuid>();
            foreach (var key in items.Keys)
            {
                if (items[key] != null)
                    SetText(items[key], key);
                else keys.Add(key);
            }
            foreach (var key in keys) items.Remove(key);
        }

        private static void LoadDictionary()
        {
            TextLookup = AssetDb.GetAllGroups()
                .SelectMany(g => g.Item2)
                .SelectMany(g => g.Entries)
                .DistinctBy(g => g.Id)
                .ToDictionary(g => g.Id, g => g.Name);
        }

        private static void Postfix(UI_AssetBrowserSlotItem __instance, NGuid ____nGuid)
        {
            var TextOnHover = __instance.gameObject.AddComponent<MouseTextOnHover>();
            if (!items.ContainsKey(____nGuid)) items.Add(____nGuid, TextOnHover);
            else items[____nGuid] = TextOnHover;
            
            if (TextLookup.ContainsKey(____nGuid))
            {
                SetText(TextOnHover, ____nGuid);
                return;
            }
            if (!TextLookup.ContainsKey(____nGuid)) LoadDictionary();
            SetText(TextOnHover,____nGuid);
        }

        private static void SetText(MouseTextOnHover TextOnHover, NGuid ____nGuid)
        {
            TextOnHover.mouseHoverText = AssetToolTip.AssetsToolTip.Value ? TextLookup[____nGuid] : "";
        }
    }
}
