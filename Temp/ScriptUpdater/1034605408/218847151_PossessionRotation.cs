using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace BurglarGheist
{
public class PossessionRotation : MonoBehaviour
    {
    [SerializeField] private float rotationSpeed = 360f;
        private bool isRotating = false;
        [SerializeField] private int time;
        private Rigidbody2D rb;
        private float knockbackForce = 2.5f;
    

    private PossessItem posses;

    private void Awake() 
    {
        posses = GetComponent<PossessItem>();
    }

    private void StartRotation()
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
            rb = GetComponent<Rigidbody2D>();
            if (collision.gameObject.name == "Wall")
            {
                transform.Rotate(0, 0, 0);
                Vector2 knockbackDirection = collision.relativeVelocity.normalized;
                if (knockbackDirection.magnitude > 0.1f)
                {
                    knockbackDirection = -collision.GetContact(0).normal;
                }
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                Thread.Sleep(time);
                rotationSpeed = -1 * rotationSpeed;
            }
        

    }  
    }
}
    

