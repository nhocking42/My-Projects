using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {


    private Inventory inventory;
    public int index;
    public GameObject cross;
    public Text text;


    private void Start()
    {
        gameObject.name = "";
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        inventory.HandSlots[0].name = "";
        text.text = this.gameObject.name;
    }

    private void Update()
    {
        text.text = this.gameObject.name;
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().inSlot = index;
        }

        if (transform.childCount <= 0)
        {
            inventory.items[index] = 0;
        }

        if (gameObject.name == "")
        {
            cross.SetActive(false);
        }
        else
        {
            cross.SetActive(true);
        }
    }

    public void Cross() {

        foreach (Transform child in transform) {
            child.GetComponent<Spawn>().SpawnItem();
            GameObject.Destroy(child.gameObject);
            gameObject.name = "";
        }
    }


}
