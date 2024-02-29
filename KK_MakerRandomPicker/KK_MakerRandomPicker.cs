﻿using System.Linq;
using HarmonyLib;
using BepInEx;
using ChaCustom;
using KKAPI.Maker;
using Random = UnityEngine.Random;

namespace KK_MakerRandomPicker
{
    [BepInProcess("Koikatu")]
    [BepInProcess("Koikatsu Party")]
    [BepInPlugin(nameof(KK_MakerRandomPicker), nameof(KK_MakerRandomPicker), VERSION)]
    public class KK_MakerRandomPicker : BaseUnityPlugin
    {
        public const string VERSION = "1.3.1";
        
        public static KK_MakerRandomPicker instance;
        public static CustomSelectListCtrl controller;
        
        private void Awake()
        {
            instance = this;
            
            MakerAPI.RegisterCustomSubCategories += Tools.MakerAPI_RegisterCustomSubCategories;
            Harmony.CreateAndPatchAll(typeof(Hooks), "MakerRandomPicker");
        }
        
        public static void PickRandomItem()
        {
            if (controller == null)
                return;

            var datas = controller.lstSelectInfo.Where(info => !info.disvisible).ToList();
            var data = datas[Random.Range(0, datas.Count)];

            if (data == null)
                return;
        
            controller.SelectItem(data.index);
        }
    }
}