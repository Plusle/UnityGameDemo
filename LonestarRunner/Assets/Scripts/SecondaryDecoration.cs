using UnityEngine;

public class SecondaryDecoration : MonoBehaviour {
    [SerializeField]
    private Collider m_OuterCollier;
    [SerializeField]
    private Collider m_InnerCollier;

    [SerializeField]
    private int m_MinSecondaryDecorations = 5;
    [SerializeField]
    private int m_MaxSecondaryDecorations = 8;

    public Vector3[] GetSecondaryDecorationPositions() {
        Vector3[] positions = new Vector3[Random.Range(m_MinSecondaryDecorations, m_MaxSecondaryDecorations)];
        for (int i = 0; i < positions.Length; i++) {
            positions[i] = GPToolkits.GetRandomVec3WithinColliderAABB(m_OuterCollier);
            positions[i].y = 0;
        }
        return positions;
    }
}
