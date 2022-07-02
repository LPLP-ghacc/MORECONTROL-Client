using MoreControl.Utilts;
using System;
using MelonLoader;
using UnityEngine;
using MoreControl.Movement;

namespace MoreControl.Visual
{
    class ThirdPersonView
    {
        private static CameraMode currentMode = CameraMode.Normal;

        private static CameraBehindMode cameraBehindMode = CameraBehindMode.Center;

        private static Camera thirdPersonCamera;

        private static Camera vrcCamera;

        public static bool IsEnabled = false;

        public static bool OnStart()
        {
            try
            {
                vrcCamera = GameObject.Find("Camera (eye)")?.GetComponent<Camera>();

                if (vrcCamera == null)
                {
                    Log.Error("Couldn't find camera... mod won't run...");
                    return false;
                }

                var originalCameraTransform = vrcCamera.transform;
                thirdPersonCamera = new GameObject("Standalone ThirdPerson Camera").AddComponent<Camera>();
                thirdPersonCamera.fieldOfView = ModSettings.FOV;
                thirdPersonCamera.nearClipPlane = ModSettings.NearClipPlane;
                thirdPersonCamera.enabled = false;
                thirdPersonCamera.transform.parent = originalCameraTransform.parent;

                GameObject.Find("UserInterface/QuickMenu/MicControls").AddComponent<QMEnableDisableListener>();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void RepositionCamera(bool isBehind, CameraBehindMode cameraBehindMode)
        {
            var vrcCameraTransform = vrcCamera.transform;
            var thirdPersonCameraTransform = thirdPersonCamera.transform;
            thirdPersonCameraTransform.parent = vrcCameraTransform;
            thirdPersonCameraTransform.position = vrcCameraTransform.position + (isBehind ? -vrcCameraTransform.forward : vrcCameraTransform.forward);
            thirdPersonCameraTransform.LookAt(vrcCameraTransform);
            if (isBehind)
            {
                if (cameraBehindMode == CameraBehindMode.RightShoulder)
                    thirdPersonCameraTransform.position += vrcCameraTransform.right * 0.5f;
                if (cameraBehindMode == CameraBehindMode.LeftShoulder)
                    thirdPersonCameraTransform.position -= vrcCameraTransform.right * 0.5f;
            }

            thirdPersonCameraTransform.position += thirdPersonCameraTransform.forward * 0.25f; // Reverse + = In  && - = Out
        }

        private static void FreeformCameraUpdate()
        {
            float h = 0;
            if (Input.GetKey(KeyCode.UpArrow)) h -= 1f;
            if (Input.GetKey(KeyCode.DownArrow)) h += 1f;
            float v = 0;
            if (Input.GetKey(KeyCode.LeftArrow)) v -= 1f;
            if (Input.GetKey(KeyCode.RightArrow)) v += 1f;
            thirdPersonCamera.transform.eulerAngles += new Vector3(h, v, 0);

            Vector3 movement = Vector3.zero;
            if (Input.GetKey(KeyCode.PageUp)) movement += thirdPersonCamera.transform.up;
            if (Input.GetKey(KeyCode.PageDown)) movement -= thirdPersonCamera.transform.up;
            if (Input.GetKey(KeyCode.K)) movement += thirdPersonCamera.transform.right;
            if (Input.GetKey(KeyCode.H)) movement -= thirdPersonCamera.transform.right;
            if (Input.GetKey(KeyCode.U)) movement += thirdPersonCamera.transform.forward;
            if (Input.GetKey(KeyCode.J)) movement -= thirdPersonCamera.transform.forward;

            thirdPersonCamera.transform.position += movement * Time.deltaTime * 1;
        }

        public static void UpdateCameraSettings()
        {
            if (thirdPersonCamera == null) return;
            thirdPersonCamera.fieldOfView = ModSettings.FOV;
            thirdPersonCamera.nearClipPlane = ModSettings.NearClipPlane;
            if (!ModSettings.Enabled)
                thirdPersonCamera.enabled = false;
        }

        public static void Update()
        {
            /*FOV CHANGE*/
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                var a = Camera.main;
                a.fieldOfView = a.fieldOfView + 2f;
            }
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                var a = Camera.main;
                a.fieldOfView = a.fieldOfView - 2f;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {

                currentMode++;
                if (currentMode > CameraMode.InFront) currentMode = CameraMode.Normal;
                if (currentMode != CameraMode.Normal)
                {
                    RepositionCamera(currentMode == CameraMode.Behind, cameraBehindMode);
                    thirdPersonCamera.enabled = true;
                }
                else
                {
                    thirdPersonCamera.enabled = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                if (currentMode == CameraMode.Freeform)
                {
                    currentMode = CameraMode.Normal;
                    thirdPersonCamera.enabled = false;
                }
                else
                {
                    currentMode = CameraMode.Freeform;
                    thirdPersonCamera.transform.parent = null;
                    thirdPersonCamera.enabled = true;
                }
            }
            if (currentMode != CameraMode.Normal)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    currentMode = CameraMode.Normal;
                    thirdPersonCamera.enabled = false;
                }

                thirdPersonCamera.transform.position += thirdPersonCamera.transform.forward * Input.GetAxis("Mouse ScrollWheel");
                if (currentMode == CameraMode.Freeform)
                {
                    FreeformCameraUpdate();
                }
                else if (currentMode == CameraMode.Behind && ModSettings.RearCameraChangedEnabled)
                {
                    if (Input.GetKeyDown(ModSettings.MoveRearCameraLeftKeyBind) && NoClip.IsEnable == false)
                    {
                        cameraBehindMode--;
                        if (cameraBehindMode <= CameraBehindMode.LeftShoulder)
                            cameraBehindMode = CameraBehindMode.LeftShoulder;
                        RepositionCamera(true, cameraBehindMode);
                    }

                    if (Input.GetKeyDown(ModSettings.MoveRearCameraRightKeyBind) && NoClip.IsEnable == false)
                    {
                        cameraBehindMode++;
                        if (cameraBehindMode > CameraBehindMode.RightShoulder)
                            cameraBehindMode = CameraBehindMode.RightShoulder;
                        RepositionCamera(true, cameraBehindMode);
                    }
                }
            }

            if (currentMode != CameraMode.Normal)
            {
                IsEnabled = true;
            }
        }

