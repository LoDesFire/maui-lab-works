using LabsApp.Services;

namespace LabsApp.Pages;

public partial class Progress : ContentPage
{
    private CancellationTokenSource? _cancellationTokenSource;

    public Progress()
    {
        InitializeComponent();
    }

    private async void OnStartClicked(object? sender, EventArgs e)
    {
        if (_cancellationTokenSource != null)
        {
            await _cancellationTokenSource.CancelAsync();
            while (_cancellationTokenSource != null)
            {
                await Task.Delay(1);
            } 
        }
        
        _cancellationTokenSource = new CancellationTokenSource();
        var integrator = Resources["IntegralProgressKey"] as Integrator;
        try
        {
            if (integrator != null)
                await integrator.Sin(_cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)  { }
        finally
        {
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }

    private void OnCancelClicked(object? sender, EventArgs e)
    {
        _cancellationTokenSource?.Cancel();
    }
}