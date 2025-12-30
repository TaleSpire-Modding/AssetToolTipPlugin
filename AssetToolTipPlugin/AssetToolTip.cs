using BepInEx;
using HarmonyLib;
using PluginUtilities;
using System.Reflection;
using UnityEngine;

namespace AssetToolTip
{
    [BepInPlugin(Guid, Name, Version)]
    [BepInDependency(SetInjectionFlag.Guid)]
    public sealed class AssetToolTip : DependencyUnityPlugin
    {
        // constants
        public const string Guid = "org.hollofox.plugins.AssetToolTip";
        private const string Version = "0.0.0.0";
        private const string Name = "Asset Tool Tip";

        private Harmony harmony;

        /// <summary>
        /// Awake plugin
        /// </summary>
        protected override void OnAwake()
        {
            Logger.LogDebug("In Awake for AssetToolTip");
            ModdingTales.ModdingUtils.AddPluginToMenuList(this, "HolloFoxes'");

            harmony = new Harmony(Guid);
            harmony.PatchAll();


            // Add MouseTextOnHover to existing slot items
            Object[] slotItems = Resources.FindObjectsOfTypeAll(typeof(UI_AssetBrowserSlotItem));

            FieldInfo field = typeof(UI_AssetBrowserSlotItem)
                .GetField("_gridData", BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (Object item in slotItems)
            {
                UI_AssetBrowserSlotItem slotItem = item as UI_AssetBrowserSlotItem;

                if (slotItem == null)
                    continue;

                MouseTextOnHover mouseText = slotItem.GetComponent<MouseTextOnHover>();
                if (mouseText != null)
                    continue;

                UI_AssetLibraryCategory.DataItem data = field.GetValue(slotItem) as UI_AssetLibraryCategory.DataItem;
                slotItem.gameObject.AddComponent<MouseTextOnHover>().mouseHoverText = data.Name;
            }
        }

        protected override void OnDestroyed()
        {
            Logger.LogDebug("In OnDestroyed for AssetToolTip");
            harmony.UnpatchSelf();
            Logger.LogDebug("Unpatched all Harmony patches for AssetToolTip");

            // Remove MouseTextOnHover from existing slot items
            Object[] slotItems = Resources.FindObjectsOfTypeAll(typeof(UI_AssetBrowserSlotItem));
            foreach (Object item in slotItems)
            {
                UI_AssetBrowserSlotItem slotItem = item as UI_AssetBrowserSlotItem;
                if (slotItem != null)
                {
                    MouseTextOnHover mouseText = slotItem.GetComponent<MouseTextOnHover>();
                    if (mouseText != null)
                    {
                        Object.Destroy(mouseText);
                    }
                }
            }
        }
    }
}