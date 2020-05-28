using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MonsterScript : MonoBehaviourPun
{
    public GameObject bullet;
    public Transform muzzle;
    public float speed = 5.0f;
    public int HP = 5;
    public Slider slider;
    public Button shotButton;
    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        slider.maxValue = HP;
        slider.value = HP;
        shotButton.onClick.AddListener(Shot);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * 0.05f;
            // animator.SetBool("walk", true);
        }
        else
        {
            // animator.SetBool("walk", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 10, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -10, 0);
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }*/
        if (HP <= 0)
        {
            Debug.Log("0");
            Destroy(this.gameObject);
        }

        
    }
    public void Shot()
    { 
        Debug.Log(this.transform.position.x);
        GameObject obj = PhotonNetwork.Instantiate(bullet.name, muzzle.position, muzzle.rotation) as GameObject;
        obj.GetComponent<Rigidbody>().velocity = transform.forward * speed;
       // Destroy(bullet, 1);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");
        HP--;
        slider.value = HP;
       // Destroy(bullet);
    }
}