﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ReadyForSpin : State
{
    public override void End()
    {
    }

    public override void Execute()
    {
    
    }

    public override void Initialize()
    {
        GameManager.Instance.Spinning.ballWager = 10;
        GameManager.Instance.Ballsdropping.ballsToBeAdded = 0;
    }
    public void DropBalls()
    {

    }
    
}

