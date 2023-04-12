using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackingIceFloor : MonoBehaviour
{
    private bool cracking = false;
    private float timer = 2.5f;
    private float speed = 20.0f; 
    private float amount = 0.007f; 
    private Vector3 startingPos;
    public AudioSource iceBreak;
    private void Start()
    {
        startingPos.y = transform.position.y;
    }
    private void FixedUpdate()
    {
        
        if (cracking)
        {
            timer -= Time.deltaTime;
            
        }
        if (timer < 2.5f && timer > 0f)
        {
            transform.position += new Vector3(0f, (Mathf.Cos(Time.time * speed) * amount), 0f);
        }
        if (timer <= 0)
        {
            transform.position -= new Vector3(0f, 0.03f, 0f);
        }
        if ( timer < -3f)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            cracking = true;
            iceBreak.Play();
        }
    }
}
