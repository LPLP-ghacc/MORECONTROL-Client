using MoreControl.Utilts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRCSDK2;

namespace MoreControl.Movement
{
    public static class PropDisabler
    {
        public static void PropController(bool IsClick)
        {
            if(IsClick)
            {
                var cached = UnityEngine.Object.FindObjectsOfType<VRC.SDKBase.VRC_Pickup>();
                Log.Msg(cached.Length);

                if(cached.Length > 0)
                {
                    foreach (var obj in cached)
                    {
                        obj.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                var cached = UnityEngine.Object.FindObjectsOfType<VRC.SDKBase.VRC_Pickup>();
                foreach (var obj in cached)
                {
                    obj.gameObject.SetActive(true);
                }
            }
        }
    }
}
