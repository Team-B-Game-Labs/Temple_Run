using UnityEngine;

public class Roccia : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);
    }
}
