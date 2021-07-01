using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;

[System.Serializable] 
public class ScoreData
{
    int score;
    int perfect, great, good, miss;
    //public bool isFullCombo;
    public string GaugeMode;

    public static ScoreData getScoreFromChart(AlignedChartData chartData)
    {
        ScoreData scoreData;
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            string path = Application.persistentDataPath +
                "/highscore-" + chartData.getTitle() + "-" + chartData.getDiffPrefix() + ".hs";
            FileStream stream = new FileStream(path, FileMode.Open);
            scoreData = (ScoreData)formatter.Deserialize(stream);
            stream.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            scoreData = new ScoreData();
            scoreData.setScore(-1, 0, 0, 0, 0);
        }
        Debug.Log(scoreData);
        return scoreData;
    }

    public int getScore() { return score; } 
    public int getPerfect() { return perfect; }
    public int getGreat() { return great; }
    public int getGood() { return good; }
    public int getMiss() { return miss; }

    public void setScore(int score, int perfect, int great, int good, int miss) {
        this.perfect = perfect;
        this.great = great;
        this.good = good;
        this.miss = miss;
        this.score = score;
    }
}