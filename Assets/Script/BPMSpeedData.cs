public class BPMSpeedData
{
    public Bunsu timing = new Bunsu(0, 1);
};

public class BPMData : BPMSpeedData
{
    public float BPM = 0.0f;
    public BPMData(float BPM, Bunsu timing)
    {
        this.BPM = BPM;
        this.timing = timing;
    }
}

public class SpeedData : BPMSpeedData
{
    public float Speed = 0.0f;
    public float second = 0.0f;
    public SpeedData(float Speed, Bunsu timing)
    {
        this.Speed = Speed;
        this.timing = timing;
    }
}