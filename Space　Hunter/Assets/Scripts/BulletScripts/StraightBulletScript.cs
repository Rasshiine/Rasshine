using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StraightBulletScript : MonoBehaviour
{
    public float bulletSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
