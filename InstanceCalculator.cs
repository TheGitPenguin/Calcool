using System;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Calcool;
using System.Net.Http.Headers;

namespace calcul;

public class InstanceCalculator : Form
{
    private Panel _ContainerCalculs;
    private Panel _DisplayCalculs;

    private Panel _ContainerButtons;
    private Panel _DisplayButtons;

    private string _CurrentCalcul;

    private List<Button> _Buttons;

    static string[] _Operators = new string[] {"*", "/", "+", "-"};


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
        _DisplayCalculs.Paint += new PaintEventHandler(PrintCalcul);

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
        _DisplayButtons.BackColor = Color.Transparent;
        _DisplayButtons.Location = new Point(25, 25);

        _DisplayButtons.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

        _ContainerButtons.Controls.Add(_DisplayButtons);

        InstantiateCalcul();

        this.Controls.Add(_ContainerButtons);
        this.Controls.Add(_ContainerCalculs);

        this.BackColor = Color.FromArgb(25, 25, 35);
    }

    private void InstantiateCalcul(){

        _CurrentCalcul = "";
        _Buttons = new List<Button>();
        for (int i = 0; i < 10; i++){
            Button button = new ButtonCalculator(i.ToString(), 50, 50, 25 + (i % 3) * 75, 25 + (i / 3) * 75);
            button.Click += new EventHandler(AddElement);
            _Buttons.Add(button);
            _DisplayButtons.Controls.Add(button);
        }

        for (int i = 0; i < _Operators.Length; i++){
            Button button = new ButtonCalculator(_Operators[i], 50, 50, 250, 25 + i * 75);
            button.Click += new EventHandler(AddElement);
            _Buttons.Add(button);
            _DisplayButtons.Controls.Add(button);
        }

        Button buttonPi = new ButtonCalculator("π", 50, 50, 100, 250);
        buttonPi.Click += new EventHandler(AddElement);
        _Buttons.Add(buttonPi);
        _DisplayButtons.Controls.Add(buttonPi);

        Button buttonClear = new ButtonCalculator("C", 50, 50, 25, 325);
        buttonClear.Click += new EventHandler(ClearCalcul);
        _Buttons.Add(buttonClear);
        _DisplayButtons.Controls.Add(buttonClear);

        Button buttonRemove = new ButtonCalculator("←", 50, 50, 100, 325);
        buttonRemove.Click += new EventHandler(RemoveElement);
        _Buttons.Add(buttonRemove);
        _DisplayButtons.Controls.Add(buttonRemove);

        Button buttonEqual = new ButtonCalculator("=", 50, 50, 175, 325);
        buttonEqual.Click += new EventHandler(ExecuteCalcul);
        _Buttons.Add(buttonEqual);
        _DisplayButtons.Controls.Add(buttonEqual);
    }

    private void AddElement(object sender, EventArgs e){
        Button button = (Button)sender;
        _CurrentCalcul += button.DataContext.ToString();
        _DisplayCalculs.Refresh();
    }

    private void RemoveElement(object sender, EventArgs e){
        if (_CurrentCalcul.Length > 0){
            _CurrentCalcul = _CurrentCalcul.Substring(0, _CurrentCalcul.Length - 1);
            _DisplayCalculs.Refresh();
        }
    }

    private void ClearCalcul(object sender, EventArgs e){
        _CurrentCalcul = "";
        _DisplayCalculs.Refresh();
    }

    private void PrintCalcul(object sender, PaintEventArgs e){
        Graphics graphics = e.Graphics;
        graphics.DrawString(_CurrentCalcul, new Font("Arial", 12), new SolidBrush(Color.White), new PointF(10, 10));
    }

    private void ExecuteCalcul(object sender, EventArgs e){
        int curseur = 0;
        string operatorRead = null;

        Calcul calcul = null;

        try{
            // while (curseur < _CurrentCalcul.Length){
            //     if (_Operators.Contains(_CurrentCalcul[curseur].ToString())){
            //         operatorRead = _CurrentCalcul[curseur].ToString();
            //         curseur++;
            //     }else{
            //         string number = "";
            //         do {
            //             number += _CurrentCalcul[curseur];
            //             curseur++;
            //         } while (curseur < _CurrentCalcul.Length && !_Operators.Contains(_CurrentCalcul[curseur].ToString()));

            //         Calcul newCalcul = new Calcul("number", null, null, double.Parse(number));

            //         if (operatorRead != null){
            //             if (calcul == null){
            //                 throw new Exception("Error");
            //             }
            //             calcul = new Calcul(operatorRead, calcul,newCalcul);
            //         }else{
            //             calcul = newCalcul;
            //         }
            //     }
            // }

            for (int i = 0; i < _Operators.Length; i++){
                int numberOfOperator = 0;
                for (int j = 0; j < _CurrentCalcul.Length; j++){
                    if (_CurrentCalcul[j].ToString() == _Operators[i]){
                        numberOfOperator++;
                    }
                }

                for (int j = 0; j < numberOfOperator; j++){
                    ChangeCalculWithOperator(_Operators[i]);
                }
            }

            // if (calcul == null){
            //     throw new Exception("Error");
            // }

            // _CurrentCalcul = calcul.GetResult().ToString();
            _DisplayCalculs.Refresh();

        }catch(Exception ex){

        }
    }

    private void ChangeCalculWithOperator(string operatorRead){
        int indexOperator = _CurrentCalcul.IndexOf(operatorRead);
        int firstIndex = 0;
        int lastIndex = 0;

        for (firstIndex = indexOperator - 1; firstIndex > 0; firstIndex--){
            if (_Operators.Contains(_CurrentCalcul[firstIndex -1].ToString())){
                break;
            }
        }

        for (lastIndex = indexOperator + 1; lastIndex < _CurrentCalcul.Length - 1; lastIndex++){
            if (_Operators.Contains(_CurrentCalcul[lastIndex+1].ToString())){
                break;
            }
        }

        Calcul firstNumber = new Calcul("number", null, null, double.Parse(_CurrentCalcul.Substring(firstIndex, indexOperator - firstIndex)));
        Calcul secondNumber = new Calcul("number", null, null, double.Parse(_CurrentCalcul.Substring(indexOperator + 1, lastIndex - indexOperator)));

        Calcul calcul = new Calcul(operatorRead, firstNumber, secondNumber);

        _CurrentCalcul = _CurrentCalcul.Substring(0, firstIndex) + calcul.GetResult().ToString() + _CurrentCalcul.Substring(firstIndex + 1, _CurrentCalcul.Length - lastIndex - 1);

    }


    
}
