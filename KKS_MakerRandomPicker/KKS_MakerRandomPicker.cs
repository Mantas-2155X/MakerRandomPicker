using System.Linq;
using HarmonyLib;
using BepInEx;
using ChaCustom;
using Random = UnityEngine.Random;

namespace KKS_MakerRandomPicker
{
    [BepInProcess("KoikatsuSunshine")]
    [BepInPlugin(nameof(KKS_MakerRandomPicker), nameof(KKS_MakerRandomPicker), VERSION)]
    public class KKS_MakerRandomPicker : BaseUnityPlugin
    {
        public const string VERSION = "1.0.0";
        
        public static CustomSelectListCtrl controller;
        
        private void Awake()
        {
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