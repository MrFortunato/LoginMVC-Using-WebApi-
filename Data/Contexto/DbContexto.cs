using Exame_Online.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static Exame_Online.Data.Entidades.IdentyModel;
using Perfil = Exame_Online.Data.Entidades.IdentyModel.Perfil;

namespace Exame_Online.Data.Contexto
{
    public class Contexto: IdentityDbContext<Usuario, Perfil, int, UsuarioClaim, PerfilUsuario, UsuarioLogin, PerfilClaim, UsuarioToken>
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<tb_Candidatura> Candidaturas { get; set; }
        public DbSet<tb_Curso> Cursos { get; set; }
        public DbSet<tb_Disciplina> Disciplinas { get; set; }
        public DbSet<tb_Pergunta> Perguntas { get; set; }
        public DbSet<tb_Teste_Pergunta> TestePerguntas { get; set; }
        public DbSet<tb_DadosPessoais> DadosPessoais { get; set; }
        public DbSet<tb_TesteDisciplina> TesteDisciplinas { get; set; }
        public DbSet<tb_Resposta> Respostas { get; set; }
        public DbSet<tb_Teste_Resultado> TesteResultados { get; set; }
        public DbSet<tb_Teste> Testes { get; set; }
        public DbSet<tb_Departamento> Departamentos { get; set; }
        public DbSet<tb_TipoPergunta> TipoPerguntas { get; set; }
        public DbSet<PerfilUsuario> PerfilUsuarios { get; set; }
        public DbSet<tb_Agendamento_Teste> AgendamentosTestes { get; set; }
        public DbSet<tb_Sala_Exame> Sala_Exames { get; set; }

        public DbSet<tb_Opcoes_Candidatura> opcoesCandidaturas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Personalize o modelo de Identity do ASP.NET e substitua os padrões, se necessário.
            // Por exemplo, você pode renomear os nomes da tabela de identidade do ASP.NET e muito mais.
            // Adicione suas personalizações após chamar base.OnModelCreating (modelBuilder);

            modelBuilder.Entity<tb_TesteDisciplina>().HasKey(a => new { a.DisciplinaId , a.CursoId });

            modelBuilder.Entity<Usuario>(b =>
            {
                // Primary key
                b.HasKey(u => u.Id);

                // Indexes for "normalized" username and email, to allow efficient lookups


                // Mapas para a tabela AspNetUsers
                b.ToTable("Usuarios");
                b.Property(p => p.Id).HasColumnName("UsuarioId");
                b.ToTable("Usuarios");
                b.Property(p => p.UserName).HasColumnName("Nome_Usuario");


                // Um token de simultaneidade para uso com a verificação de simultaneidade otimista
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                // Limite o tamanho das colunas para usar tipos de banco de dados eficientes

                b.Property(u => u.UserName).HasMaxLength(20);
                b.Property(u => u.NormalizedUserName).HasMaxLength(20);

                // Os relacionamentos entre o usuário e outros tipos de entidade
                // Observe que esses relacionamentos são configurados sem propriedades de navegação

                // Cada usuário pode ter muitos UserClaims
                b.HasMany<UsuarioClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

                // Cada usuário pode ter vários UserLogins
                b.HasMany<UsuarioLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

                // Cada usuário pode ter vários UserTokens
                b.HasMany<UsuarioToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                // Cada usuário pode ter muitas entradas na tabela de junção UserRole
                b.HasMany<PerfilUsuario>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            modelBuilder.Entity<UsuarioClaim>(b =>
            {
                // Primary key
                b.HasKey(uc => uc.Id);

                // Maps to the AspNetUserClaims table
                b.ToTable("UsuarioClaim");
            });

            modelBuilder.Entity<UsuarioLogin>(b =>
            {
                // Chave primária composta que consiste em LoginProvider e a chave a ser usada
                // com aquele provedor
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

                // Limita o tamanho das colunas-chave compostas devido às restrições comuns do DB
                b.Property(l => l.LoginProvider).HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasMaxLength(128);

                // Mapas para a tabela AspNetUserLogins
                b.ToTable("UsuarioLogin");
            });

            modelBuilder.Entity<UsuarioToken>(b =>
            {
                // Composite primary key consisting of the UserId, LoginProvider and Name
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(t => t.LoginProvider).HasMaxLength(256);
                b.Property(t => t.Name).HasMaxLength(256);

                // Maps to the AspNetUserTokens table
                b.ToTable("UsuarioToken");
            });

            modelBuilder.Entity<Perfil>(b =>
            {
                // Primary key
                b.HasKey(r => r.Id);

                // Index for "normalized" role name to allow efficient lookups
                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

                // Maps to the AspNetRoles table
                b.ToTable("Perfil");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.Name).HasMaxLength(50);
                b.Property(u => u.NormalizedName).HasMaxLength(50);

                // The relationships between Role and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each Role can have many entries in the UserRole join table
                b.HasMany<PerfilUsuario>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany<PerfilClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            modelBuilder.Entity<PerfilClaim>(b =>
            {
                // Primary key
                b.HasKey(rc => rc.Id);

                // Maps to the AspNetRoleClaims table
                b.ToTable("PerfilClaim");
            });

            modelBuilder.Entity<PerfilUsuario>(b =>
            {
                // Primary key
                b.HasKey(r => new { r.UserId, r.RoleId });

                // Maps to the AspNetUserRoles table
                b.ToTable("PerfilUsuario");
            });


        }


    }
}
