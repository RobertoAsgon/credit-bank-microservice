namespace Credit.Application.Responses
{
    public class CreditoErrorResponse
    {
        public bool Aprovado { get; set; }
        public List<string> Errors { get; set; }
    }
}
