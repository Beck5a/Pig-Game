using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class Weapon
{
    public string Name;
    public int Damage;

    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }
}

[System.Serializable]
public class WeaponList
{
    public List<Weapon> weapons;
}

public class DataManager : MonoBehaviour, IManager
{
    private string _state;
    private string _dataPath;
    private string _textFile;
    private string _streamingTextFile;
    private string _xmlLevelProgress;
    private string _xmlWeapons;
    private string _jsonWeapons;

    private List<Weapon> _weaponInventory = new List<Weapon>
    {
        new Weapon("Sword Of Doom", 100),
        new Weapon("Butterfly knives", 25),
        new Weapon("Brass knuckles", 15)
    };

    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data";

        _textFile = _dataPath + "/Save_Data.txt";
        _streamingTextFile = _dataPath + "/Streaming_Save_Data.txt";
        _xmlLevelProgress = _dataPath + "/Progress_Data.xml";
        _xmlWeapons = _dataPath + "/WeaponInventory.xml";
        _jsonWeapons = _dataPath + "/Weapon.json";

        Debug.Log("Data Path: " + _dataPath);
    }

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _state = "Data Manager initialized";
        Debug.Log(_state);

        NewDirectory();

        NewTextFile();
        UpdateTextFile();
        ReadFromFile(_textFile);

        WriteToStream(_streamingTextFile);
        ReadFromStream(_streamingTextFile);

        WriteToXML(_xmlLevelProgress);

        SerializeXML();
        DeserializeXML();

        SerializeJSON();
        DeserializeJSON();
    }

    public void NewDirectory()
    {
        if (Directory.Exists(_dataPath))
        {
            Debug.Log("Directory already exists");
            return;
        }

        Directory.CreateDirectory(_dataPath);
        Debug.Log("Directory created");
    }

    public void DeleteDirectory()
    {
        if (!Directory.Exists(_dataPath))
        {
            Debug.Log("Directory does not exist");
            return;
        }

        Directory.Delete(_dataPath, true);
        Debug.Log("Directory deleted");
    }

    public void NewTextFile()
    {
        if (File.Exists(_textFile))
        {
            Debug.Log("Text file already exists");
            return;
        }

        File.WriteAllText(_textFile, "<SAVE_DATA>\n");
        Debug.Log("New text file created");
    }

    public void UpdateTextFile()
    {
        if (!File.Exists(_textFile))
        {
            Debug.Log("Text file does not exist");
            return;
        }

        File.AppendAllText(_textFile, $"Game started: {DateTime.Now}\n");
        Debug.Log("Text file updated");
    }

    public void DeleteFiles(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File does not exist");
            return;
        }

        File.Delete(filename);
        Debug.Log("File deleted");
    }

    public void ReadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File does not exist");
            return;
        }

        Debug.Log(File.ReadAllText(filename));
    }

    public void WriteToStream(string filename)
    {
        if (!File.Exists(filename))
        {
            using (StreamWriter newStream = File.CreateText(filename))
            {
                newStream.WriteLine("<Save Data> for Hero born\n");
            }

            Debug.Log("New stream file created");
        }

        using (StreamWriter stream = File.AppendText(filename))
        {
            stream.WriteLine($"Game ended: {DateTime.Now}\n");
        }

        Debug.Log("Stream file updated");
    }

    public void ReadFromStream(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File does not exist");
            return;
        }

        using (StreamReader reader = new StreamReader(filename))
        {
            Debug.Log(reader.ReadToEnd());
        }
    }

    public void WriteToXML(string filename)
    {
        if (File.Exists(filename))
        {
            Debug.Log("XML already exists");
            return;
        }

        using (FileStream xmlStream = File.Create(filename))
        using (XmlWriter writer = XmlWriter.Create(xmlStream))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("Level_Progress");

            for (int i = 1; i <= 5; i++)
            {
                writer.WriteStartElement("Level");
                writer.WriteString("Level_" + i);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        Debug.Log("XML file created");
    }

    public void SerializeXML()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Weapon>));

        using (FileStream stream = File.Create(_xmlWeapons))
        {
            serializer.Serialize(stream, _weaponInventory);
        }

        Debug.Log("Weapons serialized to XML");
    }

    public void DeserializeXML()
    {
        if (!File.Exists(_xmlWeapons))
        {
            Debug.Log("XML file does not exist");
            return;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<Weapon>));

        using (FileStream stream = File.OpenRead(_xmlWeapons))
        {
            List<Weapon> weapons = (List<Weapon>)serializer.Deserialize(stream);

            foreach (Weapon weapon in weapons)
            {
                Debug.Log($"Weapon: {weapon.Name} - Damage: {weapon.Damage}");
            }
        }
    }

    public void SerializeJSON()
    {
        WeaponShop shop = new WeaponShop();
        shop.inventory = _weaponInventory;
        string json = JsonUtility.ToJson(shop, true);
        using(StreamWriter stream = File.CreateText(_jsonWeapons))
        {
            stream.Write(json);
        }
    }

      public void DeserializeJSON()
{
    if (File.Exists(_jsonWeapons))
    {
        using (StreamReader stream = new StreamReader(_jsonWeapons))
        {
            var jsonString = stream.ReadToEnd();
            var weaponData = JsonUtility.FromJson<WeaponShop>(jsonString);

            foreach (var weapon in weaponData.inventory)
            {
                Debug.Log($"weapon:{weapon.Name} - Damage: {weapon.Damage}");
            }
        }
    }
}

}