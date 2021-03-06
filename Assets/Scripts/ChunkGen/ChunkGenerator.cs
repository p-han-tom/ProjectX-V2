﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{

    // Chunk
    public GameObject chunk; 
    public Vector2 gridDimensions;

    GameObject[,] chunks;
    Vector2 worldSize;
    float chunkSize = 16;
    float spawnpointRadius = 2;

    // Structures
    public Structure[] largeStructures;
    public Structure[] smallStructures;

    bool spawningLargeStructures;
    bool spawningSmallStructures;

    void Start()
    {
        worldSize.x = (int) gridDimensions.x * chunkSize;
        worldSize.y = (int) gridDimensions.y * chunkSize;

        // Generate empty chunks
        chunks = new GameObject[(int)gridDimensions.x, (int)gridDimensions.y];
        for (int x = 0; x < chunks.GetLength(0); x ++) {
            for (int y = 0; y < chunks.GetLength(1); y ++) {
                chunks[x, y] = Instantiate(chunk, new Vector3(x*chunkSize, y*chunkSize, 0), Quaternion.identity);
            }
        }
        StartCoroutine("SpawnLargeStructures");
        StartCoroutine("SpawnSmallStructures");
        
    }


    IEnumerator SpawnLargeStructures() {

        spawningLargeStructures = true;

        // Fill chunks with structures
        foreach (GameObject chunk in chunks) {

            // Spawn room or large structure if possible
            int roomSpawnRate = (int) Random.Range(1,11);
            int spawnIndex = (int) Random.Range(0,chunk.transform.childCount);

            // Get random large structure
            GameObject roomToSpawn = largeStructures[(int) Random.Range(0, largeStructures.Length)].structure;

            Vector2 structureSize = roomToSpawn.GetComponent<BoxCollider2D>().size;

            // Get random spawn position within chunk if possible
            float rndOffsetY = (structureSize.y <= spawnpointRadius) ? Random.Range(-spawnpointRadius+structureSize.y,spawnpointRadius-structureSize.y) : 0;
            float rndOffsetX = (structureSize.x <= spawnpointRadius) ? Random.Range(-spawnpointRadius+structureSize.x,spawnpointRadius-structureSize.x) : 0;
            Vector3 spawnpointPos = chunk.transform.GetChild(spawnIndex).position;
            Vector3 roomSpawnPos = new Vector3(spawnpointPos.x + rndOffsetX, spawnpointPos.y + rndOffsetY, 0);

            // Randomly spawn room on current chunk
            if (roomSpawnRate <= 5 && chunk.transform.GetChild(spawnIndex).GetComponent<StructureSpawnpoint>().canSpawn) {
                Instantiate(roomToSpawn, roomSpawnPos, Quaternion.identity);
                yield return new WaitUntil(() => !chunk.transform.GetChild(spawnIndex).GetComponent<StructureSpawnpoint>().canSpawn);
            }
        }

        spawningLargeStructures = false;
    }

    IEnumerator SpawnSmallStructures() {

        while (spawningLargeStructures)
            yield return new WaitForFixedUpdate();

        foreach (GameObject chunk in chunks) {
            foreach (Transform spawnpoint in chunk.transform) {
                if (spawnpoint.GetComponent<StructureSpawnpoint>().canSpawn) {
                    // Get random chance to spawn
                    int structuresToSpawn = (int) Random.Range(0,2);
                    for (int i = 0; i < structuresToSpawn; i ++) {
                        // Get random structure
                        GameObject structToSpawn = smallStructures[(int) Random.Range(0, smallStructures.Length)].structure;


                        // Get random position within spawnpoint radius and instantiate structure
                        Vector2 structureSize = structToSpawn.GetComponent<BoxCollider2D>().size;
                        Debug.Log(structureSize);
                        float rndOffsetY = Random.Range(-spawnpointRadius+structureSize.y/2,spawnpointRadius-structureSize.y/2);
                        float rndOffsetX = Random.Range(-spawnpointRadius+structureSize.x/2,spawnpointRadius-structureSize.x/2);
                        Vector3 spawnPos = new Vector3(spawnpoint.position.x + rndOffsetX, spawnpoint.position.y + rndOffsetY, 0);

                        Instantiate(structToSpawn, spawnPos, Quaternion.identity);
                    }
                }
            }
        }
    }
}
