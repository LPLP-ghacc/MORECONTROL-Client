using System;
using UnityEngine;
using UnityEngine.UI;
using VRC;

namespace MoreControl.Utilts
{
    static class RCGetPlayer
    {
        /// <summary>
        /// Returns the player name and rank to the PlayerName textbox when the right mouse button is pointed and clicked.
        /// </summary>
        public static void Update()
        {
            if (Input.GetKey(KeyCode.Mouse1) && (GetPlayer() != null))
            {
                Text TextBox = GameObject.Find("Canvas/PlayerName").GetComponent<Text>();

                if (GetPlayer().field_Private_APIUser_0.hasLegendTrustLevel)
                {
                    TextBox.text = $"{GetPlayer().field_Private_APIUser_0.displayName} [rank: LEGEND]";
                    return;
                }
                else if (GetPlayer().field_Private_APIUser_0.hasVeteranTrustLevel)
                {
                    TextBox.text = $"{GetPlayer().field_Private_APIUser_0.displayName} [rank: Trust]";
                    return;
                }
                else if (GetPlayer().field_Private_APIUser_0.hasTrustedTrustLevel)
                {
                    TextBox.text = $"{GetPlayer().field_Private_APIUser_0.displayName} [rank: Known]";
                    return;
                }
                else if (GetPlayer().field_Private_APIUser_0.hasKnownTrustLevel)
                {
                    TextBox.text = $"{GetPlayer().field_Private_APIUser_0.displayName} [rank: User]";
                    return;
                }
                else if (GetPlayer().field_Private_APIUser_0.hasNegativeTrustLevel)
                {
                    TextBox.text = $"{GetPlayer().field_Private_APIUser_0.displayName} [rank: hasNegativeTrustLevel]";
                    return;
                }
                else if (GetPlayer().field_Private_APIUser_0.hasVeryNegativeTrustLevel)
                {
                    TextBox.text = $"{GetPlayer().field_Private_APIUser_0.displayName} [rank: hasVeryNegativeTrustLevel]";
                    return;
                }
                else
                {
                    TextBox.text = $"{GetPlayer().field_Private_APIUser_0.displayName} [rank: kid]";
                    return;
                }
            }
            if (CheckTime(5)) GameObject.Find("Canvas/PlayerName").GetComponent<Text>().text = "";
        }

        /// <summary>
        /// Returns the player when called
        /// </summary>
        /// <returns></returns>
        public static Player GetPlayer()
        {
            var Rayposition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = Camera.main.ScreenPointToRay(Rayposition); RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return (GetPlayerByRayCast(hit));
            }
            return null;
        }

        private static Player GetPlayerByRayCast(this RaycastHit RayCast)
        {
            var gameObject = RayCast.transform.gameObject;
            return gameObject.GetComponent<Player>();
        }

        #region ClearByTime
        private static bool CheckTime(int delay)
        {
            var time = UnixTimeNow();
            if (timestamp > time) return false;
            timestamp = time + delay;
            return true;
        }
        private static long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        private static long timestamp = 0;
        #endregion
    }
}
