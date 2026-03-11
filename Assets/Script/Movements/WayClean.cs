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
        poolTime.GetWall(new Vector3(0, 0, 10.3f));
    }
}
