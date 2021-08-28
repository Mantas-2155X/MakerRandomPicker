using CharaCustom;
using HarmonyLib;

namespace AI_MakerRandomPicker
{
    public static class Hooks
    {
        [HarmonyPostfix, HarmonyPatch(typeof(CustomControl), "Initialize")]
        private static void CustomControl_Initialize_SetVars()
        {
            Tools.controllers[0] = Singleton<CvsH_Hair>.Instance.sscHairType;
            Tools.controllers[1] = Singleton<CvsC_Clothes>.Instance.sscClothesType;
            Tools.controllers[2] = Singleton<CvsA_Slot>.Instance.sscAcs;
            Tools.controllers[3] = Singleton<CvsB_Skin>.Instance.sscSkinType;
            Tools.controllers[4] = Singleton<CvsB_Skin>.Instance.sscDetailType;
            Tools.controllers[5] = Singleton<CvsB_Sunburn>.Instance.sscSunburnType;
            Tools.controllers[6] = Singleton<CvsB_Nip>.Instance.sscNipType;
            Tools.controllers[7] = Singleton<CvsB_Underhair>.Instance.sscUnderhairType;
            Tools.controllers[8] = Singleton<CvsB_Paint>.Instance.sscPaintType;
            Tools.controllers[9] = Singleton<CvsF_Mole>.Instance.sscMole;
            Tools.controllers[10] = Singleton<CvsF_EyeLR>.Instance.sscPupilType;
            Tools.controllers[11] = Singleton<CvsF_EyeLR>.Instance.sscBlackType;
            Tools.controllers[12] = Singleton<CvsF_EyeHL>.Instance.sscEyeHLType;
            Tools.controllers[13] = Singleton<CvsF_Eyebrow>.Instance.sscEyebrowType;
            Tools.controllers[14] = Singleton<CvsF_Eyelashes>.Instance.sscEyelashesType;
            Tools.controllers[15] = Singleton<CvsF_MakeupEyeshadow>.Instance.sscEyeshadowType;
            Tools.controllers[16] = Singleton<CvsF_MakeupCheek>.Instance.sscCheekType;
            Tools.controllers[17] = Singleton<CvsF_MakeupLip>.Instance.sscLipType;
            Tools.controllers[18] = Singleton<CvsF_MakeupPaint>.Instance.sscPaintType;
            Tools.controllers[19] = Singleton<CvsF_FaceType>.Instance.sscSkinType;
            Tools.controllers[20] = Singleton<CvsF_FaceType>.Instance.sscDetailType;
            Tools.controllers[21] = Singleton<CvsF_FaceType>.Instance.sscFaceType;
            Tools.controllers[22] = Singleton<CvsF_FaceType>.Instance.pscFacePreset;
            
            Tools.CreateUI();
        }
    }
}