using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] bool attackPlayer = true;

    public int GetDamage()
    {
        return damage;
    }

    public bool GetAttackPlayer()
    {
        return attackPlayer;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
