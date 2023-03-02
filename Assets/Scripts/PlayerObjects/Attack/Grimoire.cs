using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class Grimoire : MonoBehaviour
    {
        private bool active;
        public GameObject windAttackPrefab;
        public GameObject windAOEPrefab;

        private void Awake()
        {

        }

        public void Unequip()
        {
            active = false;
        }

        public void Equip()
        {
            active = true;
        }

        private void FixedUpdate()
        {
            gameObject.GetComponent<Renderer>().enabled = active;
        }
        //Add method for changing the elemental glow of the grimoire
    }
}
