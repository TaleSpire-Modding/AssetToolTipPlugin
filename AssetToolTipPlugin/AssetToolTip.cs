using BepInEx;
using HarmonyLib;
using PluginUtilities;
using UnityEngine;

namespace AssetToolTip
{
    [BepInPlugin(Guid, Name, Version)]
    [BepInDependency(SetInjectionFlag.Guid)]
    public sealed class AssetToolTip : BaseUnityPlugin
    {
        // constants
        public const string Guid = "org.hollofox.plugins.AssetToolTip";
        private const string Version = "0.0.0.0";
        private const string Name = "Asset Tool Tip";

        /// <summary>
        /// Awake plugin
        /// </summary>
        void Awake()
        {
            Logger.LogInfo("In Awake for AssetToolTip");
            Debug.Log("AssetToolTip Plug-in loaded");
            ModdingTales.ModdingUtils.AddPluginToMenuList(this, "HolloFoxes'");

            var harmony = new Harmony(Guid);
            harmony.PatchAll();
        }
    }
}