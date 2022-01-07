using AspnetCore6ApiTestingDemo.Infra;
using Microsoft.EntityFrameworkCore;

namespace AspnetCore6ApiTestingDemo.Test
{
    public class DemoContextSqlite : DemoContext
    {
        /*
         * This context is here for to generate migrations.
         *
         *  - Delete Migrations folder if needed then
         *  - Run following command in main, solution directory:
         *
         *          dotnet ef migrations add InitialMigrationSqlite --context DemoContextSqlite --project AspnetCore6ApiTestingDemo.Test
         *
         */

        //empty ctor required for generating migrations from dotnet tool
        public DemoContextSqlite()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(TestWebApplicationFactory<Program>.ConnectionString);
        }

        public DemoContextSqlite(DbContextOptions<DemoContext> options) : base(options)
        {
        }
    }
}