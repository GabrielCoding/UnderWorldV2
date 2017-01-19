using UnityEngine;
using System.Collections;

public class SwordDamage : MonoBehaviour {
    public float damage;
    // Use this for initialization

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.SendMessage("applyDamage", damage);
        }
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
