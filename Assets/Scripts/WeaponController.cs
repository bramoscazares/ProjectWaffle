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
