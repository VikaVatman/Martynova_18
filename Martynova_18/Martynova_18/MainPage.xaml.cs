using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Martynova_18
{
    public partial class MainPage : ContentPage
    {
        RateViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new RateViewModel();
            // установка контекста данных
            this.BindingContext = viewModel;
        }
    }
}
