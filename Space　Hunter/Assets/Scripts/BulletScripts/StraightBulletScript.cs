using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StraightBulletScript : MonoBehaviour
{
    public float bulletSpeed = 5.0f;
    public AudioClip Explosion1;
    private AudioSource audioSource;

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
        Debug.Log(audioSource);
        //audioSource.PlayOneShot(Explosion1);
        audioSource.clip = Explosion1;
        audioSource.Play();
        Destroy(gameObject,0.2f);
    }
}
