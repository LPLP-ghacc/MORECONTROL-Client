using MoreControl.Movement;
using MoreControl.Utilts;
using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VRC;
using VRC.SDKBase;

namespace MoreControl.Visual
{
    class InitUI
    {
        /*INIT*/
        public static IEnumerator SetupHUD()
        {
            yield return new WaitForSeconds(8f);

            InitCustomCanvas.InitCanvas();

            VRCUiMenu.Init();

            /*remove ads*/ GameObject.Find("/UserInterface")?.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners").gameObject.SetActive(false);
        }

        public static void Update()
        {
            if(CheckTime(5))
            PlayerList.Refresh();
        }

        private static long timestamp = 0;

        public static bool CheckTime(int _time)
        {
            long time = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            if (timestamp >= time) return false;
            timestamp = time + _time;
            return true;
        }
    }

    #region CustomMenuButtons
    static class VRCUiMenu
    {
        public static void Init()
        {
            OnHeadButton(); propOrbitButton(); /*PlayerInfo();*/ KillButton(); PropDisablerButton(); RapeThereEarsButton();
        }

        private static void PropDisablerButton()
        {
            bool IsClick = false;

            GameObject newButton;

            GameObject realButton = GameObject.Find("/UserInterface")?.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_SelectUser").gameObject;
            newButton = UnityEngine.Object.Instantiate(realButton, GameObject.Find("/UserInterface")?.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/"));
            newButton.name = "PropDisablerButton";
            var text = GameObject.Find("/UserInterface")?.transform.Find($"Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/{newButton.name}/Text_H4").GetComponent<TextMeshProUGUI>();
            text.text = "Props enabled";

            newButton.GetComponentInChildren<Button>().onClick.AddListener(new System.Action(() =>
            {
                if (!IsClick)
                {
                    PropDisabler.PropController(true);
                    Log.Msg("props are disabled");
                    text.text = "Props disabled";
                    IsClick = !IsClick;

                    return;
                }
                if (IsClick)
                {
                    PropDisabler.PropController(false);
                    Log.Msg("props are enabled");
                    text.text = "Props enabled";
                    IsClick = !IsClick;

                    return;
                }
            }));
        }

        private static void RapeThereEarsButton()
        {
            bool IsClick = false;

            GameObject newButton;
            GameObject realButton = GameObject.Find("/UserInterface")?.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_SelectUser").gameObject;
            newButton = UnityEngine.Object.Instantiate(realButton, GameObject.Find("/UserInterface")?.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/"));
            newButton.name = "RapeThereEars";
            var text = GameObject.Find("/UserInterface")?.transform.Find($"Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/{newButton.name}/Text_H4").GetComponent<TextMeshProUGUI>();
            text.text = "RapeThereEars off";
            text.fontSize = 18;

            newButton.GetComponentInChildren<Button>().onClick.AddListener(new System.Action(() =>
            {
                if (!IsClick)
                {
                    PublicFields.IsRapeThereEars = true;
                    Log.Msg("Start Rape There Ears");
                    text.text = "RapeThereEars on";
                    IsClick = !IsClick;

                    return;
                }
                if (IsClick)
                {
                    PublicFields.IsRapeThereEars = false;
                    Log.Msg("Rape There Ears ended");
                    text.text = "RapeThereEars off";
                    IsClick = !IsClick;
                    return;
                }               
            }));
        }

        /*selectable menu*/
        private static void OnHeadButton()
        {
            GameObject newButton;
            GameObject realButton = GameObject.Find("/UserInterface")?.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_PerUserInteraction/Button_InteractionOverrides/Buttons/Button C").gameObject;
            newButton = UnityEngine.Object.Instantiate(realButton, GameObject.Find("/UserInterface")?.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_PerUserInteraction/Button_InteractionOverrides/Buttons/"));
            newButton.name = "OnHead";
            GameObject text = GameObject.Find("/UserInterface")?.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_PerUserInteraction/Button_InteractionOverrides/Buttons/OnHead/Text_H4").gameObject;
            text.GetComponent<TextMeshProUGUI>().m_text = "On Head!";
            newButton.GetComponent<RectTransform>().sizeDelta = realButton.GetComponent<RectTransform>().sizeDelta;

            newButton.GetComponentInChildren<Button>().onClick.AddListener(new System.Action(() =>
            {
                OnHead.TargetPlayer = GetPlayer.GetSelectedPlayer(); OnHead.IsEnable = true;
            }));
        }

        private static void propOrbitButton()
        {
            GameObject newButton;
            GameObject realButton = GameObject.Find("/UserInterface")?.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_PerUserInteraction/Button_InteractionOverrides/Buttons/Button C").gameObject;
            newButton = UnityEngine.Object.Instantiate(realButton, GameObject.Find("/UserInterface")?.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_PerUserInteraction/Button_InteractionOverrides/Buttons/"));
            newButton.name = "Prop Orbit";
            GameObject text = GameObject.Find("/UserInterface")?.transform.Find($"Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_PerUserInteraction/Button_InteractionOverrides/Buttons/{newButton.name}/Text_H4").gameObject;
            text.GetComponent<TextMeshProUGUI>().m_text = "Prop Orbit";

            newButton.GetComponent<RectTransform>().sizeDelta = realButton.GetComponent<RectTransform>().sizeDelta;
            newButton.GetComponentInChildren<Button>().onClick.AddListener(new System.Action(() =>
            {
                propOrbit.TargetPlayer = GetPlayer.GetSelectedPlayer(); propOrbit.IsEnable = true;
                Log.Msg("Prop Orbit on");
            }));
        }

        private static void KillButton()
        {
            GameObject newButton;
            GameObject realButton = GameObject.Find("/UserInterface")?.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_PerUserInteraction/Button_InteractionOverrides/Buttons/Button C").gameObject;
            newButton = UnityEngine.Object.Instantiate(realButton, GameObject.Find("/UserInterface")?.transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_PerUserInteraction/Button_InteractionOverrides/Buttons/"));
            newButton.name = "Kill";
            GameObject text = GameObject.Find("/UserInterface")?.transform.Find($"Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_PerUserInteraction/Button_InteractionOverrides/Buttons/{newButton.name}/Text_H4").gameObject;
            text.GetComponent<TextMeshProUGUI>().m_text = "Kill";

            newButton.GetComponent<RectTransform>().sizeDelta = realButton.GetComponent<RectTransform>().sizeDelta;
            newButton.GetComponentInChildren<Button>().onClick.AddListener(new System.Action(() =>
            {
                //MelonLoader.MelonCoroutines.Start(propOrbit.ItemCrash(GetPlayer.GetSelectedPlayer()));
                ItemCrushStart(GetPlayer.GetSelectedPlayer());
                ItemCrushStart(GetPlayer.GetSelectedPlayer());
                ItemCrushStart(GetPlayer.GetSelectedPlayer());
                ItemCrushStart(GetPlayer.GetSelectedPlayer());
                ItemCrushStart(GetPlayer.GetSelectedPlayer());
                ItemCrushStart(GetPlayer.GetSelectedPlayer());
                ItemCrushStart(GetPlayer.GetSelectedPlayer());
                ItemCrushStart(GetPlayer.GetSelectedPlayer());
                ItemCrushStart(GetPlayer.GetSelectedPlayer());
                ItemCrushStart(GetPlayer.GetSelectedPlayer());

                Log.Msg($"Kill player:  {GetPlayer.GetSelectedPlayer().field_Private_APIUser_0.displayName} done!");
            }));
        }

        private static async void ItemCrushStart(Player player)
        {
            await propOrbit.ItemCrashAsync(player);
        }
    }
    #endregion

    #region CustomCanvas
    public static class InitCustomCanvas
    {
        public static GameObject inter_canvas;

        /// <summary>
        /// For display control
        /// </summary>
        public static void Update()
        {
            try
            {
                if(NoClip.IsEnable)
                {
                    var NCtext = GameObject.Find($"Canvas/Background/NC");
                    NCtext.GetComponent<Text>().text = $"NoClip: speed - {NoClip.Velocity}";
                    NCtext.SetActive(NoClip.IsEnable);
                }
                else
                {
                    GameObject.Find($"Canvas/Background/NC").SetActive(NoClip.IsEnable);
                }
                if (ESP.IsEnable)
                {
                    GameObject.Find($"Canvas/Background/ESP").SetActive(ESP.IsEnable);
                }
                else
                {
                    GameObject.Find($"Canvas/Background/ESP").SetActive(ESP.IsEnable);
                }
            }
            catch { }
        }

        public static void InitCanvas()
        {
            var canvasObject = new GameObject("Canvas");
            UnityEngine.Object.DontDestroyOnLoad(canvasObject);
            canvasObject.transform.position = new Vector3(0f, 0f, 0f);
            canvasObject.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            canvasObject.AddComponent<GraphicRaycaster>();
            canvasObject.AddComponent<GraphicRaycaster>().m_Canvas = canvasObject.GetComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            canvasScaler.referenceResolution = new Vector2((float)Screen.width, (float)Screen.height);
            canvasObject.layer = LayerMask.NameToLayer("UiMenu");
            canvasObject.AddComponent<LayoutElement>();
            canvasObject.AddComponent<EventSystem>();
            canvasObject.AddComponent<StandaloneInputModule>();
            canvasObject.AddComponent<BaseInput>();

            inter_canvas = canvasObject;

            CreateBottomPanel(canvasObject);
        }

        private static void CreateBottomPanel(GameObject canvas)
        {
            int wight = Screen.width; int height = Screen.height;

            float startPos = -840f; float diff = 110f;

            string BGName = "Background";

            NewBox.Create(BGName, canvas.transform, new Vector2(0f, ((height / 2) * -1) + 7f), new Vector2(wight, 15f), true);

            Transform BGparent = GameObject.Find($"Canvas/{BGName}").transform;

            #region BGParent
            NewText.Create("MC", BGparent, new Vector2(startPos, 0), "Hi, sweetie!", new Color32(255, 255, 255, 51), false);

            NewText.Create("NC", BGparent, new Vector2(startPos + diff, 0), "NoClip", new Color32(255, 255, 255, 51), false);

            NewText.Create("ESP", BGparent, new Vector2(startPos + (diff * 2.5f), 0), "ESP", new Color32(255, 255, 255, 51), false);
            #endregion
            
            NewText.Create("PlayerName", canvas.transform, new Vector2(0.64f, -15f), "", new Color32(255, 255, 255, 51), false);
        }
    }
    #endregion

    public static class PlayerList
    {
        static int PlayerCount = 1;

        static float diff = 15f;

        public static void Refresh()
        {
            try
            {
                Clear();
                Add();
            }
            catch { }   
        }

        private static void Add()
        {
            foreach(var player in UnityEngine.Object.FindObjectsOfType<Player>())
            {
                AddPlayer(player);
            }
        }

        private static void CreateTextBox(Player player, Color32 Color)
        {
            var trans = GameObject.Find("Canvas/Background/").transform;
            var _player = player.field_Private_APIUser_0;
            NewText.Create("PNText", trans, new Vector2(-840f, 1064f - (diff * PlayerCount)), _player.displayName, Color, true);
        }

        private static void AddPlayer(Player player)
        {
            var _player = player.field_Private_APIUser_0;
   
            if (_player.isFriend) //friend
            {
                CreateTextBox(player, new Color32(255, 255, 0, 100));
            }
            else if(Networking.LocalPlayer.displayName == _player.displayName) //self
            {
                CreateTextBox(player, new Color32(255, 99, 71, 100));
            }
            else if (_player.hasLegendTrustLevel) //legend
            {
                CreateTextBox(player, new Color32(0, 0, 0, 100));
            }
            else if (_player.hasVeteranTrustLevel) //trust
            {
                CreateTextBox(player, new Color32(75, 0, 130, 100));
            }
            else if (_player.hasTrustedTrustLevel) //known
            {
                CreateTextBox(player, new Color32(255, 165, 0, 51));
            }
            else if (_player.hasKnownTrustLevel) //User
            {
                CreateTextBox(player, new Color32(173, 255, 47, 51));
            }
            else
            {
                CreateTextBox(player, new Color32(255, 255, 255, 51));
            }
            PlayerCount++;

        }

        private static void Clear()
        {
            foreach (var textobg in UnityEngine.Object.FindObjectsOfType<UnityEngine.UI.Text>())
            {
                if (textobg.name == "PNText") GameObject.Destroy(textobg.gameObject);
            }
            PlayerCount = 1;
        }
    }

    public static class NewText
    {
        /// <summary>
        /// Create new text
        /// </summary>
        public static void Create(string name, Transform parent, Vector2 position, string Text, Color32 Color, bool AddBg)
        {
            var textObject = new GameObject(string.Format(name));

            textObject.transform.SetParent(parent, false);

            textObject.AddComponent<CanvasRenderer>();

            textObject.AddComponent<Text>();

            textObject.AddComponent<Button>().onClick.AddListener(new System.Action(() =>
            {
                Log.Msg("test");
            }));

            textObject.GetComponent<RectTransform>().sizeDelta = new Vector2(228f, 33f);

            textObject.GetComponent<RectTransform>().localPosition = position;

            textObject.GetComponent<Text>().color = Color; /*DEFAULT: 255, 255, 255, 51*/

            textObject.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");

            textObject.GetComponent<Text>().text = Text;

            textObject.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

            textObject.layer = LayerMask.NameToLayer("UiMenu");

            textObject.SetActive(true);

            if(AddBg)
            {
                NewBox.Create("box", textObject.transform, Vector2.zero, new Vector2(228f, 17f), true);
            }
        }
    }

    public static class NewBox
    {
        public static void Create(string name, Transform parent, Vector2 position, Vector2 size, bool IsTransparent)
        {
            var BOX = new GameObject(name);

            BOX.AddComponent<CanvasRenderer>();

            BOX.AddComponent<RawImage>();

            BOX.transform.SetParent(parent, false);

            BOX.GetComponent<RectTransform>().localPosition = position;

            if(size == Vector2.zero)
            {
                BOX.GetComponent<RectTransform>().sizeDelta = new Vector2(15f, 7f);
            }
            else
            {
                BOX.GetComponent<RectTransform>().sizeDelta = size;
            }

            BOX.GetComponent<RawImage>().color = (IsTransparent) ? new Color32(51, 51, 51, 70) : new Color32(51, 51, 51, 100);
        }
    }
    public static class ConsoleController
    {
        private const string message = @" _____ ______   ________  ________  _______   ________  ________  ________   _________  ________  ________  ___          " +
                                       @"|\   _ \  _   \|\   __  \|\   __  \|\  ___ \ |\   ____\|\   __  \|\   ___  \|\___   ___|\   __  \|\   __  \|\  \         " +
                                       @"\ \  \\\__\ \  \ \  \|\  \ \  \|\  \ \   __/|\ \  \___|\ \  \|\  \ \  \\ \  \|___ \  \_\ \  \|\  \ \  \|\  \ \  \        " +
                                       @" \ \  \\|__| \  \ \  \\\  \ \   _  _\ \  \_|/_\ \  \    \ \  \\\  \ \  \\ \  \   \ \  \ \ \   _  _\ \  \\\  \ \  \       " +
                                       @"  \ \  \    \ \  \ \  \\\  \ \  \\  \\ \  \_|\ \ \  \____\ \  \\\  \ \  \\ \  \   \ \  \ \ \  \\  \\ \  \\\  \ \  \____  " +
                                       @"   \ \__\    \ \__\ \_______\ \__\\ _\\ \_______\ \_______\ \_______\ \__\\ \__\   \ \__\ \ \__\\ _\\ \_______\ \_______\" +
                                       @"    \|__|     \|__|\|_______|\|__|\|__|\|_______|\|_______|\|_______|\|__| \|__|    \|__|  \|__|\|__|\|_______|\|_______|" +
                                       @"                                                                                                                         ";

        private static bool IsClear = false;

        public static void Init()
        {
            if(!IsClear) 
            { 
                Console.Clear();
                Console.WriteLine(message, System.ConsoleColor.Red);
                Console.Title = "MORECONTROL CLIENT";
                //Log.CMsg(message, System.ConsoleColor.Red);
                IsClear = !IsClear; 
            }
        }

        /// <summary>
        /// put ConsoleColor values
        /// </summary>
        public static void ConsoleColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }
    }
}
