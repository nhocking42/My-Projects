using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreEffect : MonoBehaviour
{
    public GameObject mainCharacter;
    public int restoredHPAmount;
    public int restoredFoodAmount;
    Inventory inventory;
    void Start()
    {
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void use()
    {
        mainCharacter.GetComponent<CharacterMovement>().heal(restoredHPAmount);
        mainCharacter.GetComponent<CharacterMovement>().eatFood(restoredFoodAmount);
        inventory.slots[this.gameObject.GetComponent<Spawn>().inSlot].name = "";
        Destroy(gameObject);

        if (restoredHPAmount > restoredFoodAmount)
        {
            SoundManager.PlaySound("health_potion");
        }
        else
        {
            SoundManager.PlaySound("eat");
        }
    }

}
