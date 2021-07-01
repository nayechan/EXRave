using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartManager : MonoBehaviour
{
    inGamePlay inGamePlay;

    float prev_second = 0.0f, current_position, current_second;
    public float chart_offset = 0.0f, user_offset = 0.0f, total_offset = 0.0f, ready_time = 5.0f;
    int index_begin, index_end, index_begin_sv, index_end_sv;

    public List<GameObject> Notes;
    int total_notes = 0;

    public List<BPMSpeedData> SVs;
    public float current_speed = 1.0f, user_speed = 180.0f;
    
    public bool DataInitFinish = false, isChartReady = false, isStarted = false;

    public GameObject normalNote, longNote, slideNote;

    Vector3[] SlideBaseVector=
    {
        new Vector3(0,3.985f,0),
        new Vector3(3.985f,0,0),
        new Vector3(0,-3.985f,0),
        new Vector3(-3.985f,0,0)
    };

    Vector3[] BaseVector =
    {
        new Vector3(-2,3.985f,0),
        new Vector3(2,3.985f,0),
        new Vector3(3.985f,2,0),
        new Vector3(3.985f,-2,0),
        new Vector3(2,-3.985f,0),
        new Vector3(-2,-3.985f,0),
        new Vector3(-3.985f,-2,0),
        new Vector3(-3.985f,2,0)
    };
    
    Quaternion[] BaseRotation =
    {
        Quaternion.Euler(90,0,0),
        Quaternion.Euler(0,90,90),
        Quaternion.Euler(90,0,0),
        Quaternion.Euler(0,90,90),
    };


    // Start is called before the first frame update
    void Start()
    {
        Notes = new List<GameObject>();
        SVs = new List<BPMSpeedData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DataInitFinish && !isChartReady)
        {
            SVs.Sort(delegate (BPMSpeedData a, BPMSpeedData b)
            {
                if (a.timing > b.timing) return 1;
                else if (a.timing < b.timing) return -1;
                else return 0;
            });
            Notes.Sort(delegate (GameObject a, GameObject b)
            {
                Note A = a.GetComponent<NoteMovement>().note;
                Note B = b.GetComponent<NoteMovement>().note;
                if (A.beat > B.beat) return 1;
                else if (A.beat < B.beat) return -1;
                else return 0;
            });

            total_offset = chart_offset + user_offset;
            current_position = total_offset;

            total_notes = Notes.Count;

            current_second = total_offset - ready_time;
            current_position = total_offset - ready_time;

            isChartReady = true;
        }

        if (!GetComponent<GamePause>().PauseUI.activeSelf && isChartReady
            )
        {
            if (ready_time > 0)
            {
                ready_time -= Time.deltaTime;
                current_second += Time.deltaTime;
                current_position += Time.deltaTime;
                if (current_second > 0) current_second = 0;
                if (current_position > 0) current_position = 0;
            }
            else
            {
                if (!isStarted)
                {
                    inGamePlay.aus.Play();
                    isStarted = true;
                }
                else
                {
                    current_second += Time.deltaTime;
                    current_position += Time.deltaTime * current_speed;
                }
            }

        }

        if (SVs.Count > 0)
        {
            SpeedData speedData;
            int i = 0;
            foreach(BPMSpeedData svdata in SVs)
            {
                if (svdata.GetType() == typeof(SpeedData))
                {
                    speedData = (SpeedData)svdata;
                    if(getSecond((float)speedData.timing) <= current_second)
                    {
                        current_speed = speedData.Speed;
                        SVs.RemoveAt(i);
                        break;
                    }
                }
                i++; 
            }
        }


        prev_second = current_second;
    }

    float getSecond(float beat)
    {
        float ResultSecond = 0.0f;
        Bunsu timing = new Bunsu(0, 1);
        float cbpm = 120.0f;
        bool flag = true;
        foreach (BPMSpeedData svdata in SVs)
        {
            BPMData data = (BPMData)svdata;
            if ((float)data.timing > beat)
            {
                ResultSecond += (60 / cbpm) * (beat - (float)timing);
                cbpm = data.BPM;
                flag = false;
                break;
            }
            else
            {
                ResultSecond += (60 / cbpm) * (float)(data.timing - timing);
                cbpm = data.BPM;
                timing = data.timing;
            }

        }
        if (beat > (float)timing && flag)
        {
            ResultSecond += (60 / cbpm) * (float)(beat - (float)timing);
        }
        return ResultSecond;
    }

    float getPositionSum(float second)
    {
        float ResultPosition = 0.0f;
        float timing = 0;
        float cspeed = 1;
        foreach (BPMSpeedData svdata in SVs)
        {
            SpeedData data = (SpeedData)svdata;
            if ((float)data.second > second)
            {
                ResultPosition += (cspeed * (second - timing));
                cspeed = data.Speed;
                timing = second;
                break;
            }
            else
            {
                ResultPosition += (cspeed * (data.second - timing));
                cspeed = data.Speed;
                timing = data.second;
            }
        }
        if (second > timing)
        {
            ResultPosition += cspeed * (second - timing);
        }
        return ResultPosition;
    }

    public void AddNote(Note n)
    {
        switch(n.GetType().FullName)
        {
            case "NormalNote":
                NormalNote nn = (NormalNote)n;
                Vector3 v = BaseVector[(nn.face - 1) * 2 + nn.key - 1];
                Quaternion q = BaseRotation[nn.face - 1];
                v.z = (getPositionSum(getSecond(n.beat)) - current_position) * user_speed;
                GameObject g = GameObject.Instantiate(normalNote, v, q);
                g.name = "normalNote";
                break;
            case "LongNote":
                break;
            case "SlideNote":
                break;
        }
        total_notes++;
    }
}
