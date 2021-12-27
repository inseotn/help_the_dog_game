using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    public float smoothTimeX;
    public float smoothTimeY;

    private Vector2 velocity;

    public static object main { get; internal set; }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();



    }


    void FixedUpdate()
    {
        if (player == null)
            return;

        float posX = Mathf.SmoothDamp(transform.position.x, player.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
