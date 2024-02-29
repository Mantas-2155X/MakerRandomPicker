using System.Linq;
using HarmonyLib;
using BepInEx;
using Random = UnityEngine.Random;

namespace PH_MakerRandomPicker
{
    [BepInProcess("PlayHome64bit")]
    [BepInProcess("PlayHome32bit")]
    [BepInPlugin(nameof(PH_MakerRandomPicker), nameof(PH_MakerRandomPicker), VERSION)]
    public class PH_MakerRandomPicker : BaseUnityPlugin
    {
        public const string VERSION = "1.3.1";
        
        private void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(Hooks), "MakerRandomPicker");
        }
        
        public static void PickRandomItem(ThumbnailSelectUI controller)
        {
            if (controller == null)
                return;

            var cells = controller.cells.Where(_cell => _cell.gameObject.activeSelf).ToList();
            
            var cell = cells[Random.Range(0, cells.Count)];
            if (cell == null)
                return;
            
            cell.SetToggle(true);
        }
    }
}