namespace InnoSetupWindowsService
{
    public sealed class WindowsBackgroundService : BackgroundService
    {
        private readonly JokeService _jokeService;
        private readonly ILogger<WindowsBackgroundService> _logger;

        public WindowsBackgroundService(
            JokeService jokeService,
            ILogger<WindowsBackgroundService> logger) =>
            (_jokeService, _logger) = (jokeService, logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string joke = _jokeService.GetJoke();
                _logger.LogWarning(joke);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await SimulateLongFileReleaseAsync();
        }
         
        private static async Task SimulateLongFileReleaseAsync()
        {
            // wait a long time before service is actually stopped and files are released by
            await Task.Delay(15000);
        }
    }
}