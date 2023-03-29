using ABOGGUS.PlayerObjects.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerInventory
    {
        public float maxHealth = PlayerConstants.MAX_HEALTH;
        public float health = PlayerConstants.MAX_HEALTH;
        public int mana = UpgradeStats.mana;
        public bool invulnerable { get; set; } = false;
        public bool key { get; set; } = true;

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
        }

        public bool hasItem(string itemName)
        {
            foreach(IItem item in items)
            {
                if (item.getName().Equals(itemName)) return true;
            }

            return false;
        }

        public IItem getItem(string itemName)
        {
            foreach (IItem item in items)
            {
                if (item.getName().Equals(itemName)) return item;
            }

            return null;
        }

        public void addItem(string itemName)
        {
            IItem temp = getItem(itemName);

            if (temp != null)
            {
                temp.increaseQuantity();
            }

            else items.Add(ItemFactory.CreateItem(itemName));
        }
    }
}
