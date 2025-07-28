using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float plusOffset = 15f;
    [SerializeField] private float minusOffset = -15f;
    PlayerMovement PlayerMovement;
    void Awake()
    {
         PlayerMovement = playerTransform.GetComponent<PlayerMovement>();
    }

    
    void Update()
    {
        Follow();
    }
    private void Follow()
    {
        if (PlayerMovement != null)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = playerTransform.position;
            var offsetX = PlayerMovement.IsMovingForward ? plusOffset : minusOffset;
            float targetx = targetPosition.x + offsetX;

            float lerped = Mathf.Lerp(currentPosition.x, targetx, speed * Time.deltaTime);
            transform.position = new Vector3(lerped, transform.position.y, transform.position.z);
        }
    }
}
