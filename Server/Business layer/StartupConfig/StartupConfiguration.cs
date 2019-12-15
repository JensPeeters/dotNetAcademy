using Business_layer;
using Business_layer.Interfaces;
using Business_layer.Interfaces.Mapping;
using Business_layer.Mapping;
using Data_layer.Filter;
using Data_layer.Interfaces;
using Data_layer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ExtensionMethods
{
    public static class StartupConfiguration
    {
        public static void AddConfig(this IServiceCollection services)
        {
            services.AddTransient<ITrajectFacade, TrajectFacade>();
            services.AddTransient<ICursusFacade, CursusFacade>();
            services.AddTransient<IWinkelwagenFacade, WinkelwagenFacade>();
            services.AddTransient<IBestellingFacade, BestellingFacade>();
            services.AddTransient<IAdminFacade, AdminFacade>();
            services.AddTransient<IKlantFacade, KlantFacade>();
            services.AddTransient<IWinkelwagenRepository, WinkelwagenRepository>();
            services.AddTransient<IBestellingRepository, BestellingRepository>();
            services.AddTransient<ICursusRepository, CursusRepository>();
            services.AddTransient<ITrajectRepository, TrajectRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IKlantRepository, KlantRepository>();
            services.AddScoped<ICostCalculator, CostCalculator>();
            services.AddScoped<IContextFilter, ContextFilter>();
            services.AddScoped<ICursusMapper, CursusMapper>();
            services.AddScoped<ITrajectMapper, TrajectMapper>();
            services.AddScoped<IBestellingMapper, BestellingMapper>();
            services.AddScoped<IWinkelwagenMapper, WinkelwagenMapper>();
            services.AddScoped<IAdminMapper, AdminMapper>();
            services.AddScoped<IKlantMapper, KlantMapper>();
        }
    }
}
