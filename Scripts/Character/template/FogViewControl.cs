using UnityEngine;
using System.Collections;
using FoW; 

public class FogViewControl : MonoBehaviour {
    [SerializeField]
    FogOfWarUnit FOWUnit;
    [SerializeField]
    Player Player;
    // 
    [SerializeField]
    float Delay;
    [SerializeField]
    [Range(0,0.5f)]
    float OffsetX = 0.35f;
    float timer = 0f;
    // Use this for initialization
    void Start() {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time - timer >= Delay) {
            updateView();
            timer = Time.time;
        }    
    }

    void updateView() {
        FOWUnit.offset = new Vector2((FOWUnit.boxSize.x * OffsetX) * (Player.isfacingright() ? 1 : -1), 0);
    }

}
