namespace Com.Domain.Constants;

public static class Constants
{
    public static class Settings
    {
        public const string HourPriceSettingName = "itemPrices";
        public const string DefaultGLAccountsSettingName = "defaultGLAccounts";
        public const string PaymentConditionSettingName = "paymentCondition";
        public const string PackageSettings = "packageSettings";
    }

    public static class StringResults
    {
        public const string OK = "OK";
        public const string NOK = "NOK";
        public const string OpSuccess = "OpSuccess";
        public static string OkOpSuccess = $"{OK}|{OpSuccess}";
    }


    public static class Entry
    {
        public const int HEADERLINE = 0;
        public const string ITEM = "ITEM";
    }

    public static class VariationTypes
    {
        public const string COLOR = "COLOR";
        public const string WEIGHT = "WEIGHT";
        public const string LENGTH = "LENGTH";
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
