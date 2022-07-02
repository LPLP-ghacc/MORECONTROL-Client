using VRC;
using VRC.UI.Elements.Menus;
using VRC.DataModel.Core;

namespace MoreControl.Utilts
{
    public static class GetPlayer
    {
        public static VRCPlayer GetVRCPlayer()
        {
            return VRCPlayer.field_Internal_Static_VRCPlayer_0;
        }

        public static VRCPlayer GetVRCPlayer(this Player player)
        {
            return player.prop_VRCPlayer_0;
        }

        public static bool IsPlayerExist()
        {
            if (UnityEngine.GameObject.FindGameObjectWithTag("Player") != null)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public static Player GetSelectedPlayer()
        {
            foreach (Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
            {
                if (player.prop_APIUser_0.id == UnityEngine.GameObject.Find("UserInterface").gameObject.GetComponentInChildren<SelectedUserMenuQM>().field_Private_IUser_0.prop_String_0)
                {
                    return player;
                }
            }

            return null;
        }
    }
}
