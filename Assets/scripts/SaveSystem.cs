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
    public static void WriteString(string name, string str)
    {
        string path = "Assets/MapsData/" + name + ".txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(str);
        writer.Close();
        //Re-import the file to update the reference in the editor
    }

    public static void ReadString()
    {
        string path = "Assets/Resources/test.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

}
