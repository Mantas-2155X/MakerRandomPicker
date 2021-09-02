using KKAPI.Maker;
using KKAPI.Maker.UI;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace HS2_MakerRandomPicker
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
                button.onClick.AddListener(delegate { HS2_MakerRandomPicker.PickRandomItem(idx); });

                var buttonRect = button.gameObject.GetComponent<RectTransform>();
                buttonRect.offsetMax = new Vector2(100, 0);

                var text = target.transform.Find("SelectText");
                text.GetComponent<RectTransform>().offsetMax = new Vector2(-105, 0);
            }
        }
        
        public static void MakerAPI_RegisterCustomSubCategories(object sender, RegisterSubCategoriesEvent e)
        {
            Randomizer.template = null;
            
            var parentCat = MakerConstants.Body.All;
            var cat = new MakerCategory(parentCat.CategoryName, "MakerRandomPickerCategory", parentCat.Position + 5, "Randomize");
            e.AddSubCategory(cat);

            e.AddControl(new MakerButton("Set current character as template", cat, HS2_MakerRandomPicker.instance)).OnClick.AddListener(() =>
            {
                Randomizer.AssignTemplate(MakerAPI.GetCharacterControl());
            });

            var randButton = e.AddControl(new MakerButton("Randomize!", cat, HS2_MakerRandomPicker.instance));

            e.AddControl(new MakerSeparator(cat, HS2_MakerRandomPicker.instance));
            var randomizeBodySliders = e.AddControl(new MakerToggle(cat, "Randomize body sliders", HS2_MakerRandomPicker.instance));
            var randomizeBody = e.AddControl(new MakerToggle(cat, "Randomize body type", HS2_MakerRandomPicker.instance));
            var skinColorType = e.AddControl(new MakerRadioButtons(cat, HS2_MakerRandomPicker.instance, "Skin color", "White", "Brown", "Unchanged"));

            e.AddControl(new MakerSeparator(cat, HS2_MakerRandomPicker.instance));
            var randomizeFaceSliders = e.AddControl(new MakerToggle(cat, "Randomize face sliders", HS2_MakerRandomPicker.instance));
            var randomizeFace = e.AddControl(new MakerToggle(cat, "Randomize face type", HS2_MakerRandomPicker.instance));
            var randomizeFaceEyes = e.AddControl(new MakerToggle(cat, "Randomize eyes", HS2_MakerRandomPicker.instance));

            e.AddControl(new MakerSeparator(cat, HS2_MakerRandomPicker.instance));
            var randomizeHair = e.AddControl(new MakerToggle(cat, "Randomize hair type", HS2_MakerRandomPicker.instance));
            var randomizeHairMesh = e.AddControl(new MakerToggle(cat, "Randomize hair mesh", HS2_MakerRandomPicker.instance));
            var randomizeHairColor = e.AddControl(new MakerToggle(cat, "Randomize hair color", HS2_MakerRandomPicker.instance));

            e.AddControl(new MakerSeparator(cat, HS2_MakerRandomPicker.instance));
            var deviationSlider = e.AddControl(new MakerSlider(cat, "Deviation", 0, 1, 0.1f, HS2_MakerRandomPicker.instance));

            randomizeBodySliders.Value = true;
            randomizeFaceSliders.Value = true;

            randButton.OnClick.AddListener(() =>
            {
                var chara = MakerAPI.GetCharacterControl();
                
                if (randomizeBodySliders.Value) 
                    Randomizer.RandomizeSliders(chara, deviationSlider.Value, Randomizer.RandomizerType.BODY);
                
                if (randomizeFaceSliders.Value) 
                    Randomizer.RandomizeSliders(chara, deviationSlider.Value, Randomizer.RandomizerType.FACE);
                
                if (randomizeHair.Value || randomizeHairColor.Value || randomizeHairMesh.Value)
                    Randomizer.RandomizeHair(chara, randomizeHair.Value, randomizeHairMesh.Value, randomizeHairColor.Value);

                if (randomizeBody.Value)
                    Randomizer.RandomizeBody(chara, skinColorType.Value);

                if (randomizeFace.Value || randomizeFaceEyes.Value)
                    Randomizer.RandomizeFace(chara, randomizeFace.Value, randomizeFaceEyes.Value);

                chara.Reload();
            });
        }
    }
}