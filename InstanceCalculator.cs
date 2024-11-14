using System;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Calcool;

namespace calcul;

public class InstanceCalculator : Form
{
    private Panel _ContainerCalculs;
    private Panel _DisplayCalculs;

    private Panel _ContainerButtons;
    private Panel _DisplayButtons;

    public InstanceCalculator()
    {
        _ContainerCalculs = new Panel();
        _ContainerCalculs.Height = 265;
        _ContainerCalculs.Width = 500;
        _ContainerCalculs.Dock = DockStyle.Top;

        _DisplayCalculs = new Panel();
        _DisplayCalculs.Height = 227;
        _DisplayCalculs.Width = 450;
        _DisplayCalculs.BackColor = Color.FromArgb(55, 55, 65);
        _DisplayCalculs.Location = new Point(25, 25);

        _DisplayCalculs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right| AnchorStyles.Bottom;

        GraphicsPath pathDisplayCalculs = Program.GetPath(_DisplayCalculs.Width, _DisplayCalculs.Height, 10);
        _DisplayCalculs.Region = new Region(pathDisplayCalculs);

        _ContainerCalculs.Controls.Add(_DisplayCalculs);

        _ContainerButtons = new Panel();
        _ContainerButtons.Height = 500;
        _ContainerButtons.Width = 500;
        _ContainerButtons.Dock = DockStyle.Bottom;

        _DisplayButtons = new Panel();
        _DisplayButtons.Height = 450;
        _DisplayButtons.Width = 450;
        _DisplayButtons.BackColor = Color.Fuchsia;
        _DisplayButtons.Location = new Point(25, 25);

        _DisplayButtons.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

        _ContainerButtons.Controls.Add(_DisplayButtons);


        this.Controls.Add(_ContainerButtons);
        this.Controls.Add(_ContainerCalculs);

        this.BackColor = Color.FromArgb(25, 25, 35);
    }
}
