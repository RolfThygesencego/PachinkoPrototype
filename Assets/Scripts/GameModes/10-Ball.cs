using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;



public class Ten_Ball : GameMode
{
    public Ten_Ball()
    {
        ballBounciness = 0.25f;
        ballGravityScale = 1.5f;
        ballWager = 5;
        standardBallsAmount = 10;
        gMode = GMode.TEN_BALL;

    }

    
}

