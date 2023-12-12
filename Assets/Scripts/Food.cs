using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Food : Collectable, IInvisible
{
    public const float TIMER_TO_INVISIBLE = 2f;
    
    #region ATTRIBUTES
    private SpriteRenderer _spriteRenderer;
    #endregion

    private void Awake()
    {
        collectableGridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        //Check if the gamemode is activated
        if (DataPersistence.sharedInstance.GetMode()) {
            Invoke("MakeInvisible",TIMER_TO_INVISIBLE); //make invisible after 2 seconds
        }   
    }

    public void MakeInvisible() {
        _spriteRenderer.color = Color.clear;
    }

    public override bool TrySnakeEat(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == collectableGridPosition)
        {
            Object.Destroy(this.gameObject);
            Score.AddScore(Score.POINTS_TO_ADD); //Increase score
            return true;
        }
        else
        {
            return false;
        }
    }
}
