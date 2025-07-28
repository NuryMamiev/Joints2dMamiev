using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    public PlayerMovement playermovem;

    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playermovem.Die();
        }
    }

}
