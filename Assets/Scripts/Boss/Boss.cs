using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ABOGGUS.Gameplay;

namespace ABOGGUS.BossObjects
{
    public class Boss : MonoBehaviour
    {
        public const int CLAW_DAMAGE = 35;

        public NavMeshAgent agent;
        public Transform player;
        public LayerMask whatIsGround, whatIsPlayer;
        public float health;
        public float maxHealth;
        private Vector3 shockwavePlayerPos;
        public float speed;
        public BossHealthBar healthBar;
        public AudioFade bossMusic;

        public int direction;

        // Patrolling
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

        // Attacking
        public float timeBetweenAttacks;
        public bool attacking = false;
        public GameObject projectile;
        public float meleeDelay;
        public GameObject shockwave;
        public float shockwaveDelay;
        public float shockwaveSpawnDelay;
        bool shockwaveAttacked;

        // States
        public float sightRange, attackRange;
        public float meleeAttackRange, meleeHitRange;
        public bool playerInSightRange, playerInAttackRange;

        private int wait = 200;
        private int sparkCount;
        public int maxSparkCount;
        public GameObject spark;
        private GameObject newSpark;
        bool sparkCreated;
        public float sparkDelay;

        public const int DIRECTION_F = 0;
        public const int DIRECTION_B = 1;
        public const int DIRECTION_R = 2;
        public const int DIRECTION_L = 3;

        public const int ATTACK_L = 0;
        public const int ATTACK_B = 1;
        public const int ATTACK_R = 2;

        private bool takingDamage = false;
        private float invTime = 0.3f;
        private float damageTimer = 0f;

        void Awake()
        {
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            agent.height = 5;
            agent.baseOffset = .2f;
            sparkCount = 0;
            maxHealth = health;
        }

        private void Patrolling()
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet) agent.SetDestination(walkPoint);

            direction = GetMovingDirection(walkPoint);

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
            direction = GetMovingDirection(player.position);
            agent.SetDestination(player.position);
        }

        private void AttackPlayer()
        {
            agent.SetDestination(transform.position);

            LookTowards(player);
            if (!attacking)
            {
                if(Physics.CheckSphere(transform.position, meleeAttackRange, whatIsPlayer))
                {
                    MeleeAttack();
                } else
                {
                    ShockWaveAttack();
                }
                Invoke(nameof(MeleeHit), meleeDelay);
            }
        }

        public void TakeDamage(float damage)
        {
            if (!takingDamage)
            {
                health -= damage;
                takingDamage = true;
                //Debug.Log("Boss took " + damage + " damage.");
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
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (takingDamage)
            {
                damageTimer += Time.fixedDeltaTime;
                if (damageTimer > invTime)
                {
                    takingDamage = false;
                    damageTimer = 0;
                }
            }

            if (wait > 0) wait--;
            else
            {
                if (attacking == false)
                {
                    LookTowards(player);
                    playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
                    playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

                    if (!playerInSightRange && !playerInAttackRange) Patrolling();
                    if (playerInSightRange && !playerInAttackRange) ChasePlayer();
                    if (sparkCount < maxSparkCount && !sparkCreated)
                    {
                        CreateSpark();
                    }
                    else if (playerInAttackRange)
                    {
                        AttackPlayer();
                    }
                    else
                    {
                        BossAnimator.UpdateAnimation(false, true, false, direction);
                        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                    }
                    Invoke(nameof(ResetAttack), 2);
                }
            }
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.P)) TakeDamage(20);
        }

        private void MeleeAttack()
        {
            BossAnimator.UpdateAnimation(true, false, true, ATTACK_B);
            attacking = true;
        }

        private void ShockWaveAttack()
        {
            BossAnimator.UpdateAnimation(true, false, true, ATTACK_R);
            attacking = true;
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
            BossAnimator.UpdateAnimation(true, false, true, ATTACK_L);
            attacking = true;
            newSpark = Instantiate(spark, new Vector3(0, 1, 0), Quaternion.identity);
            sparkCount++;
            sparkCreated = true;
            Invoke(nameof(ResetSpark), sparkDelay);
        }
        private void ResetSpark()
        {
            sparkCreated = false;
        }

        private void MeleeHit()
        {
            if(Physics.CheckSphere(transform.position, meleeHitRange, whatIsPlayer))
            {
                GameController.player.TakeDamage(CLAW_DAMAGE, true);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, meleeAttackRange);
        }

        private void LookTowards(Transform targetTransform)
        {
            transform.LookAt(2 * transform.position - targetTransform.position);
        }

        private Vector3 GetForward()
        {
            return transform.forward * -1;
        }

        private int GetMovingDirection(Vector3 Destination)
        {
            Vector3 lookingTowards = GetForward();
            float destX = Destination.x;
            float destZ = Destination.z;

            //Find angle between the looking towards and moving direction
            float sin = lookingTowards.x * destZ - destX * lookingTowards.z;
            float cos = lookingTowards.x * destX + destZ * lookingTowards.z;

            float angle = Mathf.Atan2(sin, cos) * 180 / Mathf.PI;

            if (angle < 45 && angle > -45)
            {
                return DIRECTION_F;
            }
            else if (angle > -135 && angle < -45)
            {
                return DIRECTION_L;
            }
            else if (angle < 135 && angle > 45)
            {
                return DIRECTION_R;
            } else
            {
                return DIRECTION_B;
            }
        }

        private void ResetAttack()
        {
            attacking = false;
            BossAnimator.UpdateAnimation(false, false, false, -1);
        }
    }
}
