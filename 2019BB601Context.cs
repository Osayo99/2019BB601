using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using _2019BB601.Models;
namespace _2019BB601
{
    public class _2019BB601Context : DbContext
    {
        public _2019BB601Context(DbContextOptions<_2019BB601Context> options) : base(options)
        {
            
        }
        public DbSet<equipos> equipos {get; set;}
    }
}