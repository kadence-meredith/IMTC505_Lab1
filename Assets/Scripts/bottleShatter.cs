using UnityEngine;

public class bottleShatter : MonoBehaviour
{
    public AudioClip impactSound;
    public string floorTag = "floor";

    [Header("Audio Clip")]
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(floorTag))
        {
            if (audioSource != null && impactSound != null)
            {
                audioSource.PlayOneShot(impactSound);
            }
        }
    }
}
