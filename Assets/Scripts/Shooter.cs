using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Shooter : MonoBehaviour
{

    public Camera cam;
    private Vector3 destination;
    public GameObject projecttile;
    public Transform LHFirePoint, RHFirePoint;
    private bool leftHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);

        }
        if(leftHand)
        {
            leftHand = false;
            InstantiateProjecttile(LHFirePoint);
        }
        else
        {
            leftHand=true;
            InstantiateProjecttile(RHFirePoint);
        }
        
    }

    void InstantiateProjecttile( Transform firePoint)
    {
        var projecttileObj = Instantiate(projecttile, firePoint.position, Quaternion.identity) as GameObject;
    }
}
