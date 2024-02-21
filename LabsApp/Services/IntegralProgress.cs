namespace LabsApp.Services;

public class IntegralProgress : IntegralProgressProperties
{
    public async Task Sin(CancellationToken token)
    {
        token.Register(() => { Status = Status = "Задание отменено"; });

        Progress = 0;
        Status = "Идет вычисление";
        const double increment = 5e-5;
        var result = 0.0d;
        for (double i = 0; i < 1 + 1e-8; i += increment)
        {
            token.ThrowIfCancellationRequested();
            result += Math.Sin(i) * increment;
            await Task.Delay(1, token);
            if (!(i - Progress > 0.0001)) continue;
            Progress = i;
        }

        Status = $"Результат: {result}";
    }
}