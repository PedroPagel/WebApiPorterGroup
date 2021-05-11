using Entities.AreaPredial;
using Entities.Pessoa;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Context
{
    public class WebApiContext : DbContext
    {
        public WebApiContext() { }
        public DbSet<Condominio> Condominios { get; set; }
        public DbSet<Bloco> Blocos { get; set; }
        public DbSet<Apartamento> Apartamentos { get; set; }
        public DbSet<Morador> Moradores { get; set; }

        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options)
        {
        }

        public void AdicionarDados()
        {
            Condominio condominio = new()
            {
                EmailSindico = "jose@gmail.com",
                TelefoneSindico = "4830301918",
                Nome = "Royal Paradise"
            };

            Condominios.Add(condominio);

            Condominio condominio2 = new()
            {
                EmailSindico = "jose@gmail.com",
                TelefoneSindico = "4830301918",
                Nome = "Royal"
            };

            Condominios.Add(condominio2);
            SaveChanges();


            #region blocos
            Bloco bloco1 = new()
            {
                Nome = "Bloco I",
                CondominioId = condominio.Id
            };

            Blocos.Add(bloco1);

            Bloco bloco2 = new()
            {
                Nome = "Bloco II",
                CondominioId = condominio.Id
            };

            Blocos.Add(bloco2);

            Bloco bloco3 = new()
            {
                Nome = "Bloco III",
                CondominioId = condominio.Id
            };

            Blocos.Add(bloco3);

            SaveChanges();
            #endregion

            #region Apartamentos
            Apartamento apartamento1 = new()
            {
                Andar = 1,
                Numero = 101,
                CondominioId = condominio.Id,
                BlocoId = bloco1.Id
            };

            Apartamentos.Add(apartamento1);

            Apartamento apartamento2 = new()
            {
                Andar = 2,
                Numero = 201,
                CondominioId = condominio.Id,
                BlocoId = bloco1.Id
            };

            Apartamentos.Add(apartamento2);

            Apartamento apartamento3 = new()
            {
                Andar = 1,
                Numero = 101,
                CondominioId = condominio.Id,
                BlocoId = bloco2.Id
            };

            Apartamentos.Add(apartamento3);

            Apartamento apartamento4 = new()
            {
                Andar = 2,
                Numero = 201,
                CondominioId = condominio.Id,
                BlocoId = bloco2.Id
            };

            Apartamentos.Add(apartamento4);

            Apartamento apartamento5 = new()
            {
                Andar = 3,
                Numero = 301,
                CondominioId = condominio.Id,
                BlocoId = bloco2.Id
            };

            Apartamentos.Add(apartamento5);
            SaveChanges();
            #endregion

            #region Moradores
            AdicionarMorador(apartamento1.Id, "Ana Paula", "12345678988", "04830301918", "ana@gmail.com");
            AdicionarMorador(apartamento2.Id, "Jose", "12345678988", "04830301918", "jose@gmail.com");
            AdicionarMorador(apartamento3.Id, "Marta", "12345678988", "04830301918", "marta@gmail.com");
            AdicionarMorador(apartamento4.Id, "Pedro Pagel", "12345678988", "04830301918", "pedro.pagel@gmail.com");
            AdicionarMorador(apartamento1.Id, "Marcio", "12345678988", "04830301918", "marcio@gmail.com");
            AdicionarMorador(apartamento2.Id, "Laura", "12345678988", "04830301918", "laura@gmail.com");
            AdicionarMorador(apartamento3.Id, "João", "12345678988", "04830301918", "joao@gmail.com");
            AdicionarMorador(apartamento4.Id, "Maria", "12345678988", "04830301918", "maria@gmail.com");
            SaveChanges();
            #endregion
        }

        private void AdicionarMorador(int idApartamento, string nome, string cpf, string telefone, string email)
        {
            Morador morador = new()
            {
                Nome = nome,
                Cpf = cpf,
                Email = email,
                Telefone = telefone,
                DataNascimento = new DateTime(1986, 7, 9),
                ApartamentoId = idApartamento
            };

            Moradores.Add(morador);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Condominio>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Condominio>().Property(c => c.Nome);
            modelBuilder.Entity<Condominio>().Property(c => c.EmailSindico);
            modelBuilder.Entity<Condominio>().Property(c => c.TelefoneSindico);

            modelBuilder.Entity<Bloco>().Property(b => b.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Bloco>().Property(b => b.Nome);

            modelBuilder.Entity<Apartamento>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Apartamento>().Property(a => a.Andar);
            modelBuilder.Entity<Apartamento>().Property(a => a.Numero);

            modelBuilder.Entity<Morador>().Property(m => m.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Morador>().Property(m => m.Cpf);
            modelBuilder.Entity<Morador>().Property(m => m.Nome);
            modelBuilder.Entity<Morador>().Property(m => m.Email);
            modelBuilder.Entity<Morador>().Property(m => m.Telefone);
            modelBuilder.Entity<Morador>().Property(m => m.DataNascimento);

            //Ligacao filho pai
            modelBuilder.Entity<Bloco>().HasOne<Condominio>(r => r.Condominio).WithMany(u => u.Blocos).HasForeignKey(r => r.CondominioId);
            modelBuilder.Entity<Apartamento>().HasOne<Bloco>(r => r.Bloco).WithMany(u => u.Apartamentos).HasForeignKey(r => r.BlocoId);
            modelBuilder.Entity<Apartamento>().HasOne<Condominio>(r => r.Condominio).WithMany(u => u.Apartamentos).HasForeignKey(r => r.CondominioId);
            modelBuilder.Entity<Morador>().HasOne<Apartamento>(r => r.Apartamento).WithMany(u => u.Moradores).HasForeignKey(r => r.ApartamentoId);

            //Atribuicao de parentesco
            modelBuilder.Entity<Condominio>().HasMany<Bloco>(r => r.Blocos).WithOne(u => u.Condominio).HasForeignKey(r => r.CondominioId);
            modelBuilder.Entity<Condominio>().HasMany<Apartamento>(r => r.Apartamentos).WithOne(u => u.Condominio).HasForeignKey(r => r.CondominioId);
            modelBuilder.Entity<Bloco>().HasMany<Apartamento>(r => r.Apartamentos).WithOne(u => u.Bloco).HasForeignKey(r => r.BlocoId);
            modelBuilder.Entity<Apartamento>().HasMany<Morador>(r => r.Moradores).WithOne(u => u.Apartamento).HasForeignKey(r => r.ApartamentoId);
        }
    }
}
