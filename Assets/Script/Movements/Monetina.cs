using System;
using UnityEngine;

public class Monetina : MonoBehaviour, ICollider
{
    [SerializeField] float rotationSpeed;
    [SerializeField] AudioClip coinSFX;
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

    public void Collided()
    {
        SoundFXManager.instance.PlaySoundFXClip(coinSFX, transform, 1f);
        GameManager.instance.currentCoins++;
        Destroy(gameObject);
    }
}
