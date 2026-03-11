using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();


    }

    private void Update()
    {
        float offset = Time.time * scrollSpeed;

        rend.material.mainTextureOffset = new Vector2(0, - offset);
    }
}
