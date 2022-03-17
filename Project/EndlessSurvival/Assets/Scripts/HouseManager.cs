using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseManager : MonoBehaviour
{
    public int level;
    public GameObject[] houses;
    public GameObject menu;
    public GameObject mainCharacter;
    public Text message;
    public Text condition;

    private static HouseManager instance;
    public static HouseManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HouseManager>();
            }

            return instance;
        }
    }
    void Start()
    {
        level = 1;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        condition.text = "Need: x" + level * 4 + " Wood";
        for(int i=0; i < houses.Length; i++)
        {
            houses[i].SetActive(false);
        }
        houses[level-1].SetActive(true);
    }

    public void OpenHouseMenu()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, mainCharacter.transform.position);
        if (menu.activeSelf == false && distanceToPlayer <= 1.5)
        {
            menu.SetActive(true);
            message.text = "";
            mainCharacter.GetComponent<CharacterMovement>().stopMoving();
            SoundManager.PlaySound("open_features");
        }
    }
    public void QuitMenu()
    {
        menu.SetActive(false);
    }

    public void UpgradeHouse()
    {
        if (level < houses.Length)
        {
            if (mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Wood", level*4))
            {
                mainCharacter.GetComponent<Inventory>().removeItem("Wood", level*4);
                level++;
                message.color = Color.green;
                message.text = "Upgrade house successfully";
            }
            else
            {
                message.color = Color.red;
                message.text = "Not enougt materials";
            }
        }
        SoundManager.PlaySound("open_features");
    }
}
