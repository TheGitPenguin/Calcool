using System.Drawing.Drawing2D;
using Microsoft.VisualBasic;
using Calcool;

namespace MainPage;

public class MainPageForm : Form
{
    private TitleBar _TitleBar;
    public Panel _PanelView;
    public Point _LocationBeforeMinimize;

    static private MainPageForm _MainPage;

    static private Dictionary<string, Form> _Instances;

    static private Form _CurrentInstance;

    static private Thread _ThreadMainPage;



    public MainPageForm()
    {
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Size = new Size(500, 800);
        this.BackColor = Color.FromArgb(20, 20, 30);
        this.FormBorderStyle = FormBorderStyle.None;
        this.Text = "CALCOOl";
        GraphicsPath pathMainPAge = Program.GetPath(this.Width, this.Height, 10);
        this.Region = new Region(pathMainPAge);

        _PanelView = new Panel();
        _PanelView.Size = new Size(this.Width, this.Height - 35);
        _PanelView.Location = new Point(0, 35);
        _PanelView.BackColor = Color.FromArgb(25, 25, 35);

        GraphicsPath pathPanelView = Program.GetPath(_PanelView.Width, _PanelView.Height, 10);
        _PanelView.Region = new Region(pathPanelView);

        _TitleBar = new TitleBar(this);

        this.Controls.Add(_PanelView);
        this.Controls.Add(_TitleBar);

        this.FormClosing += MainPageClosing;
        this.Resize += MainPageRestoring;
        _LocationBeforeMinimize = new Point(-1, -1);
    }

    private void MainPageRestoring(object sender, EventArgs e)
    {
        if (_LocationBeforeMinimize.X == -1)
        {
            return;
        }

        if (this.WindowState != FormWindowState.Normal)
        {
            return;
        }

        int distance = Screen.PrimaryScreen.Bounds.Height - this._LocationBeforeMinimize.Y;

        int delta = distance / 15;

        for (int i = 0; i < 15; i++){
            this.FindForm().Location = new Point(this.Location.X, this.Location.Y - delta);
            Thread.Sleep(1);
        }
    }

    private void MainPageClosing(object sender, FormClosingEventArgs e)
    {
        _TitleBar.SetTitle("BYE BYE");
        for (int i = 0; i < 34; i++)
        {
            this.Opacity = this.Opacity - 0.03;
            Thread.Sleep(1);
        }
    }

    private void DisplayInstance()
    {
        if (_CurrentInstance == null)
        {
            return;
        }

        if (_PanelView.InvokeRequired)
        {
            _PanelView.Invoke(new Action(DisplayInstance));
            return;
        }

        if (_PanelView.Controls.Count != 0)
        {
            if (_CurrentInstance == _PanelView.Controls[0])
            {
                return;
            }
        }

        _CurrentInstance.TopLevel = false;
        _CurrentInstance.Dock = DockStyle.Fill;
        _CurrentInstance.FormBorderStyle = FormBorderStyle.None;

        _PanelView.Controls.Clear();
        _PanelView.Controls.Add(_CurrentInstance);

        _CurrentInstance.Show();
    }



    static public void AddInstance(string name, Form form)
    {
        _Instances.Add(name, form);
        _CurrentInstance = form;

        _MainPage.DisplayInstance();
    }

    static public void RemoveInstance(string name)
    {
        _Instances[name].Close();
        _Instances.Remove(name);

        if (_Instances.Count > 0)
        {
            _CurrentInstance = _Instances.Values.Last();
        }
        else
        {
            _CurrentInstance = null;
        }
    }


    public Point GetCenter()
    {
        return new Point(this.Size.Width / 2, this.Size.Height / 2);
    }

    static public void Lunch()
    {
        _Instances = new Dictionary<string, Form>();
        _MainPage = new MainPageForm();

        Thread _ThreadMainPage = new Thread(() =>
        {
            Application.Run(_MainPage);
        });
        _ThreadMainPage.SetApartmentState(ApartmentState.STA); // Définit l'état d'appartement sur STA
        _ThreadMainPage.Start();
    }
}