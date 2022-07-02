using System;
using UnityEngine;
using VRCSDK2;

namespace MoreControl.Utilts
{
    public static class PortableMirror
    {
		private static GameObject gameObject = null;
		private static readonly float _mirrorScaleX = 5f, _mirrorScaleY = 3f;
		private static bool _optimizedMirror = false, _canPickupMirror = true;

		public static void Update()
		{
			if(Input.GetKey(KeyCode.M) && Input.GetKeyDown(KeyCode.I))
            {
				if (gameObject != null)
					OnDestroy();
				else
					OnCreate();
			}
		}

		private static void OnCreate()
		{
			OnDestroy();

			Transform transform = GetPlayer.GetVRCPlayer().transform;
			Vector3 position = transform.position + (transform.forward * 2);
			position.y += _mirrorScaleY / 2f;
			Log.Msg("1");

			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
			gameObject.transform.position = position;
			gameObject.transform.rotation = transform.rotation;
			gameObject.transform.localScale = new Vector3(_mirrorScaleX, _mirrorScaleY, 1f);
			Log.Msg("2");

			BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
			boxCollider.size = new Vector3(1f, 1f, 0.05f);
			boxCollider.isTrigger = true;
			Log.Msg("3");

			try
			{
				/*ПОЧИНИ ЭТОТ КОД*/
				MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
				meshRenderer.material.shader = Shader.Find("FX/MirrorReflection");
				Log.Msg("4");

				VRC_MirrorReflection mirrorReflection = gameObject.AddComponent<VRC_MirrorReflection>();
				LayerMask reflectLayers = new LayerMask();
				reflectLayers.value = (_optimizedMirror ? 263680 : -1025);
				mirrorReflection.m_ReflectLayers = reflectLayers;
				Log.Msg("5");
			}
			catch { }

			VRC_Pickup pickup = gameObject.AddComponent<VRC_Pickup>();
			pickup.proximity = 0.3f;
			pickup.pickupable = _canPickupMirror;
			pickup.allowManipulationWhenEquipped = false;
			Log.Msg("6");

			Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
			rigidbody.useGravity = false;
			rigidbody.isKinematic = false;
			Log.Msg("7");

			UnityEngine.Object.DontDestroyOnLoad(gameObject);

			PortableMirror.gameObject = gameObject;
			Log.Msg("8");
		}

		private static void OnDestroy()
		{
			UnityEngine.Object.Destroy(gameObject);
			gameObject = null;
		}
	}
}
