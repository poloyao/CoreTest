using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebCore.Helper
{
    /// <summary>
    /// 中文
    /// </summary>
    public class ZHHelper
    {
        /// <summary>
        /// 计算中文+数字/英文的字段长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int CheckZhLength(string str)
        {
            ArrayList itemList = new ArrayList();
            CharEnumerator CEnumerator = str.GetEnumerator();
            Regex regex = new Regex("^[\u4E00-\u9FA5]{0,}$");
            int lenght = 0;
            while (CEnumerator.MoveNext())
            {
                if (regex.IsMatch(CEnumerator.Current.ToString(), 0))
                {
                    itemList.Add(CEnumerator.Current.ToString());
                    lenght += 3;
                }
                else
                {
                    lenght += 1;
                }
            }

            return lenght;
        }

        /// <summary>
        /// 填充到指定长度
        /// </summary>
        /// <param name="mess"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public static string ZH_Fill(string mess, int lenght, string fill = " ")
        {
            var oldLenght = CheckZhLength(mess);
            if (oldLenght <= lenght)
            {
                var AddLenght = lenght - oldLenght;
                if (AddLenght > 0)
                {
                    for (int i = 0; i < AddLenght; i++)
                    {
                        mess += fill;
                    }
                }
                else
                {
                    Console.WriteLine("长度负值");
                }
            }
            return mess;
        }
    }
}