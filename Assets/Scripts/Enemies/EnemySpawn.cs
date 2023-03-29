using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] public List<GameObject> enemyTypes;
    [SerializeField] public GameObject enemyToSpawn;
    private float range = 40f;
    private GameObject player;
    private bool enemySpawned;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemySpawned = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < range && !enemySpawned)
        {
            if (!enemySpawned)
            {
                if (enemyToSpawn != null)
                {
                    for (int i = 0; i < enemyTypes.Count; i++)
                    {
                        if (enemyTypes[i].name == enemyToSpawn.name)
                        {
                            Instantiate(enemyTypes[i], new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z), Quaternion.identity);
                            enemySpawned = true;
                            //Debug.Log("specific Enemy Spawned");
                        }
                    }
                }
            }
            if (!enemySpawned)
            {
                Instantiate(enemyTypes[Random.Range(0, enemyTypes.Count)], new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z), Quaternion.identity);
                enemySpawned = true;
                //Debug.Log("random Enemy Spawned");
            }
        }
    }
}
