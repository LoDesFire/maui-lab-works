namespace LabsApp.Services;

public class Integrator : IntegratorProperties
{
    public async Task Sin(CancellationToken token)
    {
        token.Register(() => { Status = "Задание отменено"; });
        Status = "Идет вычисление";
        Progress = 0;
        
        const double increment = 5e-5;
        var result = 0.0d;
        for (double i = 0; i < 1 + 1e-8; i += increment)
        {
            await Task.Delay(1, token);
            token.ThrowIfCancellationRequested();
            
            result += Math.Sin(i) * increment;
            if (!(i - Progress > 0.0001)) continue;
            Progress = i;
        }

        Status = $"Результат: {result}";
    }
}