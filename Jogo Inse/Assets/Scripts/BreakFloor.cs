using System.Collections;
using UnityEngine;

public class BreakFloor : MonoBehaviour
{
    public float time;
    private Transform origin;
    public BoxCollider2D extraCollider;
    public GameObject particleExplosion;
    public AudioClip fxExplode;

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D (Collider2D other)

    {
        if (other.CompareTag("Player"))
        {
            origin = gameObject.transform;

            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                       gameObject.transform.position.y - .02f,
                                                       gameObject.transform.position.z);
            StartCoroutine(DestroyFloor(other));
        }

        
    }

    IEnumerator DestroyFloor (Collider2D other)
    {
        yield return new WaitForSeconds(time);

        if(other)
        {
            // Destroy(gameObject);
            // Instantiate(breakFloor,origin);




            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            extraCollider.enabled = false;
            gameObject.transform.position = origin.position;
            Instantiate(particleExplosion, gameObject.transform.position, gameObject.transform.rotation);
            AudioManager.instance.PlaySound(fxExplode);

            yield return new WaitForSeconds(3 * time);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            extraCollider.enabled = true;

        }

    }

}
