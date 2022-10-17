using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    private float speed = 10.0f;
    private float jumpingPower = 20.0f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool isInvincible;
    private float invincibleDuration = 2.5f;
    private float invincibleDeltaTime = 0.3f;

    public Renderer renderer;
	
	public int maxHealth = 3;
    public int currentHealth;
	
	public Healthbar healthbar;
	
	public bool alive = true;
	
	void Start()
    {
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }
	
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
		
		if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1);
            Debug.Log("Pressing T");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Obstacles"))
        {
            if(!isInvincible)
            {
                TakeDamage(1);
                loseHealth();
                Debug.Log("Hit");
            }
        }
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;

        for (float i = 0; i < invincibleDuration; i += invincibleDeltaTime)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (renderer.material.color == Color.red)
            {
               renderer.material.color = Color.white;
            }
            else
            {
                renderer.material.color = Color.red;
            }
            yield return new WaitForSeconds(invincibleDeltaTime);
        }

        renderer.material.color = Color.white;
        isInvincible = false;
    }

    public void loseHealth()
    {
        StartCoroutine(BecomeTemporarilyInvincible());
    }
	
	void TakeDamage(int t_damage)
    {
        currentHealth -= t_damage;
        healthbar.setHealth(currentHealth);

        if (currentHealth == 0)
        {
            alive = false;
        }
    }
}
