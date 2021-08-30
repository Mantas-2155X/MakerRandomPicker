using ChaCustom;
using HarmonyLib;

namespace EC_MakerRandomPicker
{
    public static class Hooks
    {
        [HarmonyPostfix, HarmonyPatch(typeof(CustomControl), "Initialize")]
        private static void CustomControl_Initialize_CreateUI()
        {
            Tools.CreateUI();
        }
        
        [HarmonyPostfix, HarmonyPatch(typeof(CustomSelectListCtrl), "Update")]
        private static void CustomSelectListCtrl_Update_ChangeController(CustomSelectListCtrl __instance)
        {
            if (EC_MakerRandomPicker.controller == __instance)
                return;

            if (__instance.canvasGrp == null)
                return;

            if (!__instance.canvasGrp[0].name.Contains("win") || !__instance.canvasGrp[0].interactable || !__instance.canvasGrp[1].interactable || !__instance.canvasGrp[2].interactable)
                return;
            
            EC_MakerRandomPicker.controller = __instance;
        }
    }
}