using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] LayerMask blockLayer;

    public enum DIRECTION_TYPE
    {
        STOP,
        LEFT,
        RIGHT,
    }
    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    float speed;
    Rigidbody2D rigidbody2D;
    Animator animator;

    float jumpPower = 500;
    bool isDead = false;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }
        float x = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(x));

        if (x == 0)
        {
            direction = DIRECTION_TYPE.STOP;
        }
        else if (x > 0)
        {
            direction = DIRECTION_TYPE.RIGHT;
        }
        else if (x < 0)
        {
            direction = DIRECTION_TYPE.LEFT;
        }

        if (IsGround())
        {
            if (Input.GetKeyDown("space"))
            {
                Jump();
                animator.SetBool("IsJumping", true);
            }
            else
            {
                animator.SetBool("IsJumping", false);
            }
        }
    
    }
    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 3f;
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -3f;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    void Jump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpPower);
    }

    bool IsGround()
    {
        Vector3 leftStartPoint = transform.position - transform.right * 0.3f;
        Vector3 endPoint = transform.position - transform.up * 0.1f;
        Vector3 rightStartPoint = transform.position + transform.right * 0.3f;

        // 確認用
        Debug.DrawLine(leftStartPoint, endPoint);
        Debug.DrawLine(rightStartPoint, endPoint);

        return Physics2D.Linecast(leftStartPoint, endPoint, blockLayer) || Physics2D.Linecast(rightStartPoint, endPoint, blockLayer);
    }

    //trigerにぶつかった時に自動で発動される関数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
        {
            return;
        }
        if(collision.gameObject.tag == "Trap")
        {
            PlayerDeath();
            //Debug.Log("game over");
        }
        if (collision.gameObject.tag == "Finish")
        {
            gameManager.GameClear();
            //Debug.Log("game clear");
        }
        if(collision.gameObject.tag == "Item")
        {
            //アイテム取得
            collision.gameObject.GetComponent<ItemManager>().GetItem();

        }
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyManager enemy = collision.gameObject.GetComponent<EnemyManager>();
            if (this.transform.position.y +0.2f > enemy.transform.position.y )
            {
                enemy.DestroyEnemy();
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
                Jump();
            }
            else
            {
                
                PlayerDeath();
            }
        }

        if (collision.gameObject.tag == "Jump")
        {
            Jump();
            Jump();
        }
    }

    void PlayerDeath()
    {
        rigidbody2D.velocity = new Vector2(0, 0);
        Jump();
        animator.Play("PlayerDeathAnimation");
        isDead = true;
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        Destroy(boxCollider2D);
        gameManager.GameOver();
    }
}