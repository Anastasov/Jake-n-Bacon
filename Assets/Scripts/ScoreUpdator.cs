using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdator : MonoBehaviour
{
    private Text score;

    public JakeController player;
    
    public void Start()
    {
        this.score = this.GetComponent<Text>();
    }
 
    public void Update()
    {
        //Disposses of surplus object
        if (this.player == null)
        {
            Destroy(this.player);
            return;
        }
        //Show score when the player is Alive (other wise the Splash Screen will show it)
        else if (this.player.name == "Jake")
        {
            //No need for a score of 0
            if (this.player.score == 0)
            {
                this.score.text = "";
            }
            //Updates the score
            else
            {
                if (this.player.score > 0)
                {
                   this.score.text = this.player.score.ToString();
                }
            }
        }
    }
}
