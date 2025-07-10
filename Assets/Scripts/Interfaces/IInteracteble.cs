using System;

public interface IInteracteble
{
    event Action OnInteract;
    event Action OnInteractProcess;
    event Action OnInteractEnd;
}