using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace RegistroEmpleados.AppMovil.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    string text;

    [RelayCommand]
    void Add()
    {
        Text = string.Empty;
    }
}
