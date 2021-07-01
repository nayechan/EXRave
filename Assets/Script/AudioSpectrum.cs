using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSpectrum : MonoBehaviour
{
    public GameObject spectrum;
    public AudioSource audioSource;
    private RectTransform[] spectrums = new RectTransform[32];
    float[] samples = new float[64];
    void Start()
    {
        for (int i = 0; i < spectrums.Length; i++)//막대들 만들기
        {
            spectrums[i] = Instantiate(spectrum).GetComponent<RectTransform>();//막대생성
            spectrums[i].SetParent(transform, false);
            spectrums[i].localPosition = new Vector2(-620 + (i * 40), -140);//포지션설정

            //색깔 랜덤
            spectrums[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
        for (int i = 0; i < 32; i++)//크기 조절
        {
            float v = Mathf.Log(samples[i] * 1296, 6)/4;
            spectrums[i].GetComponent<Image>().color = new Color(0, 0, 1, 0.7f);
            spectrums[i].GetComponent<Image>().fillAmount = v;
        }
    }
}
