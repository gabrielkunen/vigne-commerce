namespace VigneCommerce.Api.Response.Base
{
    public class ResponseContentBase<T> : ResponseBase
    {
        public T Data { get; set; }
        public ResponseContentBase(bool sucesso, string mensagem, T data) : base(sucesso, mensagem)
        {
            Data = data;
        }
    }
}
