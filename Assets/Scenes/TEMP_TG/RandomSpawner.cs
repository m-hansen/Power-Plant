using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Settlement;
    [SerializeField]
    private float NumberToSpawn = 100;
    [SerializeField]
    private float range;
    private Plane plane;
    void Awake()
    {
        Spawn();
        
    }
    
    void Spawn()
    {
        for (int i = 0; i < NumberToSpawn; i++)
        {
            Vector3 spawnCenter = Vector3.zero;
            Vector3 randomPointOnPlane = plane.ClosestPointOnPlane(spawnCenter + range * Random.insideUnitSphere);
            Instantiate(Settlement, new Vector3(randomPointOnPlane.x, randomPointOnPlane.y, 0), Quaternion.identity);

        }

    }
}
