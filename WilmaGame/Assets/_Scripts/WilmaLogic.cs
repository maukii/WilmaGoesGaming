using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilmaLogic : MonoBehaviour
{

    public Transform playerPos;
    public bool left, up;

    [SerializeField] float maxDistance, speed = 3;


    void FixedUpdate()
    {
        if ((transform.position.x - playerPos.position.x) < 0)
        {
            left = false;
        }
        else
        {
            left = true;
        }

        if ((transform.position.y - playerPos.position.y) < 0)
        {
            up = true;
        }
        else
        {
            up = false;
        }

        float distanceX = Mathf.Abs(transform.position.x - playerPos.position.x);
        float distanceY = Mathf.Abs(transform.position.y - playerPos.position.y);

        if (distanceX > maxDistance)
        {
            if (left)
            {
                transform.position += -Vector3.right * speed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * speed * Time.deltaTime;

            }
        }

        if (up)
        {
            if (distanceY > maxDistance / 3)
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
        }
        else if (!up)
        {
            if (distanceY > maxDistance)
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
        }


    }
}