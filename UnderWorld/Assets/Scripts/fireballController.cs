using UnityEngine;
using System.Collections;

public class fireballController : MonoBehaviour
{
    public float speed;
    public float damage;
    GameObject player;
    Vector2 target;
    Vector2 myPos;
    Vector2 direction;
    public GameObject fireball;
    float frame = 0;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myPos = new Vector2(player.transform.position.x, player.transform.position.y);
        //target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        target = new Vector2(this.transform.position.x, this.transform.position.y);
        direction = target - myPos;
        direction.Normalize();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.SendMessage("applyDamage", damage);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        frame++;
        if (frame == 40)
        {
            Destroy(gameObject);
        }
        /*myPos.x = player.transform.position.x;
        myPos.y = player.transform.position.y;
        direction = target - myPos;
        direction.Normalize();*/

        //fireball.GetComponent<Rigidbody2D>().position =  myPos +  direction * speed * Time.deltaTime;
        //fireball.GetComponent<Rigidbody2D>().AddForce(direction * speed);

        /*
        fireball.GetComponent<Rigidbody2D>().velocity = direction * speed;*/
       // Vector2 dir = new Vector2(this.transform.position.x, this.transform.position.y);
        fireball.GetComponent<Rigidbody2D>().velocity = direction * speed * 2;

        //velocity seems to be the go to right now. Position was
        // wonky and addforce had a gradual increase in velocity.
        //i'm leaving the others commented out till projectiles
        // come into a more final state. -Gabe

    }
}
