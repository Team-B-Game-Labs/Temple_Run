using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    [Header("Movimento Laterale")]
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float laneWidth = 2.0f;

    [Header("Salto")]
    [SerializeField] private float jumpDuration = 0.6f; 
    [SerializeField] private float jumpHeight = 2.0f;   

    private Vector3 startPos;
    private Vector3 targetPos;
    private float moveTimer;
    private float jumpTimer;

    private int currentLocation = 1;
    private bool isMoving = false;
    private bool isJumping = false;
    private float groundY; 

    private void Start()
    {
        groundY = transform.position.y;
        startPos = transform.position;
        targetPos = transform.position;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && !isJumping)
        {
            isJumping = true;
            jumpTimer = 0f;
        }

        if (!isMoving)
        {
            HandleLateralInput();
        }

        UpdateMovement();
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

    private void UpdateMovement()
    {
        float currentX = transform.position.x;
        if (isMoving)
        {
            moveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(moveTimer / duration);
            currentX = Mathf.Lerp(startPos.x, targetPos.x, Mathf.SmoothStep(0, 1, t));

            if (t >= 1f) isMoving = false;
        }

        float currentY = groundY;
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float tJump = jumpTimer / jumpDuration;

            if (tJump >= 1f)
            {
                isJumping = false;
                currentY = groundY;
            }
            else
            {
                currentY = groundY + (4 * jumpHeight * tJump * (1 - tJump));
            }
        }

        transform.position = new Vector3(currentX, currentY, transform.position.z);
    }

    private void SetMove(int newLocation, float targetX)
    {
        startPos = transform.position;
        targetPos = new Vector3(targetX, groundY, transform.position.z);
        currentLocation = newLocation;
        moveTimer = 0f;
        isMoving = true;
    }
}
