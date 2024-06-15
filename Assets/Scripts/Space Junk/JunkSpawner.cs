using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawner : MonoBehaviour
{
    public GameObject[] spaceJunkPrefabs;
    public float stationarySpawnRadius = 500f;
    public float stationaryJunksToSpawn = 1000;
    public float targetingSpawnRadius = 150f;
    public float targetingSpaceJunkSpawnIntervalSeconds = 5f;
    public float updateRadius = 200f;
    public int maxSpawnAttempts = 5000;

    private Vector3 lastUpdatedPosition;
    private GameObject player;
    private List<GameObject> stationaryJunks;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        // Start off by spawning a bunch of stationary or slow moving space junk
        stationaryJunks = new List<GameObject>();
        for (int i = 0; i < stationaryJunksToSpawn; i++) {
            stationaryJunks.Add(SpawnStationarySpaceJunk());
        }

        // Now start spawning more space junk that targets the player occasionally
        StartCoroutine(SpawnRoutine());

        // Set the lastUpdatedPosition to the starting position
        lastUpdatedPosition = player.transform.position;
    }

    void Update () {
        if (Vector3.Distance(player.transform.position, lastUpdatedPosition) > updateRadius) {
            List<GameObject> toBeRemoved = new List<GameObject>();
            foreach (GameObject junk in stationaryJunks) {
                if (Vector3.Distance(player.transform.position, junk.transform.position) > stationarySpawnRadius) {
                    toBeRemoved.Add(junk);
                }
            }
            int numRemoved = toBeRemoved.Count;
            foreach (GameObject junk in toBeRemoved) {
                stationaryJunks.Remove(junk);
                Destroy(junk);
            }
            Debug.Log("Removed " + numRemoved + " space junks");
            for (int i = 0; i < numRemoved; i++) {
                int numTries = 0;
                Vector3 spawnLocation;
                do {
                    spawnLocation = player.transform.position + Random.insideUnitSphere * stationarySpawnRadius;
                    numTries++;
                } while (numTries < maxSpawnAttempts && Vector3.Distance(lastUpdatedPosition, spawnLocation) < stationarySpawnRadius);
                SpawnStationarySpaceJunk(spawnLocation);
            }
            lastUpdatedPosition = player.transform.position;
        }

    }

    IEnumerator SpawnRoutine() {
        Debug.Log("Coroutine started");
        while (true) {
            // Wait for the spawn interval
            yield return new WaitForSeconds(targetingSpaceJunkSpawnIntervalSeconds);
            SpawnTargetingSpaceJunk(); 
        }
    }

    private GameObject SpawnStationarySpaceJunk() {
        Debug.Log("Spawning stationary space junk");

        GameObject prefab = spaceJunkPrefabs[Random.Range(0, spaceJunkPrefabs.Length)];

        // Choose a spawn position within a radius of the player beyond a certain distance
        // TODO: add minimum distance from player
        Vector3 playerPos = player.transform.position;
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

    private GameObject SpawnStationarySpaceJunk(Vector3 spawnLocation) {
        Debug.Log("Spawning stationary space junk");

        GameObject prefab = spaceJunkPrefabs[Random.Range(0, spaceJunkPrefabs.Length)];

        // Generate a random movement direction
        Vector3 moveDir = Random.onUnitSphere;

        // Spawn the space junk with a random direction
        GameObject instantiatedObject = Instantiate(prefab, spawnLocation, Quaternion.identity);
        SpaceJunk junk = instantiatedObject.GetComponent<SpaceJunk>();
        junk.speed = Random.Range(0, 3f);
        junk.moveDir = moveDir;

        return instantiatedObject;
    }

    private GameObject SpawnTargetingSpaceJunk() {
        Debug.Log("Spawning new space junk");
        
        // Choose a random junk prefab
        GameObject prefab = spaceJunkPrefabs[Random.Range(0, spaceJunkPrefabs.Length)];

        // Choose a spawn position targetingSpawnRadius units away from the player in a random direction
        Vector3 playerPos = player.transform.position;
        Vector3 spawnPos = playerPos + Random.onUnitSphere * targetingSpawnRadius;
        
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
