using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace EncodingAndTransfer
{
    class Transfer
    {
        /// <summary>
        /// 字符串编码转换
        /// </summary>
        /// <param name="srcEncoding">原编码</param>
        /// <param name="dstEncoding">目标编码</param>
        /// <param name="srcBytes">原字符串</param>
        /// <returns>字符串</returns>
        public static string TransferEncoding(Encoding srcEncoding, Encoding dstEncoding, string srcStr)
        {
            byte[] srcBytes = srcEncoding.GetBytes(srcStr);
            byte[] bytes = Encoding.Convert(srcEncoding, dstEncoding, srcBytes);
            return dstEncoding.GetString(bytes);
        }

        /// <summary>
        /// html转码 ,转码后如： &lt;head&gt;you &amp; me&lt;/head&gt;
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlEncode(string html)
        {
            return System.Net.WebUtility.HtmlEncode(html);//System.Net.WebUtility.HtmlEncode(html);            
        }

        /// <summary>
        /// html解码，解码后如：<head>you & me</head>
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlDecode(string html)
        {
            return System.Net.WebUtility.HtmlDecode(html);//System.Net.WebUtility.HtmlEncode(html);            
        }

        /// <summary>
        /// Url转码，转码后如：http%3a%2f%2fwww.baidu.com%3fusername%3d%3cfind%3e%26content%3dab+c
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(string url)
        {
            return HttpUtility.UrlEncode(url);  //System.Web.HttpUtility
        }

        /// <summary>
        /// Url解码 ，解码后如：结果：http://www.baidu.com?username=<find>&content=ab c
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);  //System.Web.HttpUtility
        }

        /// <summary>
        /// Base64转码，简单加密一些数据，比如url地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToBase64(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64字符串解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FromBase64(string input)
        {
            byte[] bytes = Convert.FromBase64String(input);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 字节数组转为字符串
        /// 将指定的字节数组的每个元素的数值转换为它的等效十六进制字符串表示形式。
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BitToString(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }
            //将指定的字节数组的每个元素的数值转换为它的等效十六进制字符串表示形式。
            return BitConverter.ToString(bytes);
        }

        /// <summary>
        /// 将十六进制字符串转为字节数组
        /// </summary>
        /// <param name="bitStr"></param>
        /// <returns></returns>
        public static byte[] FromBitString(string bitStr)
        {
            if (bitStr == null)
            {
                return null;
            }

            string[] sInput = bitStr.Split("-".ToCharArray());
            byte[] data = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }

            return data;
        }

        /// <summary>
        /// 将二进制转换为图片
        /// </summary>
        /// <param name="byteData"></param>
        public static void SaveImageFromBytes(byte[] byteData)
        {
            MemoryStream imageStream = new MemoryStream(byteData, 0, byteData.Length);
            System.Drawing.Image fullImage = System.Drawing.Image.FromStream(imageStream);
            string filePath = "D;\\image.jpg";
            fullImage.Save(filePath);
        }

        /// <summary>
        /// 将图片转换为二进制，根据图片的文件地址
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static byte[] FileImageToBytes(string imagePath)
        {
            //根据图片文件的路径使用文件流打开，并保存为byte[]
            FileStream fs = new FileStream(imagePath, FileMode.Open);//可以是其他重载方法
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return byData;
        }

        /// <summary>
        /// 将图片转换为二进制，根据Image类型
        /// </summary>
        /// <param name="imgPhoto"></param>
        /// <returns></returns>
        public static byte[] DrawImageToBytes(System.Drawing.Image imgPhoto)
        {
            //将Image转换成流数据，并保存为byte[]
            MemoryStream mstream = new MemoryStream();
            imgPhoto.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] byData = new Byte[mstream.Length];
            mstream.Position = 0;
            mstream.Read(byData, 0, byData.Length);
            mstream.Close();
            return byData;
        }
    }
}
