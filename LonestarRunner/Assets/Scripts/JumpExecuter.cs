using UnityEngine;

public class JumpExecuter : MonoBehaviour {
    [SerializeField]
    private Rigidbody m_PlayerRigidbody;

    [SerializeField]
    private Transform m_GroundChecker;

    [SerializeField]
    private float m_JumpForce = 8;

    [SerializeField]
    private Animator m_Animator;

    [HideInInspector]
    public bool m_Air = false;

    private bool m_HitJump = false;

    public void Update() {
        if (!m_HitJump && Input.GetKeyDown(KeyCode.Space)) {
            m_HitJump = true;
        }

        m_Animator.SetFloat("VerticalVelocity", m_PlayerRigidbody.velocity.y);
        m_Animator.SetBool("InAir", Mathf.Abs(m_PlayerRigidbody.velocity.y) > 0.01f);
    }

    public void FixedUpdate() {
        if (!GetComponent<Player>().m_Alive) return;

        if (Physics.OverlapSphere(m_GroundChecker.position, 0.1f, LayerMask.GetMask("Ground")).Length == 0) {
            m_HitJump = false;

            return;
        }

        if (m_HitJump) {
            var velocity = m_PlayerRigidbody.velocity;
            m_PlayerRigidbody.velocity = new Vector3(velocity.x, m_JumpForce, velocity.z);
            m_HitJump = false;
        }
    }
}
