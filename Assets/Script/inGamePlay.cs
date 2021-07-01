using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inGamePlay : MonoBehaviour
{

    string title, diff;
    public AudioSource aus;
    public Text score, time;
    public ChartManager chartManager;

    // Start is called before the first frame update
    void Start()
    {

        title = PlayerPrefs.GetString("TargetChart", "error");
        diff = PlayerPrefs.GetString("TargetDiff", "error");
        aus = GetComponent<AudioSource>();

        LoadChart(title, diff);
    }

    void LoadChart(string name, string diff)
    {
        AudioClip song = Instantiate(Resources.Load<AudioClip>("chart/" + name + "/" + name));
        aus.clip = song;
        TextAsset chartData = Resources.Load<TextAsset>("chart/" + name + "/" + name + "_" + diff);
        string[] chartString = chartData.text.Split('\n');
        if (chartString[0].Trim().Equals("exrave chart file v1"))
        {
            string title = chartString[2].Replace("Title ", " ").Trim();
            string artist = chartString[3].Replace("Artist ", " ").Trim();

            int i = 3;
            while (chartString[i].Trim() != "== BPMData ==") i++;
            string initbpm = chartString[i + 1].Trim().Split(' ')[1];
            string offset = chartString[i + 2].Trim().Split(' ')[1];
            chartManager.SVs.Add(new BPMData(float.Parse(initbpm), new Bunsu(0, 1)));
            chartManager.SVs.Add(new SpeedData(1.0f, new Bunsu(0, 1)));
            chartManager.chart_offset = float.Parse(offset);
            GameObject.Find("songtitle").GetComponent<Text>().text = title;
            while (chartString[i] == "") i++;
            string str;
            while ((str = chartString[i].Trim()) != "== NoteData ==")
            {
                string[] tmp2 = str.Split(' ');
                if (tmp2[0] == "Speed")
                {
                    string speed = tmp2[1];
                    string[] timing = tmp2[2].Split('/');
                    Bunsu timingData = new Bunsu(int.Parse(timing[0]), int.Parse(timing[1]));
                    float speedv = float.Parse(speed);
                    SpeedData speedData = new SpeedData(speedv, timingData);
                    chartManager.SVs.Add(speedData);

                }
                else if (tmp2[0] == "BPM")
                {
                    string bpm = tmp2[1];
                    string[] timing = tmp2[2].Split('/');
                    Bunsu timingData = new Bunsu(int.Parse(timing[0]), int.Parse(timing[1]));
                    float bpmv = float.Parse(bpm);
                    BPMData bpmData = new BPMData(bpmv, timingData);
                    chartManager.SVs.Add(bpmData);
                }
                i++;

            }
            while ((str = chartString[i].Trim()) != "")
            {
                string[] tmp2 = str.Split(' ');
                if (tmp2[0] == "NormalNote")
                {
                    string beat = tmp2[1];
                    string face = tmp2[2];
                    string key = tmp2[3];

                    beat = beat.Replace("beat:", "");
                    face = face.Replace("face:", "");
                    key = key.Replace("key:", "");

                    float beatv = float.Parse(beat);
                    int facev = int.Parse(face);
                    int keyv = int.Parse(key);

                    NormalNote normalNote = new NormalNote(beatv, facev, keyv);
                    chartManager.AddNote(normalNote);

                }
                else if (tmp2[0] == "LongNote")
                {
                    string beat = tmp2[1];
                    string face = tmp2[2];
                    string key = tmp2[3];
                    string end = tmp2[4];

                    beat = beat.Replace("beat:", "");
                    face = face.Replace("face:", "");
                    key = key.Replace("key:", "");
                    end = end.Replace("end:", "");

                    float beatv = float.Parse(beat);
                    int facev = int.Parse(face);
                    int keyv = int.Parse(key);
                    float endv = float.Parse(end);
                    LongNote longNote = new LongNote(beatv, endv, facev, keyv);
                    chartManager.AddNote(longNote);

                }
                else if (tmp2[0] == "SlideNote")
                {
                    string beat = tmp2[1];
                    string face = tmp2[2];
                    string pos = tmp2[3];

                    beat = beat.Replace("beat:", "");
                    face = face.Replace("face:", "");
                    pos = pos.Replace("pos:", "");

                    float beatv = float.Parse(beat);
                    int facev = int.Parse(face);
                    float posv = float.Parse(pos);
                    SlideNote slideNote = new SlideNote(beatv, facev, posv);
                    chartManager.AddNote(slideNote);
                }

                i++;
            }
            
            chartManager.DataInitFinish = true;
        }
    }

    void NoteMovement()
    {
        
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
