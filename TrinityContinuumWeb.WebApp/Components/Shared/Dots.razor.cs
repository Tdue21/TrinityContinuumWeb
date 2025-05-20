using Microsoft.AspNetCore.Components;

namespace TrinityContinuum.WebApp.Components.Shared;

public partial class DotsBase : ComponentBase
{
    [Parameter] public int Value { get; set; }

    [Parameter] public int MaxValue { get; set; } = 5;

    [Parameter] public bool ReadOnly { get; set; } = false;

    [Parameter] public EventCallback<int> ValueChanged { get; set; }

    protected void UpdateValueAsync(int newValue)
    {
        if(ReadOnly)
        {
            return;
        }

        if (newValue >= 0 && newValue <= MaxValue)
        {
            Value = newValue;
            ValueChanged.InvokeAsync(newValue).GetAwaiter().GetResult();
        }
    }
}
