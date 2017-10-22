using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureScript : MonoBehaviour
{

    [SerializeField] private GameObject fractured;
    public Vector3 offset = Vector3.zero;
    public float despawnTime = 5f;
    public bool shouldFracture = false;
	// Update is called once per frame
	void Update () {
		if(shouldFracture)
            Fracture();
	}

    public void Fracture()
    {
        Destroy(Instantiate(fractured, transform.position + offset, Quaternion.identity),despawnTime);
        Destroy(gameObject);
    }
}
