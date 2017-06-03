namespace MGraph
{
    public class Label
    {
        string _text;

        public Label(string text = "")
        {
            _text = text;
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}
