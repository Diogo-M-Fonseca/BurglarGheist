using Unity.VisualScripting;
using UnityEngine;

public class PossessItem : MonoBehaviour
{
    [SerializeField]private bool triggerActive = false;
    private GameObject item;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            triggerActive = true;
            item = collision.gameObject;
            Debug.Log("Trigger Active");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            triggerActive = false;
            item = null;
            Debug.Log("Trigger Off");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive && Input.GetKey(KeyCode.E))
        {
            Possess(item);
        }
    }

    public void Possess(GameObject itemObject)
    {
        if (itemObject == null) return;

        // copy sprite info
        SpriteRenderer playerSprite = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer itemSprite = itemObject.GetComponent<SpriteRenderer>();

        if (playerSprite != null && itemSprite != null)
        {
            gameObject.transform.position = itemObject.transform.position;

            playerSprite.sprite = itemSprite.sprite;
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
        Destroy(itemObject);
    }

}
