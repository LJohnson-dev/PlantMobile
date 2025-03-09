using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBounce : MonoBehaviour
{

    private float growthRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            /* Vector3 startPos = transform.position;
            Vector3 newPos = startPos + transform.up * Time.deltaTime;
            this.transform.position = newPos; */


            // Check if the touch phase is Began or Moved
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                // Convert the touch position to world space
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;

                // Check if the touch is over the sprite
                Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);
                if (hitCollider != null && hitCollider.gameObject == gameObject)
                {
                    // Increase the size of the sprite
                    transform.localScale += Vector3.one * growthRate * Time.deltaTime;








                    /* Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    touchPosition.z = 0;
                    transform.position = touchPosition; */
                }
            }

        }
    }


    void IncreaseSize()
    {

    }
}
