using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoatScript : MonoBehaviour {
    public Vector3 capeFinalPos;
    public Vector3 capeFinalScale;
    public float lerp = 0.5f;


	// Use this for initialization
	void Start () {
        StartCoroutine("lerpCapePos");
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0); // do not move cape along the z axis

      
        

	}

    public IEnumerator lerpCapePos()
    {
        while (true)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, capeFinalPos, lerp);
            if (transform.localPosition == capeFinalPos)
            {
                StartCoroutine("lerpCapeScale");
               // break;
            }
                yield return null;
        }
        yield break;
    }
    public IEnumerator lerpCapeScale()
    {
        while (true)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, capeFinalScale, lerp);
            if (transform.localScale == capeFinalScale)
            {
                break;
            }
            else
            {
                yield return null;
            }
        }
        yield break;
    }
}
