using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{

    // Chunk
    public GameObject chunk; 
    public Vector2 chunkDimensions;

    GameObject[,] chunks;
    Vector2 worldSize;
    float chunkSize = 16;
    float spawnpointRadius = 2;

    // Structures
    public GameObject testStructure;
    public GameObject testSmallStructure;

    void Start()
    {
        worldSize.x = (int) chunkDimensions.x * chunkSize;
        worldSize.y = (int) chunkDimensions.y * chunkSize;

        // Generate empty chunks
        chunks = new GameObject[(int)chunkDimensions.x, (int)chunkDimensions.y];
        for (int x = 0; x < chunks.GetLength(0); x ++) {
            for (int y = 0; y < chunks.GetLength(1); y ++) {
                chunks[x, y] = Instantiate(chunk, new Vector3(x*chunkSize, y*chunkSize, 0), Quaternion.identity);
            }
        }
        SpawnLargeStructures();
        SpawnSmallStructures();
        
    }


    void SpawnLargeStructures() {
        // Fill chunks with structures
        foreach (GameObject chunk in chunks) {
            // Spawn room or large structure if possible
            int roomSpawnRate = (int) Random.Range(1,11);
            int spawnIndex = (int) Random.Range(0,chunk.transform.childCount);

            // TODO: Get random large structure

            Vector2 structureSize = testStructure.GetComponent<BoxCollider2D>().size;

            // Get random spawn position within chunk if possible
            float rndOffsetY = (structureSize.y <= spawnpointRadius) ? Random.Range(-spawnpointRadius+structureSize.y,spawnpointRadius-structureSize.y) : 0;
            float rndOffsetX = (structureSize.x <= spawnpointRadius) ? Random.Range(-spawnpointRadius+structureSize.x,spawnpointRadius-structureSize.x) : 0;
            Vector3 spawnpointPos = chunk.transform.GetChild(spawnIndex).position;
            Vector3 roomSpawnPos = new Vector3(spawnpointPos.x + rndOffsetX, spawnpointPos.y + rndOffsetY, 0);

            // Randomly spawn room on current chunk
            if (roomSpawnRate <= 5) {
                Instantiate(testStructure, roomSpawnPos, Quaternion.identity);
                chunk.transform.GetChild(spawnIndex).GetComponent<StructureSpawnpoint>().canSpawn = false;
            }
        }
    }

    void SpawnSmallStructures() {
        foreach (GameObject chunk in chunks) {
            foreach (Transform spawnpoint in chunk.transform) {
                if (spawnpoint.GetComponent<StructureSpawnpoint>().canSpawn) {

                    int structuresToSpawn = (int) Random.Range(0,2);
                    for (int i = 0; i < structuresToSpawn; i ++) {
                        // TODO: Get random structure
                        Vector2 structureSize = testStructure.GetComponent<BoxCollider2D>().size;
                        float rndOffsetY =Random.Range(-spawnpointRadius+structureSize.y,spawnpointRadius-structureSize.y);
                        float rndOffsetX =Random.Range(-spawnpointRadius+structureSize.x,spawnpointRadius-structureSize.x);
                        Vector3 spawnPos = new Vector3(spawnpoint.position.x + rndOffsetX, spawnpoint.position.y + rndOffsetY, 0);

                        Instantiate(testSmallStructure, spawnPos, Quaternion.identity);
                    }
                }
            }
        }
    }

    void SpawnDecorations() {

    }
}
