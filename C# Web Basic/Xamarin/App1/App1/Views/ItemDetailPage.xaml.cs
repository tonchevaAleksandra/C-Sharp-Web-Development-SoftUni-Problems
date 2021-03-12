using App1.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace App1.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}