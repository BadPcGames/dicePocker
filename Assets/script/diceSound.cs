using UnityEngine;

public class diceSound : MonoBehaviour
{
    public AudioClip[] sounds;

    private AudioSource audioSource => GetComponent<AudioSource>();

    public void PlaySound(AudioClip clip, float volume = 0.2f, bool destroyed = false, float p1 = 0.9f, float p2 = 1.1f)
    {
        audioSource.pitch = Random.Range(p1, p2);
        audioSource.PlayOneShot(clip, volume);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "boardBotom")
        //{
            PlaySound(sounds[Random.Range(0,2)]);
        //}
    }

    public void playPickSound()
    {
        PlaySound(sounds[2]);
    }
}
