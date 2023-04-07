using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Puzzles.Sokoban
{
    public class SokobanBoxGameObject : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Where the generated Sokoban level is held")]
        private GameObject electricAura;

        private void Start()
        {
            electricAura.SetActive(false);
        }

        public void SuccessAction()
        {
            electricAura.SetActive(true);
        }
    }

}
