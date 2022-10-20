using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform target;

    private Vector3 OffsetPosition;
    private float RotationSpeed;
    private float zoomDirection;
    private float zoomSpeed;
    private float zoomMaxValue;
    private float zoomMinValue;

    private void Awake()
    {
        //cameraOffsetPosition = new Vector3(0.0f, 5.0f, -9.25f);
        OffsetPosition = transform.position - target.position;
        RotationSpeed = 75.0f;

        zoomDirection = 0.0f;
        zoomSpeed = 500.0f;
        zoomMaxValue = 3.0f;
        zoomMinValue = 6.5f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
        ZoomCamera();
    }

    void RotateCamera()
    {
        transform.position = target.position + OffsetPosition;

        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(target.position, Vector3.up, RotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(target.position, Vector3.up, -RotationSpeed * Time.deltaTime);
        }

        OffsetPosition = transform.position - target.position;
    }

    void ZoomCamera()
    {
        zoomDirection = Input.GetAxis("Mouse ScrollWheel");

        if (transform.position.y <= zoomMaxValue && zoomDirection > 0)
        {
            return;
        }
        else if (transform.position.y >= zoomMinValue && zoomDirection < 0)
        {
            return;
        }

        transform.position += transform.forward * zoomDirection * zoomSpeed * Time.deltaTime;
        OffsetPosition = transform.position - target.position;
    }
}
