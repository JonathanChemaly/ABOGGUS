using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class Grimoire : MonoBehaviour
    {

        private void Awake()
        {

        }

        public void Unequip()
        {
            gameObject.SetActive(false);
        }

        public void Equip()
        {
            gameObject.SetActive(true);
        }

        //Add method for changing the elemental glow of the grimoire
    }
}
