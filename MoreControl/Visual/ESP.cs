using MoreControl.Utilts;
using UnityEngine;

namespace MoreControl.Visual
{
    class ESP
    {
        public static bool IsEnable = false;

        public static void Update()
        {
            if (Input.GetKeyDown(KeyBinds.KeyBinds.ESPKey))
            {
                if (!IsEnable)
                {
                    foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (gameObject.transform.Find("SelectRegion"))
                        {
                            HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(gameObject.transform.Find("SelectRegion").GetComponent<Renderer>(), true);
                            gameObject.transform.Find("SelectRegion").GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.magenta);
                            HighlightsFX.field_Private_Static_HighlightsFX_0.field_Protected_Material_0.SetColor("_HighlightColor", new Color(0.5f, 0f, 0.7f, 1f));
                        }
                    }

                    IsEnable = !IsEnable;
                    Log.Msg("ESP on");
                    return;
                }
                else
                {
                    foreach (GameObject gameObject2 in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (gameObject2.transform.Find("SelectRegion"))
                        {
                            HighlightsFX.prop_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(gameObject2.transform.Find("SelectRegion").GetComponent<Renderer>(), false);
                        }
                    }

                    IsEnable = !IsEnable;
                    Log.Msg("ESP off");
                    return;
                }
            }
        }
    }
}
