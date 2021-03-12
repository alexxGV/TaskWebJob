using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TaskWebJob.Data;
using TaskWebJob.Repositories;

namespace TaskWebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            String url = "Data Source=sqltajamaralex.database.windows.net;Initial Catalog=BBDDTajamar;Persist Security Info=True;User ID=adminsql;Password=Admin123";
            var provider = new ServiceCollection()
                .AddTransient<RepositoryWebJob>()
                .AddDbContext<WebJobContext>(options =>
                options.UseSqlServer(url)).BuildServiceProvider();
            RepositoryWebJob repo = provider.GetService<RepositoryWebJob>();
            Console.WriteLine("PULSE UNA TACLA PARA INICIAR");
            //Console.ReadLine();
            repo.PopulateDataWebJob();
            Console.WriteLine("PROCESO TERMINADO CUALQUIER TECLA PARA TERMINAR");
            //Console.ReadLine();
        }
    }
}
