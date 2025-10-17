using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Painter : MonoBehaviour
{
    [Tooltip("Painting speed")]
    [Range(0.1f,2), SerializeField] private float paintSpeed = 1f;
    
    [Tooltip("Ease type to use for paint animation.")]
    [SerializeField] private Ease paintAnimation;
    
    [Tooltip("Sprite mask to use for paint animation.")]
    [SerializeField] private GameObject paintMask;
    
    [Tooltip("Prefab of buttons to Instantiate.")]
    [SerializeField] private GameObject buttonPrefab;
    
    [Tooltip("The button Parent object to use when creating a new button.")]
    [SerializeField] private Transform buttonParent;
    
    [HideInInspector] public List<ColorGroup> colorGroups = new();
    
    private Sprite _whiteTexture;
    private ColorGroup _selectedGroup = new();
    private Camera _mainCam;

    private void Start()
    {
        _mainCam = Camera.main;
        _whiteTexture = GetComponent<SpriteRenderer>().sprite;
        StartLevel();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PaintSprite();
        }
    }

    private void StartLevel()
    {
        CreateButtons();
    }

    #region Painting
    private void PaintSprite()
    {
        if (!_selectedGroup.selected || !Calculators.CheckMousePosition(_mainCam, _selectedGroup)) return;
        var mouseInPixelPos = Calculators.CalculateMousePosition(_mainCam);

        var maskObj = Instantiate(paintMask);
        maskObj.transform.position = mouseInPixelPos;

        maskObj.transform.DOScale(new Vector3(6, 6, 1), paintSpeed).SetEase(paintAnimation).OnComplete(() =>
        {
            var colorPositions = _selectedGroup.positions;
            var colors = _selectedGroup.colors;
        
            for (var j = 0; j < colorPositions.Count; j++)
            {
                var colorIndex = j % colors.Count;
                var color = colors[colorIndex];
                var pos = colorPositions[j];

                _whiteTexture.texture.SetPixel(pos.x, pos.y, color);
            }
            _whiteTexture.texture.Apply();

            _selectedGroup.button.SetActive(false);
            _selectedGroup.spriteRenderer.gameObject.SetActive(false);
            _selectedGroup.selected = false;
            Destroy(maskObj);
        });
    }
    #endregion

    #region ButtonCreating
    private void CreateButtons()
    {
        for (var i = 0; i < colorGroups.Count; i++)
        {
            var colorGroup = colorGroups[i];
            var button = Instantiate(buttonPrefab, buttonParent);
            var btnImg = button.GetComponent<Image>();
            var btnText = button.GetComponentInChildren<TextMeshProUGUI>();
            btnImg.color = colorGroups[i].colors[0];
            btnText.text = i.ToString();

            var colorPositions = colorGroups[i].positions;

            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (_selectedGroup.selected == false)
                {
                    _selectedGroup = colorGroup;
                    _selectedGroup.button = button;
                    _selectedGroup.selected = true;
                    _selectedGroup.spriteRenderer.gameObject.SetActive(true);
                    _selectedGroup.spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

                    foreach (var pos in colorPositions)
                    {
                        _whiteTexture.texture.SetPixel(pos.x, pos.y, Color.clear);
                    }
                    _whiteTexture.texture.Apply();
                }
            });
        }
    }

    #endregion
}
