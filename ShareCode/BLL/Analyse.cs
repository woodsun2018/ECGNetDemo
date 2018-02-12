using System.Linq;

namespace ShareCode
{
    //算法类
    public class Analyse
    {
        public static byte AnalyseBaseline(byte[] buf)
        {
            byte[] orderBuf = buf.OrderBy(x => x).ToArray();
            byte baseline = orderBuf[orderBuf.Length / 2];

            return baseline;
        }
    }
}
