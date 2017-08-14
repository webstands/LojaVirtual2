using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Quiron.LojaVirtual.Dominio.Entidades
{
    public class Pedido
    {
        [Required(ErrorMessage = "Informe seu nome")]
        public string NomeCliente { get; set; }

        [Display(Name = "Cep:")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Informe seu Endereço")]
        [Display(Name = "Endereço:")]
        public string Endereco { get; set; }

        [Display(Name = "Complemento:")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Informe sua Cidade")]
        [Display(Name = "Cidade:")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe seu Bairro")]
        [Display(Name = "Bairro:")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe seu Email")]
        [Display(Name = "Email:")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }


        public bool EmbrulhaPresente { get; set; }
    }
}
