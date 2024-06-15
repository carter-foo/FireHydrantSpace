using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawner : MonoBehaviour
{
    public GameObject[] spaceJunkPrefabs;

    private float spawnInterval = 5f;
    private float stationarySpawnRadius = 500f;
    private float spawnRadius = 50f;

    // Start is called before the first frame update
    void Start()
    {
        // Start off by spawning a bunch of stationary or slow moving space junk
        for (int i = 0; i < 3000; i++) {
            SpawnStationarySpaceJunk();
        }

        // Now spawn more space junk that targets the player occasionally
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine() {
        Debug.Log("Coroutine started");
        while (true) {
            // Wait for the spawn interval
            yield return new WaitForSeconds(spawnInterval);
            SpawnTargetingSpaceJunk(); 
        }
    }

    private Object SpawnStationarySpaceJunk() {
        Debug.Log("Spawning stationary space junk");

        GameObject prefab = spaceJunkPrefabs[Random.Range(0, spaceJunkPrefabs.Length)];

        // Choose a spawn position within a radius of the player beyond a certain distance
        // TODO: add minimum distance from player
        Vector3 playerPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        Vector3 spawnPos = playerPos + Random.insideUnitSphere * stationarySpawnRadius;

        // Generate a random movement direction
        Vector3 moveDir = Random.onUnitSphere;

        // Spawn the space junk with a random direction
        GameObject instantiatedObject = Instantiate(prefab, spawnPos, Quaternion.identity);
        SpaceJunk junk = instantiatedObject.GetComponent<SpaceJunk>();
        junk.speed = Random.Range(0, 5f);
        junk.moveDir = moveDir;

        return instantiatedObject;
    }

    private Object SpawnTargetingSpaceJunk() {
        Debug.Log("Spawning new space junk");
        
        // Choose a random junk prefab
        GameObject prefab = spaceJunkPrefabs[Random.Range(0, spaceJunkPrefabs.Length)];

        // Choose a spawn position within a radius of the player beyond a certain distance
        // TODO: add minimum distance from player
        Vector3 playerPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        Vector3 spawnPos = playerPos + Random.insideUnitSphere * spawnRadius;
        
        // Locate the player and calculate a movement direction towards them
        Vector3 destPoint = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        Vector3 moveDir = (destPoint - spawnPos).normalized;

        // Spawn the space junk, then set its heading and speed
        GameObject instantiatedObject = Instantiate(prefab, spawnPos, Quaternion.identity);
        SpaceJunk junk = instantiatedObject.GetComponent<SpaceJunk>();
        junk.speed = Random.Range(0, 30f);
        junk.moveDir = moveDir;

        return instantiatedObject;
    }
}
