public class NormalNote : Note{
    public int key;
    public int face;
    public NormalNote(float _beat, int _face,int _key) :base(_beat)
    {
        face = _face;
        key =_key;
    }
}