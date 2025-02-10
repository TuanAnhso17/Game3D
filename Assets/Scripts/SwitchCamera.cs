using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject Thirdcam;
    public GameObject FirstCamera;
    public int CamMode;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Camera"))
        {
            if(CamMode== 1)
            {
                CamMode = 0;
            }
            else
            {
                CamMode = 1;
            }
            StartCoroutine(Camchange());
        }
    }

    IEnumerator Camchange()
    {
        yield return new WaitForSeconds(0.01f);
        if (CamMode == 0)
        {
            Thirdcam.SetActive(true);
            FirstCamera.SetActive(false);
        }
        if (CamMode == 1)
        {
            FirstCamera.SetActive(true);
            Thirdcam.SetActive(false) ; 
        }
    }
}
