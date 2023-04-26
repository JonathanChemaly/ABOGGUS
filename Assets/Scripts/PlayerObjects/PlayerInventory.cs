using ABOGGUS.PlayerObjects.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABOGGUS.Gameplay;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerInventory
    {
        public float maxHealth = PlayerConstants.MAX_HEALTH;
        public float health = PlayerConstants.MAX_HEALTH;
        public int mana = UpgradeStats.mana;
        public int totalMana = UpgradeStats.totalMana;
        public bool invulnerable { get; set; } = false;
        public bool key { get; set; } = true;
        //public bool bucket { get; set; } = false;

        private List<IItem> items = new List<IItem>();

        public PlayerInventory() { }

        public void TakeDamage(float damage)
        {
            if (!invulnerable)
            {
                health -= damage;
            }
            if (health <= 0)
            {
                //health = maxHealth;
                invulnerable = true;                
                Player.PlayerDied();
            }
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        public bool HasMana(int manaCost)
        {
            if (manaCost <= mana)
            {
                return true;
            }

            return false;
        }

        public void UseMana(int manaCost)
        {
            mana -= manaCost;
            UpgradeStats.mana -= manaCost;
            GameController.player.playerHUD.UpdateMana();
        }

        public bool HasItem(string itemName)
        {
            foreach(IItem item in items)
            {
                if (item.GetName().Equals(itemName)) return true;
            }

            return false;
        }

        public IItem GetItem(string itemName)
        {
            foreach (IItem item in items)
            {
                if (item.GetName().Equals(itemName)) return item;
            }

            return null;
        }
        public List<IItem> GetItems()
        {
            return this.items;
        }

        public void AddItem(string itemName)
        {
            IItem temp = GetItem(itemName);

            if (temp != null)
            {
                temp.IncreaseQuantity();
            }

            else items.Add(ItemFactory.CreateItem(itemName));

            items.Sort();
        }

        public void SetItems(List<IItem> itemList)
        {
            items = itemList;
        }
    }
}
