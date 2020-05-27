using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MonsterScript : MonoBehaviourPun
{
    public GameObject bullet;
    public Transform muzzle;
    public float speed = 5.0f;

    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
    　
        if (Input.GetKey("up"))
        {
            transform.position += transform.forward * 0.05f;
           // animator.SetBool("walk", true);
        }
        else
        {
           // animator.SetBool("walk", false);
        }
        if (Input.GetKey("right"))
        {
            transform.Rotate(0, 10, 0);
        }
        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -10, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }


    }
    void Shot()
    {
        GameObject obj = PhotonNetwork.Instantiate(bullet.name, muzzle.position, muzzle.rotation) as GameObject;
        obj.GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}