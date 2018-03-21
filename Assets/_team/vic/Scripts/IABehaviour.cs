using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABehaviour : MonoBehaviour
{

    private Animator Anim;
    private float walking;
    private bool isWalking = false;
    private bool isAttack = false;
    public Transform target;
    public float targetDistance;
    public float attackDistance = 1.25f;
    public float initialEnemyLife = 100;
    private float enemyLife = 100;
    private bool isUnanimate = false;
    NavMeshAgent agent;
    Collider maincollider;

    // Use this for initialization
    void Start()
    {
        
        walking = 1f;
        agent = GetComponent<NavMeshAgent>();
        maincollider = GetComponent<Collider>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyManager.AddEnemy(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUnanimate)
        {
            Anim.SetFloat("walking", walking);

            agent.destination = target.position;
            targetDistance = (target.position - transform.position).magnitude;
            if (targetDistance < attackDistance)
            {
                Anim.SetBool("isAttack", true);
            }
            else
                Anim.SetBool("isAttack", false);

            if (enemyLife < 1)
            {
                EnemyDead();
            }
        }
    }

    public void DecreaseEnemyLife()
    {
        enemyLife = enemyLife - 25;
    }

    public void ResetPlayerLife()
    {
        enemyLife = initialEnemyLife;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerWeapons")
        {
            DecreaseEnemyLife();
        }

    }

    public void EnemyDead()
    {
        isUnanimate = true;
        agent.enabled = false;
        Anim.SetBool("isFalling", true);
        maincollider.enabled = false;
        Destroy(gameObject, 10);
        //ScoreManager.IncreaseScore();
        EnemyManager.RemoveEnemy(transform);
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed");
    }
}
