using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;
using VRC.UI.Elements.Menus;

namespace MoreControl.Utilts
{
    class VRCfields
    {
        public static void publicFields(Player targetPlayer)
        {
            try
            {
                foreach (var pipapipa in targetPlayer.field_Private_APIUser_0.steamDetails)
                {
                    Log.Msg($"key = {pipapipa.key} value = {pipapipa.value}");
                }
            }
            catch
            {
                foreach (var pipapipa in targetPlayer.field_Private_APIUser_0.steamDetails)
                {
                    Log.Msg($"Key = {pipapipa.Key}, Value = {pipapipa.Value}");
                }
            }
            finally
            {
                Log.Msg("theres a problem");
            }
        }
    }
}
