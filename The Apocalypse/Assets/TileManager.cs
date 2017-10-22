
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public int renderDistance=5;
    public Vector2 playerTile = Vector2.zero;
    public int tileSize;    
    [SerializeField]
    private Transform player;
    private Dictionary<Vector2, GameObject> tileSet = new Dictionary<Vector2, GameObject>();
    private readonly Queue<Vector2> tileQueue = new Queue<Vector2>();
    public GameObject tilePrefab;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("CheckTiles");
        StartCoroutine("ValidateTiles");
        
    }
#if UNITY_EDITOR
    void Update()
    {
        if (tileSet.Count != tileQueue.Count)
            Debug.LogError("set and queue is out of sync");
    }
#endif

    private IEnumerator CheckTiles()
    {
        while (true)
        {
            if(player.position.x > 0)
                playerTile.x = (((int)((player.position.x + (tileSize / 2)) / tileSize)) * tileSize);

            else
            {
                playerTile.x = (((int)((player.position.x - (tileSize / 2)) / tileSize)) * tileSize);
            }
            if(player.position.z > 0)
                playerTile.y = (((int)((player.position.z + (tileSize / 2)) / tileSize)) * tileSize);
            else
            {
                playerTile.y = (((int)((player.position.z - (tileSize / 2)) / tileSize)) * tileSize);
            }
            Vector2 pos;

            for (int i = 0; i < renderDistance; i++)
            {
                for (int j = 0; j < renderDistance; j++)
                {
                    pos = new Vector2(playerTile.x + i*tileSize, playerTile.y + j*tileSize);
                    if (!tileSet.ContainsKey(pos))
                    {

                        tileSet.Add(pos, Instantiate(tilePrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity));
                        tileQueue.Enqueue(pos);
                    }
                    pos = new Vector2(playerTile.x - i * tileSize, playerTile.y - j * tileSize);
                    if (!tileSet.ContainsKey(pos))
                    {

                        tileSet.Add(pos, Instantiate(tilePrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity));
                        tileQueue.Enqueue(pos);
                    }
                    pos = new Vector2(playerTile.x - i * tileSize, playerTile.y + j * tileSize);
                    if (!tileSet.ContainsKey(pos))
                    {

                        tileSet.Add(pos, Instantiate(tilePrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity));
                        tileQueue.Enqueue(pos);
                    }
                    pos = new Vector2(playerTile.x + i * tileSize, playerTile.y - j * tileSize);
                    if (!tileSet.ContainsKey(pos))
                    {

                        tileSet.Add(pos, Instantiate(tilePrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity));
                        tileQueue.Enqueue(pos);
                    }
                }
            }
        
            yield return null;
        }
    }

    private IEnumerator SpawnTile(Vector2 position)
    {
        yield break;
    }
    private IEnumerator RemoveTile(Vector2 position)
    {
        GameObject removeTile;
        if (tileSet.TryGetValue(position, out removeTile))
        {
#if UNITY_EDITOR
            Debug.Log("Removing Tile: "+removeTile);
#endif
            Destroy(removeTile);
            tileSet.Remove(position);
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Remove tile failed at:" + position);
#endif
        }
        yield break;
    }
    private IEnumerator ValidateTiles()
    {
        while (true)
        {
            if (tileQueue.Count > 0)
            {
                for (int i = 0; i < tileQueue.Count; i++)
                {
                    
                    if (Mathf.Abs(tileQueue.Peek().x - playerTile.x) > renderDistance*tileSize ||
                        Mathf.Abs(tileQueue.Peek().y - playerTile.y) > renderDistance*tileSize)
                    {
                        StartCoroutine("RemoveTile", tileQueue.Peek());
                        tileQueue.Dequeue();
                    }
                    else
                    {
                        tileQueue.Enqueue(tileQueue.Peek());
                        tileQueue.Dequeue();
                    }

                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
