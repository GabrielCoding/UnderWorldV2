
using UnityEngine;
using System.Collections;

public class CrabMovement : MonoBehaviour {
    GameObject player;
    GameObject weapons;
    public float damage;
    public float health;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("sending message to player for health");
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("applyDamage", damage);
            Destroy(gameObject);
        }
    }

    void applyDamage(float damage)
    {
        health -= damage;
    }

    /*void onTriggerEnter(Collision other)
    {
        if (other.gameObject.tag == "playerWeapon")
        {
           health -= 5;
           if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }*/
    // Update is called once per frame
    void Update () {
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 1 * Time.deltaTime);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
	}
}
