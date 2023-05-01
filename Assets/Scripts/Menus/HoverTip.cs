using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ABOGGUS.Menus
{
    public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public int id = -1;
        private string textToShow = "You forgot to change the description clown";
        private float timeToWait = .5f;
        public void OnPointerEnter(PointerEventData eventData)
        {
            StopAllCoroutines();
            //StartCoroutine(StartTimer());
            //Debug.Log("Hover tip " + id);
            ShowMessage();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopAllCoroutines();
            HoverManager.OnMouseLoseFocus();
        }

        private void ShowMessage()
        {
            HoverManager.OnMouseHover(textToShow, UnityEngine.Input.mousePosition);
        }

        private System.Collections.IEnumerator StartTimer()
        {
            yield return new WaitForSeconds(timeToWait);

            ShowMessage();
        }

        public void setText(string newText)
        {
            this.textToShow = newText;
        }
    }
}
