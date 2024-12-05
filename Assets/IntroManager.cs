using System.Collections;
using UnityEngine;
using TMPro;

public class IntroManager : MonoBehaviour
{
    public TextMeshProUGUI introText; // Assign in the Inspector
    public float displayTime = 2f; // Time to display the message
    public GameObject[] gameObjectsToPause; // Objects to pause at the start

    void Start()
    {
        StartCoroutine(ShowIntro());
    }

    IEnumerator ShowIntro()
    {
        // Show the intro text
        introText.gameObject.SetActive(true);

        // Pause game objects
        foreach (GameObject obj in gameObjectsToPause)
        {
            obj.SetActive(false);
        }

        // Wait for the display time
        yield return new WaitForSeconds(displayTime);

        // Hide the intro text
        introText.gameObject.SetActive(false);

        // Resume game objects
        foreach (GameObject obj in gameObjectsToPause)
        {
            obj.SetActive(true);
        }
    }
}
