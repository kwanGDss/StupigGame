using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    public new Camera camera;
    private Animator animator;
    private NavMeshAgent agent;
    private Rigidbody rigidBody;

    private bool isMove;
    private bool isAttacking;
    private bool isComboAttacking;
    private bool isRolling;
    private float RotationSpeed;
    private int floorLayerIndex;
    private float RollingDistance;
    private Vector3 dir;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        RotationSpeed = 10.0f;
        floorLayerIndex = 1 << LayerMask.NameToLayer("Floor");
        RollingDistance = 10.0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookMoveDireciton();
        Attack();
        ComboAttack();
        Roll();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"{agent.destination}");
        }
    }

    private void Move()
    {
        if (Input.GetMouseButton(1) && !isRolling)
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 1000.0f, floorLayerIndex))
            {
                agent.SetDestination(hit.point);
                isMove = true;
                animator.SetBool("isMove", true);
            }
        }
    }

    private void LookMoveDireciton()
    {
        Debug.Log($"{agent.velocity.magnitude}");
        if (isMove)
        {
            if (agent.velocity.magnitude == 0f && agent.remainingDistance <= 0.5f)
            {
                isMove = false;
                animator.SetBool("isMove", false);
                return;
            }

            dir = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z) - transform.position;
            animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * RotationSpeed);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !isRolling)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Punch1") &&
            animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.8f)
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
            animator.GetCurrentAnimatorStateInfo(1).normalizedTime <= 0.8f)
        {
            isComboAttacking = true;
            animator.SetBool("isComboAttacking", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Punch2") &&
            animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.8f)
        {
            isAttacking = false;
            isComboAttacking = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isComboAttacking", false);
        }
    }

    private void Roll()
    {
        if (!isAttacking && !isComboAttacking && !isRolling && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Rolling");
            isRolling = true;
            isMove = false;
            animator.SetBool("isRolling", true);
            animator.SetBool("isMove", false);

            animator.transform.position = Vector3.Lerp(animator.transform.position, animator.transform.forward * RollingDistance, Time.deltaTime * 0.1f);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RollForward") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
        {
            Debug.Log("Rolling End");
            isRolling = false;
            animator.SetBool("isRolling", false);

            agent.SetDestination(agent.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("coll");
            collision.gameObject.GetComponent<Enemy>().TakeDamage(5f);
        }
    }
}
