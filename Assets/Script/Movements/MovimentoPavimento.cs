using UnityEngine;

public class MovimentoPavimento : MonoBehaviour
{
    [SerializeField] private GameObject Wall;
    

    private void Update()
    {
        Wall.transform.position += (-transform.forward * GameManager.instance.speedWall);
    }
}
