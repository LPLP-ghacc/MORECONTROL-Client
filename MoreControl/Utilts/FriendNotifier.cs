using System;
using UnityEngine;
using UnityEngine.UI;

namespace MoreControl.Utilts
{
    public static class FriendNotifier
    {
        /*sender*//*sender*//*sender*//*sender*//*sender*/
        public static string friendName = "";

        public static bool IsEnable = false;
        /*sender*//*sender*//*sender*//*sender*//*sender*/

        /*ne nado eto zapuskat'.... poka chto*/
        public static void Update()
        {
            try
            {
                FriendNotivier();
            }
            catch (Exception ex) { Log.Msg($"govno tut {ex}"); }
        }

        private static int count = 0;

        private static void FriendNotivier()
        {
            var cache = UnityEngine.Object.FindObjectsOfType<LayoutElement>();
            if(cache.Length > 0)
            {
                foreach (var list in cache)
                {
                    count++;
                    if (list.name == $"UserUiPrefab(Clone) {count.ToString()}")
                    {
                        if (list.IsActive())
                        {
                            Log.Msg($"itrying to find in {list.transform.ToString()}");

                            friendName = GameObject.Find($"{list.transform.ToString()}/Background/TitleText").GetComponent<Text>().text;

                            IsEnable = !IsEnable;

                            Log.Msg(friendName);

                            return;
                        }
                        else return;
                    }
                    else return;
                }
            }else
            {
                Log.Error($"нету элементов в cache");
            }
        }

        private static DateTime unix = new DateTime(1970, 1, 1, 0, 0, 0);

        private static long timestamp = 0;

        static public bool CheckTime(int _time)
        {
            long time = (long)(DateTime.UtcNow - unix).TotalSeconds;
            if (timestamp >= time) return false;
            timestamp = time + _time;
            return true;
        }
    }
}
