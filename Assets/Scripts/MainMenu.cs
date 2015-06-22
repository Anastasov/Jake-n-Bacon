using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private AudioSource audio;

    public void Start()
    {
        this.audio = this.GetComponent<AudioSource>();
    }
    
    public void StartGame()
    {
        Application.LoadLevel("Game");
    }
    public void Controls()
    {
        Application.LoadLevel("Controls");
    }
    public void Quit()
    {
        Debug.Log("GameQuit");
        Application.Quit();
    }
}
