using UnityEngine;

public class FastMode : MonoBehaviour
{

    private float initialSpeed = 4; //Synch with TapToStart class , this.player.forwardSpeed set
    private float timeElapsed;
    private int level = 1;
    private const int maxLevels = 152; // Synch with JakeController class , maxLevels field
    
    public JakeController player;

    public void Update()
    {
        if (this.player == null)
        {
            Destroy(this.player);
        }
        else if (this.player.name == "Jake")
        {
            if (Input.GetButton("IncreaseSpeed"))
            {
                this.player.forwardSpeed = 15;
            }
            else if (Input.GetButtonUp("IncreaseSpeed"))
            {
                this.player.forwardSpeed = initialSpeed;
            }
        }   
    }
    public void FixedUpdate()
    {
        //SAME GAME LOGIC AS JakeController IN ORDER TO PERSERVE THE PACE
        //Increases Difficulty
        this.timeElapsed += Time.deltaTime;
        if (timeElapsed >= level)
        {
            //In the first 12 sec forwardSpeed will increase by 3 ( 12 * 0.25) || Math Calculations
            if (level <= 12)
            {
                this.level++;
                this.initialSpeed += 0.25f;
            }
            //In the next 20 sec forwardSpeed will increase by 2 ( 20 * 0.1) || Math Calculations
            else if (level > 12 && level <= 32)
            {
                this.level++;
                this.initialSpeed += 0.1f;
            }
            //In the next (maxLevel - 32 ) forwardSpeed will increase with te equation [(maxLevels - 32) * 0.05] || Math Calculations
            else if (level > 32 && level <= maxLevels)
            {
                this.level++;
                this.initialSpeed += 0.05f;
            }
        }
    }
}
