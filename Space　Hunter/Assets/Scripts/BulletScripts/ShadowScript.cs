using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShadowScript : MonoBehaviour
{
    public string objectName;
    public float convergenceTime = 120;
    // Start is called before the first frame update
    void Start()
    {
        if (objectName == "Sphere")
        {
            Vector3 endvalue = new Vector3(0, 0, 0);
            transform.DOScale(endvalue, convergenceTime);
        }
        else 
        {
            Vector3 endPoint = new Vector3(0, -3, 0);
            transform.DOMove(endPoint, convergenceTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
