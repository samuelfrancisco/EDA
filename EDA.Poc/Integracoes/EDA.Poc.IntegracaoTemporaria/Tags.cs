using System;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;

namespace EDA.Poc.IntegracaoTemporaria
{
    public static class Tags
    {
        public static PalavraChave Trainee(Guid idDoConteudo)
        {
            return new PalavraChave
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaPalavraChave = Guid.Parse("7a012988-70aa-4743-b0c8-8a725941e8f4"),
                           Titulo = "trainee"
                       };
        }

        public static PalavraChave Lideranca(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("3dad1b57-e70d-4298-ae23-7386676020d4"),
                Titulo = "liderança"
            };
        }

        public static PalavraChave APP(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("09c0822d-6c0a-477a-b08c-31a69335c181"),
                Titulo = "app"
            };
        }

        public static PalavraChave LancamentoDeProdutos(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("bfb4a204-5ae6-4070-953e-b874a911d606"),
                Titulo = "lançamento de produtos"
            };
        }

        public static PalavraChave Educacao(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("bfb4a204-5ae6-4070-953e-b874a911d606"),
                Titulo = "educação"
            };
        }

        public static PalavraChave Gamificacao(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("1ca718d9-687f-4ab7-9716-c6972bbe6b55"),
                Titulo = "gamificação"
            };
        }

        public static PalavraChave Carreira(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("111c7c0c-4b5d-4682-a058-7fd749bc30cd"),
                Titulo = "carreira"
            };
        }

        public static PalavraChave AncorasDeCarreira(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("e0e6f6e7-f2c8-4ed9-9091-93d1b4660816"),
                Titulo = "âncoras de carreira"
            };
        }

        public static PalavraChave Inteligencia(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("357d03f0-ab46-459b-aa47-08d92bb56564"),
                Titulo = "inteligência"
            };
        }

        public static PalavraChave Valores(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("0b7391e7-9018-4d1c-a546-65deff7ac575"),
                Titulo = "valores"
            };
        }

        public static PalavraChave Autoconhecimento(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("cdc77cb5-d471-4414-9fa9-5442b5e2cd89"),
                Titulo = "autoconhecimento"
            };
        }

        public static PalavraChave Estagio(Guid idDoConteudo)
        {
            return new PalavraChave
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaPalavraChave = Guid.Parse("35ed0531-9874-47bb-accf-51ed7c0c91dc"),
                           Titulo = "estágio"
                       };
        }

        public static PalavraChave Competencias(Guid idDoConteudo)
        {
            return new PalavraChave
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaPalavraChave = Guid.Parse("bc9c5ee4-6abf-444f-9b12-036fd9517ff9"),
                           Titulo = "competências"
                       };
        }

        public static PalavraChave Game(Guid idDoConteudo)
        {
            return new PalavraChave
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaPalavraChave = Guid.Parse("3a155c38-9218-442d-bd1f-816bf0eefb30"),
                           Titulo = "game"
                       };
        }

        public static PalavraChave Atendimento(Guid idDoConteudo)
        {
            return new PalavraChave
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaPalavraChave = Guid.Parse("d38d508b-ec69-44b5-ad79-abfd0c82bf1f"),
                           Titulo = "atendimento"
                       };
        }

        public static PalavraChave Assessment(Guid idDoConteudo)
        {
            return new PalavraChave
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaPalavraChave = Guid.Parse("eb0f1bf2-8830-435e-ad8f-cc90b038995d"),
                           Titulo = "assessment"
                       };
        }

        public static PalavraChave Flexibilidade(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("9bcce985-29cb-48cd-94f8-1be8d308549c"),
                Titulo = "flexibilidade"
            };
        }

        public static PalavraChave FocoNoCliente(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("f23c0e4c-42f9-4f34-87e3-8efe4c24eb80"),
                Titulo = "foco no cliente"
            };
        }

        public static PalavraChave Varejo(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("da3662dd-04a6-426a-ba91-eb6de3c32fc1"),
                Titulo = "varejo"
            };
        }

        public static PalavraChave Venda(Guid idDoConteudo)
        {
            return new PalavraChave
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaPalavraChave = Guid.Parse("0f3c1682-58a2-480c-a5fb-fc879503e37e"),
                           Titulo = "venda"
                       };
        }

        public static PalavraChave Crenca(Guid idDoConteudo)
        {
            return new PalavraChave
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaPalavraChave = Guid.Parse("d36060df-4233-45c9-b0ef-68b850879b12"),
                           Titulo = "crença"
                       };
        }

        public static PalavraChave Teste(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("0da517e0-5ef0-4abb-a817-3a4460992365"),
                Titulo = "teste"
            };
        }

        public static PalavraChave Aprendizagem(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("8255e872-59bd-4fe1-8fd8-2c35b6c6578b"),
                Titulo = "aprendizagem"
            };
        }

        public static PalavraChave Habilidades(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("8255e872-59bd-4fe1-8fd8-2c35b6c6578b"),
                Titulo = "habilidades"
            };
        }

        public static PalavraChave Comportamento(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("99a7c88b-aaba-47cd-808e-4abb865db4a1"),
                Titulo = "comportamento"
            };
        }

        public static PalavraChave Pipeline(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("ca95b0b1-0c75-4253-8bc2-1403b7137af7"),
                Titulo = "pipeline"
            };
        }

        public static PalavraChave Prontidao(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("4d0927e8-0a1a-443f-9671-5a2924f894b7"),
                Titulo = "prontidão"
            };
        }

        public static PalavraChave Negociacao(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("485d1153-4022-4294-8507-3b4d856845c3"),
                Titulo = "negociacao"
            };
        }

        public static PalavraChave Perfil(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("b898e5f9-0809-4da1-acff-0b73dedfbfba"),
                Titulo = "perfil"
            };
        }

        public static PalavraChave AlbertoCouto(Guid idDoConteudo)
        {
            return new PalavraChave
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaPalavraChave = Guid.Parse("278e8c7d-3557-4633-9d7a-7a4fdec7f425"),
                           Titulo = "Alberto Couto"
                       };
        }

        public static PalavraChave Desempenho(Guid idDoConteudo)
        {
            return new PalavraChave
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaPalavraChave = Guid.Parse("2a8d638f-5390-4aca-8c11-fdbe5947cb29"),
                           Titulo = "desempenho"
                       };
        }

        public static PalavraChave Objecao(Guid idDoConteudo)
        {
            return new PalavraChave
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaPalavraChave = Guid.Parse("7ce0067d-b7d3-4637-b5ee-6ddf079ddb84"),
                           Titulo = "objeção"
                       };
        }

        public static PalavraChave Cliente(Guid idDoConteudo)
        {
            return new PalavraChave
                       {
                           IdDoConteudo = idDoConteudo,
                           SiglaDoIdioma = Constantes.SiglaDoIdioma,
                           IdDaPalavraChave = Guid.Parse("1cc4911c-4b8f-407e-bc19-7722225d2ab8"),
                           Titulo = "cliente"
                       };
        }

        public static PalavraChave CallCenter(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("0edd12e0-ce2a-46a2-a3f2-def875a10124"),
                Titulo = "call center"
            };
        }

        public static PalavraChave Motivadores(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("84c53062-abf8-42d6-a5f6-86696e30c8e4"),
                Titulo = "motivadores"
            };
        }

        public static PalavraChave Motivacao(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("10bb2efc-6a5e-4bcb-9ec3-2fe636d93c44"),
                Titulo = "motivação"
            };
        }

        public static PalavraChave Engajamento(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("75b08263-958c-410a-b1ab-826accb16068"),
                Titulo = "engajamento"
            };
        }

        public static PalavraChave GestaoDeEquipes(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("214ba9ac-e085-41e8-818a-7b54c6054d3d"),
                Titulo = "gestão de equipes"
            };
        }

        public static PalavraChave Produtividade(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("fcfead99-101e-4a6d-a15a-6405b9f806a2"),
                Titulo = "produtividade"
            };
        }

        public static PalavraChave LiderancaSituacional(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("fcfead99-101e-4a6d-a15a-6405b9f806a2"),
                Titulo = "liderança situacional"
            };
        }

        public static PalavraChave Consultiva(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("c931b90f-0c37-4bab-a3c9-9bd7b7ebe3e6"),
                Titulo = "consultiva"
            };
        }

        public static PalavraChave Trilha(Guid idDoConteudo)
        {
            return new PalavraChave
            {
                IdDoConteudo = idDoConteudo,
                SiglaDoIdioma = Constantes.SiglaDoIdioma,
                IdDaPalavraChave = Guid.Parse("bf66f8ff-a35b-44b4-8f8d-b40a7dc19543"),
                Titulo = "trilha"
            };
        }
    }
}
