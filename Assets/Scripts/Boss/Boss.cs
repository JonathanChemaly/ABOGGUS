using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ABOGGUS.Gameplay;

public class Boss : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public float maxHealth;
    private Vector3 shockwavePlayerPos;
    public float speed;
    public BossHealthBar healthBar;
    public AudioFade bossMusic;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public GameObject shockwave;
    public float shockwaveDelay;
    public float shockwaveSpawnDelay;
    bool shockwaveAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private int wait = 500;
    private int sparkCount;
    public int maxSparkCount;
    public GameObject spark;
    private GameObject newSpark;
    bool sparkCreated;
    public float sparkDelay;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        sparkCount = 0;
        maxHealth = health;
    }

    /*private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }*/

    private void AttackPlayer()
    {
        //agent.SetDestination(transform.position);

       // transform.LookAt(player);
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position + transform.forward*2, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar();
        if (health <= 0)
        {
            healthBar.OnDeath();
            bossMusic.AudioFadeOut();
            GameObject[] sparkArr = GameObject.FindGameObjectsWithTag("Spark");
            foreach (GameObject minion in sparkArr)
            {
                Destroy(minion);
            }
            Invoke(nameof(DestroyEnemy), 0.5f);
            GameController.ChangeScene("Beat game!", GameConstants.SCENE_CREDITS, false);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wait > 0) wait--;
        else
        {
            transform.LookAt(player);
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            /*if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();*/
            if (playerInAttackRange) AttackPlayer();
            else transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            ShockWaveAttack();
            if (sparkCount < maxSparkCount && !sparkCreated) CreateSpark();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) TakeDamage(20);
    }

    private void ShockWaveAttack()
    {
        if (!shockwaveAttacked)
        {
            shockwavePlayerPos = player.position;
            Invoke(nameof(CreateShockWave), shockwaveSpawnDelay);
            shockwaveAttacked = true;
            Invoke(nameof(ResetShockwaveAttack), shockwaveDelay);
        }
    }

    private void ResetShockwaveAttack()
    {
        shockwaveAttacked = false;
    }

    private void CreateShockWave()
    {
        Rigidbody rb = Instantiate(shockwave, shockwavePlayerPos, Quaternion.identity).GetComponent<Rigidbody>();
    }

    private void CreateSpark()
    {
        newSpark = Instantiate(spark, new Vector3(0, 1, 0), Quaternion.identity);
        sparkCount++;
        sparkCreated = true;
        Invoke(nameof(ResetSpark), sparkDelay);
    }
    private void ResetSpark()
    {
        sparkCreated = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
