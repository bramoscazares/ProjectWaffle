using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyController enemy = collision.GetComponent<enemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        
        Customer customer = collision.GetComponent<Customer>();
        if (customer != null)
        {
            customer.TakeOrder();
        }
    }

 
}
