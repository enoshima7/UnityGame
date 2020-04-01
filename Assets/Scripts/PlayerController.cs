using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigid2d;
    public float speed_x_constrain;
    public GameObject bulletPrefeb;
    public Image hp_bar;
    public int max_hp = 0;
    public int hp = 0;
    // Start is called before the first frame update
    void Start()
    {
        max_hp = 10;
        hp = max_hp;
        rigid2d = this.gameObject.GetComponent<Rigidbody2D>();
        print("start");
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //this.gameObject.transform.position += new Vector3(speed, 0, 0);
            //rigid2d.AddForce(new Vector2(5, 0), ForceMode2D.Force);
            rigid2d.velocity = new Vector2(speed_x_constrain, rigid2d.velocity.y);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //this.gameObject.transform.position -= new Vector3(speed, 0, 0);
            //rigid2d.AddForce(new Vector2(-5, 0), ForceMode2D.Force);
            rigid2d.velocity = new Vector2(-speed_x_constrain, rigid2d.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
        if (rigid2d.velocity.x > speed_x_constrain)
        {
            rigid2d.velocity = new Vector2(speed_x_constrain, rigid2d.velocity.y);
        }
        if (rigid2d.velocity.x < -speed_x_constrain)
        {
            rigid2d.velocity = new Vector2(-speed_x_constrain, rigid2d.velocity.y);
        }
        //fire
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(bulletPrefeb, this.transform.position, Quaternion.identity);
        }
        hp_bar.transform.localScale = new Vector3((float)hp / (float)max_hp, hp_bar.transform.localScale.y, hp_bar.transform.localScale.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            hp -= 1;
            print(collision.gameObject.name);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "portal")
        {
            collision.gameObject.GetComponent<Portal>().ChangeScene();
        }
    }
}
