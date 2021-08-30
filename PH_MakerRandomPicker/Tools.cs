using UnityEngine;
using UnityEngine.UI;

namespace PH_MakerRandomPicker
{
    public static class Tools
    {
        public static void CreateUI(MoveableThumbnailSelectUI ___itemSelectUI)
        {
            var select = ___itemSelectUI.select;
            var moveableBar = select.transform.parent.parent.parent.Find("MoveableBar");
            var original = moveableBar.transform.Find("Buttons/Min");

            var copy = Object.Instantiate(original, original.transform.parent);
            copy.name = "Random";

            Object.Destroy(copy.Find("Help").gameObject);

            var layoutElement = copy.gameObject.AddComponent<LayoutElement>();
            layoutElement.ignoreLayout = true;

            var rect = copy.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(-120, -18);
            rect.offsetMax = new Vector2(-55, -2);
            
            var text = copy.GetComponentInChildren<Text>();
            text.text = "Random";

            var textRect = text.GetComponent<RectTransform>();
            textRect.offsetMin = new Vector2(-30, -7);
            textRect.offsetMax = new Vector2(30, 9);
            
            var button = copy.GetComponent<Button>();
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(delegate { PH_MakerRandomPicker.PickRandomItem(select); });
        }
    }
}