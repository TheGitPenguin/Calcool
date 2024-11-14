namespace MainPage;

public class ButtonState : Button
{
    public Color? _BackColor;
    public Color? _BackColorHover;

    public ButtonState(string text = "", int width = -1, int height = -1, Color? backColor = null, Color? foreColor = null, Color? backColorHover = null)
    {
        if (width != -1 && height != -1)
            this.Size = new Size(width, height);
        this.Text = text;
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;

        if (backColor != null){
            this.BackColor = (Color)backColor;
        }
        if (foreColor != null){
            this.ForeColor = (Color)foreColor;
        }

        this._BackColor = backColor;
        this._BackColorHover = backColorHover;

        this.Font = new Font("Arial", 12, FontStyle.Bold);
        this.Cursor = Cursors.Hand;

        this.MouseEnter += Hover;
        this.MouseLeave += Leave;
    }


    public ButtonState(Image img = null, int width = -1, int height = -1, Color? backColor = null, Color? foreColor = null, Color? backColorHover = null, int iconSize = 20)
    {
        if (width != -1 && height != -1)
            this.Size = new Size(width, height);

        if (img != null){
            Image img2 = new Bitmap(img, new Size(iconSize, iconSize));
            this.BackgroundImage = img2;
            this.BackgroundImageLayout = ImageLayout.Center;
        }

        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;

        if (backColor != null){
            this.BackColor = (Color)backColor;
        }
        if (foreColor != null){
            this.ForeColor = (Color)foreColor;
        }

        this._BackColor = backColor;
        this._BackColorHover = backColorHover;

        this.Font = new Font("Arial", 12, FontStyle.Bold);
        this.Cursor = Cursors.Hand;

        this.MouseEnter += Hover;
        this.MouseLeave += Leave;
    }

    private void Hover(object sender, EventArgs e)
    {
        if (_BackColorHover != null)
            this.BackColor = (Color)_BackColorHover;

    }

    private void Leave(object sender, EventArgs e)
    {
        if (_BackColor != null)
            this.BackColor = (Color)_BackColor;
    }
}