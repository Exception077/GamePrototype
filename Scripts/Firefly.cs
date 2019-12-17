using UnityEngine;
using System.Collections;

public class Firefly : MonoBehaviour {
    float timer;
    [SerializeField]
    float Duration;
    [SerializeField]
    bool Fadeout = true;
    public Rigidbody2D Rbody2D;
    // Use this for initialization
    void Start() {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (Fadeout) {
            if (Time.time - timer >= Duration) {
                Destroy(gameObject);
            }
        }
    }
}
