using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public Dictionary<DataEnum.attributes, ReactiveProperty<float>> properties = new Dictionary<DataEnum.attributes, ReactiveProperty<float>>();
}
