using UnityEngine;
using System.Linq;
using TMPro;
using Unity.VisualScripting;

namespace PlatformRunner
{
    public class Brush : MonoBehaviour
    {
        [Header("Brush Settings")]
        [SerializeField] private int _basePenSize = 100;
        [SerializeField] private int _maxPenSize = 300;
        [SerializeField] private Color drawColor = Color.black;

        [Header("References")]
        [SerializeField] private PaintingBoard _board;

        private int _penSize;
        private Color[] _colors;
        private Vector2 _lastDrawPosition;
        private bool _wasDrawingLastFrame;
        private bool _isFirstTouch = true;
        private bool _enabled = false;

        private void Start()
        {
            _penSize = _basePenSize;
            PaintingManager.Instance.OnPaintingEnabled += EnablePainting;
            GenerateCircularBrush();
        }

        private void OnDestroy()
        {
            PaintingManager.Instance.OnPaintingEnabled -= EnablePainting;
        }

        private void EnablePainting()
        {
            _enabled = true;
        }

        private void GenerateCircularBrush()
        {
            _colors = new Color[_penSize * _penSize];
            float radius = _penSize * 0.5f;
            Vector2 center = new Vector2(radius, radius);

            for (int x = 0; x < _penSize; x++)
            {
                for (int y = 0; y < _penSize; y++)
                {
                    int index = y * _penSize + x;
                    Vector2 pos = new Vector2(x, y);
                    float distance = Vector2.Distance(pos, center);

                    if (distance <= radius)
                    {
                        float alpha = 1f;
                        if (distance > radius - 1)
                        {
                            alpha = 1f - (distance - (radius - 1));
                        }

                        _colors[index] = new Color(drawColor.r, drawColor.g, drawColor.b, drawColor.a * alpha);
                    }
                    else
                    {
                        _colors[index] = new Color(0, 0, 0, 0);
                    }
                }
            }
        }

        private void Update()
        {
            if (!_enabled)
                return;

            HandleTouchInput();
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
                    if (_board.ScreenToTexturePoint(touch.position, out texturePos))
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            _wasDrawingLastFrame = false;
                            _isFirstTouch = true;
                        }

                        DrawAtPosition(texturePos);
                    }
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _wasDrawingLastFrame = false;
                    _isFirstTouch = true;
                }
            }
            else
            {
                _wasDrawingLastFrame = false;
                _isFirstTouch = true;
            }
        }

        private void DrawAtPosition(Vector2 texturePos)
        {
            int originalX = (int)(texturePos.x * _board.TextureSize.x - (_penSize / 2));
            int originalY = (int)(texturePos.y * _board.TextureSize.y - (_penSize / 2));

            DrawWithBoundaryCheck(originalX, originalY);

            if (_wasDrawingLastFrame && !_isFirstTouch)
            {
                Vector2 currentPos = new Vector2(originalX, originalY);
                Vector2 delta = currentPos - _lastDrawPosition;
                float distance = delta.magnitude;
                int steps = Mathf.Max(1, Mathf.CeilToInt(distance / (_penSize * 0.25f)));

                for (int i = 1; i < steps; i++)
                {
                    float t = i / (float)steps;
                    int lerpX = (int)Mathf.Lerp(_lastDrawPosition.x, originalX, t);
                    int lerpY = (int)Mathf.Lerp(_lastDrawPosition.y, originalY, t);
                    DrawWithBoundaryCheck(lerpX, lerpY);
                }
            }

            _board.Texture.Apply();
            _lastDrawPosition = new Vector2(originalX, originalY);
            _wasDrawingLastFrame = true;
            _isFirstTouch = false;
        }

        private void DrawWithBoundaryCheck(int x, int y)
        {
            int drawX = x;
            int drawY = y;
            int drawWidth = _penSize;
            int drawHeight = _penSize;
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

            if (drawX + drawWidth > _board.TextureSize.x)
            {
                drawWidth = (int)_board.TextureSize.x - drawX;
            }
            if (drawY + drawHeight > _board.TextureSize.y)
            {
                drawHeight = (int)_board.TextureSize.y - drawY;
            }

            if (drawWidth > 0 && drawHeight > 0)
            {
                Color[] clippedColors = new Color[drawWidth * drawHeight];

                for (int py = 0; py < drawHeight; py++)
                {
                    for (int px = 0; px < drawWidth; px++)
                    {
                        int sourceIndex = (sourceY + py) * _penSize + (sourceX + px);
                        int targetIndex = py * drawWidth + px;

                        if (sourceIndex < _colors.Length && targetIndex < clippedColors.Length)
                        {
                            Color sourceColor = _colors[sourceIndex];

                            if (sourceColor.a < 1)
                            {
                                Color existingColor = _board.Texture.GetPixel(drawX + px, drawY + py);
                                sourceColor = Color.Lerp(existingColor, sourceColor, sourceColor.a);
                            }

                            clippedColors[targetIndex] = sourceColor;
                        }
                    }
                }

                _board.Texture.SetPixels(drawX, drawY, drawWidth, drawHeight, clippedColors);
                _board.UpdatePaintedArea(drawX, drawY, drawWidth, drawHeight, clippedColors);
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
            _penSize = Mathf.Max(1, newSize);
            GenerateCircularBrush();
        }
    }
}