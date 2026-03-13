using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int startPoint;
    [SerializeField] Transform[] points;

    [SerializeField] GameObject player;
    Vector3 startPos;
    Quaternion startRot;
    int i;
    bool canMove;
    private float timer;

    private void Awake()
    {
        startRot = transform.rotation;
        startPos = transform.position;
    }
    private void Start()
    {

        canMove = true;
        transform.position = points[startPoint].position;
        i = startPoint;


    }

    private void OnEnable()
    {
        StartCoroutine(CameraMove());
    }

    private void Update()
    {

        if(canMove == false)
        {
            StopAllCoroutines();
        }

    }

    IEnumerator CameraMove()


    {
       

        while (true)
        {
            Debug.Log("inizio coroutine");
            timer += Time.deltaTime;
            
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);

            if (canMove && timer >= 0.3f)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[i].position) < 0.01f)
                {
                    i++;
                    yield return null;
                }

            }

            if (i == 3)
            {
                transform.SetPositionAndRotation(startPos, startRot);
                canMove = false;
                Debug.Log("fine");
            }


            yield return null;
        }
    }
}
