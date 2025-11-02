using System.Runtime.InteropServices.WindowsRuntime;
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
        rb = GetComponent<Rigidbody2D>();
        if (collision.gameObject.name == "Wall")
        {
            // Parar a rotação
            transform.Rotate(0, 0, 0);
            
            // Obter a direção normal da parede (aponta para fora)
            Vector2 wallNormal = collision.GetContact(0).normal;
            
            // Knockback na direção oposta à parede
            rb.linearVelocity = Vector2.zero; // Resetar velocidade
            rb.AddForce(wallNormal * knockbackForce, ForceMode2D.Impulse);
            
            Thread.Sleep(time);
            rotationSpeed = -1 * rotationSpeed;
        }
    } 
    }
}
    

