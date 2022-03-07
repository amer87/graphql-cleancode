namespace Com.Application.Common.Constants;

public static class Constants
{
    public static class StringResults
    {
        public const string OK = "OK";
        public const string NOK = "NOK";
        public const string OpSuccess = "OpSuccess";
        public const string OkOpSuccess = $"{OK}|{OpSuccess}";
        public const string NotAuthenticated = "NOT_AUTHENTICATED";
    }


    public static class Entry
    {
        public const int HEADERLINE = 0;
        public const string ITEM = "ITEM";
    }

    public static class Owners
    {
        public const string ITEM = "ITEM";
        public const string USER = "USER";
        public const string SHOP = "SHOP";
    }

    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Superuser = "Superuser";
        public const string Customer = "Customer";
        public const string Guest = "Guest";
    }

}
