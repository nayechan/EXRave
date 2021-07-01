public class SlideNote : Note{
    public int face;
    public float x_pos;
    public SlideNote(float _beat, int _face,float _x_pos):base(_beat)
    {
        face = _face;
        x_pos = _x_pos;
    }
}