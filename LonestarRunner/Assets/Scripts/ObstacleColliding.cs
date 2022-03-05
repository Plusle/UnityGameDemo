using UnityEngine;

public class ObstacleColliding : MonoBehaviour {
    [SerializeField]
    private LayerMask m_ObstacleCollision;

    private void Start() {
        if (Physics.OverlapSphere(transform.position, 1.0f, m_ObstacleCollision).Length > 1) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Player") {
            var player = GameObject.FindObjectOfType<Player>();
            player.Kill();
        }
    }

}
