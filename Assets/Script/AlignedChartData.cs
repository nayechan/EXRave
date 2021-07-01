using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignedChartData
{
    string title, artist, diff_prefix;
    int normal, hard, ex, rave;

    public string getTitle()
    {
        return title;
    }

    public string getArtist()
    {
        return artist;
    }

    public string getDiffPrefix()
    {
        return diff_prefix;
    }

    public int getCurrentDiff()
    {
        switch (diff_prefix)
        {
            case "n":
                return normal;
            case "h":
                return hard;
            case "ex":
                return ex;
            case "rave":
                return rave;
        }
        return -1;
    }

    public int getNormal() { return normal; }
    public int getHard() { return hard; }
    public int getEX() { return ex; }
    public int getRave() { return rave; }

    public AlignedChartData(string title, string artist, int normal,
        int hard, int ex, int rave, string diff_prefix)
    {
        this.title = title;
        this.artist = artist;
        this.normal = normal;
        this.hard = hard;
        this.ex = ex;
        this.rave = rave;
        this.diff_prefix = diff_prefix;
    }

    public AlignedChartData(chartData chart_data, string diff_prefix)
    {
        title = chart_data.title;
        artist = chart_data.artist;
        normal = chart_data.normal;
        hard = chart_data.hard;
        ex = chart_data.ex;
        rave = chart_data.rave;
        this.diff_prefix = diff_prefix;

    }

    public AlignedChartData(AlignedChartData alignedChartData)
    {
        title = alignedChartData.title;
        artist = alignedChartData.artist;
        normal = alignedChartData.normal;
        hard = alignedChartData.hard;
        ex = alignedChartData.ex;
        rave = alignedChartData.rave;
        diff_prefix = alignedChartData.diff_prefix;
    }
}

public class SortDataByTitle : IComparer<AlignedChartData>
{
    int IComparer<AlignedChartData>.Compare(AlignedChartData x, AlignedChartData y)
    {
        return x.getTitle().CompareTo(y.getTitle());
    }
}

public class SortDataByLevel : IComparer<AlignedChartData>
{
    int IComparer<AlignedChartData>.Compare(AlignedChartData x, AlignedChartData y)
    {
        return x.getCurrentDiff().CompareTo(y.getCurrentDiff());
    }
}

public class SortDataByScore : IComparer<AlignedChartData>
{
    int IComparer<AlignedChartData>.Compare(AlignedChartData x, AlignedChartData y)
    {
        return (ScoreData.getScoreFromChart(y).getScore()).CompareTo(ScoreData.getScoreFromChart(x).getScore());
    }
}