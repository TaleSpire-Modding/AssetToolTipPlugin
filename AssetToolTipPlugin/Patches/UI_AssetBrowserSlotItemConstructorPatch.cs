using HarmonyLib;

namespace AssetToolTip.Patches
{
    [HarmonyPatch(typeof(UI_AssetBrowserSlotItem),"Setup")]
    sealed class UI_AssetBrowserSlotItemConstructorPatch
    {

        private static void Postfix(UI_AssetBrowserSlotItem __instance, ref UI_AssetLibraryCategory.DataItem dataItem)
        {
            __instance.gameObject.AddComponent<MouseTextOnHover>().mouseHoverText = dataItem.Name;
        }
    }
}
