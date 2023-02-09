using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // test data
    public int index = 1;
    public float randFloat = -1f;
    [SerializeField] private float myFloat = 5.8f;
    public bool ourBool = true;
    public Vector3 ourVector = new Vector3(0, 2, 5);

}