using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class ResultDisplay : MonoBehaviour {
	// [System.Serializable] 
	// public class ScoreData
	// {
	// 	public int score;
	// 	public int perfect, great, good, miss;
	// 	//public bool isFullCombo;
	// 	public string GaugeMode;
	// }

	// Use this for initialization
	public Sprite Ex, S, AA, A, B, C, D, F;
	public Image rank;
	public Text scoreText, resultText, pt, grt, gt, mt, comboText,titleText;

	public ScoreData highscore = new ScoreData();
	
	void Start () {
		int score = PlayerPrefs.GetInt("LastGameScore",0);
		string lastgame = PlayerPrefs.GetString("GaugeMode","");
		string diff_text = "", diff = PlayerPrefs.GetString("TargetDiff","");
		
		switch(diff)
		{
			case "n":
				diff_text = "NORMAL";
				break;
			case "h":
				diff_text = "HARD";
				break;
			case "ex":
				diff_text = "EX";
				break;
			case "rave":
				diff_text = "RAVE";
				break;
			default:
				diff_text = diff;
				break;
		}



		pt.text = ""+PlayerPrefs.GetInt("LastPerfect",0);
		grt.text = ""+PlayerPrefs.GetInt("LastGreat",0);
		gt.text = ""+PlayerPrefs.GetInt("LastGood",0);
		mt.text = ""+PlayerPrefs.GetInt("LastMiss",0);
		comboText.text = "MAX COMBO : "+PlayerPrefs.GetInt("LastMaxCombo",0);
		titleText.text = PlayerPrefs.GetString("TargetChart","").ToUpper()+" <"
		+diff_text+">";


		if(score > 1000000)
		{
			rank.sprite = F;
		}
		else if(score == 1000000)
		{
			rank.sprite = Ex;
		}
		else if(score >= 950000)
		{
			rank.sprite = S;
		}
		else if(score >= 900000)
		{
			rank.sprite = AA;
		}
		else if(score >= 800000)
		{
			rank.sprite = A;	
		}
		else if(score >= 700000)
		{
			rank.sprite = B;
		}
		else if(score >= 600000)
		{
			rank.sprite = C;
		}
		else if(score >= 500000)
		{
			rank.sprite = D;
		}
		else rank.sprite = F;

		scoreText.text = ""+score;
		resultText.text = lastgame.ToUpper()+((lastgame.Length>0)?" ":"")+"CLEAR";

		string chart_name = PlayerPrefs.GetString("TargetChart","");
		string chart_diff = diff;
		highscore.GaugeMode = lastgame;
        highscore.setScore(
            score,
            PlayerPrefs.GetInt("LastPerfect", 0),
            PlayerPrefs.GetInt("LastGreat", 0),
            PlayerPrefs.GetInt("LastGood", 0),
            PlayerPrefs.GetInt("LastMiss", 0)
            );

		BinaryFormatter formatter = new BinaryFormatter();
		ScoreData lastData = null;
		Debug.Log(Application.persistentDataPath+
		"/highscore-"+chart_name+"-"+chart_diff+".hs");
		try{
			FileStream stream = new FileStream(Application.persistentDataPath+
			"/highscore-"+chart_name+"-"+chart_diff+".hs",FileMode.Open);
			lastData = (ScoreData)formatter.Deserialize(stream);
			stream.Close();
		}
		catch(Exception e)
		{
			Debug.Log(e);
			lastData = null;
		}
		
		
		if(lastData == null || lastData.getScore() < highscore.getScore())
		{
			string path = Application.persistentDataPath;
			string filename = "highscore-"+chart_name+"-"+chart_diff+".hs";
			if(!System.IO.Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			FileStream stream = new FileStream(path+"/"+filename,FileMode.Create);
			formatter.Serialize(stream, highscore);
			stream.Close();
			Debug.Log("xeerfae");		
		}	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
