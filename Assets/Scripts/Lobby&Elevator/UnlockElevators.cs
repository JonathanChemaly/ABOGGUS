using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockElevators : MonoBehaviour
{
    [SerializeField] GameObject summer;
    [SerializeField] GameObject winter;
    [SerializeField] GameObject spring;
    [SerializeField] GameObject player;
    public TMPro.TextMeshProUGUI NotEnoughManaSummer;
    public TMPro.TextMeshProUGUI NotEnoughManaWinter;
    public TMPro.TextMeshProUGUI NotEnoughManaSpring;
    public TMPro.TextMeshProUGUI GoToBoss;

    // Update is called once per frame
    void Update()
    {
        if (UpgradeStats.totalMana >= 150)
        {
            summer.SetActive(false);
        }
        if (UpgradeStats.totalMana >= 350)
        {
            winter.SetActive(false);
        }
        if (UpgradeStats.totalMana >= 500)
        {
            spring.SetActive(false);
        }
        if (player.transform.position.x < 21)
        {
            NotEnoughManaSpring.enabled = false;
            NotEnoughManaWinter.enabled = false;
        }
        if (player.transform.position.x > 19)
        {
            NotEnoughManaSummer.enabled = false;
        }
        if (player.transform.position.x < 16)
        {
            NotEnoughManaSummer.enabled = false;
            GoToBoss.enabled = false;
        }
        if (player.transform.position.z < -4.7)
        {
            NotEnoughManaSummer.enabled = false;
            NotEnoughManaWinter.enabled = false;
        }
        else
        {
            NotEnoughManaSpring.enabled = false;
        }
    }
}
