using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Opponent : MonoBehaviour
{

    public float speed = 3.0f;
    public TextMeshProUGUI countText, winText;
    public GameObject winTextObject;
    public GameObject pickUpParent;

    private bool start;
    private int count, pickupCount;
    private float dist = 15.0f;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        start = true;
        count = 0;
        pickupCount = 0;
        winTextObject.SetActive(false);
        SetCountText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (start)
        {
            MoveSquare();
        }

        else
        {
            MoveTowardsPickUp();
        }
    }
    void MoveSquare()
    {
        if (count < (dist / speed))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
            count++;
        }

        else if (count < 2 * (dist / speed))
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            count++;
        }

        else if (count < 3 * (dist / speed))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
            count++;
        }

        else if (count < 4 * (dist / speed))
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            count++;
        }

        else
        {
            start = false;
            count = 0;
        }


    }

    void MoveTowardsPickUp()
    {
        if (target == null || !target.activeInHierarchy)
        {
            bool found = false;
            int idx = 0;
            while (!found)
            {
                if (idx < pickUpParent.transform.childCount && pickUpParent.transform.GetChild(idx).gameObject.activeInHierarchy)
                {
                    target = pickUpParent.transform.GetChild(idx).gameObject;
                    found = true;
                }
                else if (idx >= pickUpParent.transform.childCount)
                {
                    SetWinText();
                    found = true;
                }
                idx++;
            }
        }
        
        else
        {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            pickupCount++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Opponent Count: " + pickupCount.ToString();
    }

    void SetWinText()
    {
        if (pickupCount > 6)
        {
            winText.text = "Opponent Wins!";
        }

        else if (pickupCount < 6)
        {
            winText.text = "Player Wins!";
        }

        else
        {
            winText.text = "It is a Tie!";
        }

        winTextObject.SetActive(true);
    }



}
