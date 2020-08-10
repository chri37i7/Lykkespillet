using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfChance.Entities
{
    public class Player
    {
        /*
            Fields
        */

        protected string name;
        protected int points;


        /*
            Constructors
        */

        public Player()
        {

        }

        public Player(int points)
        {
            Points = points;
        }


        /*
            Properties 
        */
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if(name  != value)
                {
                    name = value;
                }
            }
        }

        public int Points
        {
            get
            {
                return points;
            }
            set
            {
                if(points != value)
                {
                    points = value;
                }
            }
        }
    }
}