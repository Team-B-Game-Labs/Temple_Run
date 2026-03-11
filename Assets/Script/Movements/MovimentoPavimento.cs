using UnityEngine;

public class MovimentoPavimento : MonoBehaviour
{
    [SerializeField] private GameObject Wall;
    

    private void Update()
    {
        Wall.transform.position += (new Vector3(0, 0, -1) * GameManager.instance.speedWall);
    }
}
