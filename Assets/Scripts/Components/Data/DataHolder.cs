using System.Collections.Generic;
using Core;
using UniRx;

public class DataHolder : GameComponent
{
    public readonly Dictionary<Attributes, ReactiveProperty<float>> Properties 
        = new Dictionary<Attributes, ReactiveProperty<float>>();
}
