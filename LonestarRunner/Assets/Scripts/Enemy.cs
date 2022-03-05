using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private Player m_Player;

    [SerializeField]
    private Rigidbody m_Rigidbody;

    [SerializeField]
    private Transform m_GroundChecker;

    [SerializeField]
    private float m_JumpForce;

    [SerializeField]
    private float m_JumpSensitivity;

    [SerializeField]
    private float m_Speed;

    [SerializeField]
    public float m_DropCycle;

    [SerializeField]
    private float m_CorrectingSpeed;

    [SerializeField]
    private float m_AccumulatingSpeedPerSecond = 0.5f;

    [SerializeField]
    private TreasurePack m_Package;

    private float m_Distance;

    private float m_Timer = 0.0f;
    private float m_AccumulatedSpeed = 0.0f;

    private void Start() {
        m_Distance = transform.position.z - m_Player.transform.position.z;
    }

    private void Update() {
        m_Timer += Time.deltaTime;

        if (transform.position.z - m_Player.transform.position.z < m_Distance) {
            m_AccumulatedSpeed += m_AccumulatingSpeedPerSecond * Time.deltaTime;
        } else {
            m_AccumulatedSpeed = 0;
        }

        if (Physics.OverlapSphere(m_GroundChecker.transform.position, 0.01f, LayerMask.GetMask("Ground")).Length == 0) return;
        if (Physics.OverlapSphere(transform.position, m_JumpSensitivity, LayerMask.GetMask("Obstacle")).Length > 0) {
            m_Rigidbody.velocity += new Vector3(0, m_JumpForce, 0);
        }
    }

    private void FixedUpdate() {
        transform.position += Time.fixedDeltaTime * m_Speed * transform.forward;
        if (Mathf.Abs(transform.position.x) > 0.5f) {
            transform.position += transform.right * GPToolkits.GetInverseSign(transform.position.x) * (m_CorrectingSpeed + m_AccumulatedSpeed);
        }

        if (m_Timer >= m_DropCycle) {
            m_Timer = 0;
            m_Package.Fall();
        }
    }
}
