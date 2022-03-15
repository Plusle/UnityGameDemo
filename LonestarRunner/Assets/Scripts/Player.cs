using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Player : MonoBehaviour {

    [SerializeField, Tooltip("Initial Speed")] 
    private float m_Speed = 5.0f;

    [SerializeField] 
    private Rigidbody m_Rigidbody;

    [SerializeField, Range(0.5f, 2.0f)] 
    private float m_HorizontalSensitivity = 1.0f;

    [SerializeField]
    private float m_AccelSpeed = 0.1f;

    [SerializeField]
    public Controller m_Controller;

    [HideInInspector]
    public bool m_Alive = true;

    [SerializeField]
    private Transform m_GroundChecker;

    [SerializeField]
    private float m_JumpForce = 8;

    [SerializeField]
    private Animator m_Animator;

    [HideInInspector]
    public bool m_Air = false;


    private void Awake() {
        m_Controller = new Controller();
        m_Controller.Player.Enable();
        m_Controller.Player.Jump.performed += Jump;
    }

    private void Start() {
        //m_Rigidbody = GetComponent<Rigidbody>();
        AudioManager.instance.Play("Running");
    }

    private void FixedUpdate() {
        if (!m_Alive) return;

        Vector3 forward_move = transform.forward * m_Speed * Time.fixedDeltaTime;
        Vector3 horizontal_move = transform.right * m_Speed * Time.fixedDeltaTime * m_HorizontalSensitivity;
        horizontal_move *= m_Controller.Player.Move.ReadValue<float>();
        m_Rigidbody.MovePosition(m_Rigidbody.position + forward_move + horizontal_move);
    }

    private void Update() {
        //m_HorizontalInput = Input.GetAxis("Horizontal");
        //m_HorizontalInput = m_Controller.Player.Jump.ReadValue<float>();
        //if (m_Controller.Player.Jump.IsPressed()) m_Jump = true;


        m_Animator.SetFloat("VerticalVelocity", m_Rigidbody.velocity.y);
        m_Animator.SetBool("InAir", Mathf.Abs(m_Rigidbody.velocity.y) > 0.01f);
    }

    private void Jump(InputAction.CallbackContext context) {
        if (context.performed) {
            if (m_Rigidbody != null) {
                if (Physics.OverlapSphere(m_GroundChecker.transform.position, 0.01f, LayerMask.GetMask("Ground")).Length != 0) {
                    var velocity = m_Rigidbody.velocity;
                    m_Rigidbody.velocity = new Vector3(velocity.x, m_JumpForce, velocity.z);
                }
            }
        }
    }

    public void Kill() {
        AudioManager.instance.Stop("Running");
        m_Alive = false;
    }

    public void Accel() {
        m_Speed += m_AccelSpeed;
    }

}
