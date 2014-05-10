using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.Properties;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Timer timer;
        public int tiker = -1;
        int tukax, tukay, tajmerx, tajmery;
        int kojdobizabrzuvanje = 5; //5 e default nikoj
        int usporuvanje = 4;
        int verojatnost = 10;
        public Snake[] snake;//
        bool disable = false;
        bool[] kojudri;//
        static int TIMER_INTERVAL = 45;
        public int WORLD_WIDTH = 17;
        Image foodImage, saat, stone, banan, straw, cake, crtaj;
        bool[][] foodWorld;
        // bool[][] nacrtano;
        public int[] brojizedeni;
        public int[][] pole;
        int posleden = -1;
        int ovosja = 2;
        public bool userstarted = false;
        int[] dvizenjavotik;
        bool postoitajmer = false;
        int players = 4;
        int bots = 4;
        int nekojbrojac = 0;
        Random random;

        int numberofstones = 8;
        bool[] vestacki;
        int[] poeni;
        bool segaezapocnata = false;
        Form2 forma;
        public Form1()
        {
            InitializeComponent();

            Application.EnableVisualStyles();

            DoubleBuffered = true;


            timer = new Timer();

            timer.Interval = TIMER_INTERVAL;
            timer.Tick += new EventHandler(timer_Tick);
            forma = new Form2();

            //  forma.Close(); ;
            timer.Start();


            newGame();



        }

        void updatepoeni()
        {
            if (!kojudri[0])
                label1.Text = "Играч 1=  " + (brojizedeni[0] * 50 + nekojbrojac / 10).ToString();



            if (players > 1) if (!kojudri[1])
                    label2.Text = "Играч 2=  " + (brojizedeni[1] * 50 + nekojbrojac / 10).ToString();


            if (players > 2) if (!kojudri[2])
                    label3.Text = "Играч 3=  " + (brojizedeni[2] * 50 + nekojbrojac / 10).ToString();
            if (players > 3) if (!kojudri[3])
                    label4.Text = "Играч 4=  " + (brojizedeni[3] * 50 + nekojbrojac / 10).ToString();
            if (progressBar1.Value > 500 / TIMER_INTERVAL) progressBar1.Value -= 500 / TIMER_INTERVAL;
            else
            {
                if (userstarted)
                {
                    TIMER_INTERVAL = 200000;
                    timer.Interval = 200000;
                    int naj = 0, poen = (brojizedeni[0] * 50 + nekojbrojac / 10);
                    if ((brojizedeni[1] * 50 + nekojbrojac / 10) > poen)
                        naj = 1;
                    if ((brojizedeni[2] * 50 + nekojbrojac / 10) > poen)
                        naj = 2;
                    if ((brojizedeni[3] * 50 + nekojbrojac / 10) > poen)
                        naj = 3;
                    //  MessageBox.Show("Крај\n" + label1.Text + "\n" + label2.Text + "\n" + label3.Text + "\n" + label4.Text + "\n");
                    MessageBox.Show("Крај\nПобедник: Играч " + (naj + 1));
                    forma = new Form2();
                    forma.izbrano = false;
                    forma.ShowDialog();//nekoj bool treba da sredi ova
                    userstarted = false;

                    while (forma.izbrano == false) ;

                    segaezapocnata = true;
                    userstarted = true;
                    TIMER_INTERVAL = forma.newbrzina;
                    players = forma.newplayers;
                    bots = forma.newbots;
                    WORLD_WIDTH = forma.newdimenzii;
                    verojatnost = forma.newverojatnost;
                    ovosja = forma.newovosja;
                    numberofstones = forma.newstones;
                    forma.izbrano = true;


                    newGame();
                    return;

                }
                else

                { segaezapocnata = true; newGame(); }
            }
        }

        public void newGame()
        {
            timer.Stop();
            label1.Text = "0"; label2.Text = "Играч 2=  0";
            label3.Text = "Играч 3=  0"; label4.Text = "Играч 4=  0";

            snake = new Snake[4];
            poeni = new int[4];

            kojudri = new bool[4];
            brojizedeni = new int[4];
            dvizenjavotik = new int[4];


            foodWorld = new bool[WORLD_WIDTH][];

            pole = new int[WORLD_WIDTH][];

            for (int i = 0; i < WORLD_WIDTH; i++)
            {
                foodWorld[i] = new bool[WORLD_WIDTH];

                pole[i] = new int[WORLD_WIDTH];
            }
            //  foodImage = Resources.slika;
            saat = Resources.saat;
            stone = Resources.stone;

            crtaj = Resources.cake2;



            vestacki = new bool[4];
            for (int i = players - bots; i < players; i++)
                vestacki[i] = true;
            tiker = -1;
            kojdobizabrzuvanje = 5;
            nekojbrojac = 0;

            for (int i = 0; i < players; i++)
            {
                snake[i] = new Snake();
                brojizedeni[i] = 0;
                kojudri[i] = false;
                dvizenjavotik[i] = 0;
                poeni[i] = 0;

            }
            for (int i = 0; i < WORLD_WIDTH; i++)
                for (int j = 0; j < WORLD_WIDTH; j++)
                {
                    foodWorld[i][j] = false;
                    pole[i][j] = 0;
                }
            //  for(int i=0;i<WORLD_WIDTH;i++)
            //  for(
            snake[0].br = new SolidBrush(Color.Green);

            snake[0].X = (WORLD_WIDTH / 4);
            snake[0].Y = (WORLD_WIDTH / 4);
            label5.Location = new Point((int)snake[0].Radius * 2 * (WORLD_WIDTH + 1), 10);

            label1.Location = new Point((int)snake[0].Radius * 2 * (WORLD_WIDTH + 1), 10 + 30);
            label2.Location = new Point((int)snake[0].Radius * 2 * (WORLD_WIDTH + 1), 40 + 30);
            label3.Location = new Point((int)snake[0].Radius * 2 * (WORLD_WIDTH + 1), 70 + 30);
            label4.Location = new Point((int)snake[0].Radius * 2 * (WORLD_WIDTH + 1), 100 + 30);
            progressBar1.Size = new Size((int)snake[0].Radius * 2 * (WORLD_WIDTH + 1), 30);
            progressBar1.Location = new Point(10, (int)snake[0].Radius * 2 * (WORLD_WIDTH + 1));
            progressBar1.Value = 3000 - 1;
            if (players > 1)
            {

                snake[1].X = (WORLD_WIDTH / 4) * 3; snake[1].Y = (WORLD_WIDTH / 4);
                snake[1].br = new SolidBrush(Color.Blue);

            }
            if (players > 2)
            {
                snake[2].X = (WORLD_WIDTH / 4); snake[2].Y = (WORLD_WIDTH / 4) * 3;
                snake[2].br = new SolidBrush(Color.BlueViolet);

            }
            if (players > 3)
            {
                snake[3].X = (WORLD_WIDTH / 4) * 3; snake[3].Y = (WORLD_WIDTH / 4) * 3;
                snake[3].br = new SolidBrush(Color.DarkRed);

            }


            this.Width = (int)snake[0].Radius * 2 * (WORLD_WIDTH + 1) + 155;
            this.Height = this.Width - 70;
            random = new Random();

            for (int i = 0; i < ovosja; i++)
            {
                bool nekoj = true;
                while (nekoj)
                {
                    tukay = random.Next(WORLD_WIDTH - 1);
                    tukax = random.Next(WORLD_WIDTH - 1);
                    if (pole[tukax][tukay] > 0) continue;
                    nekoj = false;
                    foodWorld[tukax][tukay] = true;
                    //    nacrtano[tukax][tukay] = false;
                }


            }

            for (int i = 0; i < numberofstones; i++)
            {

                int f = random.Next(WORLD_WIDTH);
                int g = random.Next(WORLD_WIDTH);
                if (foodWorld[f][g]) { i--; continue; }
                bool ok = true;
                for (int j = 0; j < players; j++)
                    if ((f == snake[j].Y && g == snake[j].X))
                        ok = false;
                if (!ok) continue;
                pole[f][g] = 1001;
            }
            timer = new Timer();

            timer.Interval = TIMER_INTERVAL;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();


        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (!forma.IsDisposed)
                if (!forma.izbrano)
                    forma.Show();//nekoj bool treba da sredi ova
            if (forma.izbrano == true && userstarted == false)
            {
                segaezapocnata = true;
                userstarted = true;
                TIMER_INTERVAL = forma.newbrzina;
                players = forma.newplayers;
                bots = forma.newbots;
                WORLD_WIDTH = forma.newdimenzii;
                verojatnost = forma.newverojatnost;
                ovosja = forma.newovosja;
                numberofstones = forma.newstones;

                newGame();
                return;

            }
            int brojzivi = 0;
            for (int i = 0; i < players; i++)
                if (!kojudri[i] && brojizedeni[i] < 49) brojzivi++;
            if (brojzivi < 1)
            {
                if (userstarted)
                {
                    TIMER_INTERVAL = 200000;
                    timer.Interval = 200000;
                    int naj = 0, poen = (brojizedeni[0] * 50 + nekojbrojac / 10);
                    if ((brojizedeni[1] * 50 + nekojbrojac / 10) > poen)
                        naj = 1;
                    if ((brojizedeni[2] * 50 + nekojbrojac / 10) > poen)
                        naj = 2;
                    if ((brojizedeni[3] * 50 + nekojbrojac / 10) > poen)
                        naj = 3;
                    //  MessageBox.Show("Крај\n" + label1.Text + "\n" + label2.Text + "\n" + label3.Text + "\n" + label4.Text + "\n");
                    MessageBox.Show("Крај\nПобедник: Играч " + (naj + 1)); forma = new Form2();
                    forma.izbrano = false;
                    forma.ShowDialog();//nekoj bool treba da sredi ova
                    userstarted = false;

                    while (forma.izbrano == false) ;

                    segaezapocnata = true;
                    userstarted = true;
                    TIMER_INTERVAL = forma.newbrzina;
                    players = forma.newplayers;
                    bots = forma.newbots;
                    WORLD_WIDTH = forma.newdimenzii;
                    verojatnost = forma.newverojatnost;
                    ovosja = forma.newovosja;
                    numberofstones = forma.newstones;
                    forma.izbrano = true;
                    newGame();
                    return;



                }
                else

                { segaezapocnata = true; newGame(); return; }

            }
            updatepoeni();
            /*  if (segaezapocnata)
              {
                  segaezapocnata = false;
                  return;
              }*/

            nekojbrojac++;
            if (!postoitajmer && tiker == -1)
            {
                random = new Random();
                tajmerx = random.Next(100);
                if (tajmerx < verojatnost)
                {
                    //   postoitajmer = false;
                    while (!postoitajmer)
                    {

                        tajmerx = random.Next(WORLD_WIDTH);
                        tajmery = random.Next(WORLD_WIDTH);
                        if (pole[tajmerx][tajmery] > 0)
                            continue;
                        postoitajmer = true;
                    }
                    //   postoitajmer = true;
                }
            }
            for (int i = 0; i < players; i++)
                dvizenjavotik[i] = 0;
            if (tiker > 0) tiker++;
            if (tiker == 85) tiker = -1;
            if (tiker == -1) kojdobizabrzuvanje = 5;
            for (int i = 0; i < players; i++)
                if (!kojudri[i])
                {
                    snake[i].slednox = snake[i].X;
                    snake[i].slednoy = snake[i].Y;
                    if (snake[i].nasoka == 0) snake[i].slednox++;
                    if (snake[i].nasoka == 1) snake[i].slednoy--;
                    if (snake[i].nasoka == 2) snake[i].slednox--;
                    if (snake[i].nasoka == 3) snake[i].slednoy++;
                    if (snake[i].slednox == WORLD_WIDTH) snake[i].slednox = 0;
                    if (snake[i].slednoy == WORLD_WIDTH) snake[i].slednoy = 0;
                    if (snake[i].slednox == -1) snake[i].slednox = WORLD_WIDTH - 1;
                    if (snake[i].slednoy == -1) snake[i].slednoy = WORLD_WIDTH - 1;
                }




            for (int i = 0; i < players; i++)
                if (!kojudri[i])
                {
                    int py = snake[i].Y;
                    int px = snake[i].X;
                    if (pole[py][px] > (i * 100) && brojizedeni[i] - 1 > pole[py][px] - (i * 100))
                        if (pole[py][px] % 100 != brojizedeni[i])
                        { //ova vrati za da ne se udira sam so sebe!!!!

                            //if (!(tiker > 0 && tiker % usporuvanje != 1))

                            kojudri[i] = true;
                        }
                    if (pole[snake[i].Y][snake[i].X] > 0 && pole[snake[i].Y][snake[i].X] < (i * 100) || pole[snake[i].Y][snake[i].X] >= (i + 1) * 100)
                    {

                        kojudri[i] = true;
                        //    disable = true;
                        //   kojudri[pole[snake[i].Y][snake[i].X] / 100] = true;
                    }
                    pole[py][px] = brojizedeni[i] + (i * 100);
                    if (kojdobizabrzuvanje != 5)
                        if (kojdobizabrzuvanje != i)
                            pole[py][px] = brojizedeni[i] * usporuvanje + (i * 100);



                }
            for (int i = 0; i < players; i++)
            {
                if (vestacki[i] && !kojudri[i])
                {
                    int z = odivamu(snake[i].Y, snake[i].X, snake[i].nasoka, i);
                    snake[i].ChangeDirection(z);
                }

                if (kojdobizabrzuvanje == 5 || kojdobizabrzuvanje == i || tiker % usporuvanje == 0)
                    snake[i].Move(WORLD_WIDTH, WORLD_WIDTH);
            }

            for (int i = 0; i < players - 1; i++)
                for (int j = i + 1; j < players; j++)
                    if (kojudri[i] == false && kojudri[j] == false)
                    {
                        //ova treba da se smeni
                        if (snake[i].X == snake[j].X && snake[i].Y == snake[j].Y)
                        {

                            kojudri[i] = true;
                            kojudri[j] = true;

                        }



                    }
            if (postoitajmer)
                for (int i = 0; i < players; i++)
                    if (kojudri[i] == false)
                    {
                        if (tajmerx == snake[i].Y)
                            if (tajmery == snake[i].X)
                            {
                                tiker = 1;
                                kojdobizabrzuvanje = i;
                                postoitajmer = false;

                            }
                    }

            for (int i = 0; i < players; i++)
                if (!kojudri[i])
                    if (pole[snake[i].Y][snake[i].X] > 1000)
                        kojudri[i] = true;

            for (int i = 0; i < players; i++)
                if (kojudri[i] == false)
                    if (foodWorld[snake[i].Y][snake[i].X] == true)
                    {

                        brojizedeni[i]++;
                        foodWorld[snake[i].Y][snake[i].X] = false;
                        //     nacrtano[snake[i].Y][snake[i].X] = false;

                        //dali da go vklucam tikerot tuka?

                        pole[snake[i].Y][snake[i].X] = brojizedeni[i] + (i * 100);
                        bool nekoj = true;
                        while (nekoj)
                        {
                            tukay = random.Next(WORLD_WIDTH - 1);
                            tukax = random.Next(WORLD_WIDTH - 1);
                            if (pole[tukax][tukay] > 0) continue;
                            nekoj = false;
                            foodWorld[tukax][tukay] = true;
                            //nacrtano[tukax][tukay] = false;
                        }


                        if (kojdobizabrzuvanje != 5)
                            if (kojdobizabrzuvanje != i)
                                pole[snake[i].Y][snake[i].X] = brojizedeni[i] * usporuvanje + (i * 100);
                    }

            Invalidate();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (disable) return;

            int koj = -1;
            int b = -1;
            if (e.KeyCode == Keys.W) { koj = 1; b = 1; }
            else
                if (e.KeyCode == Keys.A) { koj = 1; b = 2; }
                else
                    if (e.KeyCode == Keys.S) { koj = 1; b = 3; }
                    else
                        if (e.KeyCode == Keys.D) { koj = 1; b = 0; }
                        else
                            if (e.KeyCode == Keys.Up) { koj = 0; b = 1; }
                            else
                                if (e.KeyCode == Keys.Left) { koj = 0; b = 2; }
                                else
                                    if (e.KeyCode == Keys.Down) { koj = 0; b = 3; }
                                    else
                                        if (e.KeyCode == Keys.Right) { koj = 0; b = 0; }
                                        else
                                        {
                                            e.Handled = false;
                                            return;
                                        }
            if (koj == -1) return;
            if (kojudri[koj] == true) return;
            dvizenjavotik[koj]++;
            if (dvizenjavotik[koj] > 1) return;

            if (snake[koj].nasoka != b && //dali da bide prviov uslov? porano go nemav
                (snake[koj].nasoka + 2) % 4 != b)
            {
                for (int i = 0; i < foodWorld.Length; i++)
                    for (int j = 0; j < foodWorld[i].Length; j++)
                        if (pole[i][j] > (koj * 100) + 1 && pole[i][j] < (koj + 1) * 100)
                            pole[i][j]++;

                snake[koj].ChangeDirection(b);
                Invalidate();
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            // if (posleden != nekojbrojac)   posleden = nekojbrojac;            else return;


            if (disable) timer.Interval = 20000000;
            if (postoitajmer)
            {
                g.DrawImageUnscaled(saat, (int)(tajmery * snake[0].Radius * 2 + (snake[0].Radius * 2 - saat.Height) / 2),
                            (int)(tajmerx * snake[0].Radius * 2 + (snake[0].Radius * 2 - saat.Width) / 2));
            }
            for (int i = 0; i < foodWorld.Length; i++)
            {
                for (int j = 0; j < foodWorld[i].Length; j++)
                {
                    if (foodWorld[i][j])
                    {

                        g.DrawImageUnscaled(crtaj,
                            (int)(j * snake[0].Radius * 2 + (snake[0].Radius * 2 - crtaj.Height) / 2),
                            (int)(i * snake[0].Radius * 2 + (snake[0].Radius * 2 - crtaj.Width) / 2));


                    }

                    if (pole[i][j] % 100 == 0) pole[i][j] = 0;
                    else
                        if (pole[i][j] == 1001)
                        {
                            g.DrawImageUnscaled(stone,
                          (int)(j * snake[0].Radius * 2 + (snake[0].Radius * 2 - stone.Height) / 2),
                          (int)(i * snake[0].Radius * 2 + (snake[0].Radius * 2 - stone.Width) / 2));
                        }
                        else
                            if (pole[i][j] > 0)
                            {

                                if (posleden != nekojbrojac) pole[i][j]--;
                                if (posleden != nekojbrojac) if (pole[i][j] % 100 == 99) pole[i][j] = 0;
                                if (kojudri[pole[i][j] / 100] == false)
                                    if (!(i == snake[pole[i][j] / 100].Y &&
                                        j == snake[pole[i][j] / 100].X))

                                        snake[(1 + pole[i][j]) / 100].Draw2(g, j, i);
                            }


                }
            }
            for (int i = 0; i < players; i++)
                if (!kojudri[i])
                    snake[i].Draw(g);

            posleden = nekojbrojac;

        }
        int odivamu(int x, int y, int kojaenasokata, int zmija)
        {

            int[] dx = { 0, -1, 0, 1 };
            int[] dy = { 1, 0, -1, 0 };
            bool[][] a = new bool[WORLD_WIDTH][];
            for (int i = 0; i < WORLD_WIDTH; i++)
            {
                a[i] = new bool[WORLD_WIDTH];

            }
            a[x][y] = true;
            for (int i = 0; i < zmija; i++)
                if (!kojudri[i])
                    a[snake[i].slednoy]
                        [snake[i].slednox] = true;
            int h = 5;
            if (nekojbrojac > 15)
                h = 6;
            Queue<int> q = new Queue<int>();
            for (int i = 0; i < 4; i++)
                if (i != (kojaenasokata + 2) % 4)
                    if (pole[(x + dx[i] + WORLD_WIDTH) % WORLD_WIDTH][(y + dy[i] + WORLD_WIDTH) % WORLD_WIDTH] == 0)
                        if (a[(x + dx[i] + WORLD_WIDTH) % WORLD_WIDTH][(y + dy[i] + WORLD_WIDTH) % WORLD_WIDTH] == false)
                        {
                            q.Enqueue((x + dx[i] + WORLD_WIDTH) % WORLD_WIDTH);
                            q.Enqueue((y + dy[i] + WORLD_WIDTH) % WORLD_WIDTH);
                            q.Enqueue(i);
                            a[(x + dx[i] + WORLD_WIDTH) % WORLD_WIDTH][(y + dy[i] + WORLD_WIDTH) % WORLD_WIDTH] = true;
                        }

            while (q.Count > 0)
            {
                int mx = q.Dequeue();
                int my = q.Dequeue();
                int pravec = q.Dequeue();

                if (postoitajmer && mx == tajmerx && my == tajmery)
                    return pravec;

                if (!postoitajmer && foodWorld[mx][my])
                    return pravec;
                for (int i = 0; i < 4; i++)
                    if (pole[(mx + dx[i] + WORLD_WIDTH) % WORLD_WIDTH][(my + dy[i] + WORLD_WIDTH) % WORLD_WIDTH] == 0)
                        if (a[(mx + dx[i] + WORLD_WIDTH) % WORLD_WIDTH][(my + dy[i] + WORLD_WIDTH) % WORLD_WIDTH] == false)
                        {
                            q.Enqueue((mx + dx[i] + WORLD_WIDTH) % WORLD_WIDTH);
                            q.Enqueue((my + dy[i] + WORLD_WIDTH) % WORLD_WIDTH);
                            q.Enqueue(pravec);
                            a[(mx + dx[i] + WORLD_WIDTH) % WORLD_WIDTH][(my + dy[i] + WORLD_WIDTH) % WORLD_WIDTH] = true;
                        }



            }
            for (int i = 0; i < WORLD_WIDTH; i++)
                for (int j = 0; j < WORLD_WIDTH; j++)
                    a[i][j] = false;
            a[x][y] = true;
            for (int i = 0; i < zmija; i++)
                if (!kojudri[i])
                    a[snake[i].slednoy]
                        [snake[i].slednox] = true;
            for (int i = 0; i < zmija; i++)
                if (!kojudri[i])
                    a[snake[i].Y]
                        [snake[i].X] = true;

            for (int i = 0; i < 4; i++)
                if (i != (kojaenasokata + 2) % 4)
                    if (pole[(x + dx[i] + WORLD_WIDTH) % WORLD_WIDTH][(y + dy[i] + WORLD_WIDTH) % WORLD_WIDTH] == 0)
                        if (a[(x + dx[i] + WORLD_WIDTH) % WORLD_WIDTH][(y + dy[i] + WORLD_WIDTH) % WORLD_WIDTH] == false)

                            return i;
            return kojaenasokata;
        }

    }
    public class Snake
    {
        public int X { get; set; }
        public int Y { get; set; }
        public float Radius { get; set; }
        public int nasoka { get; set; }
        public bool otvorena { get; set; }
        public SolidBrush br;
        public int slednox { get; set; }
        public int slednoy { get; set; }
        public Snake()
        {
            X = 0;
            Y = 0;
            Radius = 20;
            nasoka = 1;
            otvorena = true;
            //0 desno, 1 gore, 2 levo, 3 dole
        }
        public void ChangeDirection(int direction)
        {
            nasoka = direction;
        }
        public void Move(int siroko, int visoko)
        {
            otvorena = !otvorena;
            if (nasoka == 0) X++;
            if (nasoka == 1) Y--;
            if (nasoka == 2) X--;
            if (nasoka == 3) Y++;
            if (X == siroko) X = 0;
            if (Y == visoko) Y = 0;
            if (X == -1) X = siroko - 1;
            if (Y == -1) Y = visoko - 1;
        }
        public void Draw(Graphics g)
        {
            if (!otvorena)
                g.FillEllipse(br, X * Radius * 2, Y * Radius * 2, Radius * 2, Radius * 2);
            else
            {

                if (nasoka == 0) g.FillPie(br, X * Radius * 2, Y * Radius * 2, Radius * 2, Radius * 2, 35, (float)270);
                else if (nasoka == 1) g.FillPie(br, X * Radius * 2, Y * Radius * 2, Radius * 2, Radius * 2, -35, (float)270);
                else
                    if (nasoka == 3) g.FillPie(br, X * Radius * 2, Y * Radius * 2, Radius * 2, Radius * 2, 180 - 35, (float)270);
                    else
                        g.FillPie(br, X * Radius * 2, Y * Radius * 2, Radius * 2, Radius * 2, 180 + 35, (float)270);
            }
        }
        public void Draw2(Graphics g, int px, int py)
        {
            g.FillEllipse(br, px * Radius * 2, py * Radius * 2, Radius * 2, Radius * 2);
        }

    }
}
