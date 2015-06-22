using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameNavigator : MonoBehaviour
{
    private CanvasRenderer panel;
    
    public JakeController player;
    public Text score;
    public Text highScore;

    public void Start()
    {
        //Makes The SplashScreen Invisible
        this.panel = this.GetComponent<CanvasRenderer>();
        this.panel.SetAlpha(0);
    }


    public void Update()
    {
        //Disposses of surplus object
        if (this.player == null)
        {
            Destroy(this.player);
            return;
        }
        //Fades in the SplashScreen upon GameOver
        else if (this.player.name == "DeadJake")
        {
            //Skips the fading of buttons since they have their own (using the Button class)
            if (this.panel.CompareTag("Button"))
            {
                UpdateScore();
            }
            else
            {
                UpdateScore();
                StartCoroutine("FadeIn");
            }
           
        }
    }

    //Smoothly fades in the menu
    private IEnumerator FadeIn()
    {
        for (float i = 0; i <= 1; i += 0.01f)
        {
            this.panel.SetAlpha(i);
            yield return null;
        }
    }

    //Sets Score info
    private void UpdateScore()
    {
        this.score.text = "Score : " + player.score.ToString();
        this.highScore.text = "HighScore : " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    //Redirecting to Main Menu
    public void Menu()
    {
        Application.LoadLevel("MainMenu");
    }
    
    //Replay
    public void Replay()
    {
        Application.LoadLevel("Game");
    }

    //Quiting the Game
    public void Quit()
    {
        Application.Quit();
    }

}
