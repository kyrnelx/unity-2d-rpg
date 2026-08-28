using System;

public abstract class UIController : IDisposable
{
    public abstract void Initialise();
    public abstract void Dispose();
}