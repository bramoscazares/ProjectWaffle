using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float distance;
    float health, maxHealth = 3f;
    public Transform Aim;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards
        (this.transform.position, player.transform.position, speed * Time.deltaTime);

        Aim.rotation = Quaternion.LookRotation(Vector3.forward, direction);

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
        player.GetComponent<BattleHealth>().TakeDamage(1);
    }
}

