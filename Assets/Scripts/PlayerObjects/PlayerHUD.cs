using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using ABOGGUS.PlayerObjects;

public class PlayerHUD : MonoBehaviour
{
    public Image bar;
    public TextMeshProUGUI textbox;
    public PlayerInventory playerInventory;

    private void Start()
    {
        textbox.text = playerInventory.mana.ToString();
    }

    public void UpdateHealthBar()
    {
        Debug.Log("Debug from PlayerHealthBar: " + playerInventory.health / playerInventory.maxHealth);
        bar.fillAmount = Mathf.Clamp(playerInventory.health / playerInventory.maxHealth, 0, 1f);
    }

    public void UpdateMana()
    {
        textbox.text = "" + playerInventory.mana;
    }
}
