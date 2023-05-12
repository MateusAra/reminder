namespace utils.exceptions
{
    public class ReminderException : Exception
    {
        public ReminderException() { }

        public ReminderException(string? message) : base(message) { }

        public ReminderException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}