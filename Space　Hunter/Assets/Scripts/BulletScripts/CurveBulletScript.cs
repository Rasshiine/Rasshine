using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveBulletScript : MonoBehaviour
{
    public float bulletSpeed = 1.5f;
    int curveDirection;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
        curveDirection = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (curveDirection == 0)
        {
            transform.position += transform.forward * bulletSpeed * Time.deltaTime;
            transform.Rotate(0, 1 * Time.deltaTime * 100, 0);
        } else
        {
            transform.position += transform.forward * bulletSpeed * Time.deltaTime;
            transform.Rotate(0, -1 * Time.deltaTime * 100, 0);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
