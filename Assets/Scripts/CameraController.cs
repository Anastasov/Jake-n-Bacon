using UnityEngine;
using UnityEngine.UI;
using System;

public class CameraController : MonoBehaviour
{
    public JakeController player;
    public GameObject startScreen;

    private float offsetX;

    public void Start()
    {
        //Sets the correct distance between the loop collider and the player
        this.offsetX = this.transform.position.x - this.player.transform.position.x;
    }

    public void Update()
    {
        //Disposses of surplus object
        if (this.player == null)
        {
            Destroy(this.player);
            return;
        }
        //Follows the Player
        else if (this.player.name == "Jake")
        {
            var position = this.transform.position;
            position.x = this.player.transform.position.x + offsetX;
            this.transform.position = position;
        }
    }
}
