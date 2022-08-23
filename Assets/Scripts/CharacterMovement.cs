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
    private bool isAttacking;

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

        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);
        }

        LookMoveDireciton();
        Attack();
    }

    private void SetDestination(Vector3 dest)
    {
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

    private void Attack()
    {
        if (isAttacking)
        {
            if (animator.GetCurrentAnimatorStateInfo(1).IsName("PunchLeft") &&
                animator.GetCurrentAnimatorStateInfo(1).normalizedTime <= 1f)
            {
                Debug.Log("attacking");
                isAttacking= false;
                animator.SetBool("isAttacking", false);
            }
        }
    }
}
