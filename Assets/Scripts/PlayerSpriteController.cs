using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    [SerializeField] float groundCheckDistance;
    [SerializeField] float backfliplightholdtime;
    //[SerializeField] float highpos;

    [SerializeField] Sprite idle;
    [SerializeField] Sprite airidle;
    //[SerializeField] Sprite airidlehigh;
    [SerializeField] Sprite tucked;
    [SerializeField] Sprite leanForward;
    [SerializeField] Sprite leanBackward;
    [SerializeField] Sprite backflip;
    [SerializeField] Sprite backfliplight;
    [SerializeField] Sprite frontflip;

    [SerializeField] CircleCollider2D particleCollider;
    [SerializeField] CircleCollider2D crashCollider;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Controls controller;
    [SerializeField] LayerMask groundLayer;

    private float backflipstarttime;

    void Start()
    {
        resetColliders();
    }

    void Update()
    {
        if (controller.isRotatingLeft())
        {
            StartCoroutine(WaitForAirtime());
        }

        if (controller.isTucking())
        {
            particleCollider.offset = new Vector2(0.23f, 0.19f);
            crashCollider.offset = new Vector2(0.23f, 0.31f);
            spriteRenderer.sprite = tucked;
        }
        else if (!IsGrounded())
        {

            if (controller.isRotatingRight())
            {
                particleCollider.offset = new Vector2(0.53f, 0.76f);
                crashCollider.offset = new Vector2(0.68f, 0.76f);
                spriteRenderer.sprite = frontflip;
            }
            else if (controller.isRotatingLeft())
            {
                //Debug.Log(backflipstarttime);
                if (backflipstarttime + backfliplightholdtime >= Time.time)
                {
                    particleCollider.offset = new Vector2(-0.92f, 0.71f);
                    crashCollider.offset = new Vector2(-0.92f, 0.86f);
                    spriteRenderer.sprite = backfliplight;
                }
                else
                {
                    particleCollider.offset = new Vector2(-1.43f, 0.24f);
                    crashCollider.offset = new Vector2(-1.57f, 0.24f);
                    spriteRenderer.sprite = backflip;
                }
            }
            else
            {
                //if ((transform.position.y - FindObjectOfType<GroundTargetMovement>().targetY) > highpos)
                //{
                resetColliders();
                spriteRenderer.sprite = airidle;
                //}
                //else
                //{
                //    resetColliders();
                //    spriteRenderer.sprite = airidlehigh;
                //}
            }
        }
        else
        {
            if (controller.isRotatingRight())
            {
                resetColliders();
                spriteRenderer.sprite = leanForward;
            }
            else if (controller.isRotatingLeft())
            {
                resetColliders();
                spriteRenderer.sprite = leanBackward;
            }
            else
            {
                resetColliders();
                spriteRenderer.sprite = idle;
            }
        }
    }

    IEnumerator WaitForAirtime()
    {
        
        while (IsGrounded() || controller.isTucking())
        {
            backflipstarttime = 0f;
            yield return null;
        }
        backflipstarttime = Time.time;

        //Debug.Log(backflipstarttime + "," + backfliplightholdtime + "," + Time.time);
    }
    private void resetColliders()
    {
        particleCollider.offset = new Vector2(0.06f, 0.96f);
        crashCollider.offset = new Vector2(0.06f, 1.09f);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down);

        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
