using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerInventory
    {
        public float health = PlayerConstants.MAX_HEALTH;
        public bool key { get; set; } = true;

        public PlayerInventory() { }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Player.PlayerDied();
            }
        }
    }
}
