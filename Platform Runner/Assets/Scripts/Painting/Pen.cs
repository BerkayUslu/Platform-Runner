using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

namespace PlatformRunner
{
    public class Pen : MonoBehaviour
    {
        [Header("Brush Settings")]
        [SerializeField] private int _basePenSize = 100;
        [SerializeField] private int _maxPenSize = 300;
        [SerializeField] private Color drawColor = Color.black;

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI percentageText;

        private int penSize;
        private PaintingBoard board;
        private Color[] colors;
        private Vector2 lastDrawPosition;
        private bool wasDrawingLastFrame;
        private bool isFirstTouch = true;
        private float updateTimer = 0f;
        private const float UPDATE_INTERVAL = 0.1f;
        private bool _drawable = false;

        private void Start()
        {
            penSize = _basePenSize;
            board = GetComponent<PaintingBoard>();
            GenerateCircularBrush();
            GameManager.GameStateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }

        private void GenerateCircularBrush()
        {
            colors = new Color[penSize * penSize];
            float radius = penSize * 0.5f;
            Vector2 center = new Vector2(radius, radius);

            for (int x = 0; x < penSize; x++)
            {
                for (int y = 0; y < penSize; y++)
                {
                    int index = y * penSize + x;
                    Vector2 pos = new Vector2(x, y);
                    float distance = Vector2.Distance(pos, center);

                    if (distance <= radius)
                    {
                        float alpha = 1f;
                        if (distance > radius - 1)
                        {
                            alpha = 1f - (distance - (radius - 1));
                        }

                        colors[index] = new Color(drawColor.r, drawColor.g, drawColor.b, drawColor.a * alpha);
                    }
                    else
                    {
                        colors[index] = new Color(0, 0, 0, 0);
                    }
                }
            }
        }

        private void Update()
        {
            if (!_drawable)
                return;

            HandleTouchInput();

            updateTimer += Time.deltaTime;
            if (updateTimer >= UPDATE_INTERVAL)
            {
                float percentage = board.GetPaintedPercentage();
                UpdatePercentageDisplay(percentage);
                Debug.Log(percentage);
                if (percentage >= 100)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                updateTimer = 0f;
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began ||
                    touch.phase == TouchPhase.Moved ||
                    touch.phase == TouchPhase.Stationary)
                {
                    Vector2 texturePos;
                    if (board.ScreenToTexturePoint(touch.position, out texturePos))
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            wasDrawingLastFrame = false;
                            isFirstTouch = true;
                        }

                        DrawAtPosition(texturePos);
                    }
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    wasDrawingLastFrame = false;
                    isFirstTouch = true;
                }
            }
            else
            {
                wasDrawingLastFrame = false;
                isFirstTouch = true;
            }
        }

        private void DrawAtPosition(Vector2 texturePos)
        {
            int originalX = (int)(texturePos.x * board.textureSize.x - (penSize / 2));
            int originalY = (int)(texturePos.y * board.textureSize.y - (penSize / 2));

            DrawWithBoundaryCheck(originalX, originalY);

            if (wasDrawingLastFrame && !isFirstTouch)
            {
                Vector2 currentPos = new Vector2(originalX, originalY);
                Vector2 delta = currentPos - lastDrawPosition;
                float distance = delta.magnitude;
                int steps = Mathf.Max(1, Mathf.CeilToInt(distance / (penSize * 0.25f)));

                for (int i = 1; i < steps; i++)
                {
                    float t = i / (float)steps;
                    int lerpX = (int)Mathf.Lerp(lastDrawPosition.x, originalX, t);
                    int lerpY = (int)Mathf.Lerp(lastDrawPosition.y, originalY, t);
                    DrawWithBoundaryCheck(lerpX, lerpY);
                }
            }

            board.texture.Apply();
            lastDrawPosition = new Vector2(originalX, originalY);
            wasDrawingLastFrame = true;
            isFirstTouch = false;
        }

        private void DrawWithBoundaryCheck(int x, int y)
        {
            int drawX = x;
            int drawY = y;
            int drawWidth = penSize;
            int drawHeight = penSize;
            int sourceX = 0;
            int sourceY = 0;

            if (drawX < 0)
            {
                sourceX = -drawX;
                drawWidth += drawX;
                drawX = 0;
            }
            if (drawY < 0)
            {
                sourceY = -drawY;
                drawHeight += drawY;
                drawY = 0;
            }

            if (drawX + drawWidth > board.textureSize.x)
            {
                drawWidth = (int)board.textureSize.x - drawX;
            }
            if (drawY + drawHeight > board.textureSize.y)
            {
                drawHeight = (int)board.textureSize.y - drawY;
            }

            if (drawWidth > 0 && drawHeight > 0)
            {
                Color[] clippedColors = new Color[drawWidth * drawHeight];

                for (int py = 0; py < drawHeight; py++)
                {
                    for (int px = 0; px < drawWidth; px++)
                    {
                        int sourceIndex = (sourceY + py) * penSize + (sourceX + px);
                        int targetIndex = py * drawWidth + px;

                        if (sourceIndex < colors.Length && targetIndex < clippedColors.Length)
                        {
                            Color sourceColor = colors[sourceIndex];

                            if (sourceColor.a < 1)
                            {
                                Color existingColor = board.texture.GetPixel(drawX + px, drawY + py);
                                sourceColor = Color.Lerp(existingColor, sourceColor, sourceColor.a);
                            }

                            clippedColors[targetIndex] = sourceColor;
                        }
                    }
                }

                board.texture.SetPixels(drawX, drawY, drawWidth, drawHeight, clippedColors);
                board.UpdatePaintedArea(drawX, drawY, drawWidth, drawHeight, clippedColors);
            }
        }

        private void UpdatePercentageDisplay(float percentage)
        {
            if (percentageText != null)
            {
                percentageText.text = $"Painted: {percentage:F1}%";
            }
        }

        public void ChangePenSize(float value)
        {
            value = Mathf.Clamp01(value);
            int addedSize = (int)((_maxPenSize - _basePenSize) * value);
            int size = _basePenSize + addedSize;
            SetPenSize(size);
        }

        public void SetPenColor(Color newColor)
        {
            drawColor = newColor;
            GenerateCircularBrush();
        }

        public void SetPenSize(int newSize)
        {
            penSize = Mathf.Max(1, newSize);
            GenerateCircularBrush();
        }

        private void OnGameStateChanged(GameState state)
        {
            _drawable = state == GameState.WallPainting;
        }
    }
}