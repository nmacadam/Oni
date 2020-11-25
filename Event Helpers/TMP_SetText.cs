using UnityEngine;
using TMPro;

namespace DarumaKit.EventHelpers
{
    /// <summary>
    /// Provides helper methods to set Text Mesh Pro Text with a UnityEvent
    /// </summary>
    public class TMP_SetText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text = default;

        public TextMeshProUGUI Text { get => _text; set => _text = value; }

        public void Set<T>(T value)
        {
            _text.text = value.ToString();
        }

        public void Set(int value)
        {
            Set<int>(value);
        }

        public void Set(float value)
        {
            Set<float>(value);
        }

        public void Set(Vector2 value)
        {
            Set<Vector2>(value);
        }

        public void Set(Vector3 value)
        {
            Set<Vector3>(value);
        }
    }
}

