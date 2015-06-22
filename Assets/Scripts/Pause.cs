using UnityEngine;

public class Pause : MonoBehaviour
{
    public JakeController player;
    public AudioSource audio;
    public AudioSource bonusMusic;

    public void Update()
    {
        if (this.player == null)
        {
            Destroy(this.player);
        }
        else if (this.player.forwardSpeed > 0)
        {
            if (Input.GetButtonDown("Pause") && Time.timeScale == 1)
            {
                Time.timeScale = 0;
                this.audio.Pause();
                this.bonusMusic.Pause();
            }
            else if (Input.GetButtonDown("Pause") && Time.timeScale == 0)
            {
                Time.timeScale = 1;
                this.audio.UnPause();
                this.bonusMusic.UnPause();
            }
        }
    }
}
