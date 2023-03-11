using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Interact.Statics;

namespace ABOGGUS.Interact
{
    public class LoadPrefabInThis : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(InteractStatics.pathToLoad);
            GameObject prefab = Resources.Load<GameObject>(InteractStatics.pathToLoad);
            Debug.Log(prefab);
            GameObject item = Instantiate(prefab);
            item.layer = 5; //set Layer to UI

            item.transform.parent = transform; //sets the created object so its parent is under this
            //sets all properties to start at center
            item.transform.localPosition = InteractStatics.posToLoadAt;
            item.transform.localScale = InteractStatics.scaleToLoadAt;

            //reset statics
            InteractStatics.pathToLoad = null; //set back to null so other interact menus can't open up same item by accident
            InteractStatics.posToLoadAt = Vector3.zero;
            InteractStatics.scaleToLoadAt = Vector3.one;
        }

    }
}

