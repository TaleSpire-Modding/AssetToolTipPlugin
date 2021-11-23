using System.Collections.Generic;
using HarmonyLib;
using UnityEngine.UI;

namespace AssetToolTip.Patches
{
    [HarmonyPatch(typeof(UI_SwitchButtonGroup), "Start")]
    class UI_SwitchButtonGroupStartPatch
    {
        private static List<MouseTextOnHover> items = new List<MouseTextOnHover>();

        internal static void refreshText(object o)
        {
            foreach (var item in items)
            {
                SetText(item.gameObject.GetComponent<MouseTextOnHover>());
            }
        }

        public static void Postfix(ref List<Button> ____buttons)
        {
            foreach (var button in ____buttons)
            {
                var TextOnHover = button.gameObject.AddComponent<MouseTextOnHover>();
                items.Add(TextOnHover);
                SetText(TextOnHover);
            }
        }

        private static void SetText(MouseTextOnHover TextOnHover)
        {
            TextOnHover.mouseHoverText = AssetToolTip.CategoryToolTip.Value ? TextOnHover.gameObject.name : "";
        }
    }
}
