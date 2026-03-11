using UnityEngine;

public class WayClean : MonoBehaviour
{

    [SerializeField] ObjectPooler poolTime;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        poolTime.GetRandomWall(new Vector3(0, 0, 51.47f));
    }
}
