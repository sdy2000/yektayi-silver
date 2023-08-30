namespace Core.Convertors
{
    public static class FixedText
    {
        public static string FixedEmail(this string email)
        {
            return email.ToLower().Trim();
        }
    }
}
