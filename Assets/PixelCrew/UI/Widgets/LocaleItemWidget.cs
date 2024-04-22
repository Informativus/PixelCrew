using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocaleInfo>
    {
        [SerializeField] private Text _text;

        public void SetData(LocaleInfo localeInfo, int index)
        {
            _text.text = localeInfo.LocaleId.ToUpper();
        }
    }

    public class LocaleInfo
    {
        public string LocaleId;
    }
}