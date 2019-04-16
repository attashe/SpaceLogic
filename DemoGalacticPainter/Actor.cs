using System;
using System.Collections.Generic;
namespace DemoGalacticPainter
{
    public class Actor
    {
        public double money = 0;
        public List<Ship> ship = new List<Ship>();
        public Galaxy galaxy;
        public int starInd;

        public Actor()
        {

        }
        public Actor(Galaxy galaxy, int starInd)
        {
            this.galaxy = galaxy;
            this.starInd = starInd;
        }
    }
}
