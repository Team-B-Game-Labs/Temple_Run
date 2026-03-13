using UnityEngine;

public class RuotaLuce : MonoBehaviour
{
    [SerializeField] float speed;
    float time;

    private void Update()
    { 
        time += speed * Time.deltaTime;
        float rotation = time;
        transform.rotation = Quaternion.Euler(0, 0, rotation) ;
    }
}

