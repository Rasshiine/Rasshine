using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleScript : MonoBehaviour
{
    public AudioClip start;
    private AudioSource audioSource;
    private float alpha = 0;
    bool isSpacePressed = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(start);
            Invoke("SceneMove", 2.0f);
            isSpacePressed = true;
        }

        if (isSpacePressed == true)
        {
            alpha += 0.02f;
            this.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        }
    }

    void SceneMove()
    {
          SceneManager.LoadScene("PlayScene");
    }
}
   

