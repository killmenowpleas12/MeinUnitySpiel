using UnityEngine;
using System.Collections.Generic;

public class ChunkManager : MonoBehaviour
{
    public GameObject[] biomePrefabs;      // Gras, Wald etc.
    public Transform player;               // der Spieler
    public int chunkSize = 20;             // wie groﬂ ist 1 Chunk?
    public int loadRadius = 2;             // wie viele Chunks rundherum sollen erzeugt werden?

    private Dictionary<Vector2Int, GameObject> loadedChunks = new Dictionary<Vector2Int, GameObject>();

    void Update()
    {
        // Ermittle, in welchem Chunk der Spieler gerade ist
        Vector2Int currentChunk = new Vector2Int(
            Mathf.FloorToInt(player.position.x / chunkSize),
            Mathf.FloorToInt(player.position.y / chunkSize)
        );

        // Lade umliegende Chunks
        for (int x = -loadRadius; x <= loadRadius; x++)
        {
            for (int y = -loadRadius; y <= loadRadius; y++)
            {
                Vector2Int chunkCoord = currentChunk + new Vector2Int(x, y);
                if (!loadedChunks.ContainsKey(chunkCoord))
                {
                    SpawnChunk(chunkCoord);
                }
            }
        }
    }

    void SpawnChunk(Vector2Int coord)
    {
        // Zuf‰lliges Biom ausw‰hlen
        int biomeIndex = Random.Range(0, biomePrefabs.Length);
        GameObject biomePrefab = biomePrefabs[biomeIndex];

        // Chunk erzeugen
        Vector3 position = new Vector3(coord.x * chunkSize, coord.y * chunkSize, 0);
        GameObject newChunk = Instantiate(biomePrefab, position, Quaternion.identity);
        newChunk.name = $"Chunk_{coord.x}_{coord.y}";

        loadedChunks[coord] = newChunk;
    }
}