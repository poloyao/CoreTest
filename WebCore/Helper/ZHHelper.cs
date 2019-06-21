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

        /// <summary>
        /// GB2312转换成UTF8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string gb2312_utf8(string text)
        {
            //声明字符集
            System.Text.Encoding utf8, gb2312;
            //gb2312
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            //utf8
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            byte[] gb;
            gb = gb2312.GetBytes(text);
            gb = System.Text.Encoding.Convert(gb2312, utf8, gb);
            //返回转换后的字符
            return utf8.GetString(gb);
        }

        /// <summary>
        /// UTF8转换成GB2312
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string utf8_gb2312(string text)
        {
            //声明字符集
            System.Text.Encoding utf8, gb2312;
            //utf8
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            //gb2312
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] utf;
            utf = utf8.GetBytes(text);
            utf = System.Text.Encoding.Convert(utf8, gb2312, utf);
            //返回转换后的字符
            return gb2312.GetString(utf);
        }

        public static void ConsoleOut(string mess)
        {
            Console.WriteLine(utf8_gb2312(mess));
        }
    }
}