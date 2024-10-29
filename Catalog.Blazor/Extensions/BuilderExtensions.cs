using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Catalog.Components.Account;
using Catalog.Infra;
using Catalog.Infra.Repositories.Interfaces;
using Catalog.Infra.Repositories;
using Catalog.Application.UseCases.Interfaces;
using Catalog.Application.UseCases;

namespace Catalog.Extensions;
public static class BuilderExtensions
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("Catalog"));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IListProductsPerPage, ListProductsPerPage>();
        builder.Services.AddScoped<IGetProductDetails, GetProductDetails>();
        builder.Services.AddScoped<IRegisterNewProduct, RegisterNewProduct>();
        builder.Services.AddScoped<IUpdateProduct, UpdateProduct>();
        builder.Services.AddScoped<IDeleteProduct, DeleteProduct>();

        return builder;
    }
}