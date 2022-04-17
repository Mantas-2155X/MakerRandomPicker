using BepInEx;
using CharaCustom;
using HarmonyLib;
using KKAPI.Maker;
using Random = UnityEngine.Random;

namespace AI_MakerRandomPicker
{
    [BepInProcess("AI-Syoujyo")]
    [BepInProcess("AI-Shoujo")]
    [BepInPlugin(nameof(AI_MakerRandomPicker), nameof(AI_MakerRandomPicker), VERSION)]
    public class AI_MakerRandomPicker : BaseUnityPlugin
    {
        public const string VERSION = "1.3.0";

        public static AI_MakerRandomPicker instance;

        private void Awake()
        {
            instance = this;
            
            MakerAPI.RegisterCustomSubCategories += Tools.MakerAPI_RegisterCustomSubCategories;
            Harmony.CreateAndPatchAll(typeof(Hooks), "MakerRandomPicker");
        }

        public static void PickRandomItem(int controllerIdx)
        {
            if (controllerIdx == 22)
            {
                var controller = (CustomPushScrollController)Tools.controllers[controllerIdx];
                if (controller == null)
                    return;

                var datas = controller.scrollerDatas;
                var data = datas?[Random.Range(0, datas.Length)];

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
                var data = datas?[Random.Range(0, datas.Length)];

                if (data?.info == null)
                    return;
            
                controller.OnValueChange(data, true);
                controller.view.MovePanelToItemIndex(controller.selectInfo.index / controller.countPerRow, 0);
            }
        }
    }
}