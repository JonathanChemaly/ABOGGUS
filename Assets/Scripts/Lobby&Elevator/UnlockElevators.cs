using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockElevators : MonoBehaviour
{
    [SerializeField] GameObject summer;
    [SerializeField] GameObject winter;
    [SerializeField] GameObject spring;

    // Update is called once per frame
    void Update()
    {
        if (UpgradeStats.totalMana >= 150)
        {
            summer.SetActive(false);
        }
        if (UpgradeStats.totalMana >= 200)
        {
            winter.SetActive(false);
        }
        if (UpgradeStats.totalMana >= 250)
        {
            spring.SetActive(false);
        }
    }
}
