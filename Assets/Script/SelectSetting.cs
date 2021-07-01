using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSetting : MonoBehaviour {
	public float speed = 1.0f;
	public int offset = 0;
	public string GaugeMode;
	public Text spdText, offsetText, gaugeText;

	// Use this for initialization
	void Start () {
		speed = PlayerPrefs.GetFloat("speed",1.0f);
		offset = PlayerPrefs.GetInt("offset",0);
		GaugeMode = PlayerPrefs.GetString("GaugeMode","Normal");
	}
	
	// Update is called once per frame
	void Update () {
		spdText.text = "SPEED : "+string.Format("{0:0.##}", speed);
		offsetText.text = "OFFSET : "+string.Format("{0:0.##}", offset)+"MS";
		gaugeText.text = "GAUGE (CURRENT : "+GaugeMode.ToUpper()+")";
	}

	public void spdPlusMinus(float amount)
	{
		speed+=amount;
		if(speed < 0.4f) speed = 0.4f;
		PlayerPrefs.SetFloat("speed",Mathf.Round(speed*10)/10.0f);
		PlayerPrefs.Save();
	}

	public void offsetPlusMinus(int amount)
	{
		offset+=amount;
		if(offset < -999) offset = -999;
		if(offset > 999) offset = 999;
		PlayerPrefs.SetInt("offset",offset);
		PlayerPrefs.Save();
	}
	public void SetGauge(string gaugeMode)
	{
		GaugeMode = gaugeMode;
		PlayerPrefs.SetString("GaugeMode",gaugeMode);
	}
}
