using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class WeaponManager : MonoBehaviour
{
    public GameObject weapon;
    public vThirdPersonController tcp;
    public void EnableMovement(bool enable)
    {
       /* if (enable == false)
        {
            tcp.lockMovement = true;
            tcp.lockRotation = true;
        }
        else
        {
            tcp.lockMovement = false;
            tcp.lockRotation = false;
        }*/
    }
    public void EnableWeaponCollider (int isEnable)
    {
        if (weapon != null)
        {
            var col=weapon.GetComponent<Collider>();
            if (col != null )
            {
                if(isEnable == 1)
                {
                    col.enabled = true;
                }
                else
                {
                    col.enabled = false;
                }
            }
        }
    }
}