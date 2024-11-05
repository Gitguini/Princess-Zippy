using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip prepareDash;
    public AudioClip dashTo;
    public AudioClip dashThrough;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }


    public void playSound(string soundName)
    {
        if (soundName == "jump")
        {
            source.PlayOneShot(jump);
        } else if (soundName == "prepareDash")
        {
            source.PlayOneShot(prepareDash);
        } else if (soundName == "dashTo")
        {
            source.PlayOneShot(dashTo);
        } else if (soundName == "dashThrough")
        {
            source.PlayOneShot(dashThrough);
        }
    }
}
