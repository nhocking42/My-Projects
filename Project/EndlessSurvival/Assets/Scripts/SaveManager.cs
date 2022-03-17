using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Save()
    {

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "SurvivalSave.dat", FileMode.Create);

            SaveData data = new SaveData();

            SavePlayer(data);

            bf.Serialize(file, data);

            file.Close();

            

        }
        catch (System.Exception)
        {
            //This is for handling errors
        }
    }

    private void SavePlayer(SaveData data)
    {
        Inventory.MyInstance.parseItemToID();
        data.MyPlayerData = new PlayerData(

            CharacterMovement.MyInstance.currentHealth,
            CharacterMovement.MyInstance.currentFood,
            CharacterMovement.MyInstance.transform.position,
            DayNightScript.MyInstance.days,
            DayNightScript.MyInstance.hours,
            DayNightScript.MyInstance.mins,
            WeatherManager.MyInstance.weatherStatus,
            HouseManager.MyInstance.level,
            Inventory.MyInstance.itemsInInventory);
    }


    public void Load()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "SurvivalSave.dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

            LoadPlayer(data);

        }
        catch (System.Exception)
        {
            //This is for handling errors
        }
    }

    private void LoadPlayer(SaveData data)
    {
        CharacterMovement.MyInstance.currentHealth = data.MyPlayerData.CurrentHealth;
        CharacterMovement.MyInstance.healthBar.SetHealth(data.MyPlayerData.CurrentHealth);
        CharacterMovement.MyInstance.currentFood = data.MyPlayerData.CurrentFood;
        CharacterMovement.MyInstance.foodBar.SetHealth(data.MyPlayerData.CurrentFood);
        CharacterMovement.MyInstance.transform.position = new Vector2(data.MyPlayerData.MyX, data.MyPlayerData.MyY);

        DayNightScript.MyInstance.days = data.MyPlayerData.day;
        DayNightScript.MyInstance.hours = data.MyPlayerData.hour;
        DayNightScript.MyInstance.mins = data.MyPlayerData.minute;

        WeatherManager.MyInstance.weatherStatus = data.MyPlayerData.weather;
        HouseManager.MyInstance.level = data.MyPlayerData.houseLevel;

        Inventory.MyInstance.clean();
        Inventory.MyInstance.itemsInInventory = data.MyPlayerData.items;
        Inventory.MyInstance.parseIDtoItem();

        // This have a bug more item when save and load.

    }

}
