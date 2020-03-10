using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundPicker : MonoBehaviour
{
    [SerializeField] GameObject sampleText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject text;
        Object[] sounds = Resources.LoadAll("Samples", typeof(AudioClip));
        int len = 20 * sounds.Length;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200, len);
        for (int i = 0; i < sounds.Length; i++)
        {
            text = Instantiate(sampleText, transform);
            text.transform.parent = transform;
            text.GetComponent<RectTransform>().localPosition = new Vector3(5, -len + 10 + i * 20, 0);
            text.GetComponent<Text>().text = sounds[i].name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
 