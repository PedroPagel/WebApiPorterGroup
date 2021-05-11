namespace Contracts.Request
{
    public class CondominioRequest
    {
        /// <summary>
        /// E-mail do síndico do condomínio
        /// </summary>
        public string EmailSindico { get; set; }

        /// <summary>
        /// Telefone do síndico do condomínio
        /// </summary>
        public string TelefoneSindico { get; set; }

        /// <summary>
        /// Nome do condominío
        /// </summary>
        public string Nome { get; set; }
    }
}
