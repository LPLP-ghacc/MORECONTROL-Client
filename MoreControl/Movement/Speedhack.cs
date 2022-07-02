using MoreControl.Utilts;
using UnityEngine;

namespace MoreControl.Movement
{
    class Speedhack
    {
        private static float normalTimeScale = 1f;

        private static float MoreSpeed = 2f;

        public static bool IsEnable { get; private set; }

        public static void Update()
        {
            if (Input.GetKeyDown(KeyBinds.KeyBinds.SpeedHackKey))
            {
                if (!IsEnable)
                {
                    IsEnable = !IsEnable;
                    Log.Msg("SpeedHack on");
                    return;
                }
                if (IsEnable)
                {
                    IsEnable = !IsEnable;
                    Log.Msg("SpeedHack off");
                    return;
                }
            }

            if(IsEnable)
            {
                Time.timeScale = MoreSpeed;
            }else
            {
                Time.timeScale = normalTimeScale;
            }
        }
    }
}
