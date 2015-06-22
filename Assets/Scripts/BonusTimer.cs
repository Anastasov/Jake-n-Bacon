using UnityEngine;
using UnityEngine.UI;

public class BonusTimer : MonoBehaviour
{
    private const int pointsObtainedInBonus = 220; // Synch with JakeController class, pointsObtainedInBonus field
    private int timeLeft;
    private Text text;
    
    public JakeController player;

    public void Start()
    {
        this.text = this.GetComponent<Text>();
    }
    
    public void Update()
    {
        //Disposses of surplus object
        if (this.player == null)
        {
            Destroy(this.player);
        }
        else
        {
            //Check if in Bonus mode
            if (this.player.BonusTimer == 0)
            {
                this.timeLeft = 0;
            }
            else if (this.player.BonusTimer > 0)
            {
                this.timeLeft = pointsObtainedInBonus - (int)this.player.BonusTimer;
                //Display Time Left in Bonus mode (in seconds)
                if (timeLeft <= 0)
                {
                    this.text.text = "";
                }
                else
                {
                    this.text.text = this.timeLeft.ToString();
                }             
            }
        }
        
    }
}
