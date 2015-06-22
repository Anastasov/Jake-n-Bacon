using UnityEngine;

public class ExplosionSprite : MonoBehaviour
{
    private Animator animator;

    public void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }
    public void Update()
    {
        //Game Logic:
        //The sprite is at Y > 2 by default but upon collision it is send to the exploding mine's position.All mines' Ys are under 2
        if (this.transform.position.y < 2)
        {
            this.animator.SetBool("IsExploding", true);
        }
    }
}
