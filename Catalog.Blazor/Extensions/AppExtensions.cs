
using Catalog.Components;

namespace Catalog.Extensions;

public static class AppExtensions
{
    public static WebApplication AddApplicationPipeline(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
            app.UseMigrationsEndPoint();
        }
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        app.MapAdditionalIdentityEndpoints();

        return app;
    }
}
