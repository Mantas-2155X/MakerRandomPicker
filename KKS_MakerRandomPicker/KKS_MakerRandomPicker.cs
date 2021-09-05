using System.Linq;
using HarmonyLib;
using BepInEx;
using ChaCustom;
using KKAPI.Maker;
using Random = UnityEngine.Random;

namespace KKS_MakerRandomPicker
{
    [BepInProcess("KoikatsuSunshine")]
    [BepInPlugin(nameof(KKS_MakerRandomPicker), nameof(KKS_MakerRandomPicker), VERSION)]
    public class KKS_MakerRandomPicker : BaseUnityPlugin
    {
        public const string VERSION = "1.3.0";
        
        public static KKS_MakerRandomPicker instance;
        public static CustomSelectListCtrl controller;
        
        private void Awake()
        {
            instance = this;
            
            MakerAPI.RegisterCustomSubCategories += Tools.MakerAPI_RegisterCustomSubCategories;
            Harmony.CreateAndPatchAll(typeof(Hooks));
        }
        
        public static void PickRandomItem()
        {
            if (controller == null)
                return;

            var datas = controller.lstSelectInfo.Where(info => !info.disvisible).ToList();
            var data = datas[Random.Range(0, datas.Count)];

            if (data == null)
                return;
        
            controller.SelectItem(data.index);
        }
    }
}