using UnityEngine;

public class EntityGenerator : MonoBehaviour {
    [SerializeField]
    private GameObject[] m_ObstaclePrefabs;

    [SerializeField]
    private GameObject[] m_TreasurePrefabs;

    [SerializeField]
    private GameObject[] m_DecorationPrefabs;

    [SerializeField]
    private GameObject[] m_SecondaryDecorationPrefabs;

    [SerializeField]
    private GameObject[] m_BackgroundPrefabs;

    [SerializeField]
    private GameObject[] m_FencePrefabs;

    [SerializeField]
    private GameObject[] m_CloudPrefabs;

    public enum EntityType {
        Obstacle, Treasure, Decoration, SecondaryDecoration, Background, Fence, Cloud
    }

    private GameObject[] GetPrefabs(EntityType type) {
        switch (type) {
            case EntityType.Obstacle:
                return m_ObstaclePrefabs;
            case EntityType.Treasure:
                return m_TreasurePrefabs;
            case EntityType.Decoration:
                return m_DecorationPrefabs;
            case EntityType.SecondaryDecoration:
                return m_SecondaryDecorationPrefabs;
            case EntityType.Background:
                return m_BackgroundPrefabs;
            case EntityType.Fence:
                return m_FencePrefabs;
            case EntityType.Cloud:
                return m_CloudPrefabs;
            default:
                throw new UnityException("Undetected Entity Type");
        }
    }

    public void Generate(EntityType type, Transform parent, params Vector3[] positions) {
        foreach (var position in positions) {
            Instantiate(GPToolkits.GetRandomObjectWithinArray(GetPrefabs(type)), position, GPToolkits.FixedRotation, parent);
        }
    }

    public GameObject Generate(EntityType type, Transform parent, Vector3 position) {
        return Instantiate(GPToolkits.GetRandomObjectWithinArray(GetPrefabs(type)), position, GPToolkits.FixedRotation, parent);
    }

    public GameObject Generate(EntityType type, int index, Transform parent, Vector3 position) {
        return Instantiate(GetPrefabs(type)[index], position, GPToolkits.FixedRotation, parent);
    }

    public GameObject GenerateNoFixedRoatation(EntityType type, int index, Transform parent, Vector3 position) {
        return Instantiate(GetPrefabs(type)[index], position, Quaternion.identity, parent);
    }

    public void GenerateWithRotation(EntityType type, Transform parent, Quaternion rotate, params Vector3[] positions) {
        foreach (var position in positions) {
            Instantiate(GPToolkits.GetRandomObjectWithinArray(GetPrefabs(type)), position, rotate * GPToolkits.FixedRotation, parent);
        }
    }

    public GameObject GenerateWithRotation(EntityType type, Transform parent, Quaternion rotate, Vector3 position) {
        return Instantiate(GPToolkits.GetRandomObjectWithinArray(GetPrefabs(type)), position, rotate * GPToolkits.FixedRotation, parent);
    }

    public GameObject GenerateWithRotation(EntityType type, int index, Transform parent, Quaternion rotate, Vector3 position) {
        return Instantiate(GetPrefabs(type)[index], position, rotate * GPToolkits.FixedRotation, parent);
    }

    public GameObject GenerateWithRotationNoFixedRotation(EntityType type, Transform parent, Quaternion rotate, Vector3 position) {
        return Instantiate(GPToolkits.GetRandomObjectWithinArray(GetPrefabs(type)), position, rotate, parent);
    }

    public void GenerateRandomRotation(EntityType type, Transform parent, params Vector3[] positions) {
        foreach (var position in positions) {
            Quaternion rotate = Quaternion.Euler(0, Random.Range(0, 360), 0);
            Instantiate(GPToolkits.GetRandomObjectWithinArray(GetPrefabs(type)), position, rotate * GPToolkits.FixedRotation, parent);
        }
    }

    public GameObject GenerateRandomRotation(EntityType type, Transform parent, Vector3 position) {
        Quaternion rotate = Quaternion.Euler(0, Random.Range(0, 360), 0);
        return Instantiate(GPToolkits.GetRandomObjectWithinArray(GetPrefabs(type)), position, rotate * GPToolkits.FixedRotation, parent);
    }

    public GameObject GenerateRandomRotation(EntityType type, int index, Transform parent, Vector3 position) {
        Quaternion rotate = Quaternion.Euler(0, Random.Range(0, 360), 0);
        return Instantiate(GetPrefabs(type)[index], position, rotate * GPToolkits.FixedRotation, parent);
    }
}
