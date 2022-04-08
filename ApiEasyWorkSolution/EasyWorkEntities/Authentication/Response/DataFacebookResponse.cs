namespace EasyWorkEntities.Authentication.Response
{
    public class DataFacebookResponse : GlobalHTTPResponse
    {
        public DataFacebook dataFacebook { get; set; }       
    }

    public class DataFacebook 
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public contentPicture picture { get; set; }
        public class contentPicture
        {
            public contentData data { get; set; }
        }

        public class contentData
        {
            public int height { get; set; }
            public bool is_silhouette { get; set; }
            public string url { get; set; }
            public int width { get; set; }
        }
    }
}
