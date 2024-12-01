using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUpGenerator : MonoBehaviour
{
    public static DamagePopUpGenerator current;
    public GameObject prefab;

    public void Awake()
    {
        current = this; 
    }

    public void CreatePopUp(Vector3 position, string text)
    {
        var popup=Instantiate(prefab,position,Quaternion.identity);
        var temp=popup.transform.GetChild(0).GetComponent<TextMeshPro>();
        temp.text=text;

        Destroy(popup,1f);
        Destroy(temp,1f);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
