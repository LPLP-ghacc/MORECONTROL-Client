using MelonLoader;
using MoreControl.Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreControl.Utilts
{
    class PublicFields
    {
        public static bool IsRapeThereEars = false;

        public static bool IsExperimental = false;

        #region worldController
        public static bool IsApplicationStart = false; //0

        public static bool IsLoadingScreen = false; //1

        public static bool IsEmptyWorld = false; //2

        public static bool IsWorldLoaded = false; //-1

        /// <summary>
        /// return player value
        /// </summary>
        /// <returns></returns>
        public static bool IsPlayerExist()
        {
            if (UnityEngine.GameObject.FindGameObjectWithTag("Player") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Controls the worlds states
        /// </summary>
        /// <param name="world">world number</param>
        public static void WorldLoader(int world)
        {
            switch (world)
            {
                case 0:
                    IsApplicationStart = true;
                    IsLoadingScreen = false;
                    IsEmptyWorld = false;
                    IsWorldLoaded = false;
                    break;
                case 1:
                    IsLoadingScreen = true;
                    IsEmptyWorld = false;
                    IsWorldLoaded = false;
                    break;
                case 2:
                    IsEmptyWorld = true;
                    IsWorldLoaded = false;
                    break;
                case -1:
                    IsWorldLoaded = true;
                    break;
            }
        }
        #endregion 

        public static void Update()
        {
            if(IsApplicationStart)
            {
                NoSteam.OnStart();
            }
            if(IsEmptyWorld)
            {
                ConsoleController.Init(); 
            }
        }

        //public static void dwde()
        //{
        //    var frames = player._playerNet.field_Private_Byte_0; 
        //    && ping  player._playerNet.field_Private_Byte_1
        //}
    }
}
