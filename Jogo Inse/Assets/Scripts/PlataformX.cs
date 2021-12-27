using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformX : MonoBehaviour
{

    public float TopX;
    public float DownX;
    public float speed;

   

    private void FixedUpdate()
    {
        float posAuxY = gameObject.transform.position.x + speed;

        gameObject.transform.position = new Vector3(posAuxY, gameObject.transform.position.y, gameObject.transform.position.z);

        if (posAuxY > TopX || posAuxY < DownX)
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
