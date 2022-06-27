using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Player : MonoBehaviour
{
    [SerializeField] UIController uiController;
    public Animator componentAnimator;
    public AudioSource componentAudioSource;
    [SerializeField] AudioClip[] obstacleFX;
    public AudioClip[] playerFX;
    [SerializeField] PostProcessVolume postProcessVolume;
    Vignette vignette;
    Ground ground;

    [SerializeField] bool s_HitObstacle = true;
    public bool s_AnimationDown = false;
    public bool isKnockOut = false;
    [SerializeField] bool isPreKnockOut = false;
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public bool isNoJump = false;
    public Vector2 velocity;
    public float gravity = -200;
    [SerializeField] float maxXVelocity = 100;
    [SerializeField] float maxAcceleration = 10;
    [SerializeField] float acceleration = 10;
    public float distance = 0;
    public float jumpVelocity = 65;
    public float groundHeight;
    public float maxHoldJumpTime = 0.4f;
    public float maxMaxHoldJumpTime = 0.4f;
    public float holdJumpTimer;
    [SerializeField] int knockOutLuck = 6;

    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] LayerMask obstacleLayerMask;


    void Awake()
    {
        ground = GameObject.Find("Ground").GetComponent<Ground>();
        postProcessVolume.profile.TryGetSettings(out vignette);
    }

    private void Start()
    {
        Time.fixedDeltaTime = 0.2f * 0.01f;
    }

    void Update()
    {
        Vector2 pos = transform.position;

        if (isGrounded && !s_AnimationDown && !isNoJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
                componentAnimator.SetTrigger("jump");
                componentAudioSource.PlayOneShot(playerFX[0]);
                s_AnimationDown = true;
            }
            
        }

        if (isGrounded && s_AnimationDown)
        {
            componentAnimator.SetTrigger("down");
            componentAudioSource.PlayOneShot(playerFX[1]);
            s_AnimationDown = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }


    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (isKnockOut || isPreKnockOut)
        {
            velocity.x = 0;
            return;
        }

        if (pos.y < -20)
        {
            isKnockOut = true;
        }

        if (distance > 12000)
        {
            maxXVelocity = 140;
        }

        if (!isGrounded)
        {
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            pos.y += velocity.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            Vector2 rayOrigin = new Vector2(pos.x + 2f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundLayerMask);
            if (hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    if (pos.y >= ground.groundHeight)
                    {
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        velocity.y = 0;
                        isGrounded = true;
                    }
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

            Vector2 wallOrigin = new Vector2(pos.x, pos.y);
            Vector2 wallDirection = Vector2.right;
            RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, wallDirection, velocity.x * Time.fixedDeltaTime, groundLayerMask);
            if (wallHit.collider != null)
            {
                Ground ground = wallHit.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    if (pos.y < ground.groundHeight)
                    {
                        velocity.x = 0;
                    }
                }
            }
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);
            maxHoldJumpTime = maxMaxHoldJumpTime * velocityRatio;

            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 2f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);

            if (hit2D.collider == null)
            {
                isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }

        Vector2 obstOrigin = new Vector2(pos.x, pos.y);
        RaycastHit2D obstHitX = Physics2D.Raycast(obstOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime, obstacleLayerMask);
        if (obstHitX.collider != null)
        {
            Obstacle obstacle = obstHitX.collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                HitObstacle(obstacle);
            }
        }

        RaycastHit2D obstHitY = Physics2D.Raycast(obstOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime, obstacleLayerMask);
        if (obstHitY.collider != null)
        {
            Obstacle obstacle = obstHitY.collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                HitObstacle(obstacle);
            }
        }
        transform.position = pos;
    }

    async void HitObstacle(Obstacle obstacle)
    {
        obstacle.componentAnimator.SetTrigger("obstacleDestroy");
        if (s_HitObstacle)
        {
            s_HitObstacle = false;
            velocity.x *= 0.8f;
            componentAudioSource.PlayOneShot(obstacleFX[Random.Range(0, 4)]);
            knockOutLuck = Random.Range(1, 6);
            StartCoroutine("VignetteValueUp");
            if (distance > 10000 && knockOutLuck < 2)
            {
                isNoJump = true;
            }
            await Task.Delay(1600);
            StartCoroutine("VignetteValueDown");
            StartCoroutine("ShockKnockOut");
        }
    }

    IEnumerator VignetteValueUp()
    {
        for (int i = 0; i < 44; i++)
        {
            if (i < 16)
            {
                Time.timeScale -= 0.05f;
            } 
            vignette.intensity.value += 0.015f;
            vignette.smoothness.value += 0.012f;
            yield return new WaitForSecondsRealtime(0.001f);
        }
    }

    IEnumerator VignetteValueDown()
    {
        for (int i = 0; i < 44; i++)
        {
            if (i < 16)
            {
                Time.timeScale += 0.05f;
            }
            vignette.intensity.value -= 0.015f;
            vignette.smoothness.value -= 0.012f;
            yield return new WaitForSecondsRealtime(0.001f);
        }
        s_HitObstacle = true;
    }

    IEnumerator ShockKnockOut()
    {
        for (int i = 0; i < 15; i++)
        {
            if (isNoJump && isGrounded)
            {
                isPreKnockOut = true;
                componentAnimator.SetTrigger("knockout");
                componentAudioSource.PlayOneShot(playerFX[2]);
                break;
            }
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
}