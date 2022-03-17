using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public GameObject gameName;
    public GameObject tutorialMenu;
    public GameObject[] tutorials;
    int tutorialIndex;

    public GameObject rain;
    Vector3 gameNameDefaultPosition;


    void Start()
    {
        tutorialMenu.SetActive(false);
        gameNameDefaultPosition = gameName.transform.position;
        gameName.transform.position = new Vector2(gameName.transform.position.x, gameName.transform.position.y + 4);

        tutorialIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameName.transform.position != gameNameDefaultPosition)
        {
            gameName.transform.position = Vector3.MoveTowards(gameName.transform.position, gameNameDefaultPosition, 50*Time.deltaTime);
        }

        for (int i=0; i<tutorials.Length; i++)
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
            rain.SetActive(true);
        }


    }

    public void exitGame()
    {
        Debug.Log("Game Closed!!!");
        Application.Quit();
    }

    public void playNewGame()
    {
        //SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
    
    public void tutorial()
    {
        tutorialMenu.SetActive(true);
        tutorialIndex = 0;
        rain.SetActive(false);
    }

    public void nextTutorial()
    {
        tutorialIndex++;
    }
    public void previousTutorial()
    {
        tutorialIndex--;
    }
}
