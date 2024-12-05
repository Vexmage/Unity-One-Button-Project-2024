using System.Collections;
using UnityEngine;
using TMPro; // Required for TextMeshPro

public class CrosshairController : MonoBehaviour
{
    public static float xLimit = 4f; // Static to allow global access
    public float moveSpeed = 12f;
    public float fireRadius = 0.5f; // Radius for detecting bugs when firing
    public TextMeshProUGUI killCounterText;
    private int killCount = 0; // Tracks the number of kills

    private bool movingRight = true;

    void Update()
    {
        Debug.Log("Current moveSpeed: " + moveSpeed);
        // Automatically move left and right
        if (movingRight)
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        else
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Reverse direction at boundaries
        if (transform.position.x >= xLimit)
            movingRight = false;
        else if (transform.position.x <= -xLimit)
            movingRight = true;

        // Fire on Spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        StartCoroutine(FireEffect());

        int layerMask = ~LayerMask.GetMask("Crosshair");
        Collider2D hit = Physics2D.OverlapCircle(transform.position, fireRadius, layerMask);

        // Draw the detection circle for debugging
        Debug.DrawLine(transform.position, transform.position + Vector3.up * fireRadius, Color.red, 0.5f);


        if (hit != null)
        {
            Debug.Log($"Hit detected on: {hit.gameObject.name}");

            if (hit.gameObject.CompareTag("Bug"))
            {
                BugController bugController = hit.gameObject.GetComponent<BugController>();
                if (bugController != null)
                {
                    bugController.DestroyBug();
                    IncrementKillCount();
                    Debug.Log($"Bug {hit.gameObject.name} destroyed.");
                }
                else
                {
                    Debug.LogWarning("BugController component is missing!");
                }
            }
            else
            {
                Debug.Log("Hit detected but not a bug.");
            }
        }
        else
        {
            Debug.Log("No hit detected.");
        }
    }


    void IncrementKillCount()
    {
        killCount++; // Increase kill count
        if (killCounterText != null)
        {
            killCounterText.text = $"Kills: {killCount}"; // Update the UI
        }
        else
        {
            Debug.LogWarning("KillCounterText is not assigned!");
        }
    }


    // Coroutine for the firing effect
    IEnumerator FireEffect()
    {
        // Change the color of the crosshair
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        sr.color = Color.red; // Firing color

        // Wait for a short duration
        yield return new WaitForSeconds(0.1f);

        // Revert to the original color
        sr.color = originalColor;
    }
}
