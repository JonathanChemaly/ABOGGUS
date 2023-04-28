using ABOGGUS.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.BossObjects {
    public class BossClaw : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                //GameController.player.TakeDamage(Boss.CLAW_DAMAGE);
            }
        }
    }
}
