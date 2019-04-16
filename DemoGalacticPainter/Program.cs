using System;
using System.Collections.Generic;
using System.Drawing;

namespace DemoGalacticPainter
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Galaxy galaxy = new Galaxy();
            //List<Star> stars = galaxy.GenerateStars(100, 50, 15, 5, 7);
            List<Star> stars = galaxy.GenerateHexStars(20, 10, 30);
            //List<Hyperlane> hyperlanes = galaxy.GenerateHyperlanes(stars, 0.1);
            drawStars(galaxy.stars, galaxy.hyperlanes);

            Random random = new Random();
            Player player1 = new Player(galaxy, random.Next(galaxy.stars.Count));
            Bot bot1 = new Bot(galaxy, random.Next(galaxy.stars.Count);
            // Game Cycle
            for (int i = 0; i < 25; i++) {

            }
            Console.WriteLine("Hello World!");
        }
        public static void drawStars(List<Star> stars, List<Hyperlane> hyperlanes)
        {
            Bitmap bitmap = new Bitmap(450, 650);
            Pen blackPen = new Pen(Color.Black, 3);
            Graphics g = Graphics.FromImage(bitmap);
            foreach (Star s in stars)
            {
                g.DrawArc(blackPen, (int)s.x - 2, (int)s.y - 2, 5, 5, 0, 360);
            }
            double EPSILON = 1e-4;
            foreach (Hyperlane h in hyperlanes) 
            {
                Console.WriteLine(h.s1.x + "  " + h.s1.y + "; " +
                                     h.s2.x + "  " + h.s2.y);
                if ((Math.Abs(h.s1.x - -10) < EPSILON) ||
                    (Math.Abs(h.s1.y - -10) < EPSILON) ||
                    (Math.Abs(h.s2.x - -10) < EPSILON) ||
                    (Math.Abs(h.s2.y - -10) < EPSILON)) {
                    Console.WriteLine(h.s1.x + " - " + h.s1.y + "; " +
                                     h.s2.x + " - " + h.s2.y);
                } else {
                    g.DrawLine(blackPen, (int)h.s1.x, (int)h.s1.y, (int)h.s2.x, (int)h.s2.y);
                }
            }

            bitmap.Save("test_img.png");
        }
    }
}
