﻿using System;
using BepInEx;
using CharaCustom;
using HarmonyLib;
using Random = UnityEngine.Random;

namespace AI_MakerRandomPicker
{
    [BepInProcess("AI-Syoujyo")]
    [BepInProcess("AI-Shoujo")]
    [BepInPlugin(nameof(AI_MakerRandomPicker), nameof(AI_MakerRandomPicker), VERSION)]
    public class AI_MakerRandomPicker : BaseUnityPlugin
    {
        public const string VERSION = "1.0.0";

        private void Awake()
        {
            Random.InitState(Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds()));
            Harmony.CreateAndPatchAll(typeof(Hooks));
        }

        public static void PickRandomItem(int controllerIdx)
        {
            if (controllerIdx == 22)
            {
                var controller = (CustomPushScrollController)Tools.controllers[controllerIdx];
                if (controller == null)
                    return;

                var datas = controller.scrollerDatas;
                var data = datas?[Random.Range(0, datas.Length - 1)];

                if (data?.info == null)
                    return;
            
                controller.OnClick(data);
                controller.view.MovePanelToItemIndex(data.index / controller.countPerRow, 0);
            }
            else
            {
                var controller = (CustomSelectScrollController)Tools.controllers[controllerIdx];
                if (controller == null)
                    return;

                var datas = controller.scrollerDatas;
                var data = datas?[Random.Range(0, datas.Length - 1)];

                if (data?.info == null)
                    return;
            
                controller.OnValueChange(data, true);
                controller.view.MovePanelToItemIndex(controller.selectInfo.index / controller.countPerRow, 0);
            }
        }
    }
}