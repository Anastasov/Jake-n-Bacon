using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class JakeController : MonoBehaviour
{
    //constants / Game Settings
    private const int maxLevels = 152; //Synch with FastMode class , maxLevels field
    private const int pointsObtainedInBonus = 220; // Synch with BonusTimer class, pointsObtainedInBonus field
    
    private Rigidbody2D rb;
    private AudioSource backgroundMusic;
    private BoxCollider2D boxCollider;
    private SpriteRenderer renderer;
    private bool wentUp;
    private bool wentDown;
    private float baconCounter = 1000000000; // NOTE : A number absurd enough to be bigger then the X on the first bacon eaten 
    private float timeElapsed;
    private int level = 1;
    private int highScore;
    private float[] baconPosition = { 0.75f, -0.75f, -2.35f };

    public float BonusTimer
    {
        get
        {
            if (this.score - baconCounter > 0)
            {
                return this.score - baconCounter;
            }
            else
            {
                return 0;
            }
        }
    }

    public int score;
    public float forwardSpeed;
    public AudioSource baconPancakes;



    public void Start()
    {
        ////Restart HighScore
        //PlayerPrefs.SetInt("HighScore", 0);

        //Getting Components
        this.renderer = this.GetComponent<SpriteRenderer>();
        this.boxCollider = this.GetComponent<BoxCollider2D>();
        this.rb = this.GetComponent<Rigidbody2D>();
        this.backgroundMusic = this.GetComponent<AudioSource>();

        //Stops the playing of unnecessary music 
        this.backgroundMusic.Stop();
        this.baconPancakes.Stop();
        
    }

    public void Update()
    {
        //Checks for pressed buttons
        if (Input.GetButtonDown("Up") 
            && this.transform.position.y < 1.5f 
            && forwardSpeed != 0 
            && this.name == "Jake"
            //Gives the player time to react to mines when exiting Bonus mode
            && this.transform.localScale.x < 7.5f)
        {
            this.wentUp = true;
        }
        if (Input.GetButtonDown("Down") 
            && this.transform.position.y > -1.5f 
            && forwardSpeed != 0 
            && this.name == "Jake"
            //Gives the player time to react to mines when exiting Bonus mode
            && this.transform.localScale.x < 7.5f)
        {
            this.wentDown = true;
        }
        
        //Updates current player score
        UpdateScore();
    }

    public void FixedUpdate()
    {
        //Moves the player between the lines realtime
        if (wentUp)
        {
            var position = this.transform.position;
            position.y += 1.5f;
            this.transform.position = position;
            this.wentUp = false;
        }
        if (wentDown)
        {
            var position = this.transform.position;
            position.y -= 1.5f;
            this.transform.position = position;
            this.wentDown = false;
        }

        //Adds velocity in order to start the game process
        VelocityX(this.forwardSpeed);

        //Increases Difficulty
        this.timeElapsed += Time.deltaTime;
            if (timeElapsed >= level)
            {
                //In the first 12 sec forwardSpeed will increase by 3 ( 12 * 0.25) || Math Calculations
                if (level <= 12)
                {
                    this.level++;
                    this.forwardSpeed += 0.25f;
                }
                //In the next 20 sec forwardSpeed will increase by 2 ( 20 * 0.1) || Math Calculations
                else if (level > 12 && level <= 32)
                {
                    this.level++;
                    this.forwardSpeed += 0.1f;
                }
                //In the next (maxLevel - 32 ) forwardSpeed will increase with te equation [(maxLevels - 32) * 0.05] || Math Calculations
                else if (level > 32 && level <= maxLevels)
                {
                    this.level++;
                    this.forwardSpeed += 0.05f;
                }
            }
            
            //Check if Bonus is going to end soon
            if (this.score - baconCounter == pointsObtainedInBonus - 10 && this.boxCollider.isTrigger == false)
            {
                StartCoroutine("BonusOverSoon");
            }
            //Dispossess the bonus
            else if (this.score - baconCounter == pointsObtainedInBonus && this.boxCollider.isTrigger == false)
            {
                StartCoroutine("LowerDown");
            }       
    }

    //Check Collisions
    public void OnTriggerEnter2D(Collider2D collider)
    {
        //If the player hits the mine - He dies
        if (collider.CompareTag("Mine"))
        {
            //if the player is in normal mode - Not dead and Not in bonus mode
            if (this.name == "Jake")
            {
                //Game Logic:
                //Makes the hit mine explode trough another script by making her "Exploding"
                collider.tag = "MineExplosion";
                //Physics authentic blast off from the mine
                this.rb.MoveRotation(30);
                VelocityY(30);
                this.forwardSpeed = -60;
                //When player dies his name is DeadJake and reacts to other object in a different way
                this.name = "DeadJake";
                //Disspose of surplus object
                Destroy(this.gameObject, 0.5f);
                //Change HighScore if a new one is set  || only saves local Highscores
                var currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
                if (this.score > currentHighScore)
                {
                    PlayerPrefs.SetInt("HighScore", this.score);
                }
            }
        }
        //If player eats a bacon - enters Bonus mode a.k.a GOD MODE
        if (collider.CompareTag("Bacon"))
        {
            //Moves the bacon
            MoveBacon(collider);
            //Sets the score counter for the bonus mode !! Only a limmited amount of points can be obtained in Bonus mode
            baconCounter = this.score;
            //Makes player invulnarable
            this.boxCollider.isTrigger = false;
            //Center the player in the middle lane
            var yPosition = this.transform.position;
            yPosition.y = 0;
            this.transform.position = yPosition;
            //Improves Size and adds music for the Bonus mode
            StartCoroutine("HigherUp");
            this.baconPancakes.volume = 1;
            baconPancakes.Play();
           
        }
    }

    //Set a new velocity regarding X -  only for the player (RigidBody)
    private void VelocityX(float newVelocity)
    {
        var velocity = this.rb.velocity;
        velocity.x = newVelocity;
        this.rb.velocity = velocity;
    }
    //Set a new velocity regarding Y -  only for the player (RigidBody)
    private void VelocityY(float newVelocity)
    {
        var velocity = this.rb.velocity;
        velocity.y = newVelocity;
        this.rb.velocity = velocity;
    }

    //Moving the bacon forward
    private void MoveBacon(Collider2D collider)
    {
        var position = collider.transform.position;
        position.x += 700 + pointsObtainedInBonus; //If the player eats a bacon the next one will spawn farther rather than when he misses it
        var randomY = UnityEngine.Random.Range(0, 3);
        position.y = this.baconPosition[randomY];
        collider.transform.position = position;   
    }

    //Add points to the score
    private int UpdateScore()
    {
        //If the player is not moving forward he won't earn points (e.g. when he is dead)
        if (forwardSpeed > 0)
        {
            //If player is moving forward then play background music
            if (this.backgroundMusic.mute)
            {
                this.backgroundMusic.mute = false;
                this.backgroundMusic.Play();
            }
            //Game Logic:
            //Player gets 1 point for each passed scale on the X AX
            float scoreUpdate = this.transform.position.x;
            this.score = (int)scoreUpdate + 14;
            return this.score;
        }
        //If the player isn't moving forward that means he has hit a mine
        else
        {
            //Stop background music
            if (this.backgroundMusic.mute == false)
            {
                this.backgroundMusic.mute = true;
            }          
            //Don't change current score
            return this.score;
        }   
    }
    
    //Decrease Size (upon exiting Bonus mode)
    private IEnumerator LowerDown()
    {           
        for (float i = 10.5f; i >= 4.5f; i -= 0.125f)
        {            
            if (i == 4.5f)
            {
                //Makes the player collidable again because it's the last itteration
                this.boxCollider.isTrigger = true;
                this.baconPancakes.Stop();
                ChangeSize(i);
                yield return null;
            }
            else
            {
                ChangeSize(i);
                yield return null;
            }      
        }
    }
    
    //Increase Size (upon entering Bonus mode)
    private IEnumerator HigherUp()
    {
        for (float i = 4.5f; i <= 10.5f; i += 0.5f)
        {
            ChangeSize(i);
            yield return null;
        }   
    }
   
    //Flash red to notificate that Bonus mode ends soon
    private IEnumerator BonusOverSoon()
    {
        float volumeCounter = 1; 
        for (float i = 0; i <= 1; i += 0.01f)
        {
            this.baconPancakes.volume = volumeCounter;
            volumeCounter -= 0.01f;
            var color = this.renderer.color;
            color.r = 255;
            color.b = i;
            color.g = i;
            this.renderer.color = color;
            yield return null;
        }
    }
    
    //Change size Trough itterations
    private void ChangeSize(float sizePerItteration)
    {
        var scale = this.transform.localScale;
        scale.x = sizePerItteration;
        scale.y = sizePerItteration;
        this.transform.localScale = scale;
    }
}
