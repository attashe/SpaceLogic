using System;
namespace DemoGalacticPainter
{
    public class Hyperlane
    {
        public Star s1;
        public Star s2;

        public Hyperlane()
        {

        }

        public Hyperlane(Star star1, Star star2)
        {
            this.s1 = star1;
            this.s2 = star2;
        }
    }
}
