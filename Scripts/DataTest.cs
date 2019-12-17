using UnityEngine;
using System.Collections.Generic;

public class DataTest : MonoBehaviour {
    public DataOperator Operator;
    public float CurrentHealth;
    public float TotalHealth;
    public float CurrentEnergy;
    public float TotalEnergy;
    public float EnergyCure;
    public int Coins;
    public List<ItemLoadIndex> ItemReference;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    public void Init(Item item) {
        GameObject.Instantiate(item, transform);
    }

    public void loadData() {
        // 声明临时变量存放数据
        Data data = new Data();
        DataForPlayer playerdata = new DataForPlayer();
        // 从DataAssets获取数据至playerdata
        Operator.loadData(out data);
        playerdata = (DataForPlayer)data;
        // 同步数据至人物
        TotalHealth = playerdata.TotalHealth;
        CurrentHealth = playerdata.CurrentHealth;
        TotalEnergy = playerdata.TotalEnergy;
        CurrentEnergy = playerdata.CurrentEnergy;
        Coins = playerdata.Coins;
        ItemReference = playerdata.ItemReference;
        Operator.DataAssets.Path = Application.dataPath + "/Save/PlayerData.txt";
    }
}
