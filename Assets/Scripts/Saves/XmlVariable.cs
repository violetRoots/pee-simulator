using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XmlVariable<T> where T : class, new()
{
    public T Value {  get; private set; }
    public string PathToFile => $"{path}{Path.DirectorySeparatorChar}{name}.sav";

    private XmlSerializer _xmlSerializer = new XmlSerializer(typeof(T));

    private string path;
    private string name;

    public XmlVariable(string filePath, string fileName)
    {
        path = filePath;
        name = fileName;
        Value = new();
    }

    public void Save()
    {
        using (FileStream fs = new FileStream(PathToFile, FileMode.OpenOrCreate))
        {
            _xmlSerializer.Serialize(fs, Value);
        }
    }

    public void Load()
    {
        if (!File.Exists(PathToFile)) return;

        try
        {
            using (FileStream fs = new FileStream(PathToFile, FileMode.OpenOrCreate))
            {
                Value = _xmlSerializer.Deserialize(fs) as T;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Load save error: \n{e.Message}");
            Clear();
        }
    }

    public void Clear()
    {
        Value = new();
        File.Delete(PathToFile);
    }
}
