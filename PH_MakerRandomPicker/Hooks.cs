using HarmonyLib;

namespace PH_MakerRandomPicker
{
    public static class Hooks
    {
        [HarmonyPostfix, HarmonyPatch(typeof(EditMode), "Setup")]
        private static void EditMode_Setup_CreateUI(EditMode __instance, MoveableThumbnailSelectUI ___itemSelectUI)
        {
            Tools.CreateUI(___itemSelectUI);
        }
    }
}