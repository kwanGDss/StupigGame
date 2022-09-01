using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    public new Camera camera;
    private Animator animator;
    private NavMeshAgent agent;

    private bool isMove;
    private bool isAttacking;
    private bool isComboAttacking;
    private bool isRolling;
    private float playerRotationSpeed;
    private float playerRollingDistance;
    private int floorLayerIndex;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        playerRotationSpeed = 10f;
        playerRollingDistance = 10f;
        floorLayerIndex = 1 << LayerMask.NameToLayer("Floor");
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
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 1000f, floorLayerIndex))
            {
                SetDestination(hit.point);
            }
        }

        LookMoveDireciton();
        Attack();
        ComboAttack();
        Roll();
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
            if (Vector3.Distance(animator.transform.position, agent.destination) < 0.001f) // 속력이 아닌 도착했느냐에 따라 결정
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
            isAttacking = true;
            animator.SetBool("isAttacking", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Punch1") &&
            animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.9f)
        {
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
            isComboAttacking = true;
            animator.SetBool("isComboAttacking", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Punch2") &&
            animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.9f)
        {
            isAttacking = false;
            isComboAttacking = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isComboAttacking", false);
        }
    }

    private void Roll()
    {
        //if (!isAttacking && !isComboAttacking)
        //{
        //    Debug.Log("Rolling");
        //    isRolling = true;
        //    isMove = false;
        //    animator.SetBool("isMove", false);

        //    animator.transform.position = Quaternion.Lerp(animator.transform.position, animator.transform.forward)
        //}
    }
}
