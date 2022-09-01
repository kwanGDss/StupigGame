using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform target;

    private Vector3 cameraOffsetPosition;
    private float cameraYAxisRotatiton;
    private float cameraRotationSpeed;

    private void Awake()
    {
        //cameraOffsetPosition = new Vector3(0.0f, 5.0f, -9.25f);
        cameraOffsetPosition = transform.position - target.position;
        cameraRotationSpeed = 45.0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + cameraOffsetPosition;

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log($"{target.position}");
            transform.RotateAround(target.position, Vector3.up, cameraRotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log($"{target.position}");
            transform.RotateAround(target.position, Vector3.up, -cameraRotationSpeed * Time.deltaTime);
        }

        cameraOffsetPosition = transform.position - target.position;
    }
}
