using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using ABOGGUS.PlayerObjects;
using ABOGGUS.Menus;
using ABOGGUS.PlayerObjects.Items;

public class PlayerHUD : MonoBehaviour
{
    public Image bar;
    public TextMeshProUGUI textbox;
    public PlayerInventory playerInventory;
    public Image currentWeapon;

    public Sprite swordSprite;
    public Sprite spearSprite;
    public Sprite grimoireSprite;

    public void Awake()
    {
        //swordSprite = null;//InventoryMenu.LoadNewSprite(InventoryMenu.FILE_PATH + ItemLookup.SwordName + InventoryMenu.FILE_TYPE);
        //spearSprite = null;//InventoryMenu.LoadNewSprite(InventoryMenu.FILE_PATH + ItemLookup.SpearName + InventoryMenu.FILE_TYPE);
        //grimoireSprite = null;//InventoryMenu.LoadNewSprite(InventoryMenu.FILE_PATH + ItemLookup.GrimoireName + InventoryMenu.FILE_TYPE);
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

    public void UpdateWeapon(PlayerConstants.Weapon weapon, PlayerConstants.Magic magic)
    {
        currentWeapon.color = Color.white;
        switch (weapon)
        {
            case PlayerConstants.Weapon.Sword:
                currentWeapon.sprite = swordSprite; break;
            case PlayerConstants.Weapon.Grimoire:
                switch (magic)
                {
                    case PlayerConstants.Magic.Fire:
                        currentWeapon.color = Color.red; break;
                    case PlayerConstants.Magic.Lightning:
                        currentWeapon.color = Color.yellow; break;
                    case PlayerConstants.Magic.Nature:
                        currentWeapon.color = Color.green; break;
                    case PlayerConstants.Magic.Water:
                        currentWeapon.color = Color.blue; break;
                    case PlayerConstants.Magic.Wind:
                        currentWeapon.color = Color.white; break;
                }
                currentWeapon.sprite = grimoireSprite; break;
            case PlayerConstants.Weapon.Spear:
                currentWeapon.sprite = spearSprite; break;
            case PlayerConstants.Weapon.Unarmed:
                currentWeapon.enabled = false;
                break;
        }
        
    }
}
