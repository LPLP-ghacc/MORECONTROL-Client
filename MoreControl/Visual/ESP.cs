using MoreControl.Utilts;
using System.Collections;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace MoreControl.Visual
{
    class ESP
    {
        public static bool IsEnable = false;

        public static HighlightsFXStandalone friend, trust, known, user, newUser, kid;

        public static void Update()
        {
            MelonLoader.MelonCoroutines.Start(UpdateEnum());
        }

        public static IEnumerator UpdateEnum()
        {
            yield return new WaitForEndOfFrame(); try { ESPUpdate(); } catch { }
        }
        private static void ESPUpdate()
        {
            if (Input.GetKeyDown(KeyBinds.KeyBinds.ESPKey))
            {
                if (!IsEnable)
                {
                    IsEnable = !IsEnable; Log.Msg("ESP on"); return;
                }
                if (IsEnable)
                {
                    IsEnable = !IsEnable; Disable(); Log.Msg("ESP off"); return;
                }
            }
            if(IsEnable)
            {
                foreach (var GO in GameObject.FindGameObjectsWithTag("Player"))
                {
                    var player = GO.GetComponent<Player>(); Renderer rend; Transform trans = player.gameObject.transform.Find("SelectRegion");

                    rend = ((trans != null) ? trans.GetComponent<Renderer>() : null);

                    if (rend)
                    {
                        if (friend?.gameObject == null)
                        {
                            friend = GetHighlightsFX(); friend.highlightColor = Color.yellow;
                        }
                        if (trust?.gameObject == null)
                        {
                            trust = GetHighlightsFX(); trust.highlightColor = Color.magenta;
                        }
                        if (known?.gameObject == null)
                        {
                            known = GetHighlightsFX(); known.highlightColor = Color.red;
                        }
                        if (user?.gameObject == null)
                        {
                            user = GetHighlightsFX(); user.highlightColor = Color.green;
                        }
                        if (newUser?.gameObject == null)
                        {
                            newUser = GetHighlightsFX(); newUser.highlightColor = Color.blue;
                        }
                        if (kid?.gameObject == null)
                        {
                            kid = GetHighlightsFX(); kid.highlightColor = Color.white;
                        }

                        if(player.field_Private_APIUser_0.isFriend) friend.Method_Public_Void_Renderer_Boolean_0(rend, true);

                        else if (player.field_Private_APIUser_0.hasVeteranTrustLevel) trust.Method_Public_Void_Renderer_Boolean_0(rend, true);

                        else if (player.field_Private_APIUser_0.hasTrustedTrustLevel) known.Method_Public_Void_Renderer_Boolean_0(rend, true);

                        else if (player.field_Private_APIUser_0.hasKnownTrustLevel) user.Method_Public_Void_Renderer_Boolean_0(rend, true);

                        else
                        {
                            newUser.Method_Public_Void_Renderer_Boolean_0(rend, true);
                            kid.Method_Public_Void_Renderer_Boolean_0(rend, true);
                        }
                    }
                }
            }      
        }

        public static HighlightsFXStandalone GetHighlightsFX()
        {
            return HighlightsFX.prop_HighlightsFX_0.gameObject.AddComponent<HighlightsFXStandalone>();
        }

        private static void Disable()
        {
            foreach (var GO in GameObject.FindGameObjectsWithTag("Player"))
            {
                var player = GO.GetComponent<Player>(); Renderer rend; Transform trans = player.gameObject.transform.Find("SelectRegion"); rend = ((trans != null) ? trans.GetComponent<Renderer>() : null);

                if (player.field_Private_APIUser_0.isFriend) friend.Method_Public_Void_Renderer_Boolean_0(rend, false);

                else if (player.field_Private_APIUser_0.hasVeteranTrustLevel) trust.Method_Public_Void_Renderer_Boolean_0(rend, false);

                else if (player.field_Private_APIUser_0.hasTrustedTrustLevel) known.Method_Public_Void_Renderer_Boolean_0(rend, false);

                else if (player.field_Private_APIUser_0.hasKnownTrustLevel) user.Method_Public_Void_Renderer_Boolean_0(rend, false);

                else
                {
                    newUser.Method_Public_Void_Renderer_Boolean_0(rend, false);
                    kid.Method_Public_Void_Renderer_Boolean_0(rend, false);
                }
            }
        }
    }
}
