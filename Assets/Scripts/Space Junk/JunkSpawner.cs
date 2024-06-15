using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawner : MonoBehaviour
{
    public GameObject[] spaceJunkPrefabs;
    public float leadFactor = 50f;
    public float stationarySpawnRadius = 500f;

    private float spawnInterval = 5f;
    private float spawnRadius = 150f;

    private Vector3 lastSpawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        // Start off by spawning a bunch of stationary or slow moving space junk
        for (int i = 0; i < 3000; i++) {
            SpawnStationarySpaceJunk();
        }

        // Now spawn more space junk that targets the player occasionally
        StartCoroutine(SpawnRoutine());

        // Initialize the last spawn position to 
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
        // Debug.Log("Spawning stationary space junk");

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
        junk.speed = Random.Range(0, 3f);
        junk.moveDir = moveDir;

        return instantiatedObject;
    }

    private Object SpawnTargetingSpaceJunk() {
        Debug.Log("Spawning new space junk");
        
        // Choose a random junk prefab
        GameObject prefab = spaceJunkPrefabs[Random.Range(0, spaceJunkPrefabs.Length)];

        // Choose a spawn position spawnRadius units away from the player in a random direction
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;
        Vector3 spawnPos = playerPos + Random.onUnitSphere * spawnRadius;
        
        // Make the junk move at a random speed
        float junkSpeed = Random.Range(10f, 30f);

        // Try to intercept the player based on their position and movement speed
        PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
        Debug.Log("Player speed: " + movementScript.GetCurrentVelocity());
        // Vector3 interceptionPoint = player.transform.position + movementScript.GetCurrentVelocity() * leadFactor/junkSpeed;
        // Vector3 moveDir = (interceptionPoint - spawnPos).normalized;
        Vector3 moveDir = calcAimDirection(player.transform.position, movementScript.GetCurrentVelocity(), spawnPos, junkSpeed);

        // Spawn the space junk, then set its heading and speed
        GameObject instantiatedObject = Instantiate(prefab, spawnPos, Quaternion.identity);
        SpaceJunk junk = instantiatedObject.GetComponent<SpaceJunk>();
        junk.speed = junkSpeed;
        junk.moveDir = moveDir;

        return instantiatedObject;
    }

    // idk how this works but it does
    // (from https://gamedev.stackexchange.com/questions/25277/how-to-calculate-shot-angle-and-velocity-to-hit-a-moving-target)
    // (explained at https://www.gamedeveloper.com/programming/shooting-a-moving-target)
    private Vector3 calcAimDirection (Vector3 targetPos, Vector3 targetVel, Vector3 projectilePos, float projectileSpeed) {
        Vector3 toTarget = targetPos - projectilePos;

        float a = Vector3.Dot(targetVel, targetVel) - (projectileSpeed * projectileSpeed);
        float b = 2 * Vector3.Dot(targetVel, toTarget);
        float c = Vector3.Dot(toTarget, toTarget);

        if (a == 0) {
            return new Vector3(0, 0, 0);
        }
        float p = -b / (2 * a);
        float q = (float)Mathf.Sqrt((b*b) - 4 * a * c) / (2 * a);

        float t1 = p - q;
        float t2 = p + q;
        float t;

        if (t1 > t2 && t2 > 0) {
            t = t2;
        } else {
            t = t1;
        }

        Vector3 interceptionPoint = targetPos + targetVel*t;
        Vector3 aimDirection = (interceptionPoint - projectilePos);
        // float timeToImpact = aimDirection.Length() / projectileSpeed;

        return aimDirection.normalized;
    }
}
