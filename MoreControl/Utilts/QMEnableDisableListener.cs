using MelonLoader;
using System;
using UnityEngine;

namespace MoreControl.Utilts
{ 
    [RegisterTypeInIl2Cpp]
    class QMEnableDisableListener : MonoBehaviour
    {
        public static bool Enabled;

        public QMEnableDisableListener(IntPtr obj0) : base(obj0)
        {
        }

        private void OnEnable()
        {
            Enabled = true;
        }

        private void OnDisable()
        {
            Enabled = false;
        }
    }
}
