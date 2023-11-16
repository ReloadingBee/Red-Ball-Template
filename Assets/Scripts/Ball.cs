using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpSpeed = 5;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        var hor = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector2(hor, 0));

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity += Vector2.up * jumpSpeed;
        }
    }
}
