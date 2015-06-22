using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    public Button button;
    public JakeController player;

    public void Start()
    {
        //Makes buttons inactive and invisible
        this.button.interactable = false;
        this.button.transition = Selectable.Transition.None;
    }


    public void Update()
    {
        //Disposses of surplus object
        if (this.player == null)
        {
            Destroy(this.player);
            return;
        }
        //Pops up the Splash Screen upon GameOver
        else if (this.player.name == "DeadJake")
        {  
            StartCoroutine("FadeIn");
            this.button.transition = Selectable.Transition.ColorTint;
            this.button.interactable = true;
            
        }
    }

    //Smoothly fades in the button
    private IEnumerator FadeIn()
    {
        for (float i = 0; i <= 1; i += 0.01f)
        {
            var color = this.button.colors;
            //button.colors.normalColor cannot be altered directly
            var colors = color.normalColor;
            colors.a = i;
            color.normalColor = colors;
            this.button.colors = color;
            yield return null;
        }
    }
}
