using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackController : MonoBehaviour
{
    public GameObject Melee;
    private bool isAttacking = false;
    private float atkDurarion = 0.3f;
    private float atkTimer = 0f;

    void Update()
    {
        CheckMeleeTimer();

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0))
        {
            OnAttack();
        }
    }
    void OnAttack()
    {
        if (!isAttacking)
        {
            Melee.SetActive(true);
            isAttacking = true;

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
                Melee.SetActive(false);
            }
        }
    }
}
