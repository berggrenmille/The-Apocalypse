using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public int renderDistance=5;
    public GameObject[] tiles;
    public int tileSize;    
    [SerializeField]
    private Transform player;
    private Dictionary<Vector2, GameObject> tileSet = new Dictionary<Vector2, GameObject>();
  

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("CheckTiles");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator CheckTiles()
    {
        while (true)
        {
            int closeX = (((int)player.position.x + (tileSize / 2)) / tileSize) * tileSize;
            int closeY = (((int)player.position.z + (tileSize / 2)) / tileSize) * tileSize;
            Vector2 pos;

            for (int i = 0; i < length; i++)
            {
                for (int i = 0; i < length; i++)
                {

                }
            }
        
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator SpawnTile(Vector2 position)
    {
        yield break;
    }
    private IEnumerator RemoveTile(Vector2 position)
    {
        yield break;
    }
}
