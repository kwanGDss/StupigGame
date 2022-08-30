using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform target;
    private Vector3 cameraOffsetPosition;
    private Vector3 cameraOffsetRotatiton;

    private void Awake()
    {
        cameraOffsetPosition = new Vector3(0.0f, 6.5f, -9.25f);
        cameraOffsetRotatiton = new Vector3(15.0f, 0.0f, 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + cameraOffsetPosition;
        //transform.rotation = target.rotation * Quaternion.Euler(cameraOffsetRotatiton);
    }
}
