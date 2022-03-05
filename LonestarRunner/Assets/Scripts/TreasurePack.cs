using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreasurePack : MonoBehaviour {
    [SerializeField]
    private Transform m_World;

    [SerializeField]
    private Enemy m_Owner;

    [SerializeField]
    private EntityGenerator m_EntityGenerator;

    [SerializeField]
    private float m_TreasureInitalSpeed;

    private float m_Timer = 0.0f;

    private Queue<GameObject> m_Fallings;

    private void Start() {
        m_Fallings = new Queue<GameObject>();
    }

    public void Fall() {
        Vector3 position = transform.position;
        position.y = 0.3f;
        position.z -= 1;
        GameObject treasure = m_EntityGenerator.Generate(EntityGenerator.EntityType.Treasure, 1, null, position);
        //treasure.transform.rotation = Quaternion.Euler(-90, 0, 0);
        treasure.transform.forward = new Vector3(0, 0, -1);
        treasure.transform.right = new Vector3(-1, 0, 0);
        m_Fallings.Enqueue(treasure);
    }

    private void Update() {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_Owner.m_DropCycle) {
            m_Timer = 0; 
            if (m_Fallings.Count != 0) { 
                GameObject treasure = m_Fallings.Dequeue();
                if (treasure != null) {
                    Destroy(treasure);
                }
            }
        }
    }

}
