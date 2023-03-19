using System.ComponentModel.DataAnnotations.Schema;

namespace mf_apis_web_services_fuel_manager.Models
{
    [Table("VeiculoUsuarios")]
    public class VeiculoUsuarios
    {
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; } // Prop navegação

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } // Prop navegação
    }
}

/*
  Esse model a tabela de relacionamento entre as classes. Lista todos os usuários relacionados a um veículo
*/
