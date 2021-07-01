using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class chartManager : MonoBehaviour
{
    public GameObject scrollItem, ScrollView, SongExtended;
    public GameObject diff_n, diff_h, diff_ex, diff_rave;
    public RectTransform content, descSort;
    public Image bg;
    public Text Title, Artist;

    string jsonString;
    List<GameObject> itemList;
    List<AlignedChartData> originalData, SortedData, AlignedData_byLevel, AlignedData_byScore;
    chartData_list test;

    int prev_i = 0;
    int currentDiff = 0;
    int sortMode = 0;

    public int currentDisplay = 0;

    // Start is called before the first frame update
    void Start()
    {
        itemList = new List<GameObject>();
        jsonString = Resources.Load<TextAsset>("chart/chart_data").text;
        test = JsonUtility.FromJson<chartData_list>(jsonString);

        originalData = test.Items.ConvertAll(data => new AlignedChartData(data, ""));

        SortedData = test.Items.ConvertAll(data => new AlignedChartData(data, ""));
        SortedData.Sort(new SortDataByTitle());

        AlignedData_byLevel = new List<AlignedChartData>();
        foreach (chartData data in test.Items)
        {
            if (data.normal != -1)
                AlignedData_byLevel.Add(new AlignedChartData(data, "n"));
            if (data.hard != -1)
                AlignedData_byLevel.Add(new AlignedChartData(data, "h"));
            if (data.ex != -1)
                AlignedData_byLevel.Add(new AlignedChartData(data, "ex"));
            if (data.rave != -1)
                AlignedData_byLevel.Add(new AlignedChartData(data, "rave"));
        }

        AlignedData_byScore = new List<AlignedChartData>();
        AlignedData_byScore = AlignedData_byLevel.ConvertAll(data => new AlignedChartData(data));

        AlignedData_byLevel.Sort(new SortDataByLevel());
        AlignedData_byScore.Sort(new SortDataByScore());
        
        content.sizeDelta = new Vector2(786, 0);

        Debug.Log(test.Items.Count);
        Debug.Log(originalData.Count);

        RefreshSort();
    }

    public float SnapTo(int i)
    {
        RectTransform target = itemList[i].GetComponent<RectTransform>();

        Canvas.ForceUpdateCanvases();
        Vector2 viewportLocalPosition = content.parent.localPosition;
        Vector2 childLocalPosition = target.localPosition;
        float result = -(viewportLocalPosition.y + childLocalPosition.y);
        return result;

    }
    public void RefreshSort()
    {
        int j = 0;
        prev_i = 0;
        switch (sortMode)
        {
            case 0:
                foreach (AlignedChartData data in originalData)
                {
                    GameObject si = Instantiate(scrollItem, content);
                    si.name = "scrollItem" + j;
                    si.transform.Find("title").GetComponent<Text>().text = data.getTitle();
                    si.transform.Find("artist").GetComponent<Text>().text = data.getArtist();
                    si.transform.Find("level").GetComponent<Text>().text = "";
                    si.GetComponent<RectTransform>().anchoredPosition = new Vector2(460, -j * 300 - 530);

                    content.sizeDelta = new Vector2(786, 300 * test.Items.Count + 780);

                    itemList.Add(si);
                    j++;
                }

                break;
            case 1:
                foreach (AlignedChartData data in SortedData)
                {
                    GameObject si = Instantiate(scrollItem, content);
                    si.name = "scrollItem" + j;
                    si.transform.Find("title").GetComponent<Text>().text = data.getTitle();
                    si.transform.Find("artist").GetComponent<Text>().text = data.getArtist();
                    si.transform.Find("level").GetComponent<Text>().text = "";
                    si.GetComponent<RectTransform>().anchoredPosition = new Vector2(460, -j * 300 - 530);

                    content.sizeDelta = new Vector2(786, 300 * SortedData.Count + 780);

                    itemList.Add(si);
                    j++;
                }
                break;
            case 2:
                foreach (AlignedChartData data in AlignedData_byLevel)
                {
                    string diff_name;
                    switch(data.getDiffPrefix())
                    {
                        case "n":diff_name = "Normal";break;
                        case "h":diff_name = "Hyper";break;
                        case "ex":diff_name = "EX";break;
                        case "rave":diff_name = "RAVE";break;
                        default:diff_name = data.getDiffPrefix();break;
                    }
                    GameObject si = Instantiate(scrollItem, content);
                    si.name = "scrollItem" + j;
                    si.transform.Find("title").GetComponent<Text>().text = data.getTitle();
                    si.transform.Find("artist").GetComponent<Text>().text = diff_name;
                    si.transform.Find("level").GetComponent<Text>().text = data.getCurrentDiff() + "";
                    si.GetComponent<RectTransform>().anchoredPosition = new Vector2(460, -j * 300 - 530);

                    content.sizeDelta = new Vector2(786, 300 * AlignedData_byLevel.Count + 780);

                    itemList.Add(si);
                    j++;
                }
                break;
            case 3:
                foreach (AlignedChartData data in AlignedData_byScore)
                {
                    string diff_name;
                    switch (data.getDiffPrefix())
                    {
                        case "n": diff_name = "Normal"; break;
                        case "h": diff_name = "Hyper"; break;
                        case "ex": diff_name = "EX"; break;
                        case "rave": diff_name = "RAVE"; break;
                        default: diff_name = data.getDiffPrefix(); break;
                    }
                    GameObject si = Instantiate(scrollItem, content);
                    si.name = "scrollItem" + j;
                    si.transform.Find("title").GetComponent<Text>().text = data.getTitle();
                    si.transform.Find("artist").GetComponent<Text>().text = diff_name;
                    int score = ScoreData.getScoreFromChart(data).getScore();
                    if (score == -1) si.transform.Find("level").GetComponent<Text>().text = "";
                    else si.transform.Find("level").GetComponent<Text>().text = ScoreData.getScoreFromChart(data).getScore() + "";
                    si.GetComponent<RectTransform>().anchoredPosition = new Vector2(460, -j * 300 - 530);

                    content.sizeDelta = new Vector2(786, 300 * AlignedData_byScore.Count + 780);

                    itemList.Add(si);
                    j++;
                }
                break;
        }

        if (itemList.Count > 0)
        {
            itemList[0].transform.Find("title").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            itemList[0].transform.Find("artist").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            itemList[0].transform.Find("level").GetComponent<Text>().color = new Color(1, 1, 1, 1);
        }

        string iTitle = itemList[0].transform.Find("title").GetComponent<Text>().text;
        GetComponent<AudioSource>().clip =
                Resources.Load<AudioClip>("Chart/" + iTitle + "/preview");
        GetComponent<AudioSource>().Play();
        bg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Chart/" + iTitle + "/" + iTitle);
    }
    public void ChangeSortMode()
    {
        sortMode++;
        if (sortMode > 3) sortMode = 0;

        content.anchoredPosition = new Vector2(0, 0);

        foreach (GameObject g in itemList)
        {
            GameObject.Destroy(g);
        }
        itemList.Clear();

        RefreshSort();


    }
    private void Update()
    {


        if (currentDisplay == 0)
        {
            int i = (int)Mathf.Round(content.anchoredPosition.y / 300.0f);
            if (i > itemList.Count - 1) i = itemList.Count - 1;
            if (i < 0) i = 0;

            if (prev_i != i)
            {
                if (prev_i != -1)
                {
                    itemList[prev_i].transform.Find("title").GetComponent<Text>().color = new Color(1, 1, 1, 0.25f);
                    itemList[prev_i].transform.Find("artist").GetComponent<Text>().color = new Color(1, 1, 1, 0.25f);
                    itemList[prev_i].transform.Find("level").GetComponent<Text>().color = new Color(1, 1, 1, 0.25f);
                }
                itemList[i].transform.Find("title").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                itemList[i].transform.Find("artist").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                itemList[i].transform.Find("level").GetComponent<Text>().color = new Color(1, 1, 1, 1);

            }

            prev_i = i;
            if (content.parent.parent.GetComponent<ScrollEventChecker>().isReleased)
            {
                float delta_y = (SnapTo(i) - content.localPosition.y) / 10;
                content.localPosition += (new Vector3(0, delta_y, 0));

                if (Math.Abs(delta_y) < Time.deltaTime * 10)
                {
                    string iTitle = itemList[i].transform.Find("title").GetComponent<Text>().text;
                    GetComponent<AudioSource>().clip =
                        Resources.Load<AudioClip>("Chart/" + iTitle + "/preview");
                    if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play();
                    bg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Chart/" + iTitle + "/" + iTitle);
                }

            }

            Vector3 originalPos = descSort.anchoredPosition;
            if (descSort.anchoredPosition.y > sortMode * 100.0f - 1.0f &&
                descSort.anchoredPosition.y < sortMode * 100.0f + 1.0f) { descSort.anchoredPosition = new Vector2(0, sortMode * 100.0f); }
            else if (descSort.anchoredPosition.y > sortMode * 100.0f)
                descSort.anchoredPosition = descSort.anchoredPosition + new Vector2(0, -10.0f);
            else if (descSort.anchoredPosition.y < sortMode * 100.0f)
                descSort.anchoredPosition = descSort.anchoredPosition + new Vector2(0, 10.0f);
        }
        else if(currentDisplay == 1)
        {

            int i = (int)Mathf.Round(content.anchoredPosition.y / 300.0f);

            int iNormal, iHard, iEX, iRave;
            switch(sortMode)
            {
                case 0:
                    iNormal = test.Items[i].normal;
                    iHard = test.Items[i].hard;
                    iEX = test.Items[i].ex;
                    iRave = test.Items[i].rave;
                    if (currentDiff != -1)
                    {
                        switch (currentDiff)
                        {
                            case 0:
                                if (iNormal == -1) currentDiff = -1;
                                break;
                            case 1:
                                if (iHard == -1) currentDiff = -1;
                                break;
                            case 2:
                                if (iEX == -1) currentDiff = -1;
                                break;
                            case 3:
                                if (iRave == -1) currentDiff = -1;
                                break;
                        }
                    }
                    if (currentDiff == -1) currentDiff = 0;
                    if (currentDiff == 0 && iNormal == -1)
                    {
                        currentDiff++;
                        if (iHard == -1)
                        {
                            currentDiff++;
                            if (iEX == -1)
                            {
                                currentDiff++;
                                if (iRave == -1) currentDiff = -1;
                            }
                        }
                    }

                    Title.text = test.Items[i].title;
                    Artist.text = test.Items[i].artist;
                    break;
                case 1:
                    //SortedData
                    iNormal = SortedData[i].getNormal();
                    iHard = SortedData[i].getHard();
                    iEX = SortedData[i].getEX();
                    iRave = SortedData[i].getRave();
                    if (currentDiff != -1)
                    {
                        switch (currentDiff)
                        {
                            case 0:
                                if (iNormal == -1) currentDiff = -1;
                                break;
                            case 1:
                                if (iHard == -1) currentDiff = -1;
                                break;
                            case 2:
                                if (iEX == -1) currentDiff = -1;
                                break;
                            case 3:
                                if (iRave == -1) currentDiff = -1;
                                break;
                        }
                    }
                    if (currentDiff == -1) currentDiff = 0;
                    if (currentDiff == 0 && iNormal == -1)
                    {
                        currentDiff++;
                        if (iHard == -1)
                        {
                            currentDiff++;
                            if (iEX == -1)
                            {
                                currentDiff++;
                                if (iRave == -1) currentDiff = -1;
                            }
                        }
                    }
                    Title.text = SortedData[i].getTitle();
                    Artist.text = SortedData[i].getArtist();
                    break;
                case 2:
                    //AlignedData
                    iNormal = -1;
                    iHard = -1;
                    iEX = -1;
                    iRave = -1;
                    switch (AlignedData_byLevel[i].getDiffPrefix())
                    {
                        case "n":
                            iNormal = AlignedData_byLevel[i].getNormal();
                            currentDiff = 0;
                            break;
                        case "h":
                            iHard = AlignedData_byLevel[i].getHard();
                            currentDiff = 1;
                            break;
                        case "ex":
                            iEX = AlignedData_byLevel[i].getEX();
                            currentDiff = 2;
                            break;
                        case "rave":
                            iRave = AlignedData_byLevel[i].getRave();
                            currentDiff = 3;
                            break;
                        default:
                            currentDiff = -1;
                            break;
                    }
                    Title.text = AlignedData_byLevel[i].getTitle();
                    Artist.text = AlignedData_byLevel[i].getArtist();
                    break;
                case 3:
                    iNormal = -1;
                    iHard = -1;
                    iEX = -1;
                    iRave = -1;
                    switch (AlignedData_byScore[i].getDiffPrefix())
                    {
                        case "n":
                            iNormal = AlignedData_byScore[i].getNormal();
                            currentDiff = 0;
                            break;
                        case "h":
                            iHard = AlignedData_byScore[i].getHard();
                            currentDiff = 1;
                            break;
                        case "ex":
                            iEX = AlignedData_byScore[i].getEX();
                            currentDiff = 2;
                            break;
                        case "rave":
                            iRave = AlignedData_byScore[i].getRave();
                            currentDiff = 3;
                            break;
                        default:
                            currentDiff = -1;
                            break;
                    }
                    Title.text = AlignedData_byScore[i].getTitle();
                    Artist.text = AlignedData_byScore[i].getArtist();
                    break;
                default:
                    iNormal = -1;
                    iHard = -1;
                    iEX = -1;
                    iRave = -1;
                    Title.text = "";
                    Artist.text = "";
                    break;

            }


            if (iNormal != -1)
            {
                diff_n.GetComponent<Image>().color = new Color32(0, 75, 255, 64);
                diff_n.transform.Find("level").GetComponent<Text>().text = iNormal+"";
                diff_n.transform.Find("level").GetComponent<Text>().color = new Color32(255,255,255, 64);
            }
            else
            {
                diff_n.GetComponent<Image>().color = new Color32(0, 75, 255, 64);
                diff_n.transform.Find("level").GetComponent<Text>().text = "";
                diff_n.transform.Find("level").GetComponent<Text>().color = new Color(0, 0, 0, 0);
            }


            if (iHard != -1)
            {
                diff_h.GetComponent<Image>().color = new Color32(255, 29, 0, 64);
                diff_h.transform.Find("level").GetComponent<Text>().text = iHard + "";
                diff_h.transform.Find("level").GetComponent<Text>().color = new Color32(255,255,255, 64);
            }
            else
            {
                diff_h.GetComponent<Image>().color = new Color32(255, 29, 0, 64);
                diff_h.transform.Find("level").GetComponent<Text>().text = "";
                diff_h.transform.Find("level").GetComponent<Text>().color = new Color(0, 0, 0, 0);
            }


            if (iEX != -1)
            {
                diff_ex.GetComponent<Image>().color = new Color32(190, 0, 255, 64);
                diff_ex.transform.Find("level").GetComponent<Text>().text = iEX + "";
                diff_ex.transform.Find("level").GetComponent<Text>().color = new Color32(255,255,255, 64);
            }
            else
            {
                diff_ex.GetComponent<Image>().color = new Color32(190, 0, 255, 64);
                diff_ex.transform.Find("level").GetComponent<Text>().text = "";
                diff_ex.transform.Find("level").GetComponent<Text>().color = new Color(0, 0, 0, 0);
            }


            if (iRave != -1)
            {
                diff_rave.GetComponent<Image>().color = new Color32(0, 255, 206, 64);
                diff_rave.transform.Find("level").GetComponent<Text>().text = iRave + "";
                diff_rave.transform.Find("level").GetComponent<Text>().color = new Color32(255,255,255, 64);
            }
            else
            {
                diff_rave.GetComponent<Image>().color = new Color32(0, 255, 206, 64);
                diff_rave.transform.Find("level").GetComponent<Text>().text = "";
                diff_rave.transform.Find("level").GetComponent<Text>().color = new Color(0, 0, 0, 0);
            }

            switch(currentDiff)
            {
                case 0:
                    diff_n.GetComponent<Image>().color = new Color32(0, 75, 255, 255);
                    diff_n.transform.Find("level").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                    break;
                case 1:
                    diff_h.GetComponent<Image>().color = new Color32(255, 29, 0, 255);
                    diff_h.transform.Find("level").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                    break;
                case 2:
                    diff_ex.GetComponent<Image>().color = new Color32(190, 0, 255, 255);
                    diff_ex.transform.Find("level").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                    break;
                case 3:
                    diff_rave.GetComponent<Image>().color = new Color32(0, 255, 206, 255);
                    diff_rave.transform.Find("level").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                    break;
                default:
                    break;
            }
        }
    }
    public void SetCurrentDifficulty(int d)
    {
        int i = (int)Mathf.Round(content.anchoredPosition.y / 300.0f);
        if (i > itemList.Count - 1) i = itemList.Count - 1;
        if (i < 0) i = 0;

        bool isValid = false;

        switch (d)
        {
            case 0:
                if (diff_n.transform.Find("level").GetComponent<Text>().text != "")
                {
                    diff_n.GetComponent<Image>().color = new Color32(0, 75, 255, 64);
                    currentDiff = d;
                    isValid = true;
                }
                break;
            case 1:
                if (diff_h.transform.Find("level").GetComponent<Text>().text != "")
                {
                    diff_h.GetComponent<Image>().color = new Color32(255,29,0, 64);
                    currentDiff = d;
                    isValid = true;
                }
                break;
            case 2:
                if (diff_ex.transform.Find("level").GetComponent<Text>().text != "")
                {
                    diff_ex.GetComponent<Image>().color = new Color32(190,0,255, 64);
                    currentDiff = d;
                    isValid = true;
                }
                break;
            case 3:
                if (diff_rave.transform.Find("level").GetComponent<Text>().text != "")
                {
                    diff_rave.GetComponent<Image>().color = new Color32(0, 255,206, 64);
                    currentDiff = d;
                    isValid = true;
                }
                break;
            default:
                break;
        }
        if(isValid)
        {
            switch(currentDiff)
            {
                case 0:
                    diff_n.GetComponent<Image>().color = new Color32(0, 75, 255, 255);
                    break;
                case 1:
                    diff_h.GetComponent<Image>().color = new Color32(255, 29, 0, 255);
                    break;
                case 2:
                    diff_ex.GetComponent<Image>().color = new Color32(190, 0, 255, 255);
                    break;
                case 3:
                    diff_rave.GetComponent<Image>().color = new Color32(0, 255, 206, 255);
                    break;
            }
        }
    }
    public void ChangeDisplayMode(int m)
    {
        if(/*currentDisplay == 0*/m==0)
        {
            currentDisplay = -1;

            int i = (int)Mathf.Round(content.anchoredPosition.y / 300.0f);
            string iTitle = itemList[i].transform.Find("title").GetComponent<Text>().text;
            if (bg.GetComponent<Image>().sprite.name != iTitle)
            {
                GetComponent<AudioSource>().clip =
                        Resources.Load<AudioClip>("Chart/" + iTitle + "/preview");
                GetComponent<AudioSource>().Play();
                bg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Chart/" + iTitle + "/" + iTitle);
            }

            StartCoroutine("OpenSongExtended");
        }
        else if(/*currentDisplay == 1*/m==1)
        {
            currentDisplay = -1;
            diff_n.transform.Find("level").GetComponent<Text>().text = "";
            diff_h.transform.Find("level").GetComponent<Text>().text = "";
            diff_ex.transform.Find("level").GetComponent<Text>().text = "";
            diff_rave.transform.Find("level").GetComponent<Text>().text = "";
            diff_n.GetComponent<Image>().color = new Color32(0, 75, 255, 64);
            diff_h.GetComponent<Image>().color = new Color32(255, 29, 0, 64);
            diff_ex.GetComponent<Image>().color = new Color32(190, 0, 255, 64);
            diff_rave.GetComponent<Image>().color = new Color32(0, 255, 206, 64);

            Title.text = "";
            Artist.text = "";
            StartCoroutine("OpenScrollView");
        }
    }

    IEnumerator OpenSongExtended()
    {
        
        while (true)
        {
            Color color = ScrollView.GetComponent<Image>().color;
            Vector2 pos = ScrollView.GetComponent<RectTransform>().anchoredPosition;
            pos.x -= Time.deltaTime*3400.0f;
            if(color.a > 0) color.a -= Time.deltaTime*4;
            ScrollView.GetComponent<RectTransform>().anchoredPosition = pos;
            ScrollView.GetComponent<Image>().color = color;

            if (ScrollView.GetComponent<RectTransform>().anchoredPosition.x <= -400.0f) break;
            yield return null;
        }
        ScrollView.SetActive(false);
        SongExtended.SetActive(true);
        while (true)
        {
            Color color = SongExtended.GetComponent<Image>().color;
            color.a += Time.deltaTime*3;
            SongExtended.GetComponent<Image>().color = color;
            if (SongExtended.GetComponent<Image>().color.a >= 0.875f) break;
            yield return null;
        }
        currentDisplay = 1;
    }

    IEnumerator OpenScrollView()
    {
        while (true)
        {
            Color color = SongExtended.GetComponent<Image>().color;
            color.a -= Time.deltaTime*3;
            SongExtended.GetComponent<Image>().color = color;
            if (SongExtended.GetComponent<Image>().color.a <= 0) break;
            yield return null;
        }
        SongExtended.SetActive(false);
        ScrollView.SetActive(true);
        while (true)
        {
            Color color = ScrollView.GetComponent<Image>().color;
            Vector2 pos = ScrollView.GetComponent<RectTransform>().anchoredPosition;
            if(color.a < 0.875f) color.a += Time.deltaTime*4;
            pos.x += Time.deltaTime * 3400.0f;
            ScrollView.GetComponent<RectTransform>().anchoredPosition = pos;
            ScrollView.GetComponent<Image>().color = color;

            if (ScrollView.GetComponent<RectTransform>().anchoredPosition.x >= 450.0f) break;
            yield return null;
        }
        currentDisplay = 0;
    }

    AlignedChartData getCurrentObject()
    {
        int i = (int)Mathf.Round(content.anchoredPosition.y / 300.0f);
        switch(sortMode)
        {
            case 0:
                return originalData[i];
            case 1:
                return SortedData[i];
            case 2:
                return AlignedData_byLevel[i];
            case 3:
                return AlignedData_byScore[i];
        }
        return null;
    }
    public void startChart()
    {
        string title = getCurrentObject().getTitle();
        string diff = "n";
        switch(currentDiff)
        {
            case 0:
                diff = "n";
                break;
            case 1:
                diff = "h";
                break;
            case 2:
                diff = "ex";
                break;
            case 3:
                diff = "rave";
                break;
        }
        PlayerPrefs.SetString("TargetChart", title);
        PlayerPrefs.SetString("TargetDiff", diff);
        PlayerPrefs.Save();
        SceneManager.LoadScene("inGame");
    }
}

