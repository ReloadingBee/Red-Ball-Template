using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpSpeed = 5;
    public bool isGrounded;
    public GameObject deathParticle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        var hor = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector2(hor, 0));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity += Vector2.up * jumpSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Die"))
        {
            Die();
            Destroy(gameObject);
            for (int i = 0; i < 15; i++)
            {
                Instantiate(deathParticle, transform.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f)), transform.rotation);
            }
        }
        isGrounded = true;
    }

    private void Die()
    {
        GameManager.instance.Lose();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.Win();
    }
}
