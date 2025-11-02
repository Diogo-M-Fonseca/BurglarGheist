using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PossessItem : MonoBehaviour
{
    [SerializeField]private bool triggerActive = false;

    private GameObject item;
    private GameObject possessedItem;
    private Win win;
    private bool isPossessing = false;
    private int stolen_items;
    [SerializeField] [Range(0,2)] private int sceneToSwitch;
    [SerializeField] private int winCondition;
    public bool IsPossessing
    {
        get
        {
            return isPossessing;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            triggerActive = true;
            item = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            triggerActive = false;
            item = null;
        }
    }
    public void IncrementStolenItems()
    {
        stolen_items++;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        win = GetComponent<Win>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive && Input.GetKey(KeyCode.E) && !isPossessing)
        {
            Possess(item);
        }
        if (Input.GetKey(KeyCode.Q) && isPossessing)
        {
            Unpossess();
        }
        if (stolen_items >= winCondition)
        {
            SceneManager.LoadScene(sceneToSwitch);
        }
    }

    public void Possess(GameObject itemObject)
    {
        gameObject.GetComponent<Animator>().enabled = false;
        possessedItem = itemObject;
        SpriteRenderer playerSprite = gameObject.GetComponent<SpriteRenderer>();
        if (itemObject == null) return;

        // copy sprite info
        SpriteRenderer itemSprite = itemObject.GetComponent<SpriteRenderer>();

        if (playerSprite != null && itemSprite != null)
        {
            gameObject.transform.position = itemObject.transform.position;

            gameObject.GetComponent<SpriteRenderer>().sprite = itemSprite.sprite;
            playerSprite.color = itemSprite.color;
            playerSprite.flipX = itemSprite.flipX;
            playerSprite.flipY = itemSprite.flipY;
            playerSprite.transform.localScale = itemSprite.transform.localScale;
        }

        // copy collider(s) from item to player
        Collider2D itemCol = itemObject.GetComponent<Collider2D>();
        if (itemCol != null)
        {
            // remove existing colliders on player
            Collider2D[] existing = GetComponents<Collider2D>();
            foreach (Collider2D col in existing)
            {
                Destroy(col);
            }
            // copy new collider from item
            Collider2D newCol = itemCol;
            Collider2D playerCol = gameObject.AddComponent(newCol.GetType()) as Collider2D;
        }

        itemObject.SetActive(false);
        isPossessing = true;
    }

    public void Unpossess()
    {
        gameObject.GetComponent<Animator>().enabled = true;
        if (win.Goal == false)
        {
            possessedItem.SetActive(true);
            possessedItem.transform.position = gameObject.transform.position;
        }
        else if (win.Goal == true)
        {
            IncrementStolenItems();
            Destroy(possessedItem);
        }

        isPossessing = false;

        possessedItem = null;
        item = null;
        
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        Collider2D playerCollider = GetComponent<Collider2D>();
        Transform playerTransform = GetComponent<Transform>();

        SpriteRenderer[] originalSprite = GetComponentsInChildren<SpriteRenderer>(true);
        Collider2D[] originalCollider = GetComponentsInChildren<Collider2D>(true);
        Transform[] originalTransform = GetComponentsInChildren<Transform>(true);
        
        playerSprite.sprite = originalSprite[1].sprite;
        playerSprite.color = originalSprite[1].color;
        playerSprite.flipX = originalSprite[1].flipX;
        playerSprite.flipY = originalSprite[1].flipY;

        playerSprite.transform.localScale = originalTransform[1].transform.localScale;
        playerTransform.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        // remove existing colliders on player
        Collider2D[] existing = GetComponents<Collider2D>();
        foreach (Collider2D col in existing)
        {
            Destroy(col);
        }
        playerCollider = gameObject.AddComponent(originalCollider[1].GetType()) as Collider2D;

    }
}
