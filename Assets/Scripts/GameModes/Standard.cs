using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Standard : GameMode
{
    public Standard()
    {
        ballBounciness = 0.5f;
        ballGravityScale = 1.5f;
        ballWager = 5;
        standardBallsAmount = 10;
        gMode = GMode.STANDARD;
    }
}

