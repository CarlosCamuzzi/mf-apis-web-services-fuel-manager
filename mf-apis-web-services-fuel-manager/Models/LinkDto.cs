using System.ComponentModel.DataAnnotations.Schema;

namespace mf_apis_web_services_fuel_manager.Models
{
    [NotMapped]
    public class LinkDto
    {
        public int Id { get; set; }
        public string Href { get; set; }    // Link
        public string Rel { get; set; }     // Método relacionado
        public string Metodo { get; set; }  // Método HTTP

        public LinkDto(int id, string href, string rel, string metodo)
        {
            Id = id;
            Href = href;
            Rel = rel;
            Metodo = metodo;
        }

    }
    public class LinksHATEOS
    {
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}

/*LinkDto
 Data transfer Object: Padrão de projeto. é um objeto auexiliar para transferir dados, não interfere no banco de dados, é apenas para trafegar os dados
 */
/*
    public string Href { get; set; } 
        Link Disponível
    public string Rel { get; set; } 
        Método relacionado, ação
    public string Metodo { get; set; } 
        Métdodo HTTP
 */

/*
 Essa classe deve ser associada aos modelos de dados, no caso, os veículos e consumos.
Para isso criaremos uma relação de listas de links
 */

/*
 HATEOS - Padrão utilizado
 */

/*
 É preciso adicionar depois essa class nos veic e consumos
 */