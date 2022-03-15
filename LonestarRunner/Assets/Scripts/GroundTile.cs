using UnityEngine;

public class GroundTile : MonoBehaviour {
    private GroundSpawner m_Spawner;

    [SerializeField]
    private EntityGenerator m_EntityGenerator;

    [SerializeField]
    private Transform m_ObstacleIntervalMin;
    [SerializeField]
    private Transform m_ObstacleIntervalMax;

    [SerializeField]
    private int m_ObstacleNum;

    [SerializeField]
    private float m_TreeXOffset = 8;

    [SerializeField, Range(3, 8)]
    private float m_FenceOffset = 5;

    [SerializeField]
    private Transform m_BackgroundSpawnLeft;
    [SerializeField]
    private Transform m_BackgroundSpawnRight;


    [SerializeField]
    private Collider m_CloudInterval;


    [HideInInspector]
    public Vector3 SelfSpawnPoint;
    [HideInInspector]
    public Vector3 NextSpawnPoint;

    private void Start() {
        m_Spawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnTree();
        SpawnFence();
        SpawnBackgroundRock();
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "Player") {
            m_Spawner.SpawnTile(true);
            Destroy(gameObject, 2.0f);
        }
    }

    public void SpawnObstacle() {
        for (int i = 0; i < m_ObstacleNum; i++) {
            m_EntityGenerator.Generate(EntityGenerator.EntityType.Obstacle, transform, GenerateObstaclePosition());
        }
    }

    public void SpawnCoin() {
        for (int i = 0; i < 5; i++) {
            Vector3 spawn = GPToolkits.GetRandomVec3WithinColliderAABB(GetComponent<Collider>());
            spawn.y = 0.7f;
            m_EntityGenerator.Generate(EntityGenerator.EntityType.Treasure, 0, transform, spawn);
        }
    }

    private void SpawnTree() {
        (Vector3 left, Vector3 right) = GenerateStreetTreePositions(SelfSpawnPoint, NextSpawnPoint, m_TreeXOffset, 0.7f);
        var left_tree = m_EntityGenerator.GenerateRandomRotation(EntityGenerator.EntityType.Decoration, transform, left);
        var right_tree = m_EntityGenerator.GenerateRandomRotation(EntityGenerator.EntityType.Decoration, transform, right);
        m_EntityGenerator.GenerateRandomRotation(EntityGenerator.EntityType.SecondaryDecoration,
                                   transform,
                                   left_tree.GetComponent<SecondaryDecoration>().GetSecondaryDecorationPositions());
        m_EntityGenerator.GenerateRandomRotation(EntityGenerator.EntityType.SecondaryDecoration,
                                   transform,
                                   right_tree.GetComponent<SecondaryDecoration>().GetSecondaryDecorationPositions());
    }

    private void SpawnFence() {
        float z_range = NextSpawnPoint.z - SelfSpawnPoint.z;
        float interval1 = z_range / 3;
        float interval2 = interval1 * 2;
        // For left side
        Vector3 L1 = new Vector3(-m_FenceOffset, 0, SelfSpawnPoint.z + Random.Range(0, interval1));
        Vector3 L2 = new Vector3(-m_FenceOffset, 0, SelfSpawnPoint.z + Random.Range(interval1, interval2));
        Vector3 L3 = new Vector3(-m_FenceOffset, 0, SelfSpawnPoint.z + Random.Range(interval2, z_range));
        Vector3 R1 = new Vector3(m_FenceOffset, 0, SelfSpawnPoint.z + Random.Range(0, interval1));
        Vector3 R2 = new Vector3(m_FenceOffset, 0, SelfSpawnPoint.z + Random.Range(interval1, interval2));
        Vector3 R3 = new Vector3(m_FenceOffset, 0, SelfSpawnPoint.z + Random.Range(interval2, z_range));
        m_EntityGenerator.GenerateWithRotation(EntityGenerator.EntityType.Fence, transform, Quaternion.Euler(0, 90, 0), L1, L2, L3, R1, R2, R3);
    }

    private void SpawnBackgroundRock() {
        Quaternion RotationL = Quaternion.Euler(0, 180, 0);
        Quaternion RotationR = Quaternion.Euler(0, 0, 0);
        m_EntityGenerator.GenerateWithRotationNoFixedRotation(EntityGenerator.EntityType.Background, transform, RotationL, m_BackgroundSpawnLeft.position);
        m_EntityGenerator.GenerateWithRotationNoFixedRotation(EntityGenerator.EntityType.Background, transform, RotationR, m_BackgroundSpawnRight.position);
    }

    private static (Vector3, Vector3) GenerateStreetTreePositions(Vector3 SelfSpawnPoint, Vector3 NextSpawnPoint, float XOffset, float YOffsetRange) {
        Vector3 middle = (SelfSpawnPoint + NextSpawnPoint) / 2;
        float range = Mathf.Abs(NextSpawnPoint.z - SelfSpawnPoint.z) * YOffsetRange / 2;
        middle.y += 0.1f; 
        Vector3 left = middle, right = middle;
        left.x -= XOffset; 
        right.x += XOffset;
        left.z  += Random.Range(-range, range);
        right.z += Random.Range(-range, range);
        
        return (left, right);
    }

    private Vector3 GenerateObstaclePosition() {
        Vector3 pos = GPToolkits.GetRandomVec3WithinAABB(m_ObstacleIntervalMin, m_ObstacleIntervalMax);
        pos.y = 0;
        return pos;
    }
}
