namespace SmartBoard.Models
{
    public class PessoaClienteViewModel
    {
        public PessoaModel Pessoa { get; set; }
        public TelefoneModel Telefone { get; set; }
        public ClienteModel Cliente { get; set; }

        public PessoaClienteViewModel()
        {
            Pessoa = new PessoaModel();
            Cliente = new ClienteModel();
            Telefone = new TelefoneModel();
        }
    }

    public class PessoaClienteUpdateViewModel
    {
        public PessoaUpdateModel Pessoa { get; set; }
        public TelefoneModel Telefone { get; set; }
        public ClienteModel Cliente { get; set; }

        public PessoaClienteUpdateViewModel()
        {
            Pessoa = new PessoaUpdateModel();
            Cliente = new ClienteModel();
            Telefone = new TelefoneModel();
        }
    }
}
