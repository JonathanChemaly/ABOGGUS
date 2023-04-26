using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;

public class LargeEEDrop : MonoBehaviour
{
    private bool touched = false;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private GameObject pickupAnim;
    private float timer = 0.5f;
    private bool once = false;

    public event Action eventOnPickup;
    void FixedUpdate()
    {
        if (touched)
        {
            timer -= Time.deltaTime;
            if (timer > 0)
            {
                transform.localScale -= new Vector3(0.02f, 0.02f, 0.02f);
            }
            else if (timer < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && !once)
        {
            pickupAnim.transform.gameObject.SetActive(true);
            deathSound.Play();
            GameController.player.updateMana(50);
            touched = true;
            once = true;

            eventOnPickup?.Invoke();
        }
    }
}
