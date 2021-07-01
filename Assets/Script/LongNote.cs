public class LongNote : Note
{
    public int face;
    public int key;
    public float end_beat;
    public float end_second;
    public float end_pos;
    public LongNote(float _beat, float _end_beat, int _face, int _key):base(_beat)
    {
        face = _face;
        key=_key;
        end_beat = _end_beat;
    }
}