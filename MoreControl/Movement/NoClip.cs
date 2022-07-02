using MoreControl.Utilts;
using System;
using UnityEngine;

namespace MoreControl.Movement
{
    /*ボディ*//*ボディ*//*ボディ*//*ボディ*//*ボディ*//*ボディ*//*ボディ*//*ボディ*//*ボディ*//*ボディ*//*ボディ*/
    class NoClip
    {
        private static Vector3 normalGravity = Physics.gravity;
        public static bool IsEnable = false;
        public static float Velocity = 7f;

        public static void Update()
        {
            try
            {
                TryUpdate();
            } catch { }
        }

        private static void TryUpdate()
        {
            if (Input.GetKeyDown(KeyBinds.KeyBinds.NoClipKey))
            {
                if (!IsEnable)
                {
                    IsEnable = !IsEnable;
                    Log.Msg("NoClip on");
                    return;
                }
                if (IsEnable)
                {
                    IsEnable = !IsEnable;
                    Log.Msg("NoClip off");
                    return;
                }
            }

            if (IsEnable)
            {
                Physics.gravity = Vector3.zero; GetPlayer.GetVRCPlayer().GetComponent<Collider>().enabled = false; var mouseScroll = Input.mouseScrollDelta.y;

                if (Math.Abs(Input.GetAxis("Vertical")) != 0f) GetPlayer.GetVRCPlayer().transform.position += 
                        Camera.main.transform.forward * Time.deltaTime * Input.GetAxis("Vertical") * Velocity;
                if (Input.GetKey(KeyCode.E)) GetPlayer.GetVRCPlayer().transform.position += 
                        GetPlayer.GetVRCPlayer().transform.up * Velocity * Time.deltaTime;
                if (Input.GetKey(KeyCode.Q)) GetPlayer.GetVRCPlayer().transform.position += 
                        GetPlayer.GetVRCPlayer().transform.up * -Velocity * Time.deltaTime;
                if (Input.GetKey(KeyCode.W)) GetPlayer.GetVRCPlayer().transform.position += 
                        GetPlayer.GetVRCPlayer().transform.forward * Velocity * Time.deltaTime;
                if (Input.GetKey(KeyCode.S)) GetPlayer.GetVRCPlayer().transform.position += 
                        GetPlayer.GetVRCPlayer().transform.forward * -Velocity * Time.deltaTime;
                if (Input.GetKey(KeyCode.D)) GetPlayer.GetVRCPlayer().transform.position += 
                        GetPlayer.GetVRCPlayer().transform.right * Velocity * Time.deltaTime;
                if (Input.GetKey(KeyCode.A)) GetPlayer.GetVRCPlayer().transform.position += 
                        GetPlayer.GetVRCPlayer().transform.right * -Velocity * Time.deltaTime;

                if (mouseScroll == 1f) Velocity++;

                if (mouseScroll == -1f && Velocity != 1f) Velocity--;
            }
            else
            {
                Physics.gravity = normalGravity; GetPlayer.GetVRCPlayer().GetComponent<Collider>().enabled = true;
            }
        }
    }
}