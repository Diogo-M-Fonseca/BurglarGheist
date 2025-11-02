using UnityEngine;

public class PossessionRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 360f;
    private bool isPossessing = false;
    private bool isRotating = false;


    public void setPossessing(bool value)
    {
        isPossessing = value;
    }


    public void StartRotation()
    {
        if (isPossessing)
        {
            isRotating = true;
        }
    }


    public void StopRotation()
    {
        isRotating = false;
        
    }


    void Update()
    {
        if (isRotating && isPossessing) 
        {
            transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
        }
    }
}
