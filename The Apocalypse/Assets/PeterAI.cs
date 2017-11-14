using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeterAI : MonoBehaviour {
    public float MoveFactor = 1;
    public float RotFactor = 1;
    private Transform player;
    private Rigidbody peterRB;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        peterRB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        MoveAway();
	}
    public void MoveAway()
    {
        Vector3 forward = transform.forward;
        Vector3 toPlayer = player.position - transform.position;
        float dot = Vector3.Dot(forward, toPlayer);
        Debug.Log(dot); 
        Ray[] rays = new Ray[4];
        rays[0] = new Ray(transform.position, forward);         //0 - forward
        rays[0] = new Ray(transform.position, -forward);        //1 - backward
        rays[0] = new Ray(transform.position, transform.right); //2 - right
        rays[0] = new Ray(transform.position, -transform.right);//3 - left
        Vector3 directionToRun = Vector3.zero;
        if (dot < 0)
        {
           
            RaycastHit ray0;
            RaycastHit ray2;
            RaycastHit ray3;
            if (Physics.Raycast(rays[0], out ray0, 100))
            {
                directionToRun.z = Mathf.Abs((transform.position - ray0.point).magnitude / 100);
                if (Physics.Raycast(rays[2], out ray2, 100))
                {
                    directionToRun.x += Mathf.Abs((transform.position - ray2.point).magnitude / 100);
                }
                if (Physics.Raycast(rays[3], out ray3, 100))
                {
                    directionToRun.x -= Mathf.Abs((transform.position - ray2.point).magnitude / 100);
                }
            }
            else
            {
                directionToRun.z = 1;
                //Player is behind PeterAI
                
            }
            
        }
        Debug.Log(directionToRun);
        peterRB.MovePosition(transform.position +directionToRun.normalized * Time.deltaTime * MoveFactor);
    }
}
