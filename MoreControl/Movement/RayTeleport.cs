using MoreControl.Utilts;
using UnityEngine;

namespace MoreControl.Movement
{
    class RayTeleport
    {
        public static void Update()
        {
            var rayend = new Vector3(0, 0, 0);

            Vector3 Rayposition = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.Mouse0))
            {
                GetPlayer.GetVRCPlayer().GetComponent<Collider>().enabled = false;
                Ray ray = Camera.main.ScreenPointToRay(Rayposition); RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) rayend = hit.point;
                else return;

                GetPlayer.GetVRCPlayer().transform.position = rayend;
                GetPlayer.GetVRCPlayer().GetComponent<Collider>().enabled = true;

                Log.Msg($"new player position: " +
                    $"{GetPlayer.GetVRCPlayer().transform.position.x}, " +
                    $"{GetPlayer.GetVRCPlayer().transform.position.y}, " +
                    $"{GetPlayer.GetVRCPlayer().transform.position.z}");
            }
        }
    }
}
