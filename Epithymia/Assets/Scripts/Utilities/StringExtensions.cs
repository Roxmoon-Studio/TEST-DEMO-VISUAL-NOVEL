namespace Utilities
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string s) => 
            s == string.Empty;
        
        public static bool IsNotEmpty(this string s) => 
            s != string.Empty;
    }
}
