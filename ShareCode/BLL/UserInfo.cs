namespace ShareCode
{
    //用户信息
    public class UserInfo
    {
        public enum RoleType
        {
            Patient = 100,
            Doctor = 200,
        }

        //身份
        public RoleType Role { get; set; }

        //帐号
        public string Account { get; set; }

        public UserInfo()
        {
            //初始化为患者
            Role = RoleType.Patient;
            Account = "";
        }

    }

}