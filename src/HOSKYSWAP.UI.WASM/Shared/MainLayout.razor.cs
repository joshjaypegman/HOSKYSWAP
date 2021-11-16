using HOSKYSWAP.UI.WASM.Services.JSInterop;
using Microsoft.AspNetCore.Components;

namespace HOSKYSWAP.UI.WASM.Shared;

public partial class MainLayout
{
    [Inject] private CardanoWalletInteropService? CardanoWalletInteropService { get; set; }
    [Inject] private HelperInteropService? HelperInteropService { get; set; }
    private string WalletAddress { get; set; } = string.Empty;
    private bool IsWalletConnected { get; set; } = false;
    private string UserIdenticon { get; set; } = string.Empty;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await SetAvatarAsync();
        await base.OnAfterRenderAsync(firstRender);
    }

    private async void OnConnectBtnClicked()
    {
        if (CardanoWalletInteropService is not null && !await CardanoWalletInteropService.IsWalletConnectedAsync())
        {
            IsWalletConnected = await CardanoWalletInteropService.ConnectWalletAsync();
            await SetAvatarAsync();
        }
    }

    private async Task SetAvatarAsync()
    {
        if (CardanoWalletInteropService is not null && await CardanoWalletInteropService.IsWalletConnectedAsync())
        {
            IsWalletConnected = true;
            WalletAddress = await CardanoWalletInteropService.GetWalletAddressAsync() ?? String.Empty;
            UserIdenticon = await GetIdenticonAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    private string FormatAddress()
    {
        return WalletAddress.Length > 10 ? 
            $"{WalletAddress.Substring(0,4)}...{WalletAddress[^8..]}"
            : WalletAddress;
    }

    private async Task<string> GetIdenticonAsync()
    {
        if (HelperInteropService is not null)
            return await HelperInteropService.GenerateIdenticonAsync(WalletAddress);
        else
            return string.Empty;
    }
}