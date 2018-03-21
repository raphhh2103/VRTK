using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DuckSpawner : MonoBehaviour
{
    public Camera playerCamera;
    public int max = 100;
    public int tospawn = 10;
    public int total = 0;
    public int respawnDelay;
    public GameObject zombiePrefab;
    private int offsetTime = 0;
    private int currentSpawn = 0;
    private int lastRespawn;
    private GameObject scout;
    // Use this for initialization
    void Start()
    {
        EnemyManager.AddEnemyAction += AddEnemy;
        EnemyManager.RemoveEnemyAction += RemoveEnemy;
        EnemyManager.ClearEnemyAction += ClearEnemy;
        scout = GameObject.CreatePrimitive(PrimitiveType.Cube);
        scout.transform.localScale = new Vector3(0.000001f, 0.000001f, 0.000001f);
    }

    // Update is called once per frame
    void Update()
    {
        offsetTime = (int)Time.time;

        if (offsetTime / respawnDelay > lastRespawn && offsetTime % respawnDelay == 0 && total < max)
        {
            lastRespawn = offsetTime / respawnDelay;
            while (currentSpawn < tospawn)
            {
                int layerMask = (1 << NavMesh.GetAreaFromName("Default"));
                var position = new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-30f, 30f));
                NavMeshHit hit;
                NavMesh.SamplePosition(position, out hit, 50, layerMask);
                if (!SpawnCheck(hit.position))
                {
                    Instantiate(zombiePrefab, hit.position, Quaternion.identity);
                    currentSpawn++;
                }
            }
            currentSpawn = 0;
        }
    }

    public void AddEnemy()
    {
        total++;
    }
    public void RemoveEnemy()
    {
        total--;
    }
    public void ClearEnemy()
    {
        total = 0;
    }
    private void OnDisable()
    {
        EnemyManager.AddEnemyAction -= AddEnemy;
        EnemyManager.RemoveEnemyAction -= RemoveEnemy;
        EnemyManager.ClearEnemyAction -= ClearEnemy;
    }
    private bool isInFrustrum(Bounds target, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, target);
    }

    private bool SpawnCheck(Vector3 position)
    {
        bool result;
        scout.SetActive(true);
        scout.transform.position = position;
        result = isInFrustrum(scout.GetComponent<Renderer>().bounds, playerCamera);
        scout.SetActive(true);
        return result;
    }
}
