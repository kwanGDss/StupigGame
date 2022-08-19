using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    private Camera camera;
    private Animator animator;
    private NavMeshAgent agent;

    private bool isMove;

    void Awake()
    {
        camera = Camera.main;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                SetDestination(hit.point);
            }
        }

        LookMoveDireciton();
    }

    private void SetDestination(Vector3 dest)
    {
        Debug.Log("SetDestination");
        agent.SetDestination(dest);
        isMove = true;
        animator.SetBool("isMove", true);
    }

    private void LookMoveDireciton()
    {
        if (isMove)
        {
            if (agent.velocity.magnitude == 0f)
            {
                isMove = false;
                animator.SetBool("isMove", false);
                return;
            }

            var dir = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z) - transform.position;
            animator.transform.forward = dir;
        }
    }
}
