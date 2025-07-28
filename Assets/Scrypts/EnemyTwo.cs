using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    [SerializeField] private float xDistanfce = 10;
    [SerializeField] private float speed = 2f;
    [SerializeField] PlayerMovement moveScript;
    float startPos;
    void Start()
    {
        startPos = transform.position.x;   
    }

    
    void Update()
    {

        var area = Mathf.PingPong(Time.time * speed, xDistanfce) + startPos;
        transform.position = new Vector2(area, transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            moveScript.Die();
            Destroy(gameObject);
        }
    }
}
