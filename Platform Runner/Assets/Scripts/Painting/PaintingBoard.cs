// PaintingBoard.cs
using UnityEngine;

namespace PlatformRunner
{
    public class PaintingBoard : MonoBehaviour
    {
        [SerializeField] private Vector2 _textureSize = new Vector2(2048, 2048);
        public Vector2 TextureSize { get => _textureSize; }

        [SerializeField] private float _alphaThreshold = 0.95f;

        private Texture2D _texture;
        public Texture2D Texture { get => _texture; }

        private float _percentageTreshold = 99.8f;
        private Camera _mainCamera;
        private int _totalPixels;
        private int _paintedPixels;
        private bool[] _pixelPaintedState;
        private Color[] _pixelColors;


        private void Start()
        {
            var r = GetComponent<Renderer>();
            _texture = new Texture2D((int)TextureSize.x, (int)TextureSize.y);
            r.material.mainTexture = Texture;

            _mainCamera = Camera.main;

            InitializeTracking();
        }

        private void InitializeTracking()
        {
            _totalPixels = (int)(TextureSize.x * TextureSize.y);
            _pixelPaintedState = new bool[_totalPixels];
            _pixelColors = new Color[_totalPixels];
            _paintedPixels = 0;
        }

        public bool ScreenToTexturePoint(Vector2 screenPos, out Vector2 texturePos)
        {
            texturePos = Vector2.zero;
            Ray ray = _mainCamera.ScreenPointToRay(screenPos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                texturePos = hit.textureCoord;
                return true;
            }
            return false;
        }

        public void UpdatePaintedArea(int x, int y, int width, int height, Color[] colors)
        {
            if (x < 0 || y < 0 || x + width > TextureSize.x || y + height > TextureSize.y)
                return;

            for (int py = 0; py < height; py++)
            {
                for (int px = 0; px < width; px++)
                {
                    int texIndex = (y + py) * (int)TextureSize.x + (x + px);
                    int colorIndex = py * width + px;

                    if (texIndex >= 0 && texIndex < _totalPixels && colorIndex < colors.Length)
                    {
                        Color newColor = colors[colorIndex];
                        Color currentColor = _pixelColors[texIndex];

                        Color blendedColor = Color.Lerp(currentColor, newColor, newColor.a);
                        _pixelColors[texIndex] = blendedColor;

                        if (blendedColor.a >= _alphaThreshold && !_pixelPaintedState[texIndex])
                        {
                            _pixelPaintedState[texIndex] = true;
                            _paintedPixels++;
                        }
                    }
                }
            }
            PaintingManager.Instance.PaintingProgressChanged(GetPaintedPercentage());
        }

        public float GetPaintedPercentage()
        {
            float percentage = (float)_paintedPixels / _totalPixels * 100f;
            if (percentage > _percentageTreshold)
            {
                percentage = 100;
            }
            return percentage;
        }

        public bool IsFullyPainted()
        {
            for (int i = 0; i < _pixelColors.Length; i++)
            {
                if (_pixelColors[i].a < _alphaThreshold)
                    return false;
            }
            return true;
        }

        public void ResetPaintedArea()
        {
            _paintedPixels = 0;
            System.Array.Clear(_pixelPaintedState, 0, _pixelPaintedState.Length);
            System.Array.Clear(_pixelColors, 0, _pixelColors.Length);
        }
    }
}
