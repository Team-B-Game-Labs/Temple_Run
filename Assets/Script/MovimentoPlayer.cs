using UnityEngine;
using UnityEngine.UIElements;

public class MovimentoPlayer : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 targetPos;
    [SerializeField] private float duration = 0.2f;
    private float timer;
    private int currentLocation = 1; // 1: Centro, 2: Sinistra, 3: Destra
    private bool isMoving = false;

    private void Start()
    {
        // Inizializza le posizioni per evitare salti bruschi all'avvio
        startPos = transform.position;
        targetPos = transform.position;
    }

    private void Update()
    {
        // 1. INPUT: Solo se non ci stiamo già muovendo
        if (!isMoving)
        {
            switch (currentLocation)
            {
                case 1: // CENTRO
                    if (Input.GetKeyDown(KeyCode.A)) SetMove(2, -1f); // Vai a Sinistra
                    else if (Input.GetKeyDown(KeyCode.D)) SetMove(3, 1f); // Vai a Destra
                    break;

                case 2: // SINISTRA
                    if (Input.GetKeyDown(KeyCode.D)) SetMove(1, 0f); // Torna al Centro
                    break;

                case 3: // DESTRA
                    if (Input.GetKeyDown(KeyCode.A)) SetMove(1, 0f); // Torna al Centro
                    break;
            }
        }

        // 2. MOVIMENTO: Viene eseguito ogni frame se isMoving è true
        if (isMoving)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            transform.position = Vector3.Lerp(startPos, targetPos, t);

            if (t >= 1f)
            {
                transform.position = targetPos; // Snapping finale
                isMoving = false;
                timer = 0f;
            }
        }
    }

    // Funzione di supporto per pulire lo switch
    private void SetMove(int newLocation, float targetX)
    {
        startPos = transform.position;
        targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
        currentLocation = newLocation;
        timer = 0f;
        isMoving = true;
    }
}
