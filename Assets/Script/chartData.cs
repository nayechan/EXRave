using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class chartData
{
    public string title;
    public string artist;
    public int normal;
    public int hard;
    public int ex;
    public int rave;

    public chartData(string title, string artist, int[] level)
    {
        this.title = title;
        this.artist = artist;
        normal = level[0];
        hard = level[1];
        ex = level[2];
        rave = level[3];
    }
}


[Serializable]
public class chartData_list
{
    public List<chartData> Items;
}