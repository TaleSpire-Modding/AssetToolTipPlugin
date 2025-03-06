using BepInEx;
using HarmonyLib;
using PluginUtilities;
using UnityEngine;

namespace AssetToolTip
{
    [BepInPlugin(Guid, "Asset Tool Tip", Version)]
    [BepInDependency(SetInjectionFlag.Guid)]
    public sealed class AssetToolTip : BaseUnityPlugin
    {
        // constants
        private const string Guid = "org.hollofox.plugins.AssetToolTip";
        private const string Version = "0.0.0.0";

        /// <summary>
        /// Awake plugin
        /// </summary>
        void Awake()
        {
            Logger.LogInfo("In Awake for AssetToolTip");
            Debug.Log("AssetToolTip Plug-in loaded");
            ModdingTales.ModdingUtils.Initialize(this, Logger, "HolloFoxes'");

            var harmony = new Harmony(Guid);
            harmony.PatchAll();
        }
    }
}