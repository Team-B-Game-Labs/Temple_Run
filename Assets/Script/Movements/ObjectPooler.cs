using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Configurazione Prefabs")]
    // Trascina qui tutti i tuoi diversi pezzi (Centro vuoto, Ostacolo DX, Ostacolo SX, Salto, ecc.)
    [SerializeField] private List<GameObject> wallPrefabs = new List<GameObject>();

    [SerializeField] private GameObject wallParent; // Il contenitore nella gerarchia

    // Dizionario per gestire pool separate per ogni tipo di pezzo
    private Dictionary<string, List<GameObject>> pooledObjects = new Dictionary<string, List<GameObject>>();

    public GameObject GetRandomWall(Vector3 position)
    {
        if (wallPrefabs.Count == 0) return null;

        // 1. Sceglie un prefab a caso dalla lista degli ostacoli disponibili
        int randomIndex = Random.Range(0, wallPrefabs.Count);
        GameObject selectedPrefab = wallPrefabs[randomIndex];

        // 2. Chiama la funzione di spawn specifica per quel prefab
        return SpawnItemFromPool(selectedPrefab, position);
    }

    private GameObject SpawnItemFromPool(GameObject objPrefab, Vector3 position)
    {
        string key = objPrefab.name;

        // Se non esiste ancora una lista per questo specifico prefab, la crea
        if (!pooledObjects.ContainsKey(key))
        {
            pooledObjects.Add(key, new List<GameObject>());
        }

        List<GameObject> pool = pooledObjects[key];

        // Cerca un oggetto disattivato nella pool specifica
        for (int i = 0; i < pool.Count; ++i)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].transform.position = position;
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        // Se non trova oggetti liberi, ne istanzia uno nuovo di quel tipo
        GameObject instanceObj = Instantiate(objPrefab, position, Quaternion.identity, wallParent.transform);
        pool.Add(instanceObj);

        return instanceObj;
    }
}
