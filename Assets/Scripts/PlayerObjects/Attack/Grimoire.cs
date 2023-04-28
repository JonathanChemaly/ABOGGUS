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
        public GameObject fireAttackPrefab;
        public GameObject fireAOEPrefab;
        public GameObject waterAttackPrefab;
        public GameObject waterAOEPrefab;
        public GameObject natureAttackPrefab;
        public GameObject natureAOEPrefab;
        public Material windMaterial;
        public Material fireMaterial;
        public Material waterMaterial;
        public Material natureMaterial;
        public Material lightningMaterial;
        public Material normalMaterial;
        public Material natureArmorMaterial;

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

        public bool GetStatus()
        {
            return active;
        }

        public void SetNewMaterial(PlayerConstants.Magic magic)
        {
            if (magic == PlayerConstants.Magic.Wind)
            {
                gameObject.GetComponent<Renderer>().material = windMaterial;
            }
            else if (magic == PlayerConstants.Magic.Fire)
            {
                gameObject.GetComponent<Renderer>().material = fireMaterial;
            }
            else if (magic == PlayerConstants.Magic.Water)
            {
                gameObject.GetComponent<Renderer>().material = waterMaterial;
            }
            else if (magic == PlayerConstants.Magic.Nature)
            {
                gameObject.GetComponent<Renderer>().material = natureMaterial;
            }
            else if (magic == PlayerConstants.Magic.Lightning)
            {
                gameObject.GetComponent<Renderer>().material = lightningMaterial;
            }
        }

        private void FixedUpdate()
        {
            gameObject.GetComponent<Renderer>().enabled = active;
        }
    }
}
