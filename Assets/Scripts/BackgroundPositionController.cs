using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPositionController : MonoBehaviour
{
    private Camera mainCamera;
    private RectTransform backgroundRectTransform;

    void Start()
    {

        mainCamera = Camera.main;
        backgroundRectTransform = GetComponent<RectTransform>();

        UpdateBackgroundPosition();
    }

    void Update()
    {
        UpdateBackgroundPosition();
    }

    void UpdateBackgroundPosition()
    {


        Vector3 cameraBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 backgroundPosition = new Vector3(cameraBottomLeft.x, cameraBottomLeft.y + 20f, backgroundRectTransform.position.z);

        backgroundRectTransform.position = backgroundPosition;
    }
}
