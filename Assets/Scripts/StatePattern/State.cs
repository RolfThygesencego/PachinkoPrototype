using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public abstract class State
{
    private bool initializeFinished;

    public bool InitializeFinished { get => initializeFinished; set => initializeFinished = value; }

    public abstract void Initialize();

    public abstract void Execute();
    public abstract void FixedExecute();

    public abstract void End();
}

