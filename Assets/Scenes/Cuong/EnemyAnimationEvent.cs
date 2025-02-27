using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public Collider weaponCollider;

    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
    }
}
