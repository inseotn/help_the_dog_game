using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformY : MonoBehaviour
{

    public float TopY;
    public float DownY;
    public float speed;


    private void FixedUpdate()
    {
        float posAuxY = gameObject.transform.position.y + speed;

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, posAuxY, gameObject.transform.position.z);

        if (posAuxY > TopY || posAuxY < DownY)
        {

            speed *= -1;


        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.transform.SetParent(gameObject.transform, true);
    }
    void OnCollisionExit2D(Collision2D col)
    {
        col.gameObject.transform.parent = null;
    }

}