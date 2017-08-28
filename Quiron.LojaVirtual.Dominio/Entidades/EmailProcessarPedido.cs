using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quiron.LojaVirtual.Dominio.Entidades
{
    public class EmailPedido
    {
        private object _emailConfiguracoe;
        private readonly EmailConfiguracoes _emailConfiguracoes;

        public EmailPedido(EmailConfiguracoes emailConfiguracoes)
        {
            this._emailConfiguracoes = emailConfiguracoes;
        }

        public void ProcessarPedido(Carrinho carrinho, Pedido pedido)
        {

            using (var smtpCliente = new SmtpClient())
            {
                smtpCliente.EnableSsl = _emailConfiguracoes.UsarSsl;
                smtpCliente.Host = _emailConfiguracoes.ServidorSmtp;
                smtpCliente.Port = _emailConfiguracoes.ServidorPorta;
                smtpCliente.UseDefaultCredentials = false;
                smtpCliente.Credentials = new NetworkCredential(_emailConfiguracoes.Usuario, _emailConfiguracoes.ServidorSmtp);

                if (_emailConfiguracoes.EscreverArquivo)
                {
                    smtpCliente.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpCliente.PickupDirectoryLocation = _emailConfiguracoes.PastaArquivo;
                    smtpCliente.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                .AppendLine("Novo Pedido: ")
                .AppendLine("---------")
                .AppendLine("Itens");

                foreach (var item in carrinho.ItensCarrinho)
                {
                    var subtotal = item.Produto.Preco * item.Quantidade;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c})", item.Quantidade, item.Produto.Nome, subtotal);
                }

                body.AppendFormat("Valor total do pedido: {0:c}", carrinho.ObterValorTotal())
                    .AppendLine("------------------")
                    .AppendLine("Enviar para: ")
                    .AppendLine(pedido.NomeCliente)
                    .AppendLine(pedido.Email)
                    .AppendLine(pedido.Endereco ?? "")
                    .AppendLine(pedido.Cidade)
                    .AppendLine(pedido.Complemento ?? "")
                    .AppendLine("-------------------")
                    .AppendFormat("Para presente?: {0}", pedido.EmbrulhaPresente ? "Sim" : "Não");


                    MailMessage mailMessage = new MailMessage(_emailConfiguracoes.De, _emailConfiguracoes.Para, "Novo Pedido", body.ToString());


                if (_emailConfiguracoes.EscreverArquivo)
                {
                    mailMessage.BodyEncoding = Encoding.GetEncoding("ISO-8859-1"); 
                }

                smtpCliente.Send(mailMessage);
            }
        }

    }
}
