using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float torqueAmount;
    [SerializeField] GameObject floor;
    [SerializeField] float baseGravity = 2f;
    [SerializeField] float leanBackwardBuffer;
    [SerializeField] Controls controller;

    Rigidbody2D rb2d;
    SurfaceEffector2D supportSpeed;
  
    bool canMove = true;

    public float playerXPosition;
    public Vector2 closestColliderPosition;
    public LayerMask layerMask;



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        supportSpeed = floor.GetComponent<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerXPosition = transform.position.x;
        if (canMove)
        {
            RotatePlayer();
            AddSupportSpeed();
            TuckPlayer();
            UpdateClosestColliderPosition();
        } 
    }

    private void UpdateClosestColliderPosition()
    {
        Vector2 raycastOrigin = new Vector2(transform.position.x, transform.position.y);
        Vector2 raycastDirection = Vector2.down;
        Debug.DrawRay(raycastOrigin, raycastDirection);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, Mathf.Infinity, ~layerMask);

        if (hit.collider != null)
        {
            closestColliderPosition = hit.point;
        }
    }

    public void DisableConrols()
    {
        canMove = false;
    }

    private void TuckPlayer()
    {
        if (controller.isTucking())
        {
            rb2d.gravityScale = baseGravity * 1.5f;
        }
        else
        {
            rb2d.gravityScale = baseGravity;
        }
    }

    private void AddSupportSpeed()
    {
        if (rb2d.velocity.x < 5.0f)
        {
            supportSpeed.enabled = true;
        }
        else
        {
            supportSpeed.enabled = false;
        }
    }

    private void RotatePlayer()
    {
        //if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        //{
        //    rb2d.AddTorque(0f);
        //}
        if (controller.isRotatingLeft())
        {
            rb2d.AddTorque((torqueAmount - leanBackwardBuffer) * Time.deltaTime);
        }
        else if (controller.isRotatingRight())
        {
            rb2d.AddTorque((-torqueAmount) * Time.deltaTime);
        }
    }
}
