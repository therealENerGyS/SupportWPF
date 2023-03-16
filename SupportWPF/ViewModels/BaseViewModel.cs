using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SupportWPF.ViewModels
{
    internal partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject currentVM = null!;

        [RelayCommand]
        private void ShowOrders() => CurrentVM = new ShowOrdersViewModel();

        [RelayCommand]
        private void CreateOrder() => CurrentVM = new CreateOrderViewModel();

        public BaseViewModel()
        {
            CurrentVM = new ShowOrdersViewModel();
        }
    }
}
