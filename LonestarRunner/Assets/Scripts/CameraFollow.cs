using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Player m_Player;

    private Vector3 m_Offset;

    private void Start() {
        m_Offset = transform.position - m_Player.transform.position;
    }

    private void FixedUpdate() {
        Vector3 CurrentPosition = m_Offset + m_Player.transform.position;
        CurrentPosition.x = 0;
        transform.position = CurrentPosition;
    }
}
