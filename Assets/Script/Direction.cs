using System.Collections;
using UnityEngine;

public class Direction : MonoBehaviour
{
    private GameObject ballGameObject; // Variable to hold the GameObject reference
    public Transform ballTransform;
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float colorChangeDelay;
    private SpriteRenderer[] spritesDirection;
    


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
        }
    }


    void Rotation()
    {
        float angle = transform.rotation.eulerAngles.z;

        Debug.Log(angle);

        rotationSpeed = angle > 90 ? -rotationSpeed : angle < 1 ? rotationSpeed : rotationSpeed;

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

        if (spritesDirection != null)
        {
            for (int i = 0; i < spritesDirection.Length; i++)
            {
                spritesDirection[i].color = Color.red;
                yield return new WaitForSeconds(colorChangeDelay);
            }
        }
    }
}
