using ExitGames.Client.Photon;
using MelonLoader;
using Photon.Realtime;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;

namespace MoreControl.Utilts
{
    static class RapeThereEars
    {

        public static void Update()
        {
            MelonLoader.MelonCoroutines.Start(StartRapeThereEarsEnum());
        }

        private static IEnumerator StartRapeThereEarsEnum()
        {
            while (PublicFields.IsRapeThereEars)
            {
                byte[] VoiceData = Convert.FromBase64String("AgAAAKWkyYm7hjsA+H3owFygUv4w5B67lcSx14zff9FCPADiNbSwYWgE+O7DrSy5tkRecs21ljjofvebe6xsYlA4cVmgrd0=");
                byte[] nulldata = new byte[4];
                byte[] ServerTime = BitConverter.GetBytes(Networking.GetServerTimeInMilliseconds());
                Buffer.BlockCopy(nulldata, 0, VoiceData, 0, 4);
                Buffer.BlockCopy(ServerTime, 0, VoiceData, 4, 4);
                int num;
                for (int i = 0; i < 80; i = num + 1)
                {
                    if (!PublicFields.IsRapeThereEars)
                        yield break;
                    PhotonExtensions.OpRaiseEvent(1, VoiceData, new RaiseEventOptions
                    {
                        field_Public_ReceiverGroup_0 = 0,
                        field_Public_EventCaching_0 = 0
                    }, default(SendOptions));
                    num = i;
                }
                yield return new WaitForSecondsRealtime(0.1f);
                VoiceData = null;
                nulldata = null;
                ServerTime = null;
            }
            yield break;
        }
    }
}
