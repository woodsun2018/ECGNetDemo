namespace ShareCode
{
    //�û���Ϣ
    public class UserInfo
    {
        public enum RoleType
        {
            Patient = 100,
            Doctor = 200,
        }

        //���
        public RoleType Role { get; set; }

        //�ʺ�
        public string Account { get; set; }

        public UserInfo()
        {
            //��ʼ��Ϊ����
            Role = RoleType.Patient;
            Account = "";
        }

    }

}