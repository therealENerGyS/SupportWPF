using CommunityToolkit.Mvvm.ComponentModel;

namespace SupportWPF.ViewModels
{
    internal partial class CreateOrderViewModel : ObservableObject
    {
        [ObservableProperty]
        private string firstName = null!;
        [ObservableProperty]
        private string lastName = null!;
        [ObservableProperty]
        private string email = null!;
        [ObservableProperty]
        private string phoneNumber = null!;
        [ObservableProperty]
        private string streetName = null!;
        [ObservableProperty]
        private string streetNumber = null!;
        [ObservableProperty]
        private string postalCode = null!;
        [ObservableProperty]
        private string city = null!;

        [ObservableProperty]
        private string productName = null!;
        [ObservableProperty]
        private string subject = null!;
        [ObservableProperty]
        private string priority = null!;
    }
}
