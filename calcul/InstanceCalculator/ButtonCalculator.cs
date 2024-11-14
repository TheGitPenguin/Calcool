namespace calcul;

public class ButtonCalculator : Button
{
    private string _Text;
    private int _Width;
    private int _Height;
    private int _X;
    private int _Y;

    public ButtonCalculator(string text, int width, int height, int x, int y)
    {
        _Text = text;
        _Width = width;
        _Height = height;
        _X = x;
        _Y = y;

        this.DataContext = _Text;
        this.Text = _Text;
        this.Width = _Width;
        this.Height = _Height;
        this.Location = new Point(_X, _Y);
        this.BackColor = Color.FromArgb(55, 55, 65);
        this.ForeColor = Color.White;
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;
        this.FlatAppearance.BorderColor = Color.FromArgb(55, 55, 65);
        this.FlatAppearance.MouseDownBackColor = Color.FromArgb(55, 55, 65);
        this.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 55, 65);
        this.Font = new Font("Arial", 12, FontStyle.Bold);
    }
}