using System.ComponentModel;
using System.Runtime.CompilerServices;
using HOSKYSWAP.Common;
using MudBlazor;

namespace HOSKYSWAP.UI.WASM.Services;

public class AppStateService : INotifyPropertyChanged
{
    public readonly DialogOptions DialogOptions = new() {FullWidth = true, DisableBackdropClick = true};

    private bool _isWalletConnected = false;

    public bool IsWalletConnected
    {
        get => _isWalletConnected;

        set
        {
            _isWalletConnected = value;
            NotifyPropertyChanged();
        }
    }

    private Order? _currentOrder = null;

    public Order? CurrentOrder
    {
        get => _currentOrder;
        set
        {
            _currentOrder = value;
            NotifyPropertyChanged();
        }
    }

    private decimal _marketCap = 0m;
    
    public decimal MarketCap
    {
        get => _marketCap;
        set
        {
            _marketCap = value;
            NotifyPropertyChanged();
        }
    }
    
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")  
    {  
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }  
    
    public event PropertyChangedEventHandler? PropertyChanged;
}