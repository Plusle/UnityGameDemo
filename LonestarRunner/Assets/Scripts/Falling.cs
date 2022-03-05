using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 6 /* 6. Player */) {
            Destroy(gameObject);
            other.gameObject.GetComponent<Player>().Accel();
            FindObjectOfType<AudioManager>().Play("Accel");
        }
    }
}
