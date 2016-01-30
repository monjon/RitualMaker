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

    #endregion

    void Start()
    {
        camera = Camera.main;
        oldPosition = Input.mousePosition;
    }

    Vector3 fixedPosition;

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {            
            fixedPosition = mousePosition - oldPosition;
        }
        else
        {
            fixedPosition = Vector3.zero;
        }
        oldPosition = mousePosition;
    }

    void FixedUpdate()
    {
        if (Invert == false)
            camera.transform.position += fixedPosition * Speed * Time.fixedDeltaTime;
        else
            camera.transform.position -= fixedPosition * Speed * Time.fixedDeltaTime;
    }   
}
