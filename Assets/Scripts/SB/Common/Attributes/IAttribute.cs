using UniRx;

namespace SB.Common.Attributes
{
    public interface IAttribute
    {
        float Value { get; }
        float MaxValue { get; }
        float Multiplier { get; }
        
        IReadOnlyReactiveProperty<float> AbsoluteValue { get; }
    }
}