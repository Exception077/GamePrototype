using UnityEngine;
using System.Collections;

public class FireflyTool : MonoBehaviour {
    [SerializeField]
    Firefly Firefly;
    [SerializeField]
    Player Player;
    [SerializeField]
    Vector2 Force;
    [SerializeField]
    float CD;
    float timer;
    // Use this for initialization
    void Start() {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (Time.time - timer > CD) {
                Firefly temp = GameObject.Instantiate<Firefly>(Firefly);
                temp.gameObject.transform.position = Player.LoadObj.transform.position;
                temp.Rbody2D.AddForce(new Vector2(Force.x * (Player.isfacingright() ? 1 : -1), Force.y));
                timer = Time.time;
            }
        }
    }
}
