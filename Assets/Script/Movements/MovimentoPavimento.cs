using UnityEngine;

public class MovimentoPavimento : MonoBehaviour
{
    [SerializeField] private GameObject Wall;


    private void Update()
    {
        switch (GameManager.instance.wallMovimentDirection)
        {
            case 0:
                Wall.transform.position += -transform.forward *GameManager.instance.speedWall;
                break;
            case 1:
                Wall.transform.position += transform.right * GameManager.instance.speedWall;
                break;
            case 2:
                Wall.transform.position += -transform.right * GameManager.instance.speedWall;
                break;

            case 3:
                Wall.transform.position += transform.forward * GameManager.instance.speedWall;
                break;

        }
    }
}
