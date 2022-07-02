using MoreControl.Utilts;
using UnityEngine;
using VRCSDK2;

namespace MoreControl.Movement
{
    class SpawnObjects
    {
		public static GameObject gameObject = null;
		private static bool _isKinematic = false;

		public static void Update()
        {
			if(Input.GetKeyDown(KeyCode.L))
            {
				try
				{
					OnCreate();
					if (gameObject != null)
					{
						Log.Msg($"Object spawned on {GetPlayer.GetVRCPlayer().transform.position}");
					}
				}
				catch { }
			}
		}

		private static void OnCreate()
		{
			OnDestroy();

			Transform transform = GetPlayer.GetVRCPlayer().transform;
			Vector3 position = transform.position + (transform.forward / .75f);
			position.y += .4f;
			Log.Msg("1");

			#region Create Object
			gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gameObject.transform.position = position;
			gameObject.transform.rotation = transform.rotation;
			gameObject.transform.localScale = new Vector3(.025f, .025f, .025f);
			gameObject.GetComponent<Collider>();
			Log.Msg("2");
			#endregion
			#region BoxCollider
			BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
			boxCollider.size = new Vector3(1f, 1f, 0.05f);
			boxCollider.isTrigger = true;
			Log.Msg("3");
			#endregion
			#region VRCPickup
			VRC_Pickup pickup = gameObject.AddComponent<VRC_Pickup>();
			pickup.proximity = 0.3f;
			pickup.pickupable = true;
			pickup.orientation = VRC.SDKBase.VRC_Pickup.PickupOrientation.Grip;
			pickup.allowManipulationWhenEquipped = false;
			Log.Msg("4");
			#endregion
			#region Rigidbody
			Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
			rigidbody.useGravity = true;
			rigidbody.isKinematic = _isKinematic;
			#endregion
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			Log.Msg("5");
		}

		public static void OnDestroy()
		{
			UnityEngine.Object.Destroy(gameObject);
			gameObject = null;
		}
	}
}
