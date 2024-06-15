using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunpSpawner2 : MonoBehaviour
{
    [System.Serializable]
    public struct ObstacleType {
        public GameObject prefab;
        public int amount;
    }

    [Header("Obstacles")]
    public ObstacleType[] obstacleTypes;
    public int randomSeed;
    public float obstacleRadius;
    public float obstacleCountModifier = 1.0f;


    [Header("Projectiles")]
    public GameObject[] projectilePrefabs;
    public float projectileInterval;

    public float projectileMinSpawnDistance;
    public float projectileMaxSpawnDistance;
    public float projectileMinSpeed;
    public float projectileMaxSpeed;
    [Header("References")]
    public Rigidbody player;

    void Start() {
        Random.InitState(randomSeed);

        foreach (var obstacle in obstacleTypes)
        {
            for (int i = 0; i < obstacle.amount * obstacleCountModifier; i++)
            {
                var instance = Instantiate(obstacle.prefab);
                instance.transform.position = Random.insideUnitSphere * obstacleRadius;
                instance.transform.rotation = Random.rotation;
            }
        }

        StartCoroutine(SpawnLoop());
    }

    public static Vector3 CalculateInterceptPoint(Vector3 targetPosition, Vector3 targetVelocity, Vector3 projectilePosition, float projectileSpeed)
    {
        Vector3 targetToProjectile = targetPosition - projectilePosition;
        float distanceToTarget = targetToProjectile.magnitude;

        // Calculate coefficients of the quadratic equation
        float a = targetVelocity.sqrMagnitude - projectileSpeed * projectileSpeed;
        float b = 2 * Vector3.Dot(targetVelocity, targetToProjectile);
        float c = targetToProjectile.sqrMagnitude;

        // Calculate the discriminant
        float discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
        {
            // No real solution, the projectile cannot intercept the target
            return Vector3.zero;
        }

        // Calculate the two possible solutions for time
        float sqrtDiscriminant = Mathf.Sqrt(discriminant);
        float t1 = (-b - sqrtDiscriminant) / (2 * a);
        float t2 = (-b + sqrtDiscriminant) / (2 * a);

        // Select the smallest positive time as the intercept time
        float interceptTime = Mathf.Min(t1, t2);
        if (interceptTime < 0)
        {
            interceptTime = Mathf.Max(t1, t2);
        }

        if (interceptTime < 0)
        {
            // Both times are negative, the projectile cannot intercept the target
            return Vector3.zero;
        }

        // Calculate the intercept point
        Vector3 interceptPoint = targetPosition + targetVelocity * interceptTime;
        return interceptPoint;
    }

    IEnumerator SpawnLoop() {
        while (true) {
            yield return new WaitForSeconds(projectileInterval);

            int index = Random.Range(0, projectilePrefabs.Length - 1);
            var instance = Instantiate(projectilePrefabs[index]);

            var position = player.transform.position + Random.insideUnitSphere.normalized * Random.Range(projectileMinSpawnDistance, projectileMaxSpawnDistance);
            float speed = Random.Range(projectileMinSpeed, projectileMaxSpeed);

            var interceptPos = CalculateInterceptPoint(player.transform.position, player.velocity, position, speed);
            if (interceptPos != Vector3.zero) {
                instance.transform.position = position;
                instance.GetComponent<Rigidbody>().velocity = (interceptPos - position).normalized * speed;
            } else {
                // Handle case where intercept point is not valid
                Destroy(instance);
            }
        }
    }
}
