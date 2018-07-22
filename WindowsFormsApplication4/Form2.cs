using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    //создание основной формы
    public partial class Form2 : Form
    {
        //конструктор, который вызывается в Form1 для передачи значения скорости 
        public Form2(int a)
        {
            InitializeComponent();
            v = a;
            
        }
        bool goup;//движение вверх
        bool godown;//движение вниз
        bool goleft;//движение влево
        bool goright;//движение вправо

        int v;//скорость Пакмана

        int score = 0;//счет
        //рисование основной формы с постоянными размерами
        private void Form2_Paint(object sender, PaintEventArgs e)
        { 
            this.Size = new Size(470, 540);
        }
        //обработка нажатия игрока на управляющие элементы(кнопки клавиатуры: вверх, вниз, влево, вправо)
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    goup = true;
                    break;

                case Keys.Down:
                    godown = true;
                 break;
                    
                case Keys.Left:
                    goleft = true;
                    break;

                case Keys.Right:
                    goright = true;
                break;
            }
        }
        //обработка случая, когда игрок отпустил кнопку
        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    goup = false;
                    break;
                case Keys.Down:
                    godown = false;
                    break;
                case Keys.Left:
                    goleft = false;
                    break;
                case Keys.Right:
                    goright = false;
                    break;
            }
           
        }
      
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Score " + score;//показываем счет в форме
            //флаги для обработки движения в "корридоре"
            bool passageLeft = false;
            bool passageRight = false;
            //описание движения Пакмана
            //при нажатии на клавишу меняется картинка(Пакман поварачивается)
            //координата изменяется на значение скорости
            if (goleft == true && goup == false && godown == false)
            {
                pacman.Image = WindowsFormsApplication4.Properties.Resources.Retro_Pacman44;
                pacman.Left -= v;
            }
            if (goright && !goup && !godown)
            {
                pacman.Image = WindowsFormsApplication4.Properties.Resources.Retro_Pacman11;
                pacman.Left += v;
            }
            if (goup && !goleft && !goright)
            {
                pacman.Image = WindowsFormsApplication4.Properties.Resources.Retro_Pacman22;
                pacman.Top -= v;
            }
            if (godown && !goleft && !goright)
            {
                pacman.Image = WindowsFormsApplication4.Properties.Resources.Retro_Pacman12;
                pacman.Top += v;
            } 
            //прохождение по всем визуальным компонентам в цикле, в зависимости от их тега производятся изменения
            foreach (Control x in this.Controls)
            {
                //если Пакман натолкнулся на преграду
                if (x is PictureBox && x.Tag == "walls")
                    //если преграды пересекаются с областью Пакмана
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        //возвращение Пакмана в начальное положение
                        pacman.Left = 8;
                        pacman.Top = 40;
                        pacman.Image = WindowsFormsApplication4.Properties.Resources.Retro_Pacman11;
                        timer1.Stop();//остановка таймера
                        //создание и обработка диалога с сообщением об ошибке
                        DialogOver dlg = new DialogOver();
                        dlg.label1.Text = "GAME OVER!";
                        dlg.label2.Text = "Score: " + score;
                        if (dlg.ShowDialog() == DialogResult.OK)
                            this.Close(); 
                        else
                            this.Dispose();
                    }
                //если Пакман наткнулся на еду
                if (x is PictureBox && x.Tag == "food")
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        this.Controls.Remove(x);//удаление элемента
                        score++;
                    }
                //если Пакман наткнулся на внешние границы
                if (x is PictureBox && x.Tag == "borderLeft")
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                        pacman.Left = 8;

                if (x is PictureBox && x.Tag == "borderRight")
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                        pacman.Left = ClientSize.Width - 48;

                if (x is PictureBox && x.Tag == "borderUp")
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                        pacman.Top = 40;

                if (x is PictureBox && x.Tag == "borderDown")
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                        pacman.Top = ClientSize.Height - 48;
                //обработка прохода Пакманом "коридора"
                if (x is PictureBox && x.Tag == "passageLeft")
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        passageLeft = true;
                        if(passageRight != true)
                        pacman.Left = ClientSize.Width - 40;
                    }
               if (x is PictureBox && x.Tag == "passageRight")
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        passageRight = true;
                        if (passageLeft != true)
                            pacman.Left = 3;
                    }
            }
            //обработка случая, когда Пакман съел всю еду
            if (score == 15)
            {
                //возвращение Пакмана в начальное положение
                pacman.Left = 8;
                pacman.Top = 40;
                pacman.Image = WindowsFormsApplication4.Properties.Resources.Retro_Pacman11;
                timer1.Stop();//остановка таймера
                score = 0;
                label1.Text = "Score: 15";
                //создание и обработка диалога с сообщением о выигрыше
                DialogWin dial = new DialogWin();
                dial.label1.Text = "You win!";
                if (dial.ShowDialog() == DialogResult.OK)
                    this.Close();
                else
                    this.Dispose();
            }
        }
    }
}
