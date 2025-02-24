namespace Models.Common
{
    public class ResponseApi<T>
    {
        public bool? Success { get; set; }
        public T? Data { get; set; }
        private string? _message;
        public string? Message
        {
            get => _message;
            set
            {
                if (value?.Length > 150)
                    throw new ArgumentException("Le message ne peut pas dépasser 150 caractères.");
                _message = value;
            }
        }

        // Constructeur avec paramètres
        public ResponseApi(bool success, T? data, string? message = null)
        {
            Success = success;
            Data = data;
            Message = message;
        }

        // Constructeur par défaut
        public ResponseApi() { }
    }
}
