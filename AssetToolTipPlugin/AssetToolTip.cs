using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace AssetToolTip
{
    [BepInPlugin(Guid, "HolloFoxe's Asset Tool Tip", Version)]
    public class AssetToolTip : BaseUnityPlugin
    {
        // constants
        private const string Guid = "org.hollofox.plugins.AssetToolTip";
        private const string Version = "1.0.0.0";

        /// <summary>
        /// Awake plugin
        /// </summary>
        void Awake()
        {
            Logger.LogInfo("In Awake for AssetToolTip");
            Debug.Log("AssetToolTip Plug-in loaded");
            ModdingTales.ModdingUtils.Initialize(this, Logger);

            var harmony = new Harmony(Guid);
            harmony.PatchAll();
        }
    }
}