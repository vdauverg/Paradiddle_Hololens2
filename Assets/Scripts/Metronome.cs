using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Metronome : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;
    [SerializeField] int bpm;
    int beat;
    float bps;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        bps = bpm / 60;
    }

    // Update is called once per frame
    void Update()
    {
        float currBeat = timer * bps;

        if (currBeat >= 4)
        {
            timer -= 4 / bps;
            currBeat = 0;
        }
        if ((int)currBeat == 0 && beat / 2 != (int)currBeat)
            audio.PlayOneShot(clip1, 1);
        else if (beat / 2 != (int)currBeat)
            audio.PlayOneShot(clip2, 0.5f);
        beat = (int)(currBeat * 2);

        timer += Time.deltaTime;
    }

    public bool NextBeat(int currBeat)
    {
        if (currBeat != beat)
            return true;
        else
            return false;
    }

    public int GetBeat()
    {
        return beat;
    }

    public float GetPrecision(int targetBeat)
    {
        return Mathf.Abs(timer * bps * 2 - targetBeat);
    }

    public int GetClosestBeat()
    {
        float currBeat = timer * bps * 2;

        if (Mathf.Abs(currBeat - (int)currBeat) < 0.5)
            return (int)currBeat;
        else
            return (int)currBeat + 1;
    }

    public void SetBPM(int val)
    {
        bpm = val;
        bps = bpm / 60;
    }

}
