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
    //создание начальной формы
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //рисование начальной формы с постоянными размерами
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.Size = new Size(330, 340);
        }
        //обработка нажатия на кнопку старта и проверка выбора уровня игры
        //при нажатии на кнопку создается новая форма, в которой будет основное действие игры
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Form2 f = new Form2(3);
                f.Show();
            }
            if (radioButton2.Checked)
            {
                Form2 f = new Form2(8);
                f.Show();
            }
            if (radioButton3.Checked)
            {
                Form2 f = new Form2(16);
                f.Show();
            }
  
            
        }
    }
}
