using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    float defaultSpeed;
    public Vector2 lastClickedPos;
    bool moving;
    Vector2 VTCP;
    public float corner;
    public Animator ani;

    public int maxHealth;
    public int currentHealth;
    public int maxFood;
    public int currentFood;

    public HealthBar healthBar;
    public HealthBar foodBar;

    public Collider2D myCollider;
    public Collider2D[] otherCollider;

    public GameObject houseMenu;
    public GameObject menu;
    public GameObject bag;
    public GameObject map;

    public GameObject deathPosition;
    public bool deathState;

    public Transform respawnPoint;
    public GameObject deathMenu;

    float timerHungry;
    float timerToHealWhenUnhungry;
    float  timerHealingState;
    public int hungryLevel;
    float timerToTakeDamageByHungry;
    float timerToHarvestResourcesDelay;

    public SpriteRenderer sr;

    public Text ResourcesHPDisplay;
    float timerToShowResourcesHP;

    public GameObject firstAction;

    [SerializeField]
    private Transform MinimapIcon;

    private static CharacterMovement instance;
    public static CharacterMovement MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CharacterMovement>();
            }

            return instance;
        }
    }

    private void Start()
    {
        firstAction.SetActive(true);
        menu.SetActive(false);
        bag.SetActive(false);
        map.SetActive(false);
        deathMenu.SetActive(false);

        deathState = false;

        for (int i = 0; i < otherCollider.Length; i++)
        {
            Physics2D.IgnoreCollision(myCollider, otherCollider[i], true);
        }
        corner = 0;
        lastClickedPos = Vector2.zero;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentFood = maxFood;
        foodBar.SetMaxHealth(maxFood);
        timerHungry = 100;
        timerToTakeDamageByHungry = 0;
        timerToHealWhenUnhungry = 0;
        timerHealingState = 0;
        timerToHarvestResourcesDelay = 0;
        timerToShowResourcesHP = 0;

        defaultSpeed = speed;


    }
    private void Update()
    {
        
        if (deathState == false)
        {
            if (Input.GetMouseButtonDown(0) && isScreenNotEistAnyMenu())
            {
                lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                moving = true;
                ////////////////////////////////
                VTCP = new Vector2(VTCP.x, VTCP.y);
                RaycastHit2D hit = Physics2D.Raycast(lastClickedPos, lastClickedPos);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "House")
                    {
                        hit.collider.gameObject.GetComponent<HouseManager>().OpenHouseMenu();
                    }
                    if (hit.collider.gameObject.tag == "Tree" && 
                        Vector2.Distance(this.gameObject.transform.position, hit.collider.gameObject.transform.position) <= 1.5 &&
                        timerToHarvestResourcesDelay <= 0)
                    {
                        timerToHarvestResourcesDelay = 1;
                        stopMoving();
                        ani.SetTrigger("Attack");
                        HarvestResourcesWithEachWeapon(hit.collider.gameObject, 1, 2, 5, 2);
                        showResourceHP(hit.collider.gameObject);
                    }
                    if (hit.collider.gameObject.tag == "Mineral" &&
                        Vector2.Distance(this.gameObject.transform.position, hit.collider.gameObject.transform.position) <= 1.5 &&
                        timerToHarvestResourcesDelay <= 0)
                    {
                        timerToHarvestResourcesDelay = 1;
                        stopMoving();
                        ani.SetTrigger("Attack");
                        HarvestResourcesWithEachWeapon(hit.collider.gameObject, 1, 1, 1, 8);
                        showResourceHP(hit.collider.gameObject);
                    }
                }
                ///////////////////////////////
            }
            if (moving && (Vector2)transform.position != lastClickedPos)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
            }
            else
            {
                moving = false;
            }

            checkCornerForSprites();
            foodbarWithHungry();
            toggleFeatures();
            balanceHealthAndFood();

            if(timerToHarvestResourcesDelay > 0)
            {
                timerToHarvestResourcesDelay -= Time.deltaTime;
            }

            if (timerHealingState > 0)
            {
                timerHealingState -= Time.deltaTime;
            }

            if (currentHealth <= 0)
            {
                stopMoving();
                die();
            }

            if (timerToShowResourcesHP > 0)
            {
                timerToShowResourcesHP -= Time.deltaTime;
            }
            else
            {
                ResourcesHPDisplay.text = "";

            }
  
        }

    }

    public void showResourceHP(GameObject resource)
    {
        timerToShowResourcesHP = 3;
        if (resource.GetComponent<ResourcesManager>().health > 0)
        {
            ResourcesHPDisplay.text = "Target HP: " + resource.GetComponent<ResourcesManager>().health + "|" + resource.GetComponent<ResourcesManager>().maxHealth;
            if (resource.tag == "Tree") {
                SoundManager.PlaySound("hit_tree");
            }
            if (resource.tag == "Mineral")
            {
                SoundManager.PlaySound("hit_rock");
            }
        }
        else
        {
            ResourcesHPDisplay.text = "Target was broken";
            if (resource.tag == "Tree")
            {
                SoundManager.PlaySound("break_tree");
            }
            if (resource.tag == "Mineral")
            {
                SoundManager.PlaySound("break_rock");
            }
        }
    }

    public void HarvestResourcesWithEachWeapon(GameObject resource, int handDamage, int swordDamage, int axeDamage,int pickaxeDamage)
    {
        switch (transform.GetComponent<CombatSystem>().weapon)
        {
            case 0:
                resource.GetComponent<ResourcesManager>().takeDamage(handDamage);
                break;
            case 1:
                resource.GetComponent<ResourcesManager>().takeDamage(swordDamage);
                break;
            case 2:
                resource.GetComponent<ResourcesManager>().takeDamage(axeDamage);
                break;
            case 3:
                resource.GetComponent<ResourcesManager>().takeDamage(pickaxeDamage);
                break;
        }
    }
    public bool isScreenNotEistAnyMenu()
    {
        if (bag.activeSelf == false && 
            map.activeSelf == false && 
            houseMenu.activeSelf == false && 
            deathMenu.activeSelf == false &&
            firstAction.activeSelf == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void die()
    {
        deathState = true;
        deathMenu.SetActive(true);
        speed = 0;
        Instantiate(deathPosition, transform.position, Quaternion.identity);
        sr.sortingOrder = 9;
        ani.SetBool("isMoving", false);
        ani.SetBool("Death", true);
        
        this.gameObject.GetComponent<Inventory>().dropAllItems();
        SoundManager.PlaySound("player_die");


    }

    public void respawn()
    {

        this.gameObject.transform.position = respawnPoint.position;


        speed = defaultSpeed;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        currentFood = maxFood;
        foodBar.SetHealth(currentFood);

        deathMenu.SetActive(false);
        deathState = false;

        this.gameObject.GetComponent<CombatSystem>().weapon = 0;

        ani.SetBool("Death", false);
        ani.SetTrigger("Attack");
        sr.sortingOrder = 3;

    }

    private void balanceHealthAndFood()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
        if (currentFood > maxFood)
        {
            currentFood = maxFood;
            foodBar.SetHealth(currentFood);
        }

    }

    public void foodbarWithHungry()
    {
        timerHungry -= hungryLevel * Time.deltaTime;

        if (timerHungry <= 0)
        {
            timerHungry = 100;
            if (currentFood > 0)
            {
                currentFood -= 1;
            }
            foodBar.SetHealth(currentFood);
        }

        if (currentFood <= 0)
        {
            timerToTakeDamageByHungry -= Time.deltaTime;
            if (timerToTakeDamageByHungry <= 0)
            {
                takeDamage(5);
                timerToTakeDamageByHungry = 3;
            }

            speed = defaultSpeed / 2;
        }
        else
        {
            speed = defaultSpeed;
        }

        if (currentFood >= 80 && currentHealth < 100 && timerHealingState <= 0)
        {
            if (timerToHealWhenUnhungry <= 0)
            {
                heal(1);
                timerToHealWhenUnhungry = 1;
            }
            else
            {
                timerToHealWhenUnhungry -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "campfire")
        {
            if (deathState == false)
            {
                takeDamage(1);
            }
        }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        timerHealingState = 6;
        SoundManager.PlaySound("player_take_damage");
    }
    public void heal(int amount)
    {
        currentHealth += amount;
        healthBar.SetHealth(currentHealth);
    }
    public void eatFood(int amount)
    {
        currentFood += amount;
        foodBar.SetHealth(currentFood);
    }

    void checkCornerForSprites()
    {
        VTCP = new Vector2(lastClickedPos.x - transform.position.x, lastClickedPos.y - transform.position.y);
        corner = Vector2.Angle(new Vector2(0, 1), VTCP);

        if (corner <= 45 && corner != 0)
        {
            ani.SetInteger("currentDirection", 0);
            MinimapIcon.eulerAngles = new Vector3(0, 0, 0);
        }
        if (corner >= 135)
        {
            ani.SetInteger("currentDirection", 2);
            MinimapIcon.eulerAngles = new Vector3(0, 0, 180);
        }
        if (corner < 135 && corner > 45)
        {
            if (lastClickedPos.x > transform.position.x)
            {
                ani.SetInteger("currentDirection", 1);
                MinimapIcon.eulerAngles = new Vector3(0, 0, 270);
            }
            else
            {
                ani.SetInteger("currentDirection", 3);
                MinimapIcon.eulerAngles = new Vector3(0, 0, 90);
            }

        }

        if (moving)
        {
            ani.SetBool("isMoving", true);
        }
        else
        {
            ani.SetBool("isMoving", false);
        }
    }

    public void toggleFeatures()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            firstAction.SetActive(false);
            if (bag.activeSelf)
            {
                bag.SetActive(false);    
            }
            else
            {
                bag.SetActive(true);
                map.SetActive(false);
                houseMenu.SetActive(false);
                SoundManager.PlaySound("open_features");
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (map.activeSelf)
            {
                map.SetActive(false);              
            }
            else
            {
                map.SetActive(true);
                bag.SetActive(false);
                houseMenu.SetActive(false);
                SoundManager.PlaySound("open_features");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (map.activeSelf == true)
            {
                map.SetActive(false);
            }
            if (bag.activeSelf == true)
            {
                bag.SetActive(false);
            }
            if (houseMenu.activeSelf == true)
            {
                houseMenu.SetActive(false);
            }
        }
    }

    public void stopMoving()
    {
        lastClickedPos = this.gameObject.transform.position;
    }

}
