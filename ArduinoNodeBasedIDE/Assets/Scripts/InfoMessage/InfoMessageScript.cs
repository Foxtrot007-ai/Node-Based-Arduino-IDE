using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InfoMessageScript : MonoBehaviour
{

    public GameObject field0;
    public Image image;

    public bool startFade;
    public GameObject text;
    public float fadeRate;

    void Start()
    {
        image = field0.GetComponent<Image>();
        startFade = true;
    }
    void Update()
    {
        if (startFade)
        {
            FadeOutStep();
        }
        
        if(Math.Abs(image.color.a) < 0.0001f)
        {
            Destroy(gameObject);
        }
    }
    public void SetMessage(String message, float fadeRate)
    {
        text.GetComponent<TMP_Text>().text = message;
        this.fadeRate = fadeRate;
    }

    private void FadeOutStep()
    {
        Color c = image.color;
        c.a = Mathf.MoveTowards(c.a, 0, fadeRate * Time.deltaTime);
        image.color = c;
    }
}
