using UnityEngine;
using UnityEngine.UI;

namespace AI_MakerRandomPicker
{
    public static class Tools
    {
        public static readonly object[] controllers = new object[23];

        private static readonly string[] targets =
        {
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinHair/H_Hair/Setting/Setting01/SelectBox/SelectName",                      // Hair
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinClothes/DefaultWin/C_Clothes/Setting/Setting01/SelectBox/SelectName",     // Clothes
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinAccessory/A_Slot/Setting/Setting01/SelectBox/SelectName",                 // Accessories
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinBody/B_Skin/Setting/Setting01/SelectBox/SelectName",                      // Body Skin
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinBody/B_Skin/Setting/Setting02/SelectBox/SelectName",                      // Body Detail
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinBody/B_Sunburn/Setting/Setting01/SelectBox/SelectName",                   // Body Sunburn
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinBody/B_Nip/Setting/Setting01/SelectBox/SelectName",                       // Body Nip
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinBody/B_Underhair/Setting/Setting01/SelectBox/SelectName",                 // Body Underhair
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinBody/B_Paint/Setting/Setting01/SelectBox/SelectName",                     // Body Paint
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_Mole/Setting/Setting01/SelectBox/SelectName",                      // Face Mole
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_EyeLR/Setting/Setting01/SelectBox/SelectName",                     // Face Eye Iris
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_EyeLR/Setting/Setting03/SelectBox/SelectName",                     // Face Eye Pupil
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_EyeHL/Setting/Setting01/SelectBox/SelectName",                     // Face Highlight
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_Eyebrow/Setting/Setting01/SelectBox/SelectName",                   // Face Eyebrow
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_Eyelashes/Setting/Setting01/SelectBox/SelectName",                 // Face Eyelash
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_MakeupEyeshadow/Setting/Setting01/SelectBox/SelectName",           // Face Eyeshadow
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_MakeupCheek/Setting/Setting01/SelectBox/SelectName",               // Face Cheek
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_MakeupLip/Setting/Setting01/SelectBox/SelectName",                 // Face Lip
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_MakeupPaint/Setting/Setting01/SelectBox/SelectName",               // Face Paint
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_FaceType/Setting/Setting02/SelectBox/SelectName",                  // Face Skin
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_FaceType/Setting/Setting03/SelectBox/SelectName",                  // Face Wrinkles
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_FaceType/Setting/Setting01/SelectBox/SelectName",                  // Head Type
            "CharaCustom/CustomControl/CanvasSub/SettingWindow/WinFace/F_FaceType/Setting/Setting01/PushBox/SelectName",                    // Head Sample
        };
        
        public static void CreateUI()
        {
            var orig = GameObject.Find("CharaCustom/CustomControl/CanvasSub/SettingWindow/WinClothes/DefaultWin/C_Clothes/Setting/Setting01/DefaultColor");

            for (var i = 0; i < targets.Length; i++)
            {
                var idx = i;

                var target = GameObject.Find(targets[i]);
                if (target == null)
                    continue;
                
                var copy = Object.Instantiate(orig, target.transform);
                copy.name = "Random";
                copy.GetComponentInChildren<Text>().text = "Random";

                var copyRect = copy.gameObject.GetComponent<RectTransform>();
                copyRect.offsetMin = new Vector2(288, -40);
                copyRect.offsetMax = new Vector2(388, 0);
                
                var button = copy.GetComponentInChildren<Button>();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(delegate { AI_MakerRandomPicker.PickRandomItem(idx); });

                var buttonRect = button.gameObject.GetComponent<RectTransform>();
                buttonRect.offsetMax = new Vector2(100, 0);

                var text = target.transform.Find("SelectText");
                text.GetComponent<RectTransform>().offsetMax = new Vector2(-105, 0);
            }
        }
    }
}