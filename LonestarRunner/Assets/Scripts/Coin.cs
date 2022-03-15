using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    [SerializeField, Range(0, 360)] 
    private float m_RotationSpeed = 180;

    [SerializeField]
    private LayerMask m_Standalone;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 6 /* 6. Player */) {
            Destroy(gameObject);
            GameManager.instance.ScoreIncrement();
            FindObjectOfType<AudioManager>().Play("Coin");
        }
    }

    private void Update() {
        transform.Rotate(0, 0, m_RotationSpeed * Time.deltaTime);
    }
}
