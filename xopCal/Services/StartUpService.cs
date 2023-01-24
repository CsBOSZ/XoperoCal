namespace xopCal.Services;

public class StartUpService : IHostedService 
{
   
    private readonly IServiceScopeFactory ssf;


    public StartUpService(IServiceScopeFactory ssf)
    {
        this.ssf = ssf;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = ssf.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IEventCalService>();
        await service.StartWatch();
    }
    
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        
    }
}