using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PousadaIdentity.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddSession();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(150);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.User.Identity.IsAuthenticated)
    {
        var userManager = context.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
        var signInManager = context.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();
        var appDbContext = context.RequestServices.GetRequiredService<AppDbContext>();

        // Verifica se o usuário já está autenticado
        var user = await userManager.GetUserAsync(context.User);
        if (user != null)
        {
            var pessoa = appDbContext.Pessoa.FirstOrDefault(p => p.Email == user.Email);
            if (pessoa != null)
            {
                int? nullablePessoaId = pessoa.PessoaId;
                int pessoaId = nullablePessoaId ?? 0;
                context.Session.SetInt32("SessionPessoaId", pessoaId);
            }
        }
    }

    await next();
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




