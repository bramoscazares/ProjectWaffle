using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float distance;
    float health, maxHealth = 3f;

    public GameObject Aim;
    public Transform AimTransform;
    private bool isAttacking = false;
    private float atkDurarion = 0.3f;
    private float atkTimer = 0f;
    void Start()
    {
        health = maxHealth;
        if (Aim != null)
        {
            AimTransform = Aim.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards
        (this.transform.position, player.transform.position, speed * Time.deltaTime);

        CheckMeleeTimer();

        AimTransform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        if (distance < 1.5f)
        {
            AttackPlayer();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void AttackPlayer()
    {
        OnAttack();

    }
    

    void OnAttack()
    {
        if (!isAttacking)
        {
            Aim.SetActive(true);
            isAttacking = true;
            player.GetComponent<BattleHealth>().TakeDamage(1);
            print("Player hit! 1 damage taken.");

            print("Player health: " + player.GetComponent<BattleHealth>().health);

        }
    }


    void CheckMeleeTimer()
    {
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkDurarion)
            {
                atkTimer = 0;
                isAttacking = false;
                Aim.SetActive(false);
            }
        }
    }
}

