using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class FileStreamerData
{
    public static string directory = Application.dataPath + "/Saves/";
    public static string fileName = "JsonData";

    public static void SaveItemData(ItemData itemData)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Se ha creado el nuevo archivo llamado: " + fileName);
        }
        string json = JsonUtility.ToJson(itemData);
        File.WriteAllText(directory + fileName, json);
        Debug.Log("Se han guardado los datos en: " + directory);
    }
    public static ItemData LoadItemData()
    {
        string fullPath = directory + fileName;
        ItemData data =new ItemData();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            data = JsonUtility.FromJson<ItemData>(json);
            Debug.Log("Se han cargado todos los datos de: " + fullPath);
        }
        else
            Debug.Log("Este archivo no existe");

        return data;
    }
}
