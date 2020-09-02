using Microsoft.Extensions.DependencyInjection;
using QMPT.Data.Services.Devices;
using QMPT.Data.Services.Organizations;

namespace QMPT.Data.Services
{
    public static class ServicesExtension
    {
        public static void AddRequestHandlers(this IServiceCollection services)
        {
            services.AddScoped<UsersService>();
            services.AddScoped<RefreshTokensService>();
            services.AddScoped<CustomersService>();
            services.AddScoped<OrganizationsService>();
            services.AddScoped<OrganizationNotesService>();

            services.AddScoped<ContactPersonsService>();
            services.AddScoped<ContactPersonEmailsService>();
            services.AddScoped<ContactPersonPhoneNumbersService>();

            services.AddScoped<KeyValuePairsService>();
            services.AddScoped<DevicesService>();
            services.AddScoped<PricesService>();
        }
    }
}
