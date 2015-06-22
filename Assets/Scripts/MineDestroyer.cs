using UnityEngine;


public class MineDestroyer : MonoBehaviour
{
    public GameObject explosion;
    public AudioClip explosionSound;

    private AudioSource audio;

    public void Start()
    {
        this.audio = this.GetComponent<AudioSource>();
        this.audio.Stop();
    }

    public void Update()
    {
        //Checks for a Mine Explosion
        var mine = GameObject.FindGameObjectWithTag("MineExplosion");
        
        if (mine != null)
        {
            this.audio.PlayOneShot(explosionSound);
            //Sets the location of the explosion
            var explosionPosition = mine.transform.position;
            explosionPosition.y = explosionPosition.y + 0.5f;
            this.explosion.transform.position = explosionPosition;
            //When a mine explodes , there is no mine left :p
            Destroy(mine);
        } 
    }
}
