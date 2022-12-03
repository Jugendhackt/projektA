using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    Sprite[] blocks;
    [SerializeField]
    Sprite[] templates;
    List<int> loadChunks;
    int range = 2;
    int lastChunk;

    float randomOffset;

    [SerializeField]
    Transform player;
    // Start is called before the first frame update
    private void Awake()
    {
        loadChunks = new List<int>();
        lastChunk = int.MaxValue;
        randomOffset = Random.Range(-9999,9999);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CheckChunks(player.position);
    }

    public void CheckChunks(Vector3 position)
    {
        int chunkX = Mathf.FloorToInt(position.x / 32f);
        if (lastChunk == chunkX)
            return;

        lastChunk = chunkX;

        List<int> newChunks = new List<int>();
        for(int x = chunkX-range; x <=chunkX+range; x++)
        {
            newChunks.Add(x);
        }

        for(int i = 0; i < loadChunks.Count; i++)
        {
            if (!newChunks.Contains(loadChunks[i]))
            {
                RemoveChunk(loadChunks[i] * 32, 0);
                Debug.Log(loadChunks[i]);
            }
        }

        for(int i = 0; i < newChunks.Count; i++)
        {
            if (!loadChunks.Contains(newChunks[i]))
            {
                Debug.Log(randomOffset);
               float noise = rand(new Vector2(randomOffset+newChunks[i],0));
                int t = Mathf.FloorToInt(noise * templates.Length);
                Debug.Log(t);
                SpawnChunk(newChunks[i] * 32, 0, t);
            }
        }

        loadChunks = newChunks;
        Debug.Log("Loading new Chunks");
    }

    public void SpawnChunk(int xOff, int yOff, int template)
    {
        Texture2D currentTemplate = templates[template].texture;
        for(int x = 0; x < 32; x++)
        {
            for(int y = 0; y < 32; y++)
            {
                if (currentTemplate.GetPixel(x, y).a < 1)
                    continue;

                int i = 0;
                if(x+1 < 16 && currentTemplate.GetPixel(x+1,y).a > 0)
                {
                    i |= 1 << 0;
                }
                if (y-1 >= 0 && currentTemplate.GetPixel(x, y-1).a > 0)
                {
                    i |= 1 << 1;
                }
                if (x-1 >= 0 && currentTemplate.GetPixel(x-1, y).a > 0)
                {
                    i |= 1 << 2;
                }
                if (y+1<16 && currentTemplate.GetPixel(x, y+1).a > 0)
                {
                    i |= 1 << 3;
                }
                SpawnTile(x+xOff, y+yOff, i);
            }
        }
    }

    public void SpawnTile(int x, int y, int i)
    {
        Tile tile = new Tile();
        tile.sprite = blocks[i];
        tilemap.SetTile(new Vector3Int(x, y), tile);
    }

    public void RemoveChunk(int xOff, int yOff)
    {
        /*if (tilemap.GetTile(new Vector3Int(x, y)) == null)
            return;*/

        for(int x = 0; x < 32; x++)
        {
            for(int y =0; y < 32; y++)
            {
                tilemap.SetTile(new Vector3Int(x+xOff, y+yOff), null);
            }
        }
    }

    private float rand(Vector2 co)
    {
        return (Mathf.Abs(Mathf.Cos((Mathf.Sin(Vector2.Dot(co, new Vector2(12.9898f, 78.233f))) * 43758.5453f)))) * 1;
    }

}
