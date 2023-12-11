using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Food : Collectable, IInvisible
{
    public const float TIMER_TO_INVISIBLE = 2f;
    
    #region ATTRIBUTES
    private SpriteRenderer spriteRenderer;
    #endregion

    private void Awake()
    {
        recollectableGridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        if (DataPersistence.sharedInstance.GetMode()) {
            Invoke("MakeInvisible",TIMER_TO_INVISIBLE);
        }   
    }

    public void MakeInvisible() {
        spriteRenderer.color = Color.clear;
    }

    public override bool TrySnakeEat(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == recollectableGridPosition)
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
