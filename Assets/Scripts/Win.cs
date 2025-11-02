using UnityEngine;

public class Win : MonoBehaviour
{
    private PossessItem pi;
    private bool goal;
    public bool Goal { get => goal; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Win_Area"))
        {
            goal = true;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Win_Area"))
        {
            goal = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Win_Area"))
        {
            goal = false;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pi = GetComponent<PossessItem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
