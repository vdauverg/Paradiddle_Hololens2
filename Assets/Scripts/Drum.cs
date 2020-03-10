using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    [SerializeField] GameObject metronome;
    [SerializeField] Material hitMat;
    [SerializeField] Material unhitMat;
    [SerializeField] AudioSource    audio;
    bool animate = false;
    Material mat;
    Metronome metro;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        metro = metronome.GetComponent<Metronome>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animate && mat.color != hitMat.color)
            mat.Lerp(mat, hitMat, Time.deltaTime * 20);
        else if (mat.color != unhitMat.color)
        {
            animate = false;
            mat.Lerp(mat, unhitMat, Time.deltaTime * 20);
        }
    }

    public void Hit()
    {
        audio.PlayOneShot(audio.clip, 0.75f);
        hitMat.color = Color.Lerp(Color.green, Color.red, metro.GetPrecision(metro.GetBeat()));
        animate = true;
    }

    public bool wasHit()
    {
        return animate;
    }
}
