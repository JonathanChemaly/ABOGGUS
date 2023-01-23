using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpTracker : MonoBehaviour
{
    public TextMeshProUGUI countText;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;

        SetCountText();
    }

    void SetCountText()
    {
        countText.text = "Player Count: " + count.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
