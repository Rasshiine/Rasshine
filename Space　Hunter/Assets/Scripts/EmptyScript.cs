using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyScript : MonoBehaviour
{
    public PlayerScript playerScript;
    bool isPlaying;
    bool BGMPlay = true;
    bool explosionPlay;
    bool explosionAudio = true;
    AudioSource audioSource;

    public AudioClip BGM;
    public AudioClip bigExplosion;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isPlaying = playerScript.isPlaying;
        if (isPlaying == true && BGMPlay == true)
        {
            audioSource.PlayOneShot(BGM);
            BGMPlay = false;
        }

        explosionPlay = playerScript.explosionPlay;
        if (explosionPlay == true && explosionAudio == true)
        {
            audioSource.PlayOneShot(bigExplosion);
            explosionAudio = false;
        }
    }
}
