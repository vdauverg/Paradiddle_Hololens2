using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Main_Loop : MonoBehaviour
{
    [SerializeField] GameObject[] drums;
    [SerializeField] GameObject metronome;
    [SerializeField] GameObject scoreText;
    [SerializeField] int bpm;
    int[] pattern = new int[8];
    int beat;
    int score;
    int diff = 1;
    bool isPlaying = false;
    Metronome metro;

    void Start()
    {
        metro = metronome.GetComponent<Metronome>();
        pattern = CreatePattern();
    }

    void Update()
    {
        metro.SetBPM(bpm + (score / 4));
        if (metro.NextBeat(beat))
        {
            if (beat == 0)
            {
                isPlaying = !isPlaying;
                if (!isPlaying)
                    pattern = CreatePattern();
            }
            if (!isPlaying && pattern[beat] != -1)
                drums[pattern[beat]].GetComponent<Drum>().Hit();
        }
        if (isPlaying && (pattern[beat] != -1 || pattern[metro.GetClosestBeat()] != -1) && drums[pattern[beat]].GetComponent<Drum>().wasHit())
        {
            score += 1;
            if (pattern[beat] != -1)
                pattern[beat] = -1;
            else if (pattern[metro.GetClosestBeat()] != -1)
                pattern[metro.GetClosestBeat()] = -1;
            
        }
        beat = metro.GetBeat();
        scoreText.GetComponent<TextMesh>().text = string.Format("Score: {0}", score);
    }
    
    int[] CreatePattern()
    {
        int[] pat = new int[8];

        for (int i = 0; i < pat.Length; i++)
            pat[i] = -1;
        int rand = Random.Range(0, 8);
        for (int i = 0; i < 4; i++)
        {
            while (pat[rand] != -1)
                rand = Random.Range(0, 8);
            pat[rand] = Random.Range(0, 6);
        }
        return pat;
    }
}
