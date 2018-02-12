using System.IO;
using System.Linq;

namespace ECGDevice
{
    //采集心电图
    public class SampleECG
    {
        private byte[] ECGDataBuf;
        private int ECGDataBufReadIndex = 0;
        public const int ECGSampleLen = 500;

        private static SampleECG _instance = new SampleECG();
        public static SampleECG Instance { get { return _instance; } }

        private SampleECG()
        {
            //初始化心电图演示数据
            string filename = "ECGDemo.dat";
            ECGDataBuf = File.ReadAllBytes(filename);
            ECGDataBufReadIndex = 0;
        }

        //采集心电图
        public byte[] SampleECGData()
        {
            if ((ECGDataBufReadIndex + ECGSampleLen) >= ECGDataBuf.Length)
                ECGDataBufReadIndex = 0;

            byte[] sampleBuf = ECGDataBuf.Skip(ECGDataBufReadIndex).Take(ECGSampleLen).ToArray();

            ECGDataBufReadIndex += ECGSampleLen;

            return sampleBuf;
        }

    }
}
