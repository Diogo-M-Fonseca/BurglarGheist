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
        SpriteRenderer playerSprite = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer itemSprite = itemObject.GetComponent<SpriteRenderer>();

        gameObject.transform.position = itemObject.transform.position;

        playerSprite.sprite = itemSprite.sprite;
        playerSprite.color = itemSprite.color;
        Destroy(itemObject);
    }

}
