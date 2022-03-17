using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public PlayerData MyPlayerData { get; set; }
    public SaveData()
    {

    }
}

[Serializable]
public class PlayerData
{
    public int CurrentHealth { get; set; }
    public int CurrentFood { get; set; }
    public float MyX { get; set; }
    public float MyY { get; set; }
    public int day { get; set; }
    public int hour { get; set; }
    public int minute { get; set; }
    public int weather { get; set; }
    public int houseLevel { get; set; }
    public int[] items { get; set; }


    public PlayerData(int CurrentHealth,int CurrentFood, Vector2 position, int DayInGame, int HourInGame, int MinuteInGame, int WeatherInGame, int HouseLevel, int[] Inventoryitems)
    {

        this.CurrentHealth = CurrentHealth;
        this.CurrentFood = CurrentFood;
        this.MyX = position.x;
        this.MyY = position.y;
        this.day = DayInGame;
        this.hour = HourInGame;
        this.minute = MinuteInGame;
        this.weather = WeatherInGame;
        this.houseLevel = HouseLevel;
        this.items = Inventoryitems;
    }

}
