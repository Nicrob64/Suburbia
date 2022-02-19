using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public interface DialogueCallbacks
    {
        void OnFinishText();
        void OnTextUpdate(string currentText);
    }


    public AudioSource audioo;
    public AudioClip clip;
    public float textSpeed = 20; //char p sec?
    public float breakBetweenSounds = 0.15f;
    public float frequencyVariance = 0.05f;
    public float pauseChance = 0.1f;
    string lineToPlay;
    float delta = 0;
    bool play = false;
    float timeSinceLastNoise = 0;
    public DialogueCallbacks callbacks;


    public void PlayLine(string text)
    {
        lineToPlay = text;
        delta = 0;
        play = true;
        audioo.clip = clip;
        Random.InitState(text.GetHashCode());
    }

    private void Update()
    {
        if (!play) { return; }

        delta += Time.deltaTime;

        int ch = (int)(delta * textSpeed);
        string st = lineToPlay;
        if(ch < lineToPlay.Length) { st = lineToPlay.Substring(0, ch); }
        if (callbacks != null)
        {
            callbacks.OnTextUpdate(st);
        }

        timeSinceLastNoise += Time.deltaTime;

        if (timeSinceLastNoise > breakBetweenSounds)
        {

            if (Random.Range(0.0f, 1.0f) < pauseChance)
            {
                //pause time
            }
            else
            {
                float dist = Random.Range(-frequencyVariance, frequencyVariance);
                audioo.pitch = 1.0f + dist;
                audioo.Play();
            }
            timeSinceLastNoise = 0;
        }

        if (lineToPlay.Length < delta * textSpeed)
        {
            play = false;
            if (callbacks != null)
            {
                callbacks?.OnTextUpdate(lineToPlay);
                callbacks?.OnFinishText();
            }
        }
    }

}
