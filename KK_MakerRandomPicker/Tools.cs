using ChaCustom;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KK_MakerRandomPicker
{
    public static class Tools
    {
        public static void CreateUI()
        {
            var parent = GameObject.Find("CustomScene/CustomRoot/FrontUIGroup/CustomUIGroup/CvsMenuTree");
            var original = parent.transform.Find("00_FaceTop/tglAll/AllTop/sldTemp/Button");

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
            rect.offsetMin = new Vector2(-398, 614);
            rect.offsetMax = new Vector2(-313, -2);

            var text = copy.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "Random";
            
            var button = copy.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(KK_MakerRandomPicker.PickRandomItem);
        }
    }
}