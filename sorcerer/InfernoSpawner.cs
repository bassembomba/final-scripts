using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoSpawner : MonoBehaviour
{
    bool pressed = false;
    public GameObject infernoPrefab; // Assign the Inferno prefab (area effect)
                                     // public fireball fireballref;      // Reference to fireball script
    public float effectRadius = 0.2f;  // Radius of the Inferno effect
    public float damagePerSecond = 2f; // Damage dealt to goblins per second
    public float duration = 15f;        // Duration of Inferno effect
    public sorcererunlockabilities unlockabilities;
    public AudioSource sfxAudioSource;

    public AudioClip InfernoSound;


    public bool infernoactive = false;

    private void Start()
    {
        // Optionally print or initialize values
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && unlockabilities.infernounlocked)
        {
            pressed = true;
            print("Inferno ability activated: " + pressed);
        }

        if (Input.GetMouseButtonDown(1) && pressed) // Right mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Spawn the Inferno at the clicked position
                SpawnInferno(hit.point);
                infernoactive = true;
            }
        }
    }

    void SpawnInferno(Vector3 targetPosition)
    {
        PlaySoundEffect(InfernoSound);
        print("Spawning Inferno at: " + targetPosition);

        // Instantiate the Inferno effect at the target position
        GameObject inferno = Instantiate(infernoPrefab, targetPosition, Quaternion.identity);


        // After spawning, reset the pressed flag
        pressed = false;

        // Enable fireball spawning (if needed)
        // fireballref.fireballEnabled = true;

        // Start the damage-over-time effect
        //StartCoroutine(DamageGoblinsOverTime(targetPosition));

        // Destroy Inferno after the specified duration
        //StartCoroutine(DestroyInfernoAfterTime(inferno, duration));
    }

    public void PlaySoundEffect(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
        {
            sfxAudioSource.PlayOneShot(clip, volume); // Play the clip with specific volume
        }
    }
}
