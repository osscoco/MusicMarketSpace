namespace back.Services
{
    public class TranslateException
    {
        public string TranslateExceptionEnToFr(Exception ex)
        {
            switch (ex.Message)
            {
                case "An error occurred while saving the entity changes. See the inner exception for details.":
                    return "Une erreur s'est produite lors de l'enregistrement des modifications apportées à l'entité. Consultez l'exception interne pour plus de détails.";
                default:
                    return $"Erreur inconnue : {ex.Message}";
            }
        }
    }
}