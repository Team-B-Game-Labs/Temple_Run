using System;
using UnityEngine;

public class Monetina : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    Vector3 startPos;
    private void OnEnable()
    {
        startPos = transform.localPosition;
        float randomIndex = UnityEngine.Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0, randomIndex, 0);
    }

    private void Update()
    {
        transform.Rotate( new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime);
    }
}
