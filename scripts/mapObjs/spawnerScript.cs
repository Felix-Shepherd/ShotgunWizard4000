using UnityEngine;
using System.Collections.Generic;

public class SpawnerScript : MonoBehaviour
{
    [System.Serializable]
    public class Mob
    {
        public GameObject prefab;
        public int weight = 1;
    }

    public List<Mob> mobsZone1 = new List<Mob>();
    // public List<Mob> mobsZone2 = new List<Mob>();                HAIRZS>D. 
    public int spawncount = 3;
    private int currentcount = 0;

    public bool isZone1;
    public bool isZone2;

    private GameObject currentMob;

    void Update()
    {
        if (currentMob == null && currentcount < spawncount)
        {
            if (isZone1 && mobsZone1.Count > 0)
            {
                Mob chosen = GetWeightedMob(mobsZone1);
                SpawnMob(chosen.prefab);
            }
            // else if (isZone2)
            // {
            //     Mob chosen = GetWeightedMob(mobsZone2);
            //     SpawnMob(chosen.prefab);
            // }

            currentcount++;
            if (currentcount >= spawncount)
            {
                Destroy(gameObject);
            }
        }
    }

    void SpawnMob(GameObject mobToSpawn)
    {
        currentMob = Instantiate(
            mobToSpawn,
            transform.position,
            transform.rotation
        );
    }

    Mob GetWeightedMob(List<Mob> mobs)
    {
        int totalWeight = 0;
        foreach (Mob m in mobs)
            totalWeight += m.weight;

        int roll = Random.Range(0, totalWeight);

        foreach (Mob m in mobs)
        {
            roll -= m.weight;
            if (roll < 0)
                return m;
        }

        return mobs[0];
    }
}