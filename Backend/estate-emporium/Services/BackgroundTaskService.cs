using System.Collections.Concurrent;

namespace estate_emporium.Services;

    public class BackgroundTaskService(ILogger<BackgroundTaskService> logger, IServiceScopeFactory scopeFactory)
    {
    private readonly ConcurrentQueue<Func<IServiceProvider, Task>> _workItems = new ConcurrentQueue<Func<IServiceProvider, Task>>();
    private readonly ILogger<BackgroundTaskService> _logger= logger;
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

        public void QueueBackgroundWorkItem(Func<IServiceProvider, Task> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);
            Task.Run(async () => await ProcessQueue());
        }

        private async Task ProcessQueue()
        {
            while (_workItems.TryDequeue(out var workItem))
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    try
                    {
                        await workItem(scope.ServiceProvider);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred executing background task.");
                        // Handle the exception (e.g., retry logic, alerting, etc.)
                    }
                }
            }
        }
}
