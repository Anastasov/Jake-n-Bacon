using UnityEngine;
using System.Collections;

public class MuteButton : MonoBehaviour
{
    private SpriteRenderer sprite;

    public Sprite playing;
    public Sprite muted;
   
    public void Start()
    {
        this.sprite = this.GetComponent<SpriteRenderer>();
        //Setting the correct sprite
        if (AudioListener.volume == 0 && AudioListener.pause == true)
        {
            this.sprite.sprite = muted;
        }
        else
        {
            this.sprite.sprite = playing;
        }
    }

    
    public void Update()
    {
        //Mute
        if (Input.GetButtonDown("Mute") && AudioListener.pause == false)
        {
            this.sprite.sprite = muted;
            AudioListener.volume = 0;
            AudioListener.pause = true;      
        }
        //Play
        else if (Input.GetButtonDown("Mute") && AudioListener.pause == true)
        {
            this.sprite.sprite = playing;
            AudioListener.volume = 1;
            AudioListener.pause = false;    
        }
    }
}
