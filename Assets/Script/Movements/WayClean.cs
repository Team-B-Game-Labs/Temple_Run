using UnityEngine;

public class WayClean : MonoBehaviour
{
    [SerializeField] ObjectPooler poolTime;

    private void OnTriggerEnter(Collider other)
    {
        // Disattiva il blocco che è appena passato dietro il giocatore
        other.gameObject.SetActive(false);

        // Chiede al pooler di crearne uno nuovo in coda
        poolTime.GetNextWall();
    }
}
