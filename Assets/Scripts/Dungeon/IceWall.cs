using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;
using ABOGGUS.Gameplay;

public class IceWall : MonoBehaviour
{
    public float health = 3f;
    public float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MagicAttack")
        {
            health -= 1;
        }
    }
    
    
    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 5 * health, gameObject.transform.localScale.z);
        if (health <= 0) Destroy(gameObject);
    }

}
