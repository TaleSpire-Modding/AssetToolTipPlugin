using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Reflection;
using TMPro;
using BepInEx;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Bounce.Unmanaged;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using DataModel;
using Unity.Mathematics;
using System.Collections;
using BepInEx.Logging;

namespace ModdingTales
{
    public static class ModdingUtils
    {
        private static BaseUnityPlugin parentPlugin;
        private static ManualLogSource parentLogger;

        public static TextMeshProUGUI GetUITextByName(string name)
        {
            TextMeshProUGUI[] texts = UnityEngine.Object.FindObjectsOfType<TextMeshProUGUI>();
            for (int i = 0; i < texts.Length; i++)
            {
                if (texts[i].name == name)
                {
                    return texts[i];
                }
            }
            return null;
        }

        public static void Initialize(BaseUnityPlugin parentPlugin, ManualLogSource logger)
        {
            AppStateManager.UsingCodeInjection = true;
            ModdingUtils.parentPlugin = parentPlugin;
            ModdingUtils.parentLogger = logger;
            parentLogger.LogInfo("Inside initialize");
            SceneManager.sceneLoaded += OnSceneLoaded;
            // By default do not start the socket server. It requires the caller to also call OnUpdate in the plugin update method.
        }

        public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            try
            {

                parentLogger.LogInfo("On Scene Loaded" + scene.name);
                UnityEngine.Debug.Log("Loading Scene: " + scene.name);
                if (scene.name == "UI")
                {
                    TextMeshProUGUI betaText = GetUITextByName("BETA");
                    if (betaText)
                    {
                        betaText.text = "INJECTED BUILD - unstable mods";
                    }
                }
                else
                {
                    TextMeshProUGUI modListText = GetUITextByName("TextMeshPro Text");
                    if (modListText)
                    {
                        BepInPlugin bepInPlugin = (BepInPlugin)Attribute.GetCustomAttribute(ModdingUtils.parentPlugin.GetType(), typeof(BepInPlugin));
                        if (modListText.text.EndsWith("</size>"))
                        {
                            modListText.text += "\n\nMods Currently Installed:\n";
                        }
                        modListText.text += "\n" + bepInPlugin.Name + " - " + bepInPlugin.Version;
                    }
                }
            }
            catch (Exception ex)
            {
                parentLogger.LogFatal(ex);
            }
        }
    }
}
