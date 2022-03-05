using UnityEngine;
//using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    [SerializeField, Tooltip("Initial Speed")] 
    private float m_Speed = 5.0f;

    [SerializeField] 
    private Rigidbody m_Rigidbody;

    [SerializeField, Range(0.5f, 2.0f)] 
    private float m_HorizontalSensitivity = 1.0f;

    [SerializeField]
    private float m_AccelSpeed = 0.1f;

    [HideInInspector]
    public bool m_Alive = true;
    
    private float m_HorizontalInput;

    private void Start() {
        AudioManager.instance.Play("Running");
    }

    private void FixedUpdate() {
        if (!m_Alive) return;

        Vector3 forward_move = transform.forward * m_Speed * Time.fixedDeltaTime;
        Vector3 horizontal_move = transform.right * m_HorizontalInput * m_Speed * Time.fixedDeltaTime * m_HorizontalSensitivity;
        m_Rigidbody.MovePosition(m_Rigidbody.position + forward_move + horizontal_move);
    }

    private void Update() {
        m_HorizontalInput = Input.GetAxis("Horizontal");
    }

    public void Kill() {
        AudioManager.instance.Stop("Running");
        m_Alive = false;
        //Invoke("RestartGame", 1.5f);
    }

    public void Accel() {
        m_Speed += m_AccelSpeed;
    }

    //private void RestartGame() {
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}
}
