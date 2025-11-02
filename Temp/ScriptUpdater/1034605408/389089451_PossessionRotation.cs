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
        private float knockbackForce = 15f;
    

    private PossessItem posses;

    public void Awake()
    {
        posses = GetComponent<PossessItem>();
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
        rb = GetComponent <Rigidbody2D>();
            if (collision.gameObject.name == "Wall")
            {
                transform.Rotate(0, 0, 0);
                rb.AddForce(rb.linearVelocity * knockbackForce, ForceMode2D.Impulse);
                Thread.Sleep(time);
                rotationSpeed = -1 * rotationSpeed;
            }
        }
    }  
}

    

