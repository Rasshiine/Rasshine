using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBulletScript : MonoBehaviour
{
    public AudioClip Explosion1;
    private AudioSource audioSource;

    public float bulletSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        //audioSource.clip = Explosion1;
        audioSource.PlayOneShot(Explosion1);
        Destroy(gameObject);
    }
}