        public void OnPreferencesLoaded()
        {
            ModSettings.LoadSettings();
        }
        public void OnPreferencesSaved()
        {
            ModSettings.LoadSettings();
        }
    }

    public static class ModSettings
    {
        private static readonly string categoryName = "StandaloneThirdPerson";

        private static MelonPreferences_Entry<string> keyBind,
            freeformKeyBind,
            moveRearCameraLeftKeyBind,
            moveRearCameraRightKeyBind;

        private static MelonPreferences_Entry<float> fov, nearClipPlane;
        private static MelonPreferences_Entry<bool> enabled, rearCameraChangerEnabled;
        public static bool RearCameraChangedEnabled = true;


        public static KeyCode KeyBind { get; private set; } = KeyCode.T;
        public static KeyCode FreeformKeyBind { get; private set; } = KeyCode.None;
        public static KeyCode MoveRearCameraLeftKeyBind { get; private set; } = KeyCode.Q;
        public static KeyCode MoveRearCameraRightKeyBind { get; private set; } = KeyCode.E;
        public static float FOV { get; private set; } = 80;
        public static float NearClipPlane { get; private set; } = 0.01f;
        public static bool Enabled { get; private set; } = true;


        public static void RegisterSettings()
        {
            var category = MelonPreferences.CreateCategory(categoryName, categoryName);
            keyBind = category.CreateEntry("Keybind", KeyBind.ToString(), "Keybind");
            freeformKeyBind = category.CreateEntry("Freeform Keybind", FreeformKeyBind.ToString(), "Freeform Keybind");
            fov = category.CreateEntry("Camera FOV", FOV, "Camera FOV");
            nearClipPlane = category.CreateEntry("Camera NearClipPlane Value", NearClipPlane, "Camera NearClipPlane Value");
            enabled = category.CreateEntry("Mod Enabled", Enabled, "Mod Enabled");
            rearCameraChangerEnabled = category.CreateEntry("Rear Camera Changer Enabled", RearCameraChangedEnabled, "Rear Camera Changer Enabled");
            moveRearCameraLeftKeyBind = category.CreateEntry("Move Rear Camera Left KeyBind", MoveRearCameraLeftKeyBind.ToString(), "Move Rear Camera Left KeyBind");
            moveRearCameraRightKeyBind = category.CreateEntry("Move Rear Camera Right KeyBind", MoveRearCameraRightKeyBind.ToString(), "Move Rear Camera Right KeyBind");
        }

        public static void LoadSettings()
        {
            KeyBind = keyBind.TryParseKeyCodePref();
            FreeformKeyBind = freeformKeyBind.TryParseKeyCodePref(true);
            MoveRearCameraLeftKeyBind = moveRearCameraLeftKeyBind.TryParseKeyCodePref();
            MoveRearCameraRightKeyBind = moveRearCameraRightKeyBind.TryParseKeyCodePref();
            NearClipPlane = nearClipPlane.Value;
            FOV = fov.Value;    
            Enabled = enabled.Value;
            RearCameraChangedEnabled = rearCameraChangerEnabled.Value;
            ThirdPersonView.UpdateCameraSettings();
        }

        private static KeyCode TryParseKeyCodePref(this MelonPreferences_Entry<string> pref, bool canBeNone = false)
        {
            try
            {
                if (!canBeNone && pref.Value.Equals("None"))
                    throw new ArgumentException();
                return ParseKeyCode(pref.Value);
            }
            catch (ArgumentException)
            {
                MelonLogger.Error($"Failed to parse keybind defaulting back to: {pref.DefaultValue}");
                pref.Value = pref.DefaultValue;
                return ParseKeyCode(pref.Value);
            }
        }

        private static KeyCode ParseKeyCode(string value)
        {
            return (KeyCode)Enum.Parse(typeof(KeyCode), value);
        }
    }

    public enum CameraMode
    {
        Normal = 0,
        Behind = 1,
        InFront = 2,
        Freeform = 3
    }

    public enum CameraBehindMode
    {
        LeftShoulder = 0,
        Center = 1,
        RightShoulder = 2
    }
}
