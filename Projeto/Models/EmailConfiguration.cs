namespace Projeto.Models
{
    public class EmailConfiguration
    {
        public string servidor { get; set; }
        public int porta { get; set; }
        public bool usa_ssl { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
        public string link_troca_senha { get; set; }
    }
}
