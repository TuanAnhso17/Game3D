using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private string h = "Horizontal";
    private string v = "Vertical";
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Input.GetAxis(h) != 0 || Input.GetAxis(v) != 0)
                anim.SetTrigger("SlashMark");
            else
                anim.SetTrigger("Slash");
        }
    }
}
