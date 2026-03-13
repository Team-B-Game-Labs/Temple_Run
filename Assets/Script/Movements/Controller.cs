using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject wallParent;
    [SerializeField] private float turnSpeed = 10f; // Velocità della rotazione fluida

    private CornerTrigger currentCorner;
    private Quaternion targetWorldRotation;

    void Start()
    {
        targetWorldRotation = wallParent.transform.rotation;
    }

    void Update()
    {
        // Se siamo in una curva e premiamo il tasto
        if (currentCorner != null)
        {
            if (Input.GetKeyDown(KeyCode.A) && currentCorner.isLeftTurn)
            {
                RotateWorld(-90f);
                
            }
            else if (Input.GetKeyDown(KeyCode.D) && !currentCorner.isLeftTurn)
            {
                RotateWorld(90f);
               
            }
        }

        // Ruota il mondo gradualmente verso la rotazione target
        wallParent.transform.rotation = Quaternion.Lerp(wallParent.transform.rotation, targetWorldRotation, Time.deltaTime * turnSpeed);
    }

    void RotateWorld(float angle)
    {
        // Ruota il mondo attorno alla posizione attuale del giocatore
        targetWorldRotation *= Quaternion.Euler(0, angle, 0);
        if (angle == 90)
        {
            switch (GameManager.instance.wallMovimentDirection)
            {
                case 0:
                    GameManager.instance.wallMovimentDirection = 2;
                    break;
                case 1:
                    GameManager.instance.wallMovimentDirection = 0;
                    break;
                case 2:
                    GameManager.instance.wallMovimentDirection = 3;
                    break;
                case 3:
                    GameManager.instance.wallMovimentDirection = 1;
                    break;
            }
        }
        else
        {
            switch (GameManager.instance.wallMovimentDirection)
            {
                case 0:
                    GameManager.instance.wallMovimentDirection = 1;
                    break;
                case 1:
                    GameManager.instance.wallMovimentDirection = 3;
                    break;
                case 2:
                    GameManager.instance.wallMovimentDirection = 0;
                    break;
                case 3:
                    GameManager.instance.wallMovimentDirection = 2;
                    break;
            }
        }
        currentCorner = null; // Impedisce di girare due volte nella stessa curva
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CornerTrigger corner))
        {
            currentCorner = corner;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentCorner = null;
    }
}
