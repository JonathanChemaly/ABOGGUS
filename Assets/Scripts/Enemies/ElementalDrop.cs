using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.Gameplay;
using ABOGGUS.PlayerObjects;
public class ElementalDrop : MonoBehaviour
{
    private GameObject player;
    private bool inRange = false;
    private float range = 10f;
    private float speed = 0.2f;
    [SerializeField] private AudioSource deathSound;
    private float timer = 0.3f;
    private Vector3 createdPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        createdPos = transform.position;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range && timer > 0)
        {
            inRange = true;
        }
        if (inRange)
        {
            timer -= Time.deltaTime;
            if (timer > 0)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                transform.position = Vector3.MoveTowards(transform.position, createdPos + new Vector3(0f, 1.5f, 0f), speed);
            }
            else if (timer < 0)
            {
                speed = 0.2f;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0f, 1.5f, 0f), speed);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameController.player.updateMana(1);
            if (UpgradeStats.canHealFromMana)
            {
                Debug.Log("Player Health was: " + GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health);
                if (GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health
                    <= GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.maxHealth - UpgradeStats.healFromManaVal)
                {
                    GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health += UpgradeStats.healFromManaVal;
                    GameController.player.playerHUD.UpdateHealthBar();
                }
                else if (GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health <
                    GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.maxHealth &&
                    GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health >
                    GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.maxHealth - UpgradeStats.healFromManaVal)
                {
                    GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health = GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.maxHealth;
                    GameController.player.playerHUD.UpdateHealthBar();
                }
                else
                {
                    Debug.Log("Player already at max health value of: " + GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.maxHealth);
                }
                Debug.Log("Player Health is now: " + GameObject.Find("PlayerScripts").GetComponent<Player>().inventory.health);
            }
            deathSound.Play();
            Destroy(gameObject);
        }
    }
}
