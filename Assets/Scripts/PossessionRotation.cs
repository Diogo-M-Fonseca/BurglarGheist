using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace BurglarGheist
{
public class PossessionRotation : MonoBehaviour
    {
    [SerializeField] private float rotationSpeed = 180f;
    private bool isRotating = false;
    [SerializeField] private int time;
    private Rigidbody2D rb;
    [SerializeField] private Transform TeleportTarget;
    private PossessItem posses;
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        posses = GetComponent<PossessItem>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartRotation()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
    }


    void Update()
    {

        if (posses.IsPossessing)
        {
            StartRotation();
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        changeDirection(collision);
    }


        void changeDirection(Collision2D collision)
        {

            if (posses.IsPossessing == true)
            {
                rb = GetComponent<Rigidbody2D>();
                if (collision.gameObject.name == "Wall")
                {
                    if (TeleportTarget != null)
                    {
                        transform.position = TeleportTarget.position;
                        if (rb != null)
                        {
                            rb.linearVelocity = Vector2.zero;
                        }
                        StartCoroutine(FlickerSprite());
                    }
                    Thread.Sleep(time);
                    rotationSpeed = -1 * rotationSpeed;
                }
            }
        }
        private System.Collections.IEnumerator FlickerSprite()
        {
            float flickerDuration = 1f; 
            float flickerInterval = 0.1f; 
            float elapsedTime = 0f;

            while (elapsedTime < flickerDuration)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled; 
                yield return new WaitForSeconds(flickerInterval);
                elapsedTime += flickerInterval;
            }

            spriteRenderer.enabled = true;
        }
    }  
}

    

