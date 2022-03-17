using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public Camera camera;
    public float timer;
    public int minRangeTime;
    public int maxRangeTime;
    public int weatherStatus;
    public int multiplyTime;
    // 0-7 (70%): clear
    // 8,9 (20%): rain
    // 10 (10%): snowy

    public GameObject rain;
    public GameObject snowy;

    public AudioSource rainSound;

    private static WeatherManager instance;
    public static WeatherManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WeatherManager>();
            }

            return instance;
        }
    }
    void Start()
    {
        setRandomTimeAndWeather();
    }

    void Update()
    {
        transform.position = camera.transform.position; 
        if (timer <= 0)
        {
            setRandomTimeAndWeather();
        }
        else
        {
            timer -= multiplyTime * Time.deltaTime;
        }

        if (weatherStatus >= 0 && weatherStatus <= 7)
        {
            MakeSunny();
            rainSound.mute = true;
        }
        if (weatherStatus > 7 && weatherStatus <= 9)
        {
            MakeRain();
            rainSound.mute = false;
        }
        if (weatherStatus > 9)
        {
            MakeSnowy();
            rainSound.mute = true;
        }

    }

    void setRandomTimeAndWeather()
    {
        weatherStatus = Random.Range(1, 10);
        timer = Random.Range(minRangeTime, maxRangeTime);
    }

    void MakeRain()
    {
        rain.SetActive(true);
        snowy.SetActive(false);

    }
    void MakeSnowy()
    {
        rain.SetActive(false);
        snowy.SetActive(true);
    }
    void MakeSunny()
    {
        rain.SetActive(false);
        snowy.SetActive(false);

    }

}
