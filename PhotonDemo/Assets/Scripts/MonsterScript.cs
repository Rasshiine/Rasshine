using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MonsterScript : MonoBehaviourPunCallbacks
{

    //#region IPunObservable implementation

    public GameObject bullet;
    public Transform muzzle;
    public float speed = 5.0f;
    public float HP = 5;
    public float maxMP = 1000;
    public float currentMP = 0;
    public Slider slider;
    public Slider MP;
    public Button shotButton;
    public Button tripleShotButton;

    private Animator animator;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        slider.maxValue = HP;
        slider.value = HP;
        MP.maxValue = maxMP;
        MP.value = 1;
        //currentMP = maxMP2;
        shotButton.onClick.AddListener(Shot);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentMP);
        MP.value = currentMP; //  / maxMP;
        if (currentMP >= maxMP)
        {
            currentMP = maxMP;
        }
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
            Debug.Log("HPが0になりました");
            Destroy(this.gameObject);
        }


    
        
    }

    private void FixedUpdate()
    {
        currentMP += 1f; // * Time.deltaTime;
    }
    public void Shot()
    {
        if (currentMP >= 200)
        {
            Debug.Log(this.transform.position.x);
            GameObject obj = PhotonNetwork.Instantiate(bullet.name, muzzle.position, muzzle.rotation) as GameObject;
            obj.GetComponent<Rigidbody>().velocity = transform.forward * speed;
            // Destroy(bullet, 1);
            currentMP -= 200;
        }
    }

    public void TripleShot()
    {
        if (currentMP >= 300)
        {
             Debug.Log(this.transform.position.x);
            GameObject obj = PhotonNetwork.Instantiate(bullet.name, muzzle.position, muzzle.rotation) as GameObject;
            obj.GetComponent<Rigidbody>().velocity = transform.forward * speed;
            // Destroy(bullet, 1);
            currentMP -= 300;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");
        HP--;
        slider.value = HP;
       // Destroy(bullet);
    }
    public void onPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(HP);
        } else
        {
            this.HP = (float)stream.ReceiveNext();
        }
    }
    
}