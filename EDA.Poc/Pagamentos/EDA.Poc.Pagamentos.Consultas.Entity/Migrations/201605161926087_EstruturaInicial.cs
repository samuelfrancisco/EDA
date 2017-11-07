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
                        IdDoCliente = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.CompetenciasParaOCliente",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDaCompetencia = c.Guid(nullable: false),
                        Titulo = c.String(),
                        Gaps = c.String(),
                        Medidas = c.String(),
                        Trabalhadas = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma, t.IdDaCompetencia })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.CompetenciasParaSuaEmpresa",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDaCompetencia = c.Guid(nullable: false),
                        IdDaEmpresa = c.Guid(nullable: false),
                        Titulo = c.String(),
                        Gaps = c.String(),
                        Medidas = c.String(),
                        Trabalhadas = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma, t.IdDaCompetencia, t.IdDaEmpresa })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.CompetenciasParaSuaEquipe",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDoUsuario = c.Guid(nullable: false),
                        IdDaCompetencia = c.Guid(nullable: false),
                        Titulo = c.String(),
                        Gaps = c.String(),
                        Medidas = c.String(),
                        Trabalhadas = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma, t.IdDoUsuario, t.IdDaCompetencia })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.ConsolidadoAvaliacoes",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        Avaliacao = c.String(),
                        AvaliacaoFinal = c.String(),
                        QuantidadeAvaliacoes = c.Int(nullable: false),
                        QuantidadeCincoEstrelas = c.Int(nullable: false),
                        QuantidadeQuatroEstrelas = c.Int(nullable: false),
                        QuantidadeTresEstrelas = c.Int(nullable: false),
                        QuantidadeDuasEstrelas = c.Int(nullable: false),
                        QuantidadeUmaEstrela = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma })
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.Detalhes",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
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
                        IdEscola = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma })
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.PalavrasChave",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdPalavraChave = c.Guid(nullable: false),
                        Titulo = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma, t.IdPalavraChave })
                .ForeignKey("dbo.Detalhes", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.MaisDoFornecedor",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
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
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma, t.IdMaidDoFornecedor })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.MinhaAvaliacao",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdDoUsuario = c.Guid(nullable: false),
                        IdDaAvaliacao = c.Guid(nullable: false),
                        ImagemDoUsuario = c.String(),
                        NomeDoUsuario = c.String(),
                        Avaliacao = c.String(),
                        Resenha = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma, t.IdDoUsuario, t.IdDaAvaliacao })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.Precos",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        PrecoIlimitado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecoIlimitadoLabel = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma })
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.PrecosPorQuantidadeDeUsuarios",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdPrecoPorQuantidadeDeUsuarios = c.Guid(nullable: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LicencasDe = c.Int(nullable: false),
                        LicencasAte = c.Int(),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma, t.IdPrecoPorQuantidadeDeUsuarios })
                .ForeignKey("dbo.Precos", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.Previews",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdPreview = c.Guid(nullable: false),
                        Titulo = c.String(),
                        Imagem = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma, t.IdPreview })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.Resenhas",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
                        IdDoConteudo = c.Guid(nullable: false),
                        SiglaDoIdioma = c.String(nullable: false, maxLength: 5),
                        IdResenha = c.Guid(nullable: false),
                        ImagemUsuario = c.String(),
                        NomeUsuario = c.String(),
                        DataResenha = c.DateTime(nullable: false),
                        DataLabel = c.String(),
                        Conteudo = c.String(),
                        Avaliacao = c.String(),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma, t.IdResenha })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
            CreateTable(
                "dbo.Semelhantes",
                c => new
                    {
                        IdDoCliente = c.Guid(nullable: false),
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
                .PrimaryKey(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma, t.IdSemelhante })
                .ForeignKey("dbo.PaginasDeDetalhesDosConteudos", t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma }, cascadeDelete: true)
                .Index(t => new { t.IdDoCliente, t.IdDoConteudo, t.SiglaDoIdioma });
            
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
                        IdPromocao = c.Guid(nullable: false),
                        LarguraMinima = c.String(nullable: false, maxLength: 5),
                        Banner = c.String(),
                        IdEscola = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDoCliente, t.SiglaDoIdioma, t.IdPromocao, t.LarguraMinima })
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
                        IdEscola = c.Guid(nullable: false),
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
                        IdEscola = c.Guid(nullable: false),
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
                        IdEscola = c.Guid(nullable: false),
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
                        IdEscola = c.Guid(nullable: false),
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
                        IdEscola = c.Guid(nullable: false),
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
            DropForeignKey("dbo.Semelhantes", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.Resenhas", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.Previews", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.PrecosPorQuantidadeDeUsuarios", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.Precos");
            DropForeignKey("dbo.Precos", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.MinhaAvaliacao", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.MaisDoFornecedor", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.PalavrasChave", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.Detalhes");
            DropForeignKey("dbo.Detalhes", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.ConsolidadoAvaliacoes", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.CompetenciasParaSuaEquipe", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.CompetenciasParaSuaEmpresa", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.PaginasDeDetalhesDosConteudos");
            DropForeignKey("dbo.CompetenciasParaOCliente", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" }, "dbo.PaginasDeDetalhesDosConteudos");
            DropIndex("dbo.RecomendadoParaSuaEquipeCompetencias", new[] { "IdDoCliente", "SiglaDoIdioma", "IdDoUsuario", "IdDoConteudo" });
            DropIndex("dbo.RecomendadosParaSuaEquipe", new[] { "IdDoCliente", "SiglaDoIdioma" });
            DropIndex("dbo.RecomendadoParaSuaEmpresaCompetencias", new[] { "IdDoCliente", "SiglaDoIdioma", "IdDaEmpresa", "IdDoConteudo" });
            DropIndex("dbo.RecomendadosParaSuaEmpresa", new[] { "IdDoCliente", "SiglaDoIdioma" });
            DropIndex("dbo.RecomendadoParaOClienteCompetencias", new[] { "IdDoCliente", "SiglaDoIdioma", "IdDoConteudo" });
            DropIndex("dbo.RecomendadosParaOCliente", new[] { "IdDoCliente", "SiglaDoIdioma" });
            DropIndex("dbo.MaisPopulares", new[] { "IdDoCliente", "SiglaDoIdioma" });
            DropIndex("dbo.Lancamentos", new[] { "IdDoCliente", "SiglaDoIdioma" });
            DropIndex("dbo.DestaquesDaSemana", new[] { "IdDoCliente", "SiglaDoIdioma" });
            DropIndex("dbo.Semelhantes", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.Resenhas", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.Previews", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.PrecosPorQuantidadeDeUsuarios", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.Precos", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.MinhaAvaliacao", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.MaisDoFornecedor", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.PalavrasChave", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.Detalhes", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.ConsolidadoAvaliacoes", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.CompetenciasParaSuaEquipe", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.CompetenciasParaSuaEmpresa", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
            DropIndex("dbo.CompetenciasParaOCliente", new[] { "IdDoCliente", "IdDoConteudo", "SiglaDoIdioma" });
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
            DropTable("dbo.Resenhas");
            DropTable("dbo.Previews");
            DropTable("dbo.PrecosPorQuantidadeDeUsuarios");
            DropTable("dbo.Precos");
            DropTable("dbo.MinhaAvaliacao");
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
