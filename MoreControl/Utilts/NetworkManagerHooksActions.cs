using MoreControl.Utilts;
using MoreControl.Visual;
using System;
using System.Collections;
using VRC;
using VRC.Core;
using VRC.SDKBase;

namespace MoreControl
{
    class NetworkManagerHooksActions
    {
        public static void InitializeNetworkHooks()
        {
            VRCEventDelegate<Player> field_Internal_VRCEventDelegate_1_Player_ = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_0;

            VRCEventDelegate<Player> field_Internal_VRCEventDelegate_1_Player_2 = NetworkManager.field_Internal_Static_NetworkManager_0.field_Internal_VRCEventDelegate_1_Player_1;

            field_Internal_VRCEventDelegate_1_Player_.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<Player>(OnPlayerJoin));

            field_Internal_VRCEventDelegate_1_Player_2.field_Private_HashSet_1_UnityAction_1_T_0.Add(new Action<Player>(OnPlayerLeave));
        }

        public static void OnPlayerJoin(Player player)
        {
            PlayerList.Refresh();


            Log.UtilMessage($"Player {player.field_Private_VRCPlayerApi_0.displayName} joined.");

            //Log.CMsg($"Your friend {player.field_Private_VRCPlayerApi_0.displayName} joined.", ConsoleColor.Yellow);
        }

        public static void OnPlayerLeave(Player player)
        {
            try
            {
                if (player == null)
                    return;
                else
                {
                    PlayerList.Refresh();

                    Log.Error($"Player {player.field_Private_VRCPlayerApi_0.displayName} leave.");
                }
            }
            catch
            {

            }
        }
    }
}
    