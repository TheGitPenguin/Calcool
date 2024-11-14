using System.Drawing.Drawing2D;
using calcul;
using MainPage;

namespace Calcool;

static class Program
{
    [STAThread]
    static void Main()
    {
        MainPageForm.Lunch();

        InstanceCalculator calculatrice = new InstanceCalculator();

        Thread.Sleep(100);

        MainPageForm.AddInstance("Calculatric", calculatrice); 
    }    

    public static GraphicsPath GetPath(int width, int height, int radius){
        GraphicsPath path = new GraphicsPath();
        path.AddArc(0, 0, radius, radius, 180, 90);
        path.AddArc(width - radius, 0, radius, radius, 270, 90);
        path.AddArc(width - radius, height - radius, radius, radius, 0, 90);
        path.AddArc(0, height - radius, radius, radius, 90, 90);
        path.CloseFigure();
        return path;
    }
}