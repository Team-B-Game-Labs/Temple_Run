using System.Collections; // Necessario per le Coroutine
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovimentoPlayer : MonoBehaviour
{
    [Header("Movimento Laterale")]
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float laneWidth = 2.0f;

    [Header("Salto e Slide")]
    [SerializeField] private float jumpForce = 7.0f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float slideDuration = 0.7f; // Quanto dura la scivolata


    [Header("SFX")]
    [SerializeField] AudioClip[] salto;
    [SerializeField] AudioClip atterraggio;
    [SerializeField] AudioClip caduta;
    [SerializeField] AudioClip colpo;

    Animator anim;


    private Rigidbody rb;
    private CapsuleCollider col; // Riferimento al collider
    private Vector3 startPos;
    private Vector3 targetPos;
    private float moveTimer;

    private int currentLocation = 1;
    private bool isMoving = false;
    private bool isGrounded;
    private bool isSliding = false;

    // Variabili per ripristinare il collider originale
    private float originalHeight;
    private Vector3 originalCenter;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        targetPos = transform.position;

        if (col != null)
        {
            originalHeight = col.height;
            originalCenter = col.center;
        }
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);


        // Input Salto
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded && !isSliding)
        {
            Salto();
        }

        // Input Slide
        if (Input.GetKeyDown(KeyCode.S) && isGrounded && !isSliding)
        {
            StartCoroutine(SlideRoutine());
        }

        if (GameManager.instance != null)
            GameManager.instance.IsJumping = !isGrounded;

        if (!isMoving)
        {
            HandleLateralInput();
        }
    }

    private void FixedUpdate()
    {
        UpdateLateralMovement();
    }

    private IEnumerator SlideRoutine()
    {
        isSliding = true;
        // Comunica al GameManager che il player sta slidando
        if (GameManager.instance != null)
            GameManager.instance.IsSliding = true;

           // anim.SetTrigger("Slide");
        // 1. Ruota il personaggio (90 gradi sull'asse X)
        transform.rotation = Quaternion.Euler(-90, 0, 0);

        // 3. Attende la durata dello slide
        yield return new WaitForSeconds(slideDuration);

        // 4. Ripristina rotazione, collider e stato GameManager
        transform.rotation = Quaternion.identity;
        if (col != null)
        {
            col.height = originalHeight;
            col.center = originalCenter;
        }

        if (GameManager.instance != null)
            GameManager.instance.IsSliding = false;

        isSliding = false;
    }


    private void HandleLateralInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentLocation == 1) SetMove(2, -laneWidth);
            else if (currentLocation == 3) SetMove(1, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentLocation == 1) SetMove(3, laneWidth);
            else if (currentLocation == 2) SetMove(1, 0f);
        }
    }

    private void SetMove(int newLocation, float targetX)
    {
        switch (GameManager.instance.wallMovimentDirection)
        {
            case 0:
                startPos = transform.position;
                targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
                currentLocation = newLocation;
                moveTimer = 0f;
                isMoving = true;
                break;
            case 1:
                startPos = transform.position;
                targetPos = new Vector3(transform.position.z, transform.position.y, targetX);
                currentLocation = newLocation;
                moveTimer = 0f;
                isMoving = true;
                break;
            case 2:
                startPos = transform.position;
                targetPos = new Vector3(transform.position.z, transform.position.y, -targetX);
                currentLocation = newLocation;
                moveTimer = 0f;
                isMoving = true;
                break;
            case 3:
                startPos = transform.position;
                targetPos = new Vector3(-targetX, transform.position.y, transform.position.z);
                currentLocation = newLocation;
                moveTimer = 0f;
                isMoving = true;
                break;
        }
        
    }

    private void UpdateLateralMovement()
    {
        if (isMoving)
        {
            if (GameManager.instance.wallMovimentDirection == 0 || GameManager.instance.wallMovimentDirection == 3)
            {

                moveTimer += Time.fixedDeltaTime;
                float t = Mathf.Clamp01(moveTimer / duration);
                float newX = Mathf.Lerp(startPos.x, targetPos.x, Mathf.SmoothStep(0, 1, t));

                // Manteniamo la Y e Z attuali del Rigidbody per non interferire con gravità e salto
                rb.MovePosition(new Vector3(newX, rb.position.y, rb.position.z));

                if (t >= 1f) isMoving = false;
            }
            else if (GameManager.instance.wallMovimentDirection == 1 || GameManager.instance.wallMovimentDirection == 2)
            {

                moveTimer += Time.fixedDeltaTime;
                float s = Mathf.Clamp01(moveTimer / duration);
                float newZ = Mathf.Lerp(startPos.z, targetPos.z, Mathf.SmoothStep(0, 1, s));

                // Manteniamo la Y e Z attuali del Rigidbody per non interferire con gravità e salto
                rb.MovePosition(new Vector3(rb.position.x, rb.position.y, newZ));

                if (s >= 1f) isMoving = false;
            }
        }
    }

    private void Salto()
    {
        int rand = Random.Range(0, salto.Length);
        //SoundFXManager.instance.PlaySoundFXClip(salto[rand], transform, 1f);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        if (isGrounded) SoundFXManager.instance.PlaySoundFXClip(atterraggio, transform, 1f);
        anim.SetTrigger("Jump");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollider coin))
        {
            coin.Collided();
        }

        //tutte le condizioni di morte
        if (other.gameObject.layer == 9 && isSliding == false)
        {
            SoundFXManager.instance.PlaySoundFXClip(caduta, transform, 1f);
            UIManager.instance.DeathByTree();
        }

        if (other.gameObject.layer == 7 && other.gameObject.layer == 10)
        {
            SoundFXManager.instance.PlaySoundFXClip(caduta, transform, 1f);
            UIManager.instance.DeathByTree();
        }

        if (other.gameObject.layer == 6)
        {
            if(GameManager.instance.rootHit == true)
            {
             UIManager.instance.DeathByRoot();

            }
            GameManager.instance.rootHit = true; 
            SoundFXManager.instance.PlaySoundFXClip(colpo, transform, 1f);

        }

        if (other.gameObject.layer == 4)
        {
            SoundFXManager.instance.PlaySoundFXClip(caduta, transform, 1f);
            UIManager.instance.DeathByWater();
        }
    }
}