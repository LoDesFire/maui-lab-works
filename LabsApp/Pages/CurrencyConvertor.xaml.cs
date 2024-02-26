using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsApp.Pages;

public partial class CurrencyConvertor : ContentPage
{
    public CurrencyConvertor(CurrencyConvertorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}