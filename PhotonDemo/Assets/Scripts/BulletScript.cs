using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BulletScript : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        Destroy(gameObject);
    }
}
