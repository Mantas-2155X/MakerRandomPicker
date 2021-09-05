using ChaCustom;
using KKAPI.Maker;
using KKAPI.Maker.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KKS_MakerRandomPicker
{
    public static class Tools
    {
        public static void CreateUI()
        {
            var parent = GameObject.Find("CustomScene/CustomRoot/FrontUIGroup/CustomUIGroup/CvsMenuTree");
            var original = parent.transform.Find("00_FaceTop/tglAll/AllTop/sldTemp/btnReset");

            // Face
            {
                var faceObj = parent.transform.Find("00_FaceTop");
                var face = faceObj.GetComponent<CustomChangeFaceMenu>();
                
                var cvsItems = new []
                {
                    Singleton<CvsFaceAll>.Instance.gameObject,
                    face.cvsEyebrow.gameObject,
                    face.cvsEye01.gameObject,
                    face.cvsEye02.gameObject,
                    Singleton<CvsNose>.Instance.gameObject,
                    face.cvsMouth.gameObject,
                    face.cvsMole.gameObject,
                    face.cvsMakeup.gameObject
                };
                
                foreach (var ctrl in cvsItems)
                {
                    for (var i = 0; i < ctrl.transform.childCount; i++)
                    {
                        var child = ctrl.transform.GetChild(i);
                        
                        if(!child.name.Contains("win"))
                            continue;

                        if (!child.name.Contains("Kind") && !child.name.Contains("Layout")) 
                            continue;
                        
                        SetupRandomButton(child.Find("customSelectWindow/BasePanel"), original);
                    }
                }
            }
            
            // Body
            {
                var bodyObj = parent.transform.Find("01_BodyTop");
                var body = bodyObj.GetComponent<CustomChangeBodyMenu>();
                
                var cvsItems = new []
                {
                    body.cvsBodyAll.gameObject,
                    body.cvsBodyPaint.gameObject,
                    body.cvsBreast.gameObject,
                    body.cvsSunburn.gameObject,
                    body.cvsUnderhair.gameObject
                };
                
                foreach (var ctrl in cvsItems)
                {
                    for (var i = 0; i < ctrl.transform.childCount; i++)
                    {
                        var child = ctrl.transform.GetChild(i);
                        
                        if(!child.name.Contains("win"))
                            continue;

                        if (!child.name.Contains("Kind") && !child.name.Contains("Layout")) 
                            continue;
                        
                        SetupRandomButton(child.Find("customSelectWindow/BasePanel"), original);
                    }
                }
            }

            // Hair
            {
                var hairObj = parent.transform.Find("02_HairTop");
                var hair = hairObj.GetComponent<CustomChangeHairMenu>();
                
                foreach (var ctrl in hair.cvsHair)
                    SetupRandomButton(ctrl.transform.Find("winHairKind/customSelectWindow/BasePanel"), original);

                var cvsHairEtc = hairObj.Find("tglEtc/EtcTop").GetComponent<CvsHairEtc>();
                SetupRandomButton(cvsHairEtc.transform.Find("winGlossKind/customSelectWindow/BasePanel"), original);
            }
            
            // Clothes
            {
                var clothesObj = parent.transform.Find("03_ClothesTop");
                var clothes = clothesObj.GetComponent<CustomChangeClothesMenu>();
                
                foreach (var ctrl in clothes.cvsClothes)
                {
                    for (var i = 0; i < ctrl.transform.childCount; i++)
                    {
                        var child = ctrl.transform.GetChild(i);

                        if (!child.name.Contains("win") || !child.name.Contains("Kind")) 
                            continue;
                        
                        SetupRandomButton(child.Find("customSelectWindow/BasePanel"), original);
                    }
                }
            }
            
            // Accessories
            {
                var accessoryObj = parent.transform.Find("04_AccessoryTop");
                var accessory = accessoryObj.GetComponent<CustomAcsChangeSlot>();
                
                foreach (var kind in accessory.customAcsSelectKind)
                    SetupRandomButton(kind.transform.Find("customSelectWindow/BasePanel"), original);
            }
        }
        
        private static void SetupRandomButton(Transform window, Transform original)
        {
            var copy = Object.Instantiate(original.gameObject, window);
            copy.name = "Random";
            
            var rect = copy.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(-395, 606);
            rect.offsetMax = new Vector2(-313, -5);

            var text = copy.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "Random";
            
            var button = copy.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(KKS_MakerRandomPicker.PickRandomItem);

            var title = window.Find("MenuTitle/WindowTitle");
            if (title == null)
                return;

            var titleRect = title.GetComponent<RectTransform>();
            titleRect.offsetMax = new Vector2(20, -8);
        }
        
                public static void MakerAPI_RegisterCustomSubCategories(object sender, RegisterSubCategoriesEvent e)
        {
            Randomizer.template = null;
            
            var parentCat = MakerConstants.Body.All;
            var cat = new MakerCategory(parentCat.CategoryName, "MakerRandomPickerCategory", parentCat.Position + 5, "Randomize");
            e.AddSubCategory(cat);

            e.AddControl(new MakerButton("Set current character as template", cat, KKS_MakerRandomPicker.instance)).OnClick.AddListener(() =>
            {
                Randomizer.AssignTemplate(MakerAPI.GetCharacterControl());
            });

            var randButton = e.AddControl(new MakerButton("Randomize!", cat, KKS_MakerRandomPicker.instance));

            e.AddControl(new MakerSeparator(cat, KKS_MakerRandomPicker.instance));
            var randomizeBodySliders = e.AddControl(new MakerToggle(cat, "Randomize body sliders", KKS_MakerRandomPicker.instance));
            var randomizeBody = e.AddControl(new MakerToggle(cat, "Randomize body type", KKS_MakerRandomPicker.instance));
            var skinColorType = e.AddControl(new MakerRadioButtons(cat, KKS_MakerRandomPicker.instance, "Skin color", "White", "Brown", "Unchanged"));

            e.AddControl(new MakerSeparator(cat, KKS_MakerRandomPicker.instance));
            var randomizeFaceSliders = e.AddControl(new MakerToggle(cat, "Randomize face sliders", KKS_MakerRandomPicker.instance));
            var randomizeFace = e.AddControl(new MakerToggle(cat, "Randomize face type", KKS_MakerRandomPicker.instance));
            var randomizeFaceEyes = e.AddControl(new MakerToggle(cat, "Randomize eyes", KKS_MakerRandomPicker.instance));

            e.AddControl(new MakerSeparator(cat, KKS_MakerRandomPicker.instance));
            var randomizeHair = e.AddControl(new MakerToggle(cat, "Randomize hair type", KKS_MakerRandomPicker.instance));
            var randomizeHairColor = e.AddControl(new MakerToggle(cat, "Randomize hair color", KKS_MakerRandomPicker.instance));

            e.AddControl(new MakerSeparator(cat, KKS_MakerRandomPicker.instance));
            var deviationSlider = e.AddControl(new MakerSlider(cat, "Deviation", 0, 1, 0.1f, KKS_MakerRandomPicker.instance));

            randomizeBodySliders.Value = true;
            randomizeFaceSliders.Value = true;

            randButton.OnClick.AddListener(() =>
            {
                var chara = MakerAPI.GetCharacterControl();
                
                if (randomizeBodySliders.Value) 
                    Randomizer.RandomizeSliders(chara, deviationSlider.Value, Randomizer.RandomizerType.BODY);
                
                if (randomizeFaceSliders.Value) 
                    Randomizer.RandomizeSliders(chara, deviationSlider.Value, Randomizer.RandomizerType.FACE);
                
                if (randomizeHair.Value || randomizeHairColor.Value)
                    Randomizer.RandomizeHair(chara, randomizeHair.Value, randomizeHairColor.Value);

                if (randomizeBody.Value)
                    Randomizer.RandomizeBody(chara, skinColorType.Value);

                if (randomizeFace.Value || randomizeFaceEyes.Value)
                    Randomizer.RandomizeFace(chara, randomizeFace.Value, randomizeFaceEyes.Value);

                chara.Reload();
            });
        }
    }
}