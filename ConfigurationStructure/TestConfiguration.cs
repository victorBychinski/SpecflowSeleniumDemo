namespace ConfigurationStructure
{
    public class TestConfiguration
    {
        public string BaseUrl { get; set; }
        public BrowserSettings BrowserSettings { get; set; }
    }
    public class BrowserSettings
    {
        public BrowserType BrowserType { get; set; }
        public long TimeOut { get; set; }
        public bool IsRemote { get; set; }
    }
}