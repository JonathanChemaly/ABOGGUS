using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.Interact.Statics
{
    public static class InteractStatics
    {
        public static bool interactActionSuccess = false;

        //Statics for loading
        public static string pathToLoad = null;
        public static Vector3 posToLoadAt = new(0, 0, 0);
        public static Vector3 scaleToLoadAt = new(1, 1, 1);
    }
}

