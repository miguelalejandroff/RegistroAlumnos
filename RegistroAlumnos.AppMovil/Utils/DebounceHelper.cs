namespace RegistroAlumnos.AppMovil.Utils
{
    public static class DebounceHelper
    {
        public static void ApplyDebounce(Action action, int delayMilliseconds, ref CancellationTokenSource tokenSource)
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            Task.Delay(delayMilliseconds, token)
                .ContinueWith(task =>
                {
                    if (task.IsCanceled) return;
                    action();
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
