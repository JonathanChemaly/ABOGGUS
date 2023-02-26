using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossHealthBar : MonoBehaviour
{
    public Image bar;
    public Boss boss;
    public void UpdateHealthBar()
    {
        bar.fillAmount = Mathf.Clamp(boss.health / boss.maxHealth, 0, 1f);
    }
}
