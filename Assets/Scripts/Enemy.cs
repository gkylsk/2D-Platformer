using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer enemySprite;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;

    private Vector3 destination;
    public float speed = 2f;
    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        destination = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        //if the currentPosition is equal to the destination the change the destination
        if(transform.position == destination)
        {
            destination = (destination == pointA.position) ? pointB.position : pointA.position;
            enemySprite.flipX = (destination == pointA.position) ? false : true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Hit();
            //change destination if collided with player
            if (transform.position == destination)
            {
                destination = (destination == pointA.position) ? pointB.position : pointA.position;
            }
        }
    }
}
