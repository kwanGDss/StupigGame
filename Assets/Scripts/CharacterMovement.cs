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
    private bool isComboAttacking;
    private float playerRotationSpeed;

    void Awake()
    {
        camera = Camera.main;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        playerRotationSpeed = 10f;
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
        Attack();
        ComboAttack();
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
            animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * playerRotationSpeed);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attacking");
            isAttacking = true;
            animator.SetBool("isAttacking", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Punch1") &&
            animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.9f)
        {
            Debug.Log("Attack_End");
            isAttacking = false;
            isComboAttacking = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isComboAttacking", false);
        }
    }

    private void ComboAttack()
    {
        if (Input.GetMouseButtonDown(0) &&
            animator.GetCurrentAnimatorStateInfo(1).IsName("Punch1") &&
            animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.1f &&
            animator.GetCurrentAnimatorStateInfo(1).normalizedTime <= 0.9f)
        {
            Debug.Log("ComboAttacking");
            isComboAttacking = true;
            animator.SetBool("isComboAttacking", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Punch2") &&
            animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.9f)
        {
            Debug.Log("ComboAttack_End");
            isAttacking = false;
            isComboAttacking = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isComboAttacking", false);
        }
    }
}
