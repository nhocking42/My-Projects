using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject[] craftingTable;
    public GameObject mainCharacter;
    public GameObject[] product;
    public GameObject houseMenuToLookTo;
    public Text message;
    int index;
    void Start()
    {
            for (int i = 0; i < craftingTable.Length; i++)
            {
                craftingTable[i].SetActive(false);
            }
            message.text = "";
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (houseMenuToLookTo.activeSelf == false)
        {
            for (int i = 0; i < craftingTable.Length; i++)
            {
                craftingTable[i].SetActive(false);
            }
            message.text = "";
        }
    }

    public void openFirstCraftingTable()
    {
        for (int i = 0; i < craftingTable.Length; i++)
        {
            craftingTable[i].SetActive(false);
        }
        craftingTable[0].SetActive(true);
        SoundManager.PlaySound("open_features");
    }

    public void nextButton()
    {
        if (index < craftingTable.Length-1)
        {
            index++;
            changeCraftingTable(index);
        }
        SoundManager.PlaySound("open_features");
    }
    public void previousButton()
    {
        if (index > 0)
        {
            index--;
            changeCraftingTable(index);
        }
        SoundManager.PlaySound("open_features");
    }

    public void changeCraftingTable(int number)
    {
        for (int i = 0; i < craftingTable.Length; i++)
        {
            craftingTable[i].SetActive(false);
        }
        craftingTable[number].SetActive(true);
        message.text = "";
    }

    public void craftHealthPotion()
    {
        int itemID = 0;
        for (int i = 0; i < product.Length; i++)
        {
            if (product[i].name == "Health_Potion")
            {
                itemID = i;
                break;
            }
        }
        if (mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Gold", 5) &&
            mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Beef_steak", 5))
        {
            Instantiate(product[itemID], mainCharacter.transform.position, Quaternion.identity);
            mainCharacter.GetComponent<Inventory>().removeItem("Gold", 5);
            mainCharacter.GetComponent<Inventory>().removeItem("Beef_steak", 5);
            message.color = Color.green;
            message.text = "Craft Successfully !";
        }
        else
        {
            message.color = Color.red;
            message.text = "Not enought materials !";
        }
    }
    public void craftCookedChicken()
    {
        int itemID = 0;
        for (int i = 0; i < product.Length; i++)
        {
            if (product[i].name == "Cooked_chicken")
            {
                itemID = i;
                break;
            }
        }
        if (mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Raw_chicken", 1) &&
            mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Coal", 1))
        {
            Instantiate(product[itemID], mainCharacter.transform.position, Quaternion.identity);
            mainCharacter.GetComponent<Inventory>().removeItem("Raw_chicken", 1);
            mainCharacter.GetComponent<Inventory>().removeItem("Coal", 1);
            message.color = Color.green;
            message.text = "Craft Successfully !";
        }
        else
        {
            message.color = Color.red;
            message.text = "Not enought materials !";
        }
    }

    public void craftCookedMutton()
    {
        int itemID = 0;
        for (int i = 0; i < product.Length; i++)
        {
            if (product[i].name == "Cooked_mutton")
            {
                itemID = i;
                break;
            }
        }
        if (mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Raw_mutton", 1) &&
            mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Coal", 1))
        {
            Instantiate(product[itemID], mainCharacter.transform.position, Quaternion.identity);
            mainCharacter.GetComponent<Inventory>().removeItem("Raw_mutton", 1);
            mainCharacter.GetComponent<Inventory>().removeItem("Coal", 1);
            message.color = Color.green;
            message.text = "Craft Successfully !";
        }
        else
        {
            message.color = Color.red;
            message.text = "Not enought materials !";
        }
    }

    public void craftCookedPorkchop()
    {
        int itemID = 0;
        for (int i = 0; i < product.Length; i++)
        {
            if (product[i].name == "Cooked_porkchop")
            {
                itemID = i;
                break;
            }
        }
        if (mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Raw_porkchop", 1) &&
            mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Coal", 1))
        {
            Instantiate(product[itemID], mainCharacter.transform.position, Quaternion.identity);
            mainCharacter.GetComponent<Inventory>().removeItem("Raw_porkchop", 1);
            mainCharacter.GetComponent<Inventory>().removeItem("Coal", 1);
            message.color = Color.green;
            message.text = "Craft Successfully !";
        }
        else
        {
            message.color = Color.red;
            message.text = "Not enought materials !";
        }
    }

    public void craftBeefSteak()
    {
        int itemID = 0;
        for (int i = 0; i < product.Length; i++)
        {
            if (product[i].name == "Beef_steak")
            {
                itemID = i;
                break;
            }
        }
        if (mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Raw_beef", 1) &&
            mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Coal", 1))
        {
            Instantiate(product[itemID], mainCharacter.transform.position, Quaternion.identity);
            mainCharacter.GetComponent<Inventory>().removeItem("Raw_beef", 1);
            mainCharacter.GetComponent<Inventory>().removeItem("Coal", 1);
            message.color = Color.green;
            message.text = "Craft Successfully !";
        }
        else
        {
            message.color = Color.red;
            message.text = "Not enought materials !";
        }
    }

    public void craft4WoodPlank()
    {
        int itemID = 0;
        for (int i=0; i<product.Length; i++)
        {
            if (product[i].name == "Wood_plank")
            {
                itemID = i;
                break;
            }
        }
        if (mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Wood", 1))
        {
            for (int i = 0; i < 4; i++)
            {
                Instantiate(product[itemID], mainCharacter.transform.position, Quaternion.identity);
            }
            mainCharacter.GetComponent<Inventory>().removeItem("Wood", 1);
            message.color = Color.green;
            message.text = "Craft Successfully !";
        }
        else
        {
            message.color = Color.red;
            message.text = "Not enought materials !";
        }
    }

    public void craftSword()
    {
        int itemID = 0;
        for (int i = 0; i < product.Length; i++)
        {
            if (product[i].name == "Sword")
            {
                itemID = i;
                break;
            }
        }
        if (mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Wood_plank", 2) && 
            mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Iron", 4))
        {
            Instantiate(product[itemID], mainCharacter.transform.position, Quaternion.identity);
            mainCharacter.GetComponent<Inventory>().removeItem("Wood_plank", 2);
            mainCharacter.GetComponent<Inventory>().removeItem("Iron", 4);
            message.color = Color.green;
            message.text = "Craft Successfully !";
        }
        else
        {
            message.color = Color.red;
            message.text = "Not enought materials !";
        }
    }
    public void craftAxe()
    {
        int itemID = 0;
        for (int i = 0; i < product.Length; i++)
        {
            if (product[i].name == "Axe")
            {
                itemID = i;
                break;
            }
        }
        if (mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Wood_plank", 2) &&
            mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Iron", 2))
        {
            Instantiate(product[itemID], mainCharacter.transform.position, Quaternion.identity);
            mainCharacter.GetComponent<Inventory>().removeItem("Wood_plank", 2);
            mainCharacter.GetComponent<Inventory>().removeItem("Iron", 2);
            message.color = Color.green;
            message.text = "Craft Successfully !";
        }
        else
        {
            message.color = Color.red;
            message.text = "Not enought materials !";
        }
    }
    public void craftPickAxe()
    {
        int itemID = 0;
        for (int i = 0; i < product.Length; i++)
        {
            if (product[i].name == "PickAxe")
            {
                itemID = i;
                break;
            }
        }
        if (mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Wood_plank", 1) &&
            mainCharacter.GetComponent<Inventory>().checkItemExistInInventory("Iron", 3))
        {
            Instantiate(product[itemID], mainCharacter.transform.position, Quaternion.identity);
            mainCharacter.GetComponent<Inventory>().removeItem("Wood_plank", 1);
            mainCharacter.GetComponent<Inventory>().removeItem("Iron", 3);
            message.color = Color.green;
            message.text = "Craft Successfully !";
        }
        else
        {
            message.color = Color.red;
            message.text = "Not enought materials !";
        }
    }
}
