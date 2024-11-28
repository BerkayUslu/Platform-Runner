// PaintingBoard.cs
using UnityEngine;

namespace PlatformRunner
{
    public class PaintingBoard : MonoBehaviour
    {
        public Texture2D texture;
        public Vector2 textureSize = new Vector2(2048, 2048);
        public Camera mainCamera;
        
        [SerializeField] private float alphaThreshold = 0.95f;
        
        private int totalPixels;
        private int paintedPixels;
        private bool[] pixelPaintedState;
        private Color[] pixelColors;

        private void Start()
        {
            var r = GetComponent<Renderer>();
            texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
            r.material.mainTexture = texture;

            if (mainCamera == null)
                mainCamera = Camera.main;

            InitializeTracking();
        }

        private void InitializeTracking()
        {
            totalPixels = (int)(textureSize.x * textureSize.y);
            pixelPaintedState = new bool[totalPixels];
            pixelColors = new Color[totalPixels];
            paintedPixels = 0;
        }

        public bool ScreenToTexturePoint(Vector2 screenPos, out Vector2 texturePos)
        {
            texturePos = Vector2.zero;
            Ray ray = mainCamera.ScreenPointToRay(screenPos);
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
            if (x < 0 || y < 0 || x + width > textureSize.x || y + height > textureSize.y)
                return;

            for (int py = 0; py < height; py++)
            {
                for (int px = 0; px < width; px++)
                {
                    int texIndex = (y + py) * (int)textureSize.x + (x + px);
                    int colorIndex = py * width + px;

                    if (texIndex >= 0 && texIndex < totalPixels && colorIndex < colors.Length)
                    {
                        Color newColor = colors[colorIndex];
                        Color currentColor = pixelColors[texIndex];
                        
                        Color blendedColor = Color.Lerp(currentColor, newColor, newColor.a);
                        pixelColors[texIndex] = blendedColor;

                        if (blendedColor.a >= alphaThreshold && !pixelPaintedState[texIndex])
                        {
                            pixelPaintedState[texIndex] = true;
                            paintedPixels++;
                        }
                    }
                }
            }
        }

        public float GetPaintedPercentage()
        {
            float percentage = (float)paintedPixels / totalPixels * 100f;
            float remainingPixels = totalPixels - paintedPixels;
            
            if (remainingPixels <= totalPixels * 0.005f)
            {
                bool hasUnpaintedPixels = false;
                
                if (!hasUnpaintedPixels)
                    return 100f;
            }
            
            return percentage;
        }

        public bool IsFullyPainted()
        {
            for (int i = 0; i < pixelColors.Length; i++)
            {
                if (pixelColors[i].a < alphaThreshold)
                    return false;
            }
            return true;
        }

        public void ResetPaintedArea()
        {
            paintedPixels = 0;
            System.Array.Clear(pixelPaintedState, 0, pixelPaintedState.Length);
            System.Array.Clear(pixelColors, 0, pixelColors.Length);
        }
    }
}
