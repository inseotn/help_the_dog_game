using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enimy : MonoBehaviour
{


   
    private Rigidbody2D rb2d;
    public bool touchedWall;
    private bool facengRight;
    public float speed;
    private int layerMask;
    private int layerMaskLimit;
    public bool touchedLimit;
    public Transform groundCheck;

    

    //visual effects

    public GameObject particleDead;
    public GameObject particleDamage;
  

    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        touchedWall = false;
        facengRight = true;
      
        layerMask = 1 << LayerMask.NameToLayer("Ground");
        layerMaskLimit = 1 << LayerMask.NameToLayer("ObjectFront");
    }

    
    void Update()
    {
      

        if (touchedWall || touchedLimit)
        {

            facengRight = !facengRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            speed *= -1;

        }

        touchedWall = Physics2D.Linecast(transform.position, groundCheck.position, layerMask);
        touchedLimit = Physics2D.Linecast(transform.position, groundCheck.position, layerMaskLimit);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speed, 0f);

    }

   
}
