using UnityEngine;

public class GroundSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject m_GroundTile;

    [SerializeField, Range(5, 20)]
    private int m_DefaultPreloadTiles;

    private Vector3 m_NextSpawner;

    public void SpawnTile(bool item) {
        GameObject next = Instantiate(m_GroundTile, m_NextSpawner, Quaternion.identity);
        var tile = next.GetComponent<GroundTile>();
        tile.SelfSpawnPoint = m_NextSpawner;
        tile.NextSpawnPoint = next.transform.GetChild(0).transform.position;                // GetChilde(0 = index of NextTileSpawn in GroundTile)
        m_NextSpawner = tile.NextSpawnPoint;

        if (item) {
            next.GetComponent<GroundTile>().SpawnCoin();
            next.GetComponent<GroundTile>().SpawnObstacle();
        }
    }

    private void Start() {
        for (int i = 0; i < m_DefaultPreloadTiles; ++i) {
            SpawnTile(i >= 3);
        }
    }
}
