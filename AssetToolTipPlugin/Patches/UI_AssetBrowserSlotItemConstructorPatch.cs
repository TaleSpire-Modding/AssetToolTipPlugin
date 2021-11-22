using System.Linq;
using Bounce.Unmanaged;
using UnityEngine;
using HarmonyLib;

namespace AssetToolTip.Patches
{
    [HarmonyPatch(typeof(UI_AssetBrowserSlotItem),"Setup")]
    class UI_AssetBrowserSlotItemConstructorPatch
    {
        private static void Postfix(UI_AssetBrowserSlotItem __instance, AssetDb.DbEntry.EntryKind ____entityKind, NGuid ____nGuid)
        {
            var TextOnHover = __instance.gameObject.AddComponent<MouseTextOnHover>();

            var groups = AssetDb.GetAllGroups();
            var group = groups.Single(i => i.Item1 == ____entityKind);
            foreach (var g in group.Item2)
            {
                foreach (var a in g.Entries)
                {
                    if (a.Id == ____nGuid)
                    {
                        TextOnHover.mouseHoverText = a.Name;
                        return;
                    }
                }        
            }
        }
    }
}
