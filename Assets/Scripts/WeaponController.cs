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
            Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            float knockbackForce = 10f; // Adjust this value

            enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            enemy.TakeDamage(damage);
        }

        Customer customer = collision.GetComponent<Customer>();
        if (customer != null)
        {
            customer.HandleInteraction();
        }

        WaffleStation waffleStation = collision.GetComponent<WaffleStation>();
        if (waffleStation != null)
        {
            waffleStation.HandleWaffleStationInteraction();

        }

        BaconStation baconStation = collision.GetComponent<BaconStation>();
        if (baconStation != null)
        {
            baconStation.HandleBaconStationInteraction();
        }

        EggsStation eggsStation = collision.GetComponent<EggsStation>();
        if (eggsStation != null)
        {
            eggsStation.HandleEggsStationInteraction();
        }

        JuiceStation juiceStation = collision.GetComponent<JuiceStation>();
        if (juiceStation != null)
        {
            juiceStation.HandleJuiceStationInteraction();
        }

        Trashcan trashcan = collision.GetComponent<Trashcan>();
        if (trashcan != null)
        {
            trashcan.HandleTrashcanInteraction();
        }
    }

}
