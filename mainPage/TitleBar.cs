using System.Windows.Forms.VisualStyles;

namespace MainPage;

public class TitleBar : Panel
{
    private bool _Dragging = false;
    private Point _DragCursorPoint;
    private Point _DragFormPoint;
    private Button _Close;
    private Button _Minimize;
    private Label _Title;
    private MainPageForm _Form;

    public TitleBar(MainPageForm form = null)
    {
        this.Dock = DockStyle.Top;
        this.Height = 35;
        this.BackColor = Color.FromArgb(20, 20, 30);
        this.MouseDown += TitleBar_MouseDown;
        this.MouseMove += TitleBar_MouseMove;
        this.MouseUp += TitleBar_MouseUp;

        this._Close = new ButtonState(Image.FromFile("img\\emergency-exit.png"), 40, 35, Color.Transparent, Color.White, Color.FromArgb(200, 0, 0),30);
        this._Close.Dock = DockStyle.Right;

        this._Minimize = new ButtonState(Image.FromFile("img\\eyes.png"), 40, 35, Color.Transparent, Color.White, Color.FromArgb(30, 30, 30),25);
        this._Minimize.Dock = DockStyle.Right;

        this._Close.Click += Close_Click;
        this._Minimize.Click += Minimize_Click;
        
        this._Title = new Label();
        this._Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this._Title.Location = new Point(form.GetCenter().X-(_Title.Size.Width/2), _Title.Location.Y);
        this._Title.Text = "CALCOOl";
        this._Title.Font = new Font("Arial", 12, FontStyle.Bold);
        this._Title.ForeColor = Color.White;
        this._Title.BackColor = Color.FromArgb(20, 20, 30);

        this._Title.Height = 35;

        this._Title.MouseDown += TitleBar_MouseDown;
        this._Title.MouseMove += TitleBar_MouseMove;
        this._Title.MouseUp += TitleBar_MouseUp;

        this.Controls.Add(_Minimize);
        this.Controls.Add(_Close);
        this.Controls.Add(_Title);

        _Form = form;
    }

    private void Close_Click(object sender, EventArgs e)
    {
        _Close.Cursor = Cursors.Default;
        if (_Form == null)
            this.FindForm().Close();
        else
            _Form.Close();
    }

    private void Minimize_Click(object sender, EventArgs e)
    {
        _Minimize.Cursor = Cursors.Default;

        int distance = Screen.PrimaryScreen.Bounds.Height - _Form.Location.Y;

        _Form._LocationBeforeMinimize = _Form.Location;

        int delta = distance / 15;

        for (int i = 0; i < 15; i++){
            if (_Form == null)
                this.FindForm().Location = new Point(_Form.Location.X, _Form.Location.Y + delta);
            else
                _Form.Location = new Point(_Form.Location.X, _Form.Location.Y + delta);
            Thread.Sleep(1);
        }

        if (_Form == null)
            this.FindForm().WindowState = FormWindowState.Minimized;
        else
            _Form.WindowState = FormWindowState.Minimized;

        _Minimize.Cursor = Cursors.Hand;
    }


    private void TitleBar_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right){
            return;
        }

        this.Cursor = Cursors.SizeAll;

        _Dragging = true;
        _DragCursorPoint = Cursor.Position;
        if (_Form == null)
            _DragFormPoint = this.FindForm().Location;
        else
            _DragFormPoint = this._Form.Location;
    }

    private void TitleBar_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_Dragging){
            return;
        }

        Point dif = Point.Subtract(Cursor.Position, new Size(_DragCursorPoint));
        if (_Form == null)
            this.FindForm().Location = Point.Add(_DragFormPoint, new Size(dif));
        else
            _Form.Location = Point.Add(_DragFormPoint, new Size(dif));    
    }

    private void TitleBar_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right){
            return;
        }

        this.Cursor = Cursors.Default;

        _Dragging = false;
    }

    public void SetTitle(string text)
    {
        _Title.Text = text;
        _Title.Refresh();
    }
}