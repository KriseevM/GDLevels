namespace GDLevels.Misc
{
    public class StatusMessage
    {
        private readonly string _message;
        public string Message => _message;
        private readonly bool _isSuccessful = false;
        public bool IsSuccessful => _isSuccessful;

        public StatusMessage(string message, bool isSuccessful)
        {
            _isSuccessful = isSuccessful;
            _message = message;
        }
    }
}