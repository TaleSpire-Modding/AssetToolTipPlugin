using Bounce.Unmanaged;
using HarmonyLib;
using System.Collections.Generic;
using SRF;

namespace AssetToolTip.Patches
{
    [HarmonyPatch(typeof(UI_AssetBrowserSlotItem),"Setup")]
    sealed class UI_AssetBrowserSlotItemConstructorPatch
    {
        internal static Dictionary<NGuid, string> TextLookup = new Dictionary<NGuid, string>();
        internal static Dictionary<NGuid,MouseTextOnHover> Items = new Dictionary<NGuid, MouseTextOnHover>();

        internal static void refreshText(object o)
        {
            foreach (var item in Items)
            {
                SetText(item.Value,item.Key);
            }
        }

        private static void LoadDictionary()
        {
            foreach (var creature in AssetDb.Creatures)
            {
                NGuid key = creature.Key.Value;
                if (!TextLookup.ContainsKey(key))
                {
                    TextLookup.Add(key, creature.Value.Value.Name.ToString());
                }
            }

            foreach (var placeable in AssetDb.Placeables)
            {
                NGuid key = placeable.Key.Value;
                if (!TextLookup.ContainsKey(key))
                {
                    TextLookup.Add(key, placeable.Value.Value.Name.ToString());
                }
            }
        }

        private static void Postfix(UI_AssetBrowserSlotItem __instance, NGuid ____nGuid)
        {
            var TextOnHover = __instance.gameObject.AddComponent<MouseTextOnHover>();
            if (!Items.ContainsKey(____nGuid)) Items.Add(____nGuid, TextOnHover);
            else Items[____nGuid] = TextOnHover;
            
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
