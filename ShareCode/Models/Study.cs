using System;
using System.Collections.Generic;

namespace ShareCode
{
    //一次检查记录
    public class Study
    {
        //记录号，数据库创建时自动赋值，自动递增
        public int ID { get; set; }

        //检查记录的唯一编号，为了简单起见，用GUID
        public Guid SampleID { get; set; }

        //检查采样时间
        public DateTime SampleTime { get; set; }

        //采样的设备编号
        public Int32 DeviceID { get; set; }

        //检测的数据序号
        public Int32 FrameNo { get; set; }

        //检测的数据
        public byte[] BioBuf { get; set; }
        public const int BioBufLen = 500;

        //患者姓名
        public string PatientName { get; set; }
        public const int PersonNameMaxLen = 10;

        //诊断结果
        public string Diagnose { get; set; }
        public const int DiagnoseMaxLen = 10;

        //诊断医生
        public string DoctorName { get; set; }

        //修改时间
        public DateTime ModifyTime { get; set; }

        public Study()
        {
            ID = 0;
            SampleID = Guid.NewGuid();
            SampleTime = DateTime.Now;
            DeviceID = 0;
            FrameNo = 0;
            BioBuf = new byte[0];
            PatientName = "";
            Diagnose = "";
            DoctorName = "";
            ModifyTime = DateTime.Now;
        }
    }

    //全局检查记录列表，用于传参
    public class GlobalStudyList
    {
        //private List<Study> _StudyList = null;
        private readonly List<Study> _StudyList = new List<Study>();
        public List<Study> StudyList
        {
            get { return _StudyList; }
            set
            {
                //_StudyList = value;
                //保持引用地址不变
                _StudyList.Clear();
                _StudyList.AddRange(value);

                SelectedIndex = -1;
            }
        }

        public int SelectedIndex { get; set; }

        public Study SelectedStudy
        {
            get
            {
                return (SelectedIndex == -1) ? null : StudyList[SelectedIndex];
            }
        }

        private static GlobalStudyList _instance = new GlobalStudyList();
        public static GlobalStudyList Instance { get { return _instance; } }

        private GlobalStudyList()
        {
            StudyList = new List<Study>();
        }
    }

}
