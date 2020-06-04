using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBulletScript : MonoBehaviour
{
    public float bulletSpeed = 1.5f;

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
