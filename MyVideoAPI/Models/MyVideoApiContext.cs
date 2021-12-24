using Microsoft.EntityFrameworkCore;

namespace MyVideoAPI.Models
{
  public class MyVideoApiContext : DbContext
  {
    public MyVideoApiContext(DbContextOptions options)
      :base(options)
    {
    }

    public DbSet<VideoModel> MyVideos { get; set; }
  }
}
