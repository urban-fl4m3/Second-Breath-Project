using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public readonly Dictionary<Attributes, ReactiveProperty<float>> Properties 
        = new Dictionary<Attributes, ReactiveProperty<float>>();
}
