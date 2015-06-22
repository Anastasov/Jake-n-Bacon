using UnityEngine;

public class BackgroundCollider : MonoBehaviour
{
    public GameObject background;
    public GameObject background1;
    public GameObject bacon;

    //Possible positions for the bacon and the mines regarding Y
    private float[] minesPosition = { 0.5f, -1, -2.5f };
    private float[] baconPosition = { 0.75f, -0.75f, -2.35f };


    public void Start()
    {
        //Create a collection of all the mines
        var mines = GameObject.FindGameObjectsWithTag("Mine");
        RandomizeMines(mines);
        RandomizeBaconPosition();
    }
    
    //Loops objects
    public void OnTriggerEnter2D(Collider2D collider)
    {
            if (collider.CompareTag("Background"))
            {
                MoveBackground(background);
            }
            if (collider.CompareTag("Background1"))
            {
                MoveBackground(background1);
            }
            if (collider.CompareTag("Mine"))
            {
                var position = collider.transform.position;
                position.x += 70;
                var randomY = Random.Range(0, 3);
                position.y = this.minesPosition[randomY];
                collider.transform.position = position;
            }
            if (collider.CompareTag("Bacon"))
            {
                var position = collider.transform.position;
                position.x += 700;
                var randomY = Random.Range(0, 3);
                position.y = this.baconPosition[randomY];
                collider.transform.position = position;
            }     
    }

    //Randomizes all of the mines' possitions regarding Y
    private void RandomizeMines(GameObject[] mines)
    {
        for (int i = 0; i < mines.Length; i++)
        {
            var currentMine = mines[i];
            var randomY = Random.Range(0, 3);
            var minePosition = currentMine.transform.position;
            minePosition.y = this.minesPosition[randomY];
            currentMine.transform.position = minePosition;
        }
    }

    //Randomizes Bacon initial position
    private void RandomizeBaconPosition()
    {
        var position = this.bacon.transform.position;
        var randomY = Random.Range(0, 3);
        position.y = this.baconPosition[randomY];
        this.bacon.transform.position = position;
    }

    private void MoveBackground(GameObject bg)
    {
        var position = bg.transform.position;
        position.x += 81.5f;
        bg.transform.position = position;
    }
}
