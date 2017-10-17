using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeterAI : MonoBehaviour {
    public float MoveFactor = 1;
    public Transform player;
    private NavMeshAgent navAgent;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        MoveAway();
	}
    public void MoveAway()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - player.position);
            
        Vector3 runTo = transform.position + transform.forward * MoveFactor;

        NavMeshHit hit;
        NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));

        navAgent.SetDestination(hit.position);
    }
}
