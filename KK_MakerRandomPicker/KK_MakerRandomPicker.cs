using System.Linq;
using HarmonyLib;
using BepInEx;
using ChaCustom;
using Random = UnityEngine.Random;

namespace KK_MakerRandomPicker
{
    [BepInProcess("Koikatu")]
    [BepInProcess("Koikatsu Party")]
    [BepInPlugin(nameof(KK_MakerRandomPicker), nameof(KK_MakerRandomPicker), VERSION)]
    public class KK_MakerRandomPicker : BaseUnityPlugin
    {
        public const string VERSION = "1.1.0";
        
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