using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace ABOGGUS.BossObjects
{
    public class BossHealthBar : MonoBehaviour
    {
        public Image bar;
        public GameObject bossName;
        public Boss boss;
        private float fadeOutSpeed = 0.5f;
        public void UpdateHealthBar()
        {
            bar.fillAmount = Mathf.Clamp(boss.health / boss.maxHealth, 0, 1f);
        }

        public void OnDeath()
        {
            StartCoroutine(FadeOut());
        }

        IEnumerator FadeOut()
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * fadeOutSpeed)
            {
                GetComponent<Image>().color = new Color(0, 0, 0, i);
                bossName.GetComponent<TMP_Text>().color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
