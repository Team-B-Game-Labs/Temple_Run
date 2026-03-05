using UnityEngine;

public class MovimentoPavimento : MonoBehaviour
{
    [SerializeField] private GameObject Wall;
    [SerializeField] public float speed; //Deve stare nel GameManager

    private void Update()
    {
        Wall.transform.position += (new Vector3(0, 0, -1) * speed);
    }
}
