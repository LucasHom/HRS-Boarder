using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class GroundTargetMovement : MonoBehaviour
{
    [SerializeField] SpriteShapeRenderer spriteShapeRenderer;
    [SerializeField] PlayerController playerController;
    float playerX;
    float nearestY;

    //public float targetY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //targetY = gameObject.transform.position.y;

        playerX = playerController.playerXPosition;
        nearestY = playerController.closestColliderPosition.y;

        transform.position = new Vector2(playerX + 2f, nearestY - 4f);



    }


}
