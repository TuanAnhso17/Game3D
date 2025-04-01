using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNumber : MonoBehaviour
{
    public int diem { get; set; }
    public void congdiem(int x)
    {
        diem += x;
    }
}
