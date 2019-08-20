using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnableDeadUI : MonoBehaviour{
    public bool Ready;
    public float GraySpeed;
    public Text Tips;
    public Text Count;
    public BlackTest GrayControl;
    public Player MyPlayer;

    // Update is called once per frame
    void Update(){
        if(Ready && GrayControl.grayScaleAmount < 1) {
            GrayControl.grayScaleAmount += GraySpeed;
        }
        if(GrayControl.grayScaleAmount > 1) {
            GrayControl.grayScaleAmount = 1;
        }
        if(Ready && GrayControl.grayScaleAmount >= 1) {
            Tips.gameObject.SetActive(true);
            Debug.Log("ready");
            //Time.timeScale = 0;
            if (Input.anyKeyDown) {
                MyPlayer.revive();
                GrayControl.grayScaleAmount = 0;
                Tips.gameObject.SetActive(false);
                Ready = false;
                gameObject.SetActive(false);
            }
        } else {
            Tips.gameObject.SetActive(false);
        }
    }

    public void enableFuncton() {
        Ready = true;
        Count.text = PlayerPrefs.GetInt("DeadCount").ToString();
    }
}
