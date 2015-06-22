using UnityEngine;

public class TapToStart : MonoBehaviour
{
    public JakeController player;
    
    public void Update()
    {
        //Start Game upon click
        if (Input.GetButtonDown("Fire1"))
        {
            Time.timeScale = 1;
            this.player.forwardSpeed = 4; //Synch with FastMode class , initialSpeed field
            Destroy(this.gameObject);
        }
        //Wait for player to get ready to play
        else
        {
            Time.timeScale = 0;
        }
    }
}
