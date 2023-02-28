using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerInventory
    {
        public float maxHealth = PlayerConstants.MAX_HEALTH;
        public float health = PlayerConstants.MAX_HEALTH;
        public int mana = 0;
        public bool invulnerable { get; set; } = false;
        public bool key { get; set; } = false;

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
    }
}
