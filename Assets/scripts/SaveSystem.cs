using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SaveMap(int[,] Iblocks){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Map1.map";
        FileStream stream = new FileStream(path, FileMode.Create);

        Map data = new Map(Iblocks);

        formatter.Serialize(stream, data);

        stream.Close();

    }

    public static int[,] LoadMap(){

        string path = Application.persistentDataPath + "/Map1.map";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int[,] map = formatter.Deserialize(stream) as int[,];
            stream.Close();
            return map;
        }

        else{
            return new int[0,0];
        }

        

    }
}
