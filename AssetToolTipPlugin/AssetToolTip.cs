﻿using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using PluginUtilities;
using UnityEngine;

namespace AssetToolTip
{
    [BepInPlugin(Guid, "Asset Tool Tip", Version)]
    [BepInDependency(SetInjectionFlag.Guid)]
    [BepInDependency(ConfigurationManager.ConfigurationManager.Guid)]
    public sealed class AssetToolTip : BaseUnityPlugin
    {
        // constants
        private const string Guid = "org.hollofox.plugins.AssetToolTip";
        private const string Version = "0.0.0.0";
        internal static ConfigEntry<bool> AssetsToolTip;
        internal static ConfigEntry<bool> CategoryToolTip;
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

            AssetsToolTip = Config.Bind("ToolTips","Assets",true, 
                new ConfigDescription("",null,new ConfigurationManagerAttributes { CallbackAction = Patches.UI_AssetBrowserSlotItemConstructorPatch.refreshText}));

            CategoryToolTip = Config.Bind("ToolTips","Category",true,
                new ConfigDescription("", null, new ConfigurationManagerAttributes { CallbackAction = Patches.UI_SwitchButtonGroupStartPatch.refreshText }));
        }
    }
}