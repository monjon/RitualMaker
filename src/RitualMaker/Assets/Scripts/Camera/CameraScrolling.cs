using UnityEngine;
using System.Collections;

public class CameraScrolling : MonoBehaviour
{
    #region Properties

    public float Speed = 0.05f;
    public bool Invert = false;

    #endregion

    #region Fields

    Camera camera;
    Vector3 oldPosition;
    float oldScroll;

    #endregion

    void Start()
    {
        camera = Camera.main;
        oldPosition = Input.mousePosition;
        oldScroll = Input.mouseScrollDelta.y;
    }

    Vector3 fixedPosition;

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        if (Input.GetMouseButton(1) ||
            Input.GetMouseButton(2))
        {
            fixedPosition = mousePosition - oldPosition;
        }
        else
        {
            fixedPosition = Vector3.zero;
        }

        Camera.main.orthographicSize -= Input.mouseScrollDelta.y;// *Time.deltaTime * 50;

        if (Camera.main.orthographicSize < 5)
            Camera.main.orthographicSize = 5;
        if (Camera.main.orthographicSize > 15)
            Camera.main.orthographicSize = 15;

        oldPosition = mousePosition;
    }

    void FixedUpdate()
    {
        if (Invert == false)
            camera.transform.position += fixedPosition * Speed * Time.fixedDeltaTime * Camera.main.orthographicSize;
        else
            camera.transform.position -= fixedPosition * Speed * Time.fixedDeltaTime * Camera.main.orthographicSize;
    }
}
