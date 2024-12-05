using UnityEngine;

public class BugController : MonoBehaviour
{
    public Transform spriteHolder; // Reference to the SpriteHolder child GameObject
    public float moveSpeed = 2f; // Speed of downward movement
    private float initialX;     // Store the initial X position

    // Add references for particle effects and animations
    public ParticleSystem bugDeathEffect; // Particle effect to play on destruction
    public Animator animator; // Animator for death animation (optional)

    void Start()
    {
        // Access CrosshairController's xLimit dynamically
        float xLimit = CrosshairController.xLimit; // Requires xLimit to be static in CrosshairController
        initialX = Random.Range(-xLimit, xLimit);
        transform.position = new Vector3(initialX, 5f, 0f); // Start at the top of the screen

        // Rotate the bug's sprite, not the entire bug GameObject
        if (spriteHolder != null)
        {
            spriteHolder.localRotation = Quaternion.Euler(0f, 0f, 180f); // Rotate the sprite downward
        }
    }

    void Update()
    {
        // Move straight down
        transform.position = new Vector3(initialX, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);

        if (transform.position.y < -5f) // Destroy off-screen bugs
        {
            Destroy(gameObject);
        }
    }

    public void DestroyBug()
    {
        if (bugDeathEffect != null)
        {
            // Instantiate the particle effect at the bug's position
            Vector3 effectPosition = transform.position;
            ParticleSystem effect = Instantiate(bugDeathEffect, transform.position, Quaternion.identity);

            // Detach and play the particle system
            effect.Play(); // Play the particle effect

            Debug.Log("Bug death effect triggered at position: " + effect.transform.position);

            // Destroy the particle system after it finishes
            Destroy(effect.gameObject, effect.main.duration);
        }
        else
        {
            Debug.LogWarning("No particle system assigned for bug destruction!");
        }

        // Destroy the bug object
        Destroy(gameObject);
        Debug.Log("Bug destroyed: " + gameObject.name);
    }



}
