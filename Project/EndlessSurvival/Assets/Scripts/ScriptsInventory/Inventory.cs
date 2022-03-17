using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {


    public int[] items;
    public GameObject[] slots;
    public GameObject[] HandSlots;

    public GameObject[] itemIDList;

    public int[] itemsInInventory; // items list of inventory in ID

    public int amountItem;
    public bool isCleanInventory;
    bool checkOn;
    private static Inventory instance;
    public static Inventory MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Inventory>();
            }

            return instance;
        }

    }

    private void Start()
    {
        checkOn = false;
        clean();

        int starterToolID=999;
        for (int i=0; i<itemIDList.Length; i++)
        {
            if (itemIDList[i].name == "PickAxe")
            {
                starterToolID = i;
                break;
            }
        }
        Instantiate(itemIDList[starterToolID].GetComponent<Pickup>().itemButton, slots[0].transform, false);
        slots[0].name = itemIDList[starterToolID].name;
        items[0] = 1;

    }
    private void Update()
    {
        isCleanInventory = checkCleanInventory();
        if (checkOn && isCleanInventory== false)
        {
            sorting();
        }
        if (isCleanInventory)
        {
            checkOn = false;
        }
    }

    public void clean()
    {
        foreach (Transform child in HandSlots[0].transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        HandSlots[0].name = "";

        for (int i=0; i<slots.Length; i++)
        {
            foreach (Transform child in slots[i].transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            slots[i].name = "";
            items[i] = 0;
        }
        for (int i = 0; i < itemsInInventory.Length; i++)
        {
            itemsInInventory[i] = 999;
        }

        this.gameObject.GetComponent<CombatSystem>().weapon = 0;

    }

    public void parseItemToID() // function để save inventory
    {
        for (int i = 0; i < itemsInInventory.Length; i++) // clear list
        {
            itemsInInventory[i] = 999;
        }

        int tempSlot = 0;
        for (int i=0; i<slots.Length; i++)
        {
            if (slots[i].name != "")
            {
                for (int a=0; a<itemIDList.Length; a++)
                {
                    if (itemIDList[a].name == slots[i].name)
                    {
                        itemsInInventory[tempSlot] = a;
                        tempSlot++;
                        break;
                    }
                }
            }
        }

        if (HandSlots[0].name != "")
        {
            for (int a = 0; a < itemIDList.Length; a++)
            {
                if (HandSlots[0].name == itemIDList[a].name)
                {
                    itemsInInventory[tempSlot] = a;
                    tempSlot++;
                    break;
                }
            }
        }
    }

    public void parseIDtoItem() // function để load inventory đã save lên game
    {
        for (int i=0; i<itemsInInventory.Length; i++)
        {
            if (itemsInInventory[i] != 999)
            {
                Instantiate(itemIDList[itemsInInventory[i]], transform.position, Quaternion.identity);
            }
            else
            {
                break;
            }
        }
    }

    public void dropAllItems()
    {

        for (int i = 0; i<HandSlots.Length; i++)
        {
            foreach (Transform child in HandSlots[i].transform)
            {

                child.GetComponent<Spawn>().SpawnItem();
                GameObject.Destroy(child.gameObject);
            }
            HandSlots[i].name = "";
        }
        if (amountItem > 0)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].name != "")
                {
                    foreach (Transform child in slots[i].transform)
                    {
                        child.GetComponent<Spawn>().SpawnItem();
                        GameObject.Destroy(child.gameObject);
                    }
                    slots[i].name = "";
                }
            }
        }
        sortingStateOn();

    }

    public void sortingStateOn()
    {
        checkOn = true;
    }
    public void sorting()
    {
        Debug.Log("SORT ! ! !");
        for (int i = 0; i < slots.Length - 1; i++)
        {
            if (slots[i].name == "" && slots[i + 1].name != "")
            {
                Instantiate(slots[i + 1].transform.GetChild(0), slots[i].transform, false);

                slots[i].name = slots[i + 1].name;
                foreach (Transform child in slots[i + 1].transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                slots[i + 1].name = "";
                items[i] = items[i + 1];
            }
        }
    }

    public bool checkCleanInventory()
    {
        amountItem = 0;
        for (int i=0; i<slots.Length; i++)
        {
            if (slots[i].name != "")
            {
                amountItem++;
            }
        }
        for (int i = 0; i < amountItem; i++)
        {
            if (slots[i].name == "")
            {
                return false;
            }
        }
        return true;

    }

    public void removeItem(string item, int amount)
    {
        if (checkItemExistInInventory(item, amount))
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].name == item && amount > 0)
                {
                    foreach (Transform child in slots[i].transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }
                    slots[i].name = "";
                    amount--;
                }
            }
        }
    }

    public bool checkItemExistInInventory(string itemName, int amount)
    {
        int count = 0;
        for (int i=0; i<slots.Length; i++)
        {
            if (slots[i].name == itemName)
            {
                count++;
            }
        }

        
        if (count >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
