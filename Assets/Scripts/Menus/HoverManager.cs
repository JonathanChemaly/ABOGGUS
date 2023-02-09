using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ABOGGUS.Menus
{
    public class HoverManager : MonoBehaviour
    {
        public TextMeshProUGUI tipText;
        public RectTransform tipWindow;

        private const int MOUSE_SIZE = 32;

        public static Action<string, Vector2> OnMouseHover;
        public static Action OnMouseLoseFocus;
        // Start is  called before the first frame update
        void Start()
        {
            tipText.text = default;
            tipWindow.gameObject.SetActive(false);
        }

        void Update()
        {
            Vector2 mousePos = UnityEngine.Input.mousePosition;
            tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x / 2 + MOUSE_SIZE, mousePos.y);
        }

        private void OnEnable()
        {
            OnMouseHover += ShowTip;
            OnMouseLoseFocus += HideTip;
        }
        private void OnDisable()
        {
            OnMouseHover -= ShowTip;
            OnMouseLoseFocus -= HideTip;
        }
        private void ShowTip(string tip, Vector2 mousePos)
        {
            this.tipText.text = tip;
            tipWindow.sizeDelta = new Vector2(this.tipText.preferredWidth > 200 ? 200 : this.tipText.preferredWidth, this.tipText.preferredHeight);

            tipWindow.gameObject.SetActive(true);
            tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x / 2, mousePos.y);
        }

        public void HideTip()
        {
            tipText.text = default;
            tipWindow.gameObject.SetActive(false);
        }
    }
}