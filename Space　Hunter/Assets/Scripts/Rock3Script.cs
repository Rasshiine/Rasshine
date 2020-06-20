using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock3Script : MonoBehaviour
{
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Untagged" && other.gameObject.tag != "Player1" && other.gameObject.tag != "Player2" )
        {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);

        }

    }
}
