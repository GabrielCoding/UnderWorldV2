using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public float health;
    Animator knight;
    GameObject player;
    public GameObject blockUp;
    public GameObject blockDown;
    public GameObject blockRight;
    public GameObject blockLeft;
    public GameObject fireball;
    GameObject fireballClone;
    public GameObject healthBar;
    bool blockCD = true;
    double timeStampBlock = Time.time;
    double timeStampSwing = Time.time;
    //float

    void Start () {

        knight = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void applyDamage(float damage)
    {
        Debug.Log("applying damage to player");
        health -= damage;
        healthBar.SendMessage("healthDisplay", health);
    }
    // Update is called once per frame
    void Update() {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position gets a Vector3(x,y,z) of the character's current position. Here we're adding a vector to the player's current position to change it over time.
        //If you know of a better way feel free to change this

        /* logic: IF not hitting a movement key 
         * OR hitting left+right 
         * OR hitting up+down
         * THEN play idle
         * ELSE do movement
         * */
        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            knight.SetLayerWeight(1, 0f);
            //knight.Play("knight_idle");
        }
        else
        if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            knight.SetLayerWeight(1, 0f);
            //knight.Play("knight_idle");
        }
        else
        {
            knight.SetLayerWeight(1, .7f);
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-4f * Time.deltaTime, 0, 0);
                knight.Play("LinkWalkLeft");
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, 4f * Time.deltaTime, 0);
                if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                    knight.Play("LinkWalkUp");
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(0, -4f * Time.deltaTime, 0);
                if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                    knight.Play("LinkWalkDown");
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(4f * Time.deltaTime, 0, 0);
                knight.Play("LinkWalkRight");
            }
        }




        //Projectiles
        if (Input.GetMouseButtonDown(1) && blockCD) //right click
        {
            //knight.SetTrigger("Shoot");
            Vector2 myPos = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 direction = target - myPos;
            direction.Normalize();

            fireballClone = Instantiate(fireball, direction*2 + myPos, Quaternion.identity) as GameObject;

            //Debug.Log("Player pos: " + myPos.x + ", " + myPos.y + "\n" + "Fireball pos: " + target.x + ", " + target.y);
            
        }
        //Sword swing
        if (Input.GetMouseButtonDown(0) && blockCD)
        {
            
            float playerX = player.transform.position.x; //where the player is
            float playerY = player.transform.position.y;

            float xDiff = mousePosition.x - playerX; 
            float yDiff = mousePosition.y - playerY;

            timeStampSwing = Time.time + 0.55;

            if (Mathf.Abs(yDiff) > Mathf.Abs(xDiff))
            {
                if (yDiff >= 0)
                    knight.SetTrigger("SwingUp");
                else
                    knight.SetTrigger("SwingDown");
            }
            else
            {
                if (xDiff >= 0)
                    knight.SetTrigger("SwingRight");
                else
                    knight.SetTrigger("SwingLeft");
            }
        }
        //blocking
        if (Input.GetKeyDown(KeyCode.Space) && blockCD && (timeStampSwing <= Time.time))
        {
            timeStampBlock = Time.time + 2;
            blockCD = false;
            float playerX = player.transform.position.x; //where the player is
            float playerY = player.transform.position.y;

            float xDiff = mousePosition.x - playerX;
            float yDiff = mousePosition.y - playerY;
            if (Mathf.Abs(yDiff) > Mathf.Abs(xDiff))
            {
                if (yDiff >= 0)
                {
                    blockUp.SetActive(true);
                }
                else
                    blockDown.SetActive(true);
            }
            else
            {
                if (xDiff >= 0)
                    blockRight.SetActive(true);
                else
                    blockLeft.SetActive(true);
            }
        }
        if(blockCD == false && (timeStampBlock <= Time.time))
            {
            blockUp.SetActive(false);
            blockDown.SetActive(false);
            blockLeft.SetActive(false);
            blockRight.SetActive(false);
            blockCD = true;
            }
        healthBar.SendMessage("healthDisplay", health); // update healthbar
    }
}
