/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     DataOperator.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-01-21
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//[System.Serializable]
public class DataOperator : MonoBehaviour{
    public DataForPlayer DataAssets;

    public void saveData() {
        //保存数据      
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(DataAssets.Path)) {
            File.Delete(DataAssets.Path);
        }
        FileStream file = File.Create(DataAssets.Path);
        bf.Serialize(file, DataAssets);
        file.Close();
    }

    public DataForPlayer loadData() {
        //如果路径上有文件，就读取文件
        if (File.Exists(DataAssets.Path)) {
            //读取数据
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(DataAssets.Path, FileMode.Open);
            //DataAssets = (DataForPlayer)bf.Deserialize(file);
            //DataAssets = bf.Deserialize(file) as DataForPlayer;
            object temp = bf.Deserialize(file);
            DataAssets = (DataForPlayer)temp;
            file.Close();
        }
        //如果没有文件，就new出一个Data
        else {
            DataAssets = new DataForPlayer();
        }

        return DataAssets;
    }
}