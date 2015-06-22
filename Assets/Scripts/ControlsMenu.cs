using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public GameObject camera;


    public void Next()
    {
        Application.LoadLevel("Controls - Bonus Mode");
    }

    public void Back()
    {
        Application.LoadLevel("Controls");
   }
    public void MainMenu()
    {
        Application.LoadLevel("MainMenu");
    }

}
