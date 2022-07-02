using MelonLoader;
using MoreControl.Utilts;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace MoreControl.Movement
{
    class propOrbit
    {
        public static Player TargetPlayer;

        public static VRC_Pickup[] cached;

        public static bool IsEnable = false;

        public static void Update()
        {
            try
            {
                if (IsEnable && TargetPlayer != null)
                {
                    cached = UnityEngine.Object.FindObjectsOfType<VRC_Pickup>();

                    foreach (VRC_Pickup vrc_Pickup in cached)
                    {
                        if (Networking.GetOwner(vrc_Pickup.gameObject) != Networking.LocalPlayer)
                        {
                            Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
                        }
                        var playerhead = TargetPlayer.prop_VRCPlayer_0.field_Internal_Animator_0.GetBoneTransform(HumanBodyBones.Head);

                        vrc_Pickup.transform.position = playerhead.transform.position;
                    }
                }
            }
            catch { }

            if (Input.GetKeyDown(KeyCode.H))
            {
                TargetPlayer = null; IsEnable = false;
            }
        }

        public static async Task ItemCrashAsync(Player player)
        {
            await Task.Run(() => MelonCoroutines.Start(ItemCrash(player)));
        }

        public static IEnumerator ItemCrash(Player Player)
        {
            if(Player.field_Private_VRCPlayerApi_0.displayName != Networking.LocalPlayer.displayName)
            {
                cached = UnityEngine.Object.FindObjectsOfType<VRC_Pickup>();

                if (cached != null)
                {
                    foreach (var Pickup in cached)
                    {
                        Networking.SetOwner(Networking.LocalPlayer, Pickup.gameObject);
                        Pickup.pickupable = true;
                        Pickup.ThrowVelocityBoostMinSpeed = int.MaxValue;
                        Pickup.ThrowVelocityBoostScale = int.MaxValue;
                        Pickup.transform.position = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);
                    }
                    yield return new WaitForSeconds(0.1f);

                    foreach (var Pickup in cached)
                    {
                        Networking.SetOwner(Networking.LocalPlayer, Pickup.gameObject);
                        Pickup.pickupable = true;
                        Pickup.ThrowVelocityBoostMinSpeed = int.MaxValue;
                        Pickup.ThrowVelocityBoostScale = int.MaxValue;
                        Pickup.transform.position = Player.field_Private_VRCPlayerApi_0.GetBonePosition(HumanBodyBones.Head);
                    }
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        public static bool IsAllPlayersItemCrash_Enable = false;

        public static void AllPlayersItemCrash_Update()
        {
            if(IsAllPlayersItemCrash_Enable)
            {
                try
                {
                    foreach (Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
                    {
                        if (Networking.LocalPlayer.displayName != player.field_Private_APIUser_0.displayName)
                        {
                            Log.Msg($"AllPlayersItemCrash! for: {player.field_Private_APIUser_0.displayName}");
                            MelonCoroutines.Start(ItemCrash(player));
                        }
                    }
                }
                catch { }
            }
        }
    }
}
