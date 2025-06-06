using Microsoft.EntityFrameworkCore;
using WebBanHang215.Models;
using WebBanHang215.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebBanHangCong215Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký DI
builder.Services.AddScoped<ILoaiSanPhamRepository, LoaiSanPhamRepository>();
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); 

app.UseAuthorization();

//  ho tro area route trước default route
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Route mac dinh
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DangNhap}/{action=Login}/{id?}");

// Add this after database creation
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WebBanHangCong215Context>();
    
    // Check if admin exists
    if (!context.NguoiDungs.Any(u => u.VaiTro == "Admin"))
    {
        var admin = new NguoiDung
        {
            TenDangNhap = "admin",
            MatKhau = "123456", // Use proper hashing in production
            HoVaTen = "Quản trị viên",
            Email = "admin@shop.com",
            VaiTro = "Admin"
        };
        context.NguoiDungs.Add(admin);
        context.SaveChanges();
    }
}

app.Run();
