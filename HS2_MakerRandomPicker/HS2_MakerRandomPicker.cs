using BepInEx;
using CharaCustom;
using HarmonyLib;
using Random = UnityEngine.Random;

namespace HS2_MakerRandomPicker
{
    [BepInProcess("HoneySelect2")]
    [BepInPlugin(nameof(HS2_MakerRandomPicker), nameof(HS2_MakerRandomPicker), VERSION)]
    public class HS2_MakerRandomPicker : BaseUnityPlugin
    {
        public const string VERSION = "1.0.0";

        private void Awake()
        {
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