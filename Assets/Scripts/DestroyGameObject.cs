using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public int time;
    void Start()
    {
        Destroy(this.gameObject,time);
    }

}
