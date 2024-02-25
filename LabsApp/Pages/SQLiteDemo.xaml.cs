namespace LabsApp.Pages;

public partial class SqLiteDemo : ContentPage
{
    public SqLiteDemo(SqLiteDemoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}