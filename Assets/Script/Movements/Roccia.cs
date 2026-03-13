using System.Collections;
using UnityEngine;

public class Roccia : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    MeshRenderer mr;
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);

        if(GameManager.instance.rootHit == true)
        {
            StartCoroutine(Deactivate());
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(5f);
        GameManager.instance.rootHit = false;
        mr.enabled = false;

        yield return null;


    }
}
