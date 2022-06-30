using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stars
{
	
	public partial class Form1 : Form
	{
		public class Star//Класс для хранения координат звезд
		{
			public float X { get; set; }
			public float Y { get; set; }
			public float Z { get; set; }
		}

		private Star[] stars = new Star[15000]; //Создаем массив типа Star
		private Random random = new Random();
		private Graphics graphics;
		private bool isRunning = true;

		public Form1()
		{
			InitializeComponent();
		}

		private void timer1_Tick(object sender, EventArgs e)//В этом методе будет отрабатывать движение звезд и отрисовка
		{
			graphics.Clear(Color.Black);//Очищаем панель каждый раз

			foreach (var star in stars) //Каждая звезда присваивается в переменную star
			{
				DrawStar(star);
				MoveStar(star);
			}

			pictureBox1.Refresh();//Метод, чтобы все чтo было отрисовано раньше отображалось в пикчербоксе
		}

		private void MoveStar(Star star)
		{
			star.Z -= 10;
			if (star.Z<1)
			{
				star.X = random.Next(-pictureBox1.Width, pictureBox1.Width);
				star.Y = random.Next(-pictureBox1.Height, pictureBox1.Height);
				star.Z = random.Next(1, pictureBox1.Height);
			}
		}

		private void DrawStar(Star star)//Метод для того, чтобы отрисовывать звезды
		{
			float starsize =  Map(star.Z, 0, pictureBox1.Width/2, 7,0);
			float x = Map(star.X / star.Z, 0, 1, 0, pictureBox1.Width) + pictureBox1.Width / 2;
			float y = Map(star.Y / star.Z, 0, 1, 0, pictureBox1.Height) + pictureBox1.Height / 2;
			graphics.FillEllipse(Brushes.Blue, x, y, starsize, starsize);
		}

		private float Map (float n, float start1, float stop1, float start2, float stop2)//Сами создали метод, который изменяешь шкалу значений
		{
			return ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//Bitmap - объект, который хранит картинки - можно задать размер данной картинки
			pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			graphics = Graphics.FromImage(pictureBox1.Image);//выделяем место в оператвн памяти

			for (int i=0; i<stars.Length; i++)//Создаем звезды
			{
				stars[i] = new Star()//Создали место в оперативке
				{
					X = random.Next(-pictureBox1.Width, pictureBox1.Width),//В скобках это границы для рандомных значений
					Y = random.Next(-pictureBox1.Height, pictureBox1.Height),
					Z = random.Next(1, pictureBox1.Height)
				};
			}

			timer1.Start();
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
			if (e.KeyCode == Keys.Space && isRunning)
			{
				isRunning = !isRunning;
				timer1.Stop();
			}
			else if (e.KeyCode == Keys.Space && !isRunning)
			{
				isRunning = !isRunning;
				timer1.Start();
			}
		}
	}
}
