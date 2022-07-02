using System;
using System.IO;
using System.Runtime.InteropServices;
using HarmonyLib;
using MelonLoader;

namespace MoreControl.Utilts
{
    class NoSteam
    {
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        public static void OnStart()
        {
            try
            {
                var path = MelonUtils.GetGameDataDirectory() + "\\Plugins\\steam_api64.dll";
                if (!File.Exists(path)) path = MelonUtils.GetGameDataDirectory() + "\\Plugins\\x86_64\\steam_api64.dll";
                if (!File.Exists(path)) path = MelonUtils.GetGameDataDirectory() + "\\Plugins\\x86\\steam_api64.dll";
                var library = LoadLibrary(path);
                if (library == IntPtr.Zero)
                {
                    MelonLogger.Error($"Library load failed; used path: {path}");
                    return;
                }
                var names = new[]
                {
                "SteamAPI_Init",
                "SteamAPI_RestartAppIfNecessary",
                "SteamAPI_GetHSteamUser",
                "SteamAPI_RegisterCallback",
                "SteamAPI_UnregisterCallback",
                "SteamAPI_RunCallbacks",
                "SteamAPI_Shutdown"
            };

                foreach (var name in names)
                {
                    unsafe
                    {
                        var address = GetProcAddress(library, name);
                        if (address == IntPtr.Zero)
                        {
                            MelonLogger.Error($"Procedure {name} not found");
                            continue;
                        }
                        MelonUtils.NativeHookAttach((IntPtr)(&address),
                            AccessTools.Method(typeof(NoSteam), nameof(InitFail)).MethodHandle
                                .GetFunctionPointer());
                    }
                }
            }
            catch
            {
                Log.Error("something went wrong, but okay...");
            }
        }

        public static bool InitFail()
        {
            return false;
        }
    }
}
