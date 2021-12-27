using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //House
    public AudioClip fxHouse;
    public GameObject particleHouse; 

    //Walk
    private Rigidbody2D rb2D;
    [SerializeField]
    public int speed1;
    public int speed2;

    //Run
    public bool isRunning = false;

    //Jump
    private bool jumping;
    [SerializeField]
    public float jumpforce;
    public bool grounded;
    [SerializeField]
    private Transform groudedCheck;
    public int totalJump;
    private int maxJump;
    public AudioClip fxJump;

    // Life and Dead
    public bool isDead;
    public int life;
    public GameObject particleDead;
    public GameObject particleLife;
    public AudioClip fxLife;

    //Damage
    private SpriteRenderer sprite;
    public GameObject particleDamage;
    public AudioClip fxHurt;
   

    //BonePoint
    public int boneCounter;
    public int numberPointAdd;
    public int numberPointLess;
  
    public AudioClip fxPoint;

    //OutBackground
    private Vector3 playInitialPosition;
    public Vector3 lastGroundedPosition;

    //Change side face 
    private bool facingRigth;

     //Animation
    private Animator anim;

    void Start()
    {
        PlayerPrefs.SetInt("PlayerScore", 
            0);

        boneCounter = 0;

        //walk
        rb2D = GetComponent<Rigidbody2D>();
        facingRigth = true;

        //Jump
        grounded = true;
        maxJump = 0;

        //Life and Dead
        isDead = false;

        //Damage
        sprite = GetComponent<SpriteRenderer>();
        playInitialPosition = transform.position;


        //Animation
        anim = GetComponent<Animator>();

        //image life
        HudLife.instance.RefreshLife(life);

    }

    //OutBackground
    private void PlayerRespawn()
    {

        transform.position = playInitialPosition;

    }

    private void Update()
    {

        //Animation
        SetAnimations();

        //Jump

        grounded = Physics2D.Linecast(transform.position, groudedCheck.position, 1 << LayerMask.NameToLayer("Ground"));

     if (Input.GetButtonDown("Jump") && maxJump > 0)
        {
            jumping = true;
            grounded = false;

        }
        if(grounded)
        {
            maxJump = totalJump;
        }

        //Life and Dead

        if (isDead)
            return;
    }

    //Run
    private void CheckIfRunning()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }


    }


    private void FixedUpdate()
    {

        //Walk
        float move = Input.GetAxis("Horizontal");
        CheckIfRunning();

        if (isRunning == false)
        {
            rb2D.velocity = new Vector2(move * speed1, rb2D.velocity.y);

            //Animation Don't run/walk
            anim.SetBool("Run", (isRunning = false));
        }

        //Run
        else
        {
            rb2D.velocity = new Vector2(move * speed2, rb2D.velocity.y);

            //animação correr
            anim.SetBool("Run", (isRunning = true));

        }

        //Jump
        if (jumping)
        {
            maxJump--;
            jumping = false;
            
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
            rb2D.AddForce(new Vector2(0f, jumpforce));
            AudioManager.instance.PlaySound(fxJump);

        }

        //Change side face
        if ((move < 0f && facingRigth) || (move > 0f && !facingRigth))

        {

            facingRigth = !facingRigth;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);


        }

        //Life and Dead

        if (isDead)
            return;

    }

    //Animation

    private void SetAnimations()
    {
        anim.SetBool("Walk", (rb2D.velocity.x != 0f));

        //Jump
        anim.SetFloat("VelY", rb2D.velocity.y);
        anim.SetBool("JumpFall", !grounded);

    }

    IEnumerator DamageEffect()
    {

        sprite.enabled = false;
        yield return new WaitForSeconds(.1f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.1f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.1f);
        sprite.enabled = true;

      
    }

    private void DamagePlayer()
    {

        life--;
        

        if (life == 0)
        {
            isDead = true;
            Instantiate(particleDead, gameObject.transform.position, gameObject.transform.rotation);
            speed1 = 0;
            rb2D.velocity = new Vector2(0f, 0f);
            anim.SetTrigger("HeroDead");
            // After die, start in the same position
            Invoke("ReloadFase1", 2f);

           
           }
        else
        {
            StartCoroutine(DamageEffect());
            Instantiate(particleDamage, gameObject.transform.position, gameObject.transform.rotation);
            AudioManager.instance.PlaySound(fxHurt);
        }
        
        //Image life
        HudLife.instance.RefreshLife(life);

    }
     
    //MoreLife
    private void RestoreLife ()
    {
        if (life < 3)
        {
            life++;
            AudioManager.instance.PlaySound(fxLife);
            HudLife.instance.RefreshLife(life);
            Instantiate(particleLife, gameObject.transform.position, gameObject.transform.rotation);

            
        }
    }
    //Points Bone
    private void BoneAddPoints()
    {
        boneCounter = boneCounter + numberPointAdd;

        PlayerPrefs.SetInt("PlayerScore", boneCounter);
       
        AudioManager.instance.PlaySound(fxPoint);
    }

    private void BoneLessPoints()
    {
        if (boneCounter == 0)
        {

        }
        else
        {
            boneCounter = boneCounter - numberPointLess;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("House"))
        {
            Instantiate(particleHouse, gameObject.transform.position, gameObject.transform.rotation);
            Invoke("ReloadFase2", 4f);
           // AudioManager.instance.PlaySound(fxHouse);
            

        }

             if (isDead)
            return;

        if (other.CompareTag("Hole"))
        {
            PlayerRespawn(); 
            DamagePlayer();
            BoneLessPoints();
         }

        if (other.CompareTag("Enemy"))
        {
            DamagePlayer();
            BoneLessPoints();
        }

        //MoreLife
        if (other.CompareTag("Heart"))
        {
            RestoreLife();
            Object.Destroy(other.gameObject);
        }
        //Points
        if (other.CompareTag("Bone"))
        {
            BoneAddPoints();
            Object.Destroy(other.gameObject);
        }

        //CheckPoint

        if(other.CompareTag("CheckPoint"))
        {
            playInitialPosition = other.gameObject.transform.position;

        }

        

    }
 


    //After die, start in the same position
    public void ReloadFase1()
    {

        SceneManager.LoadScene("9GameOver", LoadSceneMode.Single);

    }

    public void ReloadFase2()
    {

        SceneManager.LoadScene("10Podium", LoadSceneMode.Single);
      
    }
 
}
