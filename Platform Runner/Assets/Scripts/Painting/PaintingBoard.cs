using UnityEngine;

namespace PlatformRunner
{
    public class PaintingBoard : MonoBehaviour
    {
        public Texture2D texture;
        public Vector2 textureSize = new Vector2(2048, 2048);
        public Camera mainCamera;

        private int totalPixels;
        private int paintedPixels;
        private Color[] pixelStates;

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
            pixelStates = new Color[totalPixels];
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

                    if (texIndex >= 0 && texIndex < totalPixels &&
                        colors[colorIndex].a > 0.1f &&
                        pixelStates[texIndex].a < 0.1f)
                    {
                        paintedPixels++;
                        pixelStates[texIndex] = Color.white;
                    }
                }
            }
        }

        public float GetPaintedPercentage()
        {
            return (float)paintedPixels / totalPixels * 100f;
        }

        public void ResetPaintedArea()
        {
            paintedPixels = 0;
            for (int i = 0; i < pixelStates.Length; i++)
            {
                pixelStates[i] = Color.clear;
            }
        }
    }
}