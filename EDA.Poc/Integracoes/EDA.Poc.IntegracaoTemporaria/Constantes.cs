using EDA.Poc.Pagamentos.ReadModel.Usuarios;
using System;

namespace EDA.Poc.IntegracaoTemporaria
{
    public static class Constantes
    {
        public static readonly Guid IdDoCliente = Guid.Parse("76f91d5c-f602-4823-9086-c8d7678e9557");
        public static readonly Guid IdDaEmpresa = Guid.Parse("01a2a426-5c95-4c0e-9e22-eb353fc7fa8a");
        public static readonly Guid IdDoUsuario = Guid.Parse("a59e8e09-6669-4412-aa7d-9a3b2e82adb1");
        public static readonly Guid IdDaEscola = Guid.Parse("ac1a6b9e-5d91-4e94-8672-226835694dde");
        public static readonly Guid IdDaPromocao = Guid.Parse("eadfd677-00b2-45a5-93d2-4970c6d1b9c3");
        public const string SiglaDoIdioma = "pt-BR";
        public static readonly Usuario DiegoMarques = new Usuario { Id = Guid.Parse("92996551-dd4c-4a62-9dd2-50125a3bfe05"), Nome = "Diego Marques" };
        public static readonly Usuario LeonardoOliveira = new Usuario { Id = Guid.Parse("cd97aa2d-d7c8-4def-8bea-c2d3d1ad8240"), Nome = "Leonardo Oliveira" };
        public static readonly Usuario RafaelDiGenova = new Usuario { Id = Guid.Parse("e8ae5559-9016-4966-98b5-7880d2a04cb1"), Nome = "Rafael Di Genova" };
        public static readonly Usuario SamuelFrancisco = new Usuario { Id = Guid.Parse("366ed639-2e49-44c6-8a9f-30aa5ef0165c"), Nome = "Samuel Francisco" };
        public static readonly Usuario FlavioPortugal = new Usuario { Id = Guid.Parse("81cda2c4-c779-4ea3-9097-3d974ef79cab"), Nome = "Flavio Portugal" };
        public static readonly Usuario FelipeAzevedo = new Usuario { Id = Guid.Parse("a59e8e09-6669-4412-aa7d-9a3b2e82adb1"), Nome = "Felipe Azevedo" };
    }
}
