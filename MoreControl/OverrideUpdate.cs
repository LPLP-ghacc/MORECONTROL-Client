using MoreControl.Movement;
using MoreControl.Utilts;
using MoreControl.Visual;
using System.Threading.Tasks;

namespace MoreControl
{
    class OverrideUpdate
    {
        public static async void InfluxAsync(bool IsPlayerExist) //dont use
        {
            await Task.Run(() => Influx(IsPlayerExist));
        }

        public static int Influx(bool IsPlayerExist)
        {
            if(IsPlayerExist)
            {
                ESP.Update();
                NoClip.Update();
                OnHead.Update();
                InitUI.Update();
                propOrbit.Update();
                Speedhack.Update();
                RayTeleport.Update();
                RCGetPlayer.Update();
                SpawnObjects.Update();
                RapeThereEars.Update();
                PortableMirror.Update();
                FriendNotifier.Update();
                ThirdPersonView.Update();
                InitCustomCanvas.Update();
                propOrbit.AllPlayersItemCrash_Update();
                return 1;
            }
            else return 0;
        }
    }
}
