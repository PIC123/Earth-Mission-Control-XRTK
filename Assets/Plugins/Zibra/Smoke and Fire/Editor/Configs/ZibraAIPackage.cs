namespace com.zibraai.smoke_and_fire
{
    internal static class ZibraAIPackage
    {
        public const string ZibraAiSupportEmail = "support@zibra.ai";
        public const string ZibraAiCeoEMail = "hello@zibra.ai";
        public const string ZibraAiWebsiteRootUrl = "https://zibra.ai/";

        public const string PackageName = "com.zibraai.smoke_and_fire";
        public const string DisplayName = "Zibra AI - Smoke And Fire";
        public const string RootMenu = "Zibra AI/" + DisplayName + "/";

        public static readonly string RootPath = "Assets/Plugins/Zibra/Smoke and Fire";

        internal static readonly string WindowTabsPath = $"{RootPath}/Editor/Window/Tabs";

        internal static readonly string UIToolkitPath = $"{RootPath}/Editor/UIToolkit";
        internal static readonly string UIToolkitControlsPath = $"{UIToolkitPath}/Controls";

        public static readonly string EditorArtAssetsPath = $"{RootPath}/Editor/Art";
        public static readonly string EditorIconAssetsPath = $"{EditorArtAssetsPath}/Icons";
        public static readonly string EditorFontAssetsPath = $"{EditorArtAssetsPath}/Fonts";
    }
}
