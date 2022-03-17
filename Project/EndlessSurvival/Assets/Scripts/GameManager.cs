using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mainCharacter;
    public GameObject menu;
    public GameObject option;
    public GameObject dateTimeSystem;

    public GameObject tutorialMenu;
    public GameObject[] tutorials;
    int tutorialIndex;

    float defaultTimeRate;

    public AudioSource sfxVolume;
    public AudioSource musicVolume;
    public AudioSource weatherVolume;

    public HealthBar musicBar;
    public HealthBar sfxBar;
    void Start()
    {
        menu.SetActive(false);
        tutorialMenu.SetActive(false);
        defaultTimeRate = dateTimeSystem.GetComponent<DayNightScript>().tick;
        tutorialIndex = 0;

        musicBar.SetMaxHealth((int)(musicVolume.volume*10));
        sfxBar.SetMaxHealth((int)(sfxVolume.volume*10));

        musicBar.SetHealth((int)(musicVolume.volume * 10));
        sfxBar.SetHealth((int)(sfxVolume.volume * 10));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainCharacter.GetComponent<CharacterMovement>().isScreenNotEistAnyMenu())
            {
                if(menu.activeSelf == false)
                {
                    menu.SetActive(true);
                    option.SetActive(false);
                    tutorialMenu.SetActive(false);
                    PauseGame();
                    SoundManager.PlaySound("open_features");
                }
                else
                {
                    menu.SetActive(false);
                    ResumeGame();
                    SoundManager.PlaySound("open_features");
                }
            }
        }

        for (int i = 0; i < tutorials.Length; i++)
        {
            if (i == tutorialIndex)
            {
                tutorials[i].SetActive(true);
            }
            else
            {
                tutorials[i].SetActive(false);
            }
        }

        if (tutorialIndex == tutorials.Length || tutorialIndex < 0)
        {
            tutorialMenu.SetActive(false);
        }
    }

    public void tutorial()
    {
        tutorialMenu.SetActive(true);
        tutorialIndex = 0;
        SoundManager.PlaySound("open_features");
    }

    public void nextTutorial()
    {
        tutorialIndex++;
        SoundManager.PlaySound("open_features");
    }
    public void previousTutorial()
    {
        tutorialIndex--;
        SoundManager.PlaySound("open_features");
    }

    public void openOptionmenu()
    {
        option.SetActive(true);
        SoundManager.PlaySound("open_features");
    }

    public void musicVolumeUp()
    {
        musicVolume.volume += 0.2f;
        musicBar.SetHealth((int)(musicVolume.volume * 10));
        SoundManager.PlaySound("open_features");
    }
    public void musicVolumeDown()
    {
        musicVolume.volume -= 0.2f;
        musicBar.SetHealth((int)(musicVolume.volume * 10));
        SoundManager.PlaySound("open_features");
    }
    public void sfxVolumeUp()
    {
        sfxVolume.volume += 0.2f;
        weatherVolume.volume += 0.2f;
        sfxBar.SetHealth((int)(sfxVolume.volume * 10));
        SoundManager.PlaySound("open_features");
    }
    public void sfxVolumeDown()
    {
        sfxVolume.volume -= 0.2f;
        weatherVolume.volume -= 0.2f;
        sfxBar.SetHealth((int)(sfxVolume.volume * 10));
        SoundManager.PlaySound("open_features");
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        dateTimeSystem.GetComponent<DayNightScript>().tick = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        dateTimeSystem.GetComponent<DayNightScript>().tick = defaultTimeRate;
        if (menu.activeSelf == true)
        {
            menu.SetActive(false);
        }
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenuGUI");
        Time.timeScale = 1;
    }
}
