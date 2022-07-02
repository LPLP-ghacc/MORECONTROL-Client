using MelonLoader;
using MoreControl;
using MoreControl.Utilts;
using MoreControl.Visual;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

[assembly: MelonInfo(typeof(InitMoreControl), "MoreControl", "1.1.5", "LPLP")]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonColor(ConsoleColor.Red)]

namespace MoreControl
{
    public partial class InitMoreControl : MelonMod
    {
        private int scenesLoaded;

        public override void OnSceneWasInitialized(int buildIndex, string sceneName) => PublicFields.WorldLoader(buildIndex);

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            scenesLoaded++;
            if (scenesLoaded == 2 && scenesLoaded <= 2)
            {
                MelonCoroutines.Start(StartUiManagerInitIEnumerator());
            }
        }

        private IEnumerator StartUiManagerInitIEnumerator()
        {
            while (VRCUiManager.prop_VRCUiManager_0 == null) yield return new WaitForEndOfFrame();

            MelonCoroutines.Start(InitUI.SetupHUD()); 
            
            NetworkManagerHooksActions.InitializeNetworkHooks();

            ThirdPersonView.OnStart(); ConsoleController.Init();
        }

        public override void OnUpdate() //Wollt ihr das Bett in Flammen sehen? 
        {
            ACRAM.Update();

            PublicFields.Update();

            OverrideUpdate.Influx(PublicFields.IsPlayerExist());
        }

        public override void OnApplicationQuit() => Process.GetCurrentProcess().Kill();
    }
}
