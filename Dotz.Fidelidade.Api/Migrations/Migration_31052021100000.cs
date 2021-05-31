using FluentMigrator;
using System;

namespace Dotz.Fidelidade.Data.Migrations
{
    [Migration(31052021100000)]
    public class Migration_31052021100000 : Migration
    {
        public override void Down()
        {
            Delete.Table("ProdutoResgateMovimentacao");
            Delete.Table("ProdutoResgateTotalizador");
            Delete.Table("Pedido");
            Delete.Table("UsuarioEndereco");
            Delete.Table("Produto");
            Delete.Table("Usuario");
        }

        public override void Up()
        {
            Create.Table("Usuario")
                .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("Email").AsAnsiString(100).NotNullable()
                .WithColumn("Senha").AsAnsiString(20).NotNullable();

            Create.Table("UsuarioEndereco")
                 .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                 .WithColumn("UsuarioId").AsInt32().NotNullable()
                 .WithColumn("Numero").AsAnsiString(10).NotNullable()
                 .WithColumn("Bairro").AsAnsiString(255).NotNullable()
                 .WithColumn("Cidade").AsAnsiString(255).NotNullable()
                 .WithColumn("Estado").AsAnsiString(2).NotNullable()
                 .WithColumn("Cep").AsAnsiString(10).NotNullable()
                 .WithColumn("PadraoEntrega").AsBoolean().NotNullable();

            Create.ForeignKey("FK_Usuario_UsuarioEndereco")
                .FromTable("UsuarioEndereco").ForeignColumn("UsuarioId")
                .ToTable("Usuario").PrimaryColumn("Id");


            Create.Table("Produto")
                 .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                 .WithColumn("Nome").AsAnsiString(255).NotNullable()
                 .WithColumn("QuantidadePontosPremiacao").AsInt32().NotNullable();


            Create.Table("ProdutoResgateMovimentacao")
                  .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                  .WithColumn("UsuarioId").AsInt32().NotNullable()
                  .WithColumn("ProdutoId").AsInt32().NotNullable()
                  .WithColumn("QuantidadePontoUtilizado").AsInt32().NotNullable()
                  .WithColumn("DataResgate").AsDateTime().Nullable()
                  .WithColumn("TipoMovimentacaoId").AsInt32().Nullable()
                  .WithColumn("DataEntrada").AsDateTime().Nullable();

            Create.ForeignKey("FK_Usuario_ProdutoResgateMovimentacao")
                .FromTable("ProdutoResgateMovimentacao").ForeignColumn("UsuarioId")
                .ToTable("Usuario").PrimaryColumn("Id");

            Create.ForeignKey("FK_Produto_ProdutoResgateMovimentacao")
                   .FromTable("ProdutoResgateMovimentacao").ForeignColumn("ProdutoId")
                   .ToTable("Produto").PrimaryColumn("Id");


            Create.Table("ProdutoResgateTotalizador")
                 .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                 .WithColumn("UsuarioId").AsInt32().NotNullable()
                 .WithColumn("PontosAcumulados").AsInt32().NotNullable()
                 .WithColumn("DataUltimaAtualizacao").AsDateTime().NotNullable();

            Create.ForeignKey("FK_Usuario_ProdutoResgateTotalizador")
                .FromTable("ProdutoResgateTotalizador").ForeignColumn("UsuarioId")
                .ToTable("Usuario").PrimaryColumn("Id");

            Create.Table("Pedido")
               .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
               .WithColumn("UsuarioId").AsInt32().NotNullable()
               .WithColumn("Numero").AsInt32().NotNullable()
               .WithColumn("StatusEntrega").AsInt32().NotNullable();

            Create.ForeignKey("FK_Usuario_Pedido")
                .FromTable("Pedido").ForeignColumn("UsuarioId")
                .ToTable("Usuario").PrimaryColumn("Id");

            InsertDadosGeograficos();
        }

        private void InsertDadosGeograficos()
        {
            InsertProduto(1, "Ventilador", 20);
            InsertProduto(2, "Liquidificador", 50);
            InsertProduto(3, "Geladeira", 500);
            InsertProduto(5, "TV", 1000);

            InsertUsuario(1, "eide.leticia@gmail.com", "123456");

            InsertProdutoResgateMovimentacaoSaida(1, 1, 1, 100, 2);
            InsertProdutoResgateMovimentacaoEntrada(2, 1, 1, 100, 1);

            InsertProdutoResgateTotalizador(0, 1, 2000);
        }

        private void InsertProduto(int id, string nome, int quantidadePontosPremiacao)
        {
            Insert.IntoTable("Produto").Row(new
            {
                Id = id,
                Nome = nome,
                QuantidadePontosPremiacao = quantidadePontosPremiacao
            });
        }

        private void InsertProdutoResgateTotalizador(int id, int usuarioId, int pontosAcumulados)
        {
            Insert.IntoTable("ProdutoResgateTotalizador").Row(new
            {
                Id = id,
                UsuarioId = usuarioId,
                PontosAcumulados = pontosAcumulados,
                DataUltimaAtualizacao = DateTime.Now
            });
        }

        private void InsertUsuario(int id, string email, string senha)
        {
            Insert.IntoTable("Usuario").Row(new
            {
                Id = id,
                Email = email,
                Senha = senha              
            });
        }

        private void InsertProdutoResgateMovimentacaoSaida(int id, int usuarioId, int produtoId, 
            int quantidadePontoUtilizado, int tipoMovimentacaoId)
        {           

            Insert.IntoTable("ProdutoResgateMovimentacao").Row(new
            {
                Id = id,
                UsuarioId = usuarioId,
                ProdutoId = produtoId,               
                QuantidadePontoUtilizado = quantidadePontoUtilizado,
                DataResgate = DateTime.Now,               
                TipoMovimentacaoId = tipoMovimentacaoId
            });
        }

        private void InsertProdutoResgateMovimentacaoEntrada(int id, int usuarioId, int produtoId,
           int quantidadePontoUtilizado, int tipoMovimentacaoId)
        {

            Insert.IntoTable("ProdutoResgateMovimentacao").Row(new
            {
                Id = id,
                UsuarioId = usuarioId,
                ProdutoId = produtoId,
                QuantidadePontoUtilizado = quantidadePontoUtilizado,
                DataEntrada = DateTime.Now,                
                TipoMovimentacaoId = tipoMovimentacaoId
            });
        }
    }
}
