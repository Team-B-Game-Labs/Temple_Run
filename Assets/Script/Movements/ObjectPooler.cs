using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] GameObject wallFabrics;
    [SerializeField] GameObject wall;
    List<GameObject> pool_WallPrefabs = new List<GameObject>();
    
    

    public GameObject GetWall(Vector3 position)
    {
        return SpawnItemFromPool(wallFabrics, pool_WallPrefabs, position);
    }
    
    private GameObject SpawnItemFromPool(GameObject objPrefab, List<GameObject> pool, Vector3 position)
    {
        //Debug.Log("Instanced: " + instancedCount + "\n" + "Polled" + pooledCount);

        for (int i = 0; i < pool.Count; ++i)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].transform.position = position;
                pool[i].gameObject.SetActive(true);
                
                return pool[i];
            }

        }

        GameObject InstanceObj = Instantiate(objPrefab, position, Quaternion.identity, wall.transform);
        pool.Add(InstanceObj);
        
        return InstanceObj;
    }
}
