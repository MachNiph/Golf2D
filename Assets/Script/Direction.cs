using System.Collections;
using UnityEngine;

public class Direction : MonoBehaviour
{
    private GameObject ballGameObject; // Variable to hold the GameObject reference
    public Transform ballTransform;
    [SerializeField]
    private float rotationSpeed;
    private float rotationAngle;

    [SerializeField]
    private float colorChangeDelay;
    private SpriteRenderer[] spritesDirection;
    private int spriteDirectionIndex;
    private bool isMouseClickHeld;

    public Rigidbody2D rb;

    private void Start()
    {
        // Getting a reference to the GameObject this script is attached to
        ballGameObject = gameObject;
        spritesDirection = ballGameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
       

        if(!Input.GetMouseButton(0))
        {
            Rotation();
        }

        else
        {
           ChangeColor();
           isMouseClickHeld = true;
          
        }


        if(isMouseClickHeld && !Input.GetMouseButton(0))
        {
            float speed = spriteDirectionIndex + 18;
            float angleRadians = rotationAngle * Mathf.Deg2Rad;
            Debug.Log(angleRadians);
            Vector3 direction = new Vector3(Mathf.Cos(angleRadians),  Mathf.Sin(angleRadians),0).normalized;
            Debug.Log(direction);
            rb.velocity= direction * (speed*speed) * Time.deltaTime;
            isMouseClickHeld = false;
        }
        
    }


    void Rotation()
    {
        rotationAngle = transform.rotation.eulerAngles.z;


        rotationSpeed = rotationAngle > 90 ? -rotationSpeed : rotationAngle < 1 ? rotationSpeed : rotationSpeed;

        if (ballGameObject != null)
        {
            transform.RotateAround(ballTransform.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
        }
    }


    void  ChangeColor()
    {
        StartCoroutine(ChangeColorCoroutine());
    }


    IEnumerator ChangeColorCoroutine()
    {
        if (spritesDirection != null && spritesDirection.Length > 0)
        {
            int i = 0;
            bool movingForward = true;

            while (Input.GetMouseButton(0))
            {
                // If moving forward, set color to red
                if (movingForward)
                {
                    spritesDirection[i].color = Color.red;
                    i++;

                    // If reached the top, start moving backward
                    if (i >= spritesDirection.Length)
                    {
                        movingForward = false;
                        i = spritesDirection.Length - 1;
                    }
                }
                else // If moving backward, set color to white
                {
                    spritesDirection[i].color = Color.white;
                    i--;

                    // If reached the bottom, start moving forward
                    if (i < 0)
                    {
                        movingForward = true;
                        i = 0;
                    }
                }
                spriteDirectionIndex = i;
                yield return new WaitForSeconds(colorChangeDelay);
            }
        }
    }

    

}
