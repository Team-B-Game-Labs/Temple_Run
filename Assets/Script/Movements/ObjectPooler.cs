using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Configurazione Prefabs")]
    [SerializeField] private GameObject straightRoadPrefab;
    [SerializeField] private List<GameObject> variationsPrefabs = new List<GameObject>(); // Metti qui ostacoli e CURVE
    [SerializeField] private GameObject wallParent;

    [Header("Parametri Strada")]
    [SerializeField] private float stepZ = 10.3f; // Lunghezza del blocco

    // Variabili di stato per la "costruzione" della strada
    private Vector3 nextRelativePosition = new Vector3(0, 0, 40.9f);
    private Quaternion currentRotation = Quaternion.identity;
    private Vector3 currentForward = Vector3.forward;

    private int spawnCount = 0;
    private int variationIndex = 0;
    private List<GameObject> shuffledVariations = new List<GameObject>();
    private Dictionary<string, List<GameObject>> pooledObjects = new Dictionary<string, List<GameObject>>();

    private void Start() => ShuffleVariations();

    private void ShuffleVariations()
    {
        shuffledVariations = new List<GameObject>(variationsPrefabs);
        for (int i = 0; i < shuffledVariations.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledVariations.Count);
            GameObject temp = shuffledVariations[i];
            shuffledVariations[i] = shuffledVariations[randomIndex];
            shuffledVariations[randomIndex] = temp;
        }
    }

    public void GetNextWall()
    {
        GameObject selectedPrefab;

        // 1. Selezione Prefab (Primi 5 dritti, poi varianti/curve)
        if (spawnCount < 5)
        {
            selectedPrefab = straightRoadPrefab;
        }
        else
        {
            selectedPrefab = shuffledVariations[variationIndex];
            variationIndex = (variationIndex + 1) % shuffledVariations.Count;
            if (variationIndex == 0) ShuffleVariations();
        }

        // 2. Spawn del blocco come FIGLIO del WallParent
        // Usiamo transform.TransformPoint per calcolare la posizione nel mondo rispetto al padre
        Vector3 worldSpawnPos = wallParent.transform.TransformPoint(nextRelativePosition);
        Quaternion worldRotation = wallParent.transform.rotation * currentRotation;

        SpawnItemFromPool(selectedPrefab, worldSpawnPos, worldRotation);

        // 3. LOGICA CURVA: Controlliamo se il pezzo era una curva per cambiare i prossimi
        // Assicurati che i tuoi prefab curva abbiano queste scritte nel nome!
        if (selectedPrefab.name.Contains("CurvaDX"))
        {
            currentRotation *= Quaternion.Euler(0, 90, 0);
        }
        else if (selectedPrefab.name.Contains("CurvaSX"))
        {
            currentRotation *= Quaternion.Euler(0, -90, 0);
        }

        // 4. Aggiorna la direzione e la posizione relativa per il PROSSIMO blocco
        currentForward = currentRotation * Vector3.forward;
        nextRelativePosition += currentForward * stepZ;

        spawnCount++;
    }

    private void SpawnItemFromPool(GameObject objPrefab, Vector3 pos, Quaternion rot)
    {
        string key = objPrefab.name;
        if (!pooledObjects.ContainsKey(key)) pooledObjects.Add(key, new List<GameObject>());

        foreach (GameObject g in pooledObjects[key])
        {
            if (!g.activeInHierarchy)
            {
                g.transform.position = pos;
                g.transform.rotation = rot;
                g.SetActive(true);
                return;
            }
        }

        GameObject newObj = Instantiate(objPrefab, pos, rot, wallParent.transform);
        newObj.name = key;
        pooledObjects[key].Add(newObj);
    }
}
