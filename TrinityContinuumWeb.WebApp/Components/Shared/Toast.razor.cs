using Microsoft.AspNetCore.Components;
using TrinityContinuum.WebApp.Services;

namespace TrinityContinuum.WebApp.Components.Shared;


// https://dotnetcopilot.com/creating-dynamic-toast-notifications-in-blazor-a-step-by-step-guide/
// https://github.com/rijwanansari/Eshal.Blazor 

public partial class ToastBase : ComponentBase
{
    [Inject]
    public required ToastService ToastService { get; set; } = null!;

    protected bool ShowMessage { get; set; } = false;
    protected string MessageContent { get; set; } = string.Empty;
    protected string MessageType { get; set; } = "success"; // default to success
    public int DismissAfter { get; set; } = 3;

    protected string MessageTypeClass => MessageType switch
    {
        "success" => "toast-message-success",
        "failure" => "toast-message-failure",
        "alert" => "toast-message-alert",
        "warning" => "toast-message-warning",
        _ => "toast-message-default"
    };

    protected override async Task OnParametersSetAsync()
    {
        if (ShowMessage && DismissAfter > 0)
        {
            await Task.Delay(DismissAfter * 1000);
            HideMessage();
        }
    }

    protected override void OnInitialized()
    {
        ToastService.OnShow += ShowToast;
        ToastService.OnHide += HideMessage;
    }

    private async void ShowToast(string message, string type, int dismissAfter)
    {
        MessageContent = message;
        MessageType = type;
        ShowMessage = true;

        await InvokeAsync(StateHasChanged); // Ensure the UI updates

        if (dismissAfter > 0)
        {
            await Task.Delay(dismissAfter * 1000);
            HideMessage();
        }
    }

    protected void HideMessage()
    {
        ShowMessage = false;
        InvokeAsync(StateHasChanged); // Ensure the UI updates
    }

    public void Dispose()
    {
        ToastService.OnShow -= ShowToast;
        ToastService.OnHide -= HideMessage;
    }

}
