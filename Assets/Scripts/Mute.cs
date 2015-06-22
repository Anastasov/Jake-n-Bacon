using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour
{
    private Image image;

    public Sprite muted;
    public Sprite playing;

    public void Start()
    {
        this.image = this.GetComponent<Image>();
        //Setting the correct Sprite
        if (AudioListener.volume == 0 && AudioListener.pause == true)
        {
            this.image.sprite = muted;
        }
        else
        {
            this.image.sprite = playing;
        }
    }

    public void Update()
    {
        //Mute
        if (Input.GetButtonDown("Mute") && AudioListener.pause == false)
        {
            this.image.sprite = muted;
            AudioListener.volume = 0;
            AudioListener.pause = true;  
            
        }
        //Play
        else if (Input.GetButtonDown("Mute") && AudioListener.pause == true)
        {
            this.image.sprite = playing;
            AudioListener.volume = 1;
            AudioListener.pause = false;
        }
    }
}
