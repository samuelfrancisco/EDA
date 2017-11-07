namespace EGuru.MarketPlace.Loja.Consultas.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturaInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Licencas",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDaLicenca = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        QuantidadeDeUsuarios = c.Int(),
                        Titulo = c.String(),
                        Objetivo = c.String(),
                        Descricao = c.String(),
                        CargaHoraria = c.String(),
                        ImagemIlustrativa = c.String(),
                        Banner = c.String(),
                        Link = c.String(),
                        DataDeInicio = c.DateTime(nullable: false),
                        QuantidadeDeMeses = c.Int(),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDaLicenca, t.IdDoConteudo });
            
            CreateTable(
                "dbo.PaginasDeDetalhesDosConteudos",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        MinhaAvaliacao_IdDoConteudo = c.Guid(),
                        MinhaAvaliacao_IdDoUsuario = c.Guid(),
                    })
                .PrimaryKey(t => t.IdDoConteudo)
                .ForeignKey("dbo.Resenhas", t => new { t.MinhaAvaliacao_IdDoConteudo, t.MinhaAvaliacao_IdDoUsuario })
                .Index(t => new { t.MinhaAvaliacao_IdDoConteudo, t.MinhaAvaliacao_IdDoUsuario });
            
            CreateTable(
                "dbo.CompetenciasParaOCliente",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        IdDoCliente = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDaCompetencia = c.Guid(nullable: false),
                        Titulo = c.String(),
                        Gaps = c.String(),
                        Medidas = c.String(),
                        Trabalhadas = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoConteudo, t.IdDoCliente, t.SiglaDoIdioma, t.IdDaCompetencia })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => t.IdDoConteudo, cascadeDelete: true)
                .Index(t => t.IdDoConteudo);
            
            CreateTable(
                "dbo.CompetenciasParaSuaEmpresa",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        IdDaEmpresa = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDaCompetencia = c.Guid(nullable: false),
                        Titulo = c.String(),
                        Gaps = c.String(),
                        Medidas = c.String(),
                        Trabalhadas = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoConteudo, t.IdDaEmpresa, t.SiglaDoIdioma, t.IdDaCompetencia })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => t.IdDoConteudo, cascadeDelete: true)
                .Index(t => t.IdDoConteudo);
            
            CreateTable(
                "dbo.CompetenciasParaSuaEquipe",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        IdDoUsuario = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDaCompetencia = c.Guid(nullable: false),
                        Titulo = c.String(),
                        Gaps = c.String(),
                        Medidas = c.String(),
                        Trabalhadas = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoConteudo, t.IdDoUsuario, t.SiglaDoIdioma, t.IdDaCompetencia })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => t.IdDoConteudo, cascadeDelete: true)
                .Index(t => t.IdDoConteudo);
            
            CreateTable(
                "dbo.ConsolidadoAvaliacoes",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        QuantidadeAvaliacoes = c.Int(nullable: false),
                        MediaDasAvaliacoesDoConteudo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantidadeDeAvaliacoesPorValorDeAvaliacaoAsXml = c.String(),
                    })
                .PrimaryKey(t => t.IdDoConteudo)
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => t.IdDoConteudo)
                .Index(t => t.IdDoConteudo);
            
            CreateTable(
                "dbo.Detalhes",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        Imagem = c.String(),
                        QuantidadeDeAquisicoes = c.Int(nullable: false),
                        CustoLabel = c.String(),
                        Tipo = c.String(),
                        Titulo = c.String(),
                        Avaliacao = c.String(),
                        Objetivo = c.String(),
                        Conteudo = c.String(),
                        CargaHoraria = c.String(),
                        IdDaEscola = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoConteudo, t.SiglaDoIdioma })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => t.IdDoConteudo, cascadeDelete: true)
                .Index(t => t.IdDoConteudo);
            
            CreateTable(
                "dbo.PalavrasChave",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDaPalavraChave = c.Guid(nullable: false),
                        Titulo = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoConteudo, t.SiglaDoIdioma, t.IdDaPalavraChave })
                .ForeignKey("dbo.Detalhes", t => new { t.IdDoConteudo, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.MaisDoFornecedor",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdMaidDoFornecedor = c.Guid(nullable: false),
                        Tipo = c.String(),
                        Titulo = c.String(),
                        Imagem = c.String(),
                        Avaliacao = c.String(),
                        Custo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustoLabel = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoConteudo, t.SiglaDoIdioma, t.IdMaidDoFornecedor })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => t.IdDoConteudo, cascadeDelete: true)
                .Index(t => t.IdDoConteudo);
            
            CreateTable(
                "dbo.Resenhas",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        IdDoUsuario = c.Guid(nullable: false),
                        Avaliacao = c.Int(nullable: false),
                        ImagemUsuario = c.String(),
                        NomeUsuario = c.String(),
                        DataLabel = c.String(),
                        Conteudo = c.String(),
                        DataResenha = c.DateTime(nullable: false),
                        PaginaDeDetalhesDoConteudo_IdDoConteudo = c.Guid(),
                    })
                .PrimaryKey(t => new { t.IdDoConteudo, t.IdDoUsuario })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => t.IdDoConteudo, cascadeDelete: true)
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => t.PaginaDeDetalhesDoConteudo_IdDoConteudo)
                .Index(t => t.IdDoConteudo)
                .Index(t => t.PaginaDeDetalhesDoConteudo_IdDoConteudo);
            
            CreateTable(
                "dbo.Precos",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        PrecoIlimitado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecoIlimitadoLabel = c.String(),
                    })
                .PrimaryKey(t => t.IdDoConteudo)
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => t.IdDoConteudo)
                .Index(t => t.IdDoConteudo);
            
            CreateTable(
                "dbo.PrecosPorQuantidadeDeUsuarios",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        IdPrecoPorQuantidadeDeUsuarios = c.Guid(nullable: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LicencasDe = c.Int(nullable: false),
                        LicencasAte = c.Int(),
                    })
                .PrimaryKey(t => new { t.IdDoConteudo, t.IdPrecoPorQuantidadeDeUsuarios })
                .ForeignKey("dbo.Precos", t => t.IdDoConteudo, cascadeDelete: true)
                .Index(t => t.IdDoConteudo);
            
            CreateTable(
                "dbo.Previews",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        IdDoPreview = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        Titulo = c.String(),
                        Imagem = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoConteudo, t.IdDoPreview, t.SiglaDoIdioma })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => t.IdDoConteudo, cascadeDelete: true)
                .Index(t => t.IdDoConteudo);
            
            CreateTable(
                "dbo.Semelhantes",
                c => new
                    {
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdSemelhante = c.Guid(nullable: false),
                        Tipo = c.String(),
                        Titulo = c.String(),
                        Imagem = c.String(),
                        Avaliacao = c.String(),
                        Custo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustoLabel = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoConteudo, t.SiglaDoIdioma, t.IdSemelhante })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => t.IdDoConteudo, cascadeDelete: true)
                .Index(t => t.IdDoConteudo);
            
            CreateTable(
                "dbo.PaginasIniciais",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.DestaquesDaSemana",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDaPromocao = c.Guid(nullable: false),
                        LarguraMinima = c.String(nullable: false, maxLength: 5),
                        Banner = c.String(),
                        IdDaEscola = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDaPromocao, t.LarguraMinima })
                .ForeignKey("dbo.PaginasIniciais", t => new { t.IdDoCliente, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.Lancamentos",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDoConteudo = c.Guid(nullable: false),
                        Tipo = c.String(),
                        Titulo = c.String(),
                        Imagem = c.String(),
                        Avaliacao = c.String(),
                        Custo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustoLabel = c.String(),
                        IdDaEscola = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDoConteudo })
                .ForeignKey("dbo.PaginasIniciais", t => new { t.IdDoCliente, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.MaisPopulares",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDoConteudo = c.Guid(nullable: false),
                        Tipo = c.String(),
                        Titulo = c.String(),
                        Imagem = c.String(),
                        Avaliacao = c.String(),
                        Custo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustoLabel = c.String(),
                        IdDaEscola = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDoConteudo })
                .ForeignKey("dbo.PaginasIniciais", t => new { t.IdDoCliente, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.RecomendadosParaOCliente",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDoConteudo = c.Guid(nullable: false),
                        Tipo = c.String(),
                        Titulo = c.String(),
                        Imagem = c.String(),
                        Avaliacao = c.String(),
                        Custo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustoLabel = c.String(),
                        IdDaEscola = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDoConteudo })
                .ForeignKey("dbo.PaginasIniciais", t => new { t.IdDoCliente, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.RecomendadoParaOClienteCompetencias",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDoConteudo = c.Guid(nullable: false),
                        IdDaCompetencia = c.String(nullable: false, maxLength: 128),
                        QuantidadeDeVezesMedidas = c.Int(nullable: false),
                        PorcentagemDosGaps = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDoConteudo, t.IdDaCompetencia })
                .ForeignKey("dbo.RecomendadosParaOCliente", t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDoConteudo }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDoConteudo });
            
            CreateTable(
                "dbo.RecomendadosParaSuaEmpresa",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDaEmpresa = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        Tipo = c.String(),
                        Titulo = c.String(),
                        Imagem = c.String(),
                        Avaliacao = c.String(),
                        Custo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustoLabel = c.String(),
                        IdDaEscola = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDaEmpresa, t.IdDoConteudo })
                .ForeignKey("dbo.PaginasIniciais", t => new { t.IdDoCliente, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.RecomendadoParaSuaEmpresaCompetencias",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDaEmpresa = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        IdDaCompetencia = c.String(nullable: false, maxLength: 128),
                        QuantidadeDeVezesMedidas = c.Int(nullable: false),
                        PorcentagemDosGaps = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDaEmpresa, t.IdDoConteudo, t.IdDaCompetencia })
                .ForeignKey("dbo.RecomendadosParaSuaEmpresa", t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDaEmpresa, t.IdDoConteudo }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDaEmpresa, t.IdDoConteudo });
            
            CreateTable(
                "dbo.RecomendadosParaSuaEquipe",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDoUsuario = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        Tipo = c.String(),
                        Titulo = c.String(),
                        Imagem = c.String(),
                        Avaliacao = c.String(),
                        Custo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustoLabel = c.String(),
                        IdDaEscola = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDoUsuario, t.IdDoConteudo })
                .ForeignKey("dbo.PaginasIniciais", t => new { t.IdDoCliente, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.RecomendadoParaSuaEquipeCompetencias",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDoUsuario = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        IdDaCompetencia = c.String(nullable: false, maxLength: 128),
                        QuantidadeDeVezesMedidas = c.Int(nullable: false),
                        PorcentagemDosGaps = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDoUsuario, t.IdDoConteudo, t.IdDaCompetencia })
                .ForeignKey("dbo.RecomendadosParaSuaEquipe", t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDoUsuario, t.IdDoConteudo }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdDoUsuario, t.IdDoConteudo });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecomendadosParaSuaEquipe", new[] { "IdDoCliente", "SiglaDoIdioma" }, "dbo.PaginasIniciais");
            DropForeignKey("dbo.RecomendadoParaSuaEquipeCompetencias", new[] { "IdDoCliente", "SiglaDoIdioma", "IdDoUsuario", "IdDoConteudo" }, "dbo.RecomendadosParaSuaEquipe");
            DropForeignKey("dbo.RecomendadosParaSuaEmpresa", new[] { "IdDoCliente", "SiglaDoIdioma" }, "dbo.PaginasIniciais");
            DropForeignKey("dbo.RecomendadoParaSuaEmpresaCompetencias", new[] { "IdDoCliente", "SiglaDoIdioma", "IdDaEmpresa", "IdDoConteudo" }, "dbo.RecomendadosParaSuaEmpresa");
            DropForeignKey("dbo.RecomendadosParaOCliente", new[] { "IdDoCliente", "SiglaDoIdioma" }, "dbo.PaginasIniciais");
            DropForeignKey("dbo.RecomendadoParaOClienteCompetencias", new[] { "IdDoCliente", "SiglaDoIdioma", "IdDoConteudo" }, "dbo.RecomendadosParaOCliente");
            DropForeignKey("dbo.MaisPopulares", new[] { "IdDoCliente", "SiglaDoIdioma" }, "dbo.PaginasIniciais");
            DropForeignKey("dbo.Lancamentos", new[] { "IdDoCliente", "SiglaDoIdioma" }, "dbo.PaginasIniciais");
            DropForeignKey("dbo.DestaquesDaSemana", new[] { "IdDoCliente", "SiglaDoIdioma" }, "dbo.PaginasIniciais");
            DropForeignKey("dbo.Semelhantes", "IdDoConteudo", "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.Resenhas", "PaginaDeDetalhesDoConteudo_IdDoConteudo", "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.Previews", "IdDoConteudo", "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.PrecosPorQuantidadeDeUsuarios", "IdDoConteudo", "dbo.Precos");
            DropForeignKey("dbo.Precos", "IdDoConteudo", "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.PaginasDeDetalhesDosConteudos", new[] { "MinhaAvaliacao_IdDoConteudo", "MinhaAvaliacao_IdDoUsuario" }, "dbo.Resenhas");
            DropForeignKey("dbo.Resenhas", "IdDoConteudo", "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.MaisDoFornecedor", "IdDoConteudo", "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.PalavrasChave", new[] { "IdDoConteudo", "SiglaDoIdioma" }, "dbo.Detalhes");
            DropForeignKey("dbo.Detalhes", "IdDoConteudo", "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.ConsolidadoAvaliacoes", "IdDoConteudo", "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.CompetenciasParaSuaEquipe", "IdDoConteudo", "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.CompetenciasParaSuaEmpresa", "IdDoConteudo", "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.CompetenciasParaOCliente", "IdDoConteudo", "dbo.PaginasDeDetalhesDosConteudos");
            DropIndex("dbo.RecomendadoParaSuaEquipeCompetencias", new[] { "IdDoCliente", "SiglaDoIdioma", "IdDoUsuario", "IdDoConteudo" });
            DropIndex("dbo.RecomendadosParaSuaEquipe", new[] { "IdDoCliente", "SiglaDoIdioma" });
            DropIndex("dbo.RecomendadoParaSuaEmpresaCompetencias", new[] { "IdDoCliente", "SiglaDoIdioma", "IdDaEmpresa", "IdDoConteudo" });
            DropIndex("dbo.RecomendadosParaSuaEmpresa", new[] { "IdDoCliente", "SiglaDoIdioma" });
            DropIndex("dbo.RecomendadoParaOClienteCompetencias", new[] { "IdDoCliente", "SiglaDoIdioma", "IdDoConteudo" });
            DropIndex("dbo.RecomendadosParaOCliente", new[] { "IdDoCliente", "SiglaDoIdioma" });
            DropIndex("dbo.MaisPopulares", new[] { "IdDoCliente", "SiglaDoIdioma" });
            DropIndex("dbo.Lancamentos", new[] { "IdDoCliente", "SiglaDoIdioma" });
            DropIndex("dbo.DestaquesDaSemana", new[] { "IdDoCliente", "SiglaDoIdioma" });
            DropIndex("dbo.Semelhantes", new[] { "IdDoConteudo" });
            DropIndex("dbo.Previews", new[] { "IdDoConteudo" });
            DropIndex("dbo.PrecosPorQuantidadeDeUsuarios", new[] { "IdDoConteudo" });
            DropIndex("dbo.Precos", new[] { "IdDoConteudo" });
            DropIndex("dbo.Resenhas", new[] { "PaginaDeDetalhesDoConteudo_IdDoConteudo" });
            DropIndex("dbo.Resenhas", new[] { "IdDoConteudo" });
            DropIndex("dbo.MaisDoFornecedor", new[] { "IdDoConteudo" });
            DropIndex("dbo.PalavrasChave", new[] { "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.Detalhes", new[] { "IdDoConteudo" });
            DropIndex("dbo.ConsolidadoAvaliacoes", new[] { "IdDoConteudo" });
            DropIndex("dbo.CompetenciasParaSuaEquipe", new[] { "IdDoConteudo" });
            DropIndex("dbo.CompetenciasParaSuaEmpresa", new[] { "IdDoConteudo" });
            DropIndex("dbo.CompetenciasParaOCliente", new[] { "IdDoConteudo" });
            DropIndex("dbo.PaginasDeDetalhesDosConteudos", new[] { "MinhaAvaliacao_IdDoConteudo", "MinhaAvaliacao_IdDoUsuario" });
            DropTable("dbo.RecomendadoParaSuaEquipeCompetencias");
            DropTable("dbo.RecomendadosParaSuaEquipe");
            DropTable("dbo.RecomendadoParaSuaEmpresaCompetencias");
            DropTable("dbo.RecomendadosParaSuaEmpresa");
            DropTable("dbo.RecomendadoParaOClienteCompetencias");
            DropTable("dbo.RecomendadosParaOCliente");
            DropTable("dbo.MaisPopulares");
            DropTable("dbo.Lancamentos");
            DropTable("dbo.DestaquesDaSemana");
            DropTable("dbo.PaginasIniciais");
            DropTable("dbo.Semelhantes");
            DropTable("dbo.Previews");
            DropTable("dbo.PrecosPorQuantidadeDeUsuarios");
            DropTable("dbo.Precos");
            DropTable("dbo.Resenhas");
            DropTable("dbo.MaisDoFornecedor");
            DropTable("dbo.PalavrasChave");
            DropTable("dbo.Detalhes");
            DropTable("dbo.ConsolidadoAvaliacoes");
            DropTable("dbo.CompetenciasParaSuaEquipe");
            DropTable("dbo.CompetenciasParaSuaEmpresa");
            DropTable("dbo.CompetenciasParaOCliente");
            DropTable("dbo.PaginasDeDetalhesDosConteudos");
            DropTable("dbo.Licencas");
        }
    }
}
