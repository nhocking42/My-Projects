using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldInHand : MonoBehaviour
{
    Inventory inventory;
    GameObject mainCharacter;
    public bool isUsing;
    public GameObject item;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        if (this.gameObject.transform.position == inventory.HandSlots[0].transform.position)
        {
            isUsing = true;
        }
        else
        {
            isUsing = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickToUse()
    {
        if (inventory.HandSlots[0].name == "")
        {
            Instantiate(this.gameObject, inventory.HandSlots[0].transform, false);
            inventory.HandSlots[0].name = this.gameObject.name;
            inventory.slots[this.gameObject.GetComponent<Spawn>().inSlot].name = "";
            Destroy(this.gameObject);

            switch (item.name)
            {
                case "Sword": mainCharacter.GetComponent<CombatSystem>().weapon = 1; break;
                case "Axe": mainCharacter.GetComponent<CombatSystem>().weapon = 2; break;
                case "PickAxe": mainCharacter.GetComponent<CombatSystem>().weapon = 3; break;
            }
            mainCharacter.GetComponent<CharacterMovement>().ani.SetTrigger("Attack");
            SoundManager.PlaySound("pickup_item");

        }

        if (isUsing == true)
        {
            Instantiate(item, mainCharacter.transform.position, Quaternion.identity);
            inventory.HandSlots[0].name = "";
            Destroy(this.gameObject);
            mainCharacter.GetComponent<CombatSystem>().weapon = 0;
            mainCharacter.GetComponent<CharacterMovement>().ani.SetTrigger("Attack");

        }
    }
}
