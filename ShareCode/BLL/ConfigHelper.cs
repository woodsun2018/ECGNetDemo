using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace ShareCode
{
    //配置信息
    public class ConfigInfo
    {
        //网站地址
        public string WebServerUrl { get; set; } = "http://193.112.19.82:9000";//腾讯云

        //当前用户信息
        public UserInfo CurrentUserInfo { get; set; } = new UserInfo();

    }

    //配置管理者
    public class ConfigHelper
    {
        //配置信息
        public ConfigInfo Config { get; private set; } = new ConfigInfo();

        //配置文件名
        public readonly string Filename = "config.txt";
        private string FilePath = "";

        private static ConfigHelper _instance = new ConfigHelper();
        public static ConfigHelper Instance { get { return _instance; } }

        private ConfigHelper()
        {
        }

        //设置配置文件路径
        public void Init(string filePath)
        {
            FilePath = filePath;
        }

        //读取配置信息
        public void ReadConfig()
        {
            if (!File.Exists(FilePath))
                return;

            string content = File.ReadAllText(FilePath, Encoding.UTF8);

            Config = JsonConvert.DeserializeObject<ConfigInfo>(content);
        }

        //保存配置信息
        public void SaveConfig()
        {
            string content = JsonConvert.SerializeObject(Config);

            File.WriteAllText(FilePath, content, Encoding.UTF8);
        }
    }
}
