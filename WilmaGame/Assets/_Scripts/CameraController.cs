using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float smoothTime = 5f;
    public float extraSpace = 3f;
    Vector3 velocity = Vector3.zero;

    Vector3 wantedPosition;

    PlayerMovement PM;

    void Start()
    {
        wantedPosition = player.transform.position;
        wantedPosition.z = -10;

        PM = player.GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        wantedPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        if (PM.inputH != 0 && !PM.movingV)
        {
            wantedPosition.x += PM.inputH * extraSpace;
        }

        if (PM.inputV != 0 && !PM.movingH)
        {
            wantedPosition.y += PM.inputV * extraSpace;
        }

        transform.position = Vector3.SmoothDamp(transform.position, wantedPosition, ref velocity, smoothTime);
    }

}
