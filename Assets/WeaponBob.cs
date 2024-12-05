using System.Collections;
using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    public Transform crosshair; // Reference to the crosshair GameObject
    public ParticleSystem muzzleFlash; // Reference to the particle system
    public float bobSpeed = 2f; // Speed of the bobbing motion
    public float bobAmount = 0.1f; // Amplitude of the bobbing motion
    private bool isFiring = false;
    private float recoilTime = 0.1f; // Duration of the recoil effect
    private Vector3 initialPosition;

    void Start()
    {
        // Save the starting position relative to the crosshair
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Check for Spacebar press and trigger Fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        // Sync the crossbow's X position with the crosshair
        if (crosshair != null)
        {
            Debug.Log("Crosshair Position: " + crosshair.position.x);
            transform.position = new Vector3(crosshair.position.x, initialPosition.y, initialPosition.z);
        }
        else
        {
            Debug.LogWarning("Crosshair reference is missing!");
        }

        // Apply bobbing effect if not firing
        if (!isFiring)
        {
            float bob = Mathf.Sin(Time.time * bobSpeed) * bobAmount;
            transform.localPosition = new Vector3(transform.localPosition.x, initialPosition.y + bob, initialPosition.z);
        }
    }


    public void Fire()
    {
        // Trigger the particle effect
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();

            StartCoroutine(StopMuzzleFlash());
        }

        // Shooting logic (destroy bugs, etc.)
        Collider2D hit = Physics2D.OverlapPoint(transform.position);
        if (hit != null && hit.gameObject.CompareTag("Bug"))
        {
            Destroy(hit.gameObject); // Destroy the bug
            Debug.Log("Bug Squashed!");
        }

        // Start recoil animation
        StartCoroutine(Recoil());
    }

    private IEnumerator Recoil()
    {
        isFiring = true;

        // Simulate a quick recoil effect
        transform.localPosition += new Vector3(0, -0.2f, 0);
        yield return new WaitForSeconds(recoilTime);

        // Reset position
        isFiring = false;
    }

    private IEnumerator StopMuzzleFlash()
    {
        yield return new WaitForSeconds(0.1f); // Adjust the delay to match the duration of your particle system
        muzzleFlash.Stop();
    }


}
