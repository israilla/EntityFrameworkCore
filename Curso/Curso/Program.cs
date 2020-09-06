using Microsoft.EntityFrameworkCore;
using System;

namespace Curso
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();
            //aplicando as migrations na base de dados
            db.Database.Migrate();

            Console.WriteLine("Hello World!");
        }
    }
}
