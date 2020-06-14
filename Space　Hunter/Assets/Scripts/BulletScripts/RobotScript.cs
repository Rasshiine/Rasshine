using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    GameObject player;
    public string targetName;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(targetName);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Untagged")
            Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
