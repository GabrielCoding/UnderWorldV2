using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
    float barDisplay = 0.0f;
    Vector2 pos = new Vector2(20,10);
    Vector2 size= new Vector2(60,20);
    Texture2D progressBarEmpty;
    Texture2D progressBarFull;
    float health;

	// Use this for initialization
	void Start () {
	
	}


    void OnGUI()
    {

        // draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), progressBarEmpty);

        // draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), progressBarFull);
        GUI.EndGroup();

        GUI.EndGroup();

    }

    void healthDisplay(float health)
    {
        //barDisplay = health;
        this.health = health;
    }
    void Update()
    {
        // for this example, the bar display is linked to the current time,
        // however you would set this value based on your desired display
        // eg, the loading progress, the player's health, or whatever.
        barDisplay = health;
    }
}
