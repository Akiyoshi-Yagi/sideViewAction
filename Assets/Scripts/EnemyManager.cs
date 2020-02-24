using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    [SerializeField] GameObject deathEffect;

    public enum DIRECTION_TYPE
    {
        STOP,
        LEFT,
        RIGHT,
    }
    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;
    float speed;
    Rigidbody2D rigidbody2D;

    //float jumpPower = 400;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        direction = DIRECTION_TYPE.RIGHT;
    }

    void Update()
    {
        if (!IsGround())
        {
            ChangeDirection();
        }
      
    }

    bool IsGround()
    {
        Vector3 startVec = transform.position + transform.right * 0.5f * transform.localScale.x;
        Vector3 endVec = startVec - transform.up * 0.5f;
        Debug.DrawLine(startVec, endVec);
        return Physics2D.Linecast(startVec,endVec,blockLayer);
    }

    void ChangeDirection()
    {
        if (direction == DIRECTION_TYPE.RIGHT)
        {
            direction = DIRECTION_TYPE.LEFT;
        }
        else if (direction == DIRECTION_TYPE.LEFT)
        {
            direction = DIRECTION_TYPE.RIGHT;
        }
    }

    void FixedUpdate()
    {
        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 3;
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -3;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    public void DestroyEnemy()
    {
        //prefabを発生させる
        Instantiate(deathEffect, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }

    //void Jump()
    //{
    //    rigidbody2D.AddForce(Vector2.up * jumpPower);
    //}


    /*
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
        if (collision.gameObject.tag == "Trap")
        {
            gameManager.GameOver();
            Debug.Log("game over");
        }
        if (collision.gameObject.tag == "Finish")
        {
            gameManager.GameClear();
            Debug.Log("game clear");
        }
        if (collision.gameObject.tag == "Item")
        {
            //アイテム取得
            collision.gameObject.GetComponent<ItemManager>().GetItem();

        }
    }
    */
}
