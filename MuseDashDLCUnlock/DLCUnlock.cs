using MelonLoader;
using HarmonyLib;
using Il2CppSteamworks;
using Il2Cpp;
using MuseDashDLCUnlock;

[assembly: MelonInfo(typeof(DLCUnlock), "Muse Dash DLC Unlock moddified", "1.1.0", "hoshisuzu")]
[assembly: MelonGame("PeroPeroGames", "MuseDash")]

namespace MuseDashDLCUnlock
{
    // 模拟 SteamManager 类
    namespace MuseDash
    {
        public class SteamManager
        {
            public bool m_DoSomething1;
            public bool m_DoSomething3;

            public void DLCVerify()
            {
                // 空实现，仅用于 Harmony Patch
            }
        }
    }

    [HarmonyPatch(typeof(SteamApps), nameof(SteamApps.BIsDlcInstalled))]
    public class DLCPatch
    {
        static bool Prefix(ref bool __result, AppId_t appID)
        {
#if DEBUG
            Melon<DLCUnlock>.Logger.Msg($"Called IsDlcInstalled for DLC {appID.m_AppId}");
#endif
            __result = true;
            return false;
        }
    }

    [HarmonyPatch(typeof(MuseDash.SteamManager), nameof(MuseDash.SteamManager.DLCVerify))]
    public class DLCVerifyPatch
    {
        static bool Prefix(MuseDash.SteamManager __instance)
        {
#if DEBUG
            Melon<DLCUnlock>.Logger.Msg("Bypassing DLCVerify");
#endif
            __instance.m_DoSomething1 = true;
            __instance.m_DoSomething3 = true;
            return false; // 不执行原始方法
        }
    }

    public class DLCUnlock : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Muse Dash DLC Unlocker loaded successfully.");
        }
    }
}