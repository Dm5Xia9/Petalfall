using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class DateTimeUI : MonoBehaviour
{
    public static DateTimeUI Instance { get; private set; }
    public TextMeshProUGUI Tx;

    public UnityEngine.UI.Image Image;

    public Sprite Night;
    public Sprite Day;

    public AudioClip NightClip;
    public AudioClip DayClip;
    public AudioSource source;

    public bool? isDay = null;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        Instance = this;
    }

    public void SetDateTime(DateTime dt)
    {
        Tx.text = dt.ToString("HH:mm");
        if (dt.Hour < 6 || dt.Hour >= 21)
        {

            if (isDay == null)
            {
                isDay = true;
            }
            if (isDay.Value == true)
            {
                source.Stop();
                source.PlayOneShot(NightClip);
                Debug.Log("Play Night");
            }
            isDay = false;
            Image.sprite = Night;
        }
        else
        {
            if(isDay == null)
            {
                isDay = false;
            }
            if(isDay.Value == false)
            {
                source.Stop();
                source.PlayOneShot(DayClip);
                Debug.Log("Play Day");
            }
            isDay = true;
            Image.sprite = Day;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
