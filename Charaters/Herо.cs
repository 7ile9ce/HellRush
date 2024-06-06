using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Herо : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public int lives;
    [SerializeField] public float jumpForce;
    [SerializeField] public GameObject AttackHitBox;
    public bool isGrounded;
    public bool isAttacking = false;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public int extraJumps;
    public int extaJumpsValue;

    int playerObject, collideObject;
    bool jumpOffEnable = false;

    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sprite;

    public static Herо instance;
    public Text score, highScore;
    public int scoreCounter, highScoreCounter;

    void Awake()
    {
        instance = this;
    }
    
    public States State
    {
        get { return (States)anim.GetInteger("state");}
        set 
        { 
            anim.SetInteger("state", (int)value);
            Debug.Log("State set to: " + value);
        }
    }

        
        void Start()
    {
        extraJumps = extaJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        playerObject = LayerMask.NameToLayer("Player");
        collideObject = LayerMask.NameToLayer("Collide");
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        AttackHitBox.SetActive(false);
    }

    public void Update()
    {
        
        if (Input.GetButtonDown("Fire1") && !isAttacking && isGrounded == true)
        {
            isAttacking = true;

            int choose = Random.Range(1, 3);
            if(choose == 1)
            {
                State = States.attack1;
            }
            else 
            {
                State = States.attack2;
            }
           
            StartCoroutine(DoAttack());
        }
        if (isGrounded) 
        {
            if (!isAttacking)
            {
                State = States.idle;
            }
        }
        
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
        if (isGrounded == true)
        {
            extraJumps = extaJumpsValue;
        }
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            Jump();
            extraJumps--;
        }else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
        {
            Jump();
        }

        if (rb.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(playerObject, collideObject, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(playerObject, collideObject, false);
        }

        if (Input.GetKey (KeyCode.S))
        {
            StartCoroutine("JumpOff");
        }
        
        
        
    }
    


    IEnumerator JumpOff() 
    {
        jumpOffEnable = true;
        Physics2D.IgnoreLayerCollision(playerObject, collideObject, true);
        yield return new WaitForSeconds(0.4f);
        Physics2D.IgnoreLayerCollision(playerObject, collideObject, false);
        jumpOffEnable = false;
    }

    public void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (isGrounded && !isAttacking)
            {
                State = States.run;
            } 
            
            transform.localScale = new Vector3(1,1,1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (isGrounded && !isAttacking) 
            {
                State = States.run;
            }
            
            transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        CheckGround();
        anim.SetFloat ("vSpeed", rb.velocity.y);
        
    }
    
    
    public void Run()
    {
       if (isGrounded && !isAttacking) State = States.run;
    }
    public void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    public void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        
        if (!isGrounded) State = States.jump;
            
    }
    public void DisableControl()
    {
        enabled = false;
        rb.velocity = Vector2.zero;
    }

    
    public enum States
    {
        idle,
        run,
        jump,
        attack1,
        attack2,
        dead
    }

    IEnumerator DoAttack()
    {
        AttackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        AttackHitBox.SetActive(false);

        isAttacking = false;
    }

    public float GetStateAnimationLength(States state)
    {
        AnimatorClipInfo[] clipInfos = anim.GetCurrentAnimatorClipInfo(0);
        foreach (var clipInfo in clipInfos)
        {
            if (clipInfo.clip.name == state.ToString())
            {
                return clipInfo.clip.length;
            }
        }
        return 0;
    }

}
