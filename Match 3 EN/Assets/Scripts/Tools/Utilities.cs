using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Reflection;
using System.Text.RegularExpressions;
using Core;
using UnityEngine;

public enum TimeFormat
{
    HHMMSS,
    HHMM,
    MMSS,
    SS,
    MM,
    HH,
}

public class Utilities
{
    private static int number = 0;
    public static object ObjectLock = new object();
    private static System.Random random = new System.Random();

    /// <summary>
    /// 截取字符串(PID,开始下标,截取长度)t
    /// </summary>
    public static int GetSubstring(string str, int index, int length)
    {
        int var = 0;
        if (str != null && index < str.Length && (index + length) <= str.Length)
            var = int.Parse(str.Substring(index, length));
        return var;
    }

    public static List<string> Params2List(string param)
    {
        List<string> listData = new List<string>();
        if (param == null || param == string.Empty)
            return listData;
        string str = param.Trim();
        string[] arr = str.Split(';');
        for (int i = 0; i < arr.Length; i++)
        {
            listData.Add(arr [i]);

        }
        return listData;
    }

    /// <returns>The array.</returns>
    /// <param name="param">Parameter.</param>
    //return array from "xxx=xxx;xxx=xxx"
    public static string[] Params2Array(String param)
    {
        string[] arr = new string[] { };

        if (param == null || param == string.Empty)
            return arr;

        string str = param.Trim();
        if (str.Length < 3)
            return arr;
        if (str.Substring(str.Length - 1, 1) == ";")
        {
            str = str.Substring(0, str.Length - 1);
        }

        arr = str.Split(new char[] { '=', ';' });
        if (arr.Length % 2 == 1)
        {
            arr = new string[] { };
        }
        return arr;
    }

    /// <summary>
    /// return dictionary from "xxx=xxx;xxx=xxx"
    /// </summary>
    /// <returns>The dictionary.</returns>
    /// <param name="param">Parameter.</param>
    /// <param name="defaultValue">Default value.</param>
    public static Dictionary<string, string> Params2Dictionary(string param, string defaultValue)
    {
        Dictionary<string, string> dicData = new Dictionary<string, string>();
        if (param == null || param == string.Empty)
            return dicData;
        string str = param.Trim();
        if (str.Substring(str.Length - 1) == ";")
            str = str.Substring(0, str.Length - 1);
        string[] arr = str.Split(';');
        for (int i = 0; i < arr.Length; i++)
        {
            string[] arrKvp = arr [i].Split('=');
            if (arrKvp.Length != 2)
            {
                if (arrKvp.Length >= 1)
                {
                    string key = arrKvp [0].Trim();
                    dicData.Add(key, defaultValue);
                }
            } else
            {
                dicData.Add(arrKvp [0], arrKvp [1]);
            }
        }
        return dicData;
    }

    //Params2List | Params2Dictionary | ConvertType _AddedByZjl
    public static List<T> Params2List<T>(string param)
    {
        List<T> listData = new List<T>();
        if (string.IsNullOrEmpty(param))
            return listData;
        param = param.Replace(" ", "");
        if (param.Substring(param.Length - 1) == ";")
            param = param.Substring(0, param.Length - 1);
        string[] arr = param.Split(';');
        T t;
        for (int i = 0; i < arr.Length; i++)
        {
            t = ConvertType<T>(arr [i]);
            listData.Add(t);
        }
        return listData;
    }

    public static Dictionary<K, V> Params2Dictionary<K, V>(string param)
    {
        Dictionary<K, V> dicData = new Dictionary<K, V>();
        if (string.IsNullOrEmpty(param))
            return dicData;
        param = param.Replace(" ", "");
        if (param.Substring(param.Length - 1) == ";")
            param = param.Substring(0, param.Length - 1);
        string[] values = param.Split(';');
        string[] value;
        for (int i = 0; i < values.Length; i++)
        {
            value = values [i].Split('=');
            if (value.Length != 2)
            {
                GameDebug.Log("Error : param format is not support , " + values [i]);
                continue;
            }
            K k = ConvertType<K>(value [0]);
            V v = ConvertType<V>(value [1]);
            dicData.Add(k, v);
        }
        return dicData;
    }

    private static T ConvertType<T>(string str)
    {
        if (typeof(T) == typeof(int))
        {
            return (T)(object)int.Parse(str);
        } else if (typeof(T) == typeof(float))
        {
            return (T)(object)float.Parse(str);
        }
        return (T)(object)str;
    }

    public static double Radian2Angle(float radian)
    {
        return radian * 180 / Math.PI;
    }

    public static double Angle2Radian(float angle)
    {
        return angle / 180 * Math.PI;
    }

    public static T CreateObjectByName<T>(string className)
    {
        try
        {
            Type t = System.Reflection.Assembly.GetExecutingAssembly().GetType(className);
            if (t == null)
                return default(T);
            return (T)Activator.CreateInstance(t);

        } catch (Exception e)
        {
            return default(T);
        }
    }

    public static T CreateObjectByType<T>(Type type)
    {
        try
        {
            if (type == null)
                return default(T);
            else
                return (T)Activator.CreateInstance(type);
        } catch (Exception e)
        {
            return default(T);
        }
    }

    public static Type CreateTypeByName(string className)
    {
        try
        {
            Type t = System.Reflection.Assembly.GetExecutingAssembly().GetType(className);
            return t;

        } catch (Exception e)
        {
            return null;
        }
    }

    public static int NextNumber()
    {
        lock (ObjectLock)
        {
            return number++;
        }
        ;
    }

    public static int RandomNumber()
    {
        return random.Next();
    }

    public static int RandomNumber(int min, int max)
    {
        lock (random)
        {
            max += 1;
            return random.Next(min, max);
        }
    }

    /// <summary>
    /// 得到本地时间
    /// </summary>
    /// <returns>单位秒</returns>
    public static long getTime()
    {
        DateTime timeStamp = new DateTime(1970, 1, 1);
        long time = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000;
        return time;
    }


    public static DateTime getDateTime(long timestampSec)
    {

        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timestampSec + "0000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }

    public static long getMillTime(DateTime dt)
    {
        DateTime timeStamp = new DateTime(1970, 1, 1);
        long time = (dt.Ticks - timeStamp.Ticks) / 10000;
        return time;
    }

    public static long getMillTime()
    {
        DateTime timeStamp = new DateTime(1970, 1, 1);
        long time = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000;
        return time;
    }

    public static DateTime getDateTimeDataByTime(long time)
    {
        return new DateTime(time * 10000000);
    }

    //单位 毫秒
    public static bool isSameDay(long s1, long s2)
    {
        DateTime dateTime1 = MillisecondsToDateTime(s1);
        DateTime dateTime2 = MillisecondsToDateTime(s2);
        return dateTime1.Year == dateTime2.Year && dateTime1.Month == dateTime2.Month && dateTime1.Day == dateTime2.Day;
    }

    public static DateTime MillisecondsToDateTime(long milliseconds)
    {
        DateTime dt_1970 = new DateTime(1970, 1, 1);

        //// .net开发中计算的都是标准时区的差，但java的解析时间跟时区有关，
        // 而我们的java服务器系统时区不是标准时区，解析时间会差8个小时。
        TimeSpan span = TimeSpan.FromMilliseconds(milliseconds) + TimeSpan.FromHours(8);

        return dt_1970 + span;
    }

    /// <summary>  
    /// 获取当前时间戳  
    /// </summary>  
    /// <param name="bflag">为真时获取10位时间戳,为假时获取13位时间戳.</param>  
    /// <returns></returns>  
    public static string GetTimeStamp(bool bflag = true)
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        string ret = string.Empty;
        if (bflag)
            ret = Convert.ToInt64(ts.TotalSeconds).ToString();
        else
            ret = Convert.ToInt64(ts.TotalMilliseconds).ToString();

        return ret;
    }

    /// <summary>
    /// 返回指定格式的时间//
    /// </summary>
    /// <param name="time"></param>
    /// <param name="format">时间格式</param>
    /// <param name="isAlign">是否对齐，若为true，则固定格式，若为false则按照具体的时间选择格式</param>
    /// <returns></returns>
    public static string getTimeFormat(long time, TimeFormat format = TimeFormat.HHMMSS, bool isAlign = false)
    {
        if (time < 0)
        {
            return string.Empty;
        }

        string str = string.Empty;
        DateTime dateTime = getDateTimeDataByTime(time);
        int day = dateTime.Day - 1;
        int hour = dateTime.Hour;
        int min = dateTime.Minute;
        int sec = dateTime.Second;

        hour += day * 24;
        if (isAlign)
        {
            switch (format)
            {
                case TimeFormat.HHMMSS:
                    str = string.Format("{0:D}:{1:D2}:{2:D2}", hour, min, sec);
                    break;
                case TimeFormat.HHMM:
                    str = string.Format("{0:D}:{1:D2}", hour, min);
                    break;
                case TimeFormat.MMSS:
                    str = string.Format("{0:D}:{1:D2}", hour * 60 + min, sec);
                    break;
                case TimeFormat.SS:
                    str = string.Format("{0:D}", hour * 60 * 60 + min * 60 + sec);
                    break;
                case TimeFormat.MM:
                    str = string.Format("{0:D}", hour * 60 + min);
                    break;
                case TimeFormat.HH:
                    str = string.Format("{0:D}", hour);
                    break;
                default:
                    break;
            }
        } else
        {
            if (hour > 0)
            {
                str = string.Format("{0:D}:{1:D2}:{2:D2}", hour, min, sec);
            } else if (min > 0)
            {
                str = string.Format("{0:D}:{1:D2}", min, sec);
            } else if (sec > 0)
            {
                str = string.Format("{0:D}:{1:D2}", min, sec);
            }
        }


        return str;
    }

    public static string getTimeClockText(long time)
    {
        if (time < 0)
        {
            return string.Empty;
        }

        string str;
        DateTime dateTime = getDateTimeDataByTime(time);
        int day = dateTime.Day - 1;
        int hour = dateTime.Hour;
        int min = dateTime.Minute;
        int sec = dateTime.Second;

        hour += day * 24;

        if (hour > 0)
        {
            str = string.Format("{0:D}:{1:D2}:{2:D2}", hour, min, sec);
        } else if (min > 0)
        {
            str = string.Format("{0:D}:{1:D2}", min, sec);
        } else if (sec > 0)
        {
            str = string.Format("{0:D}", sec);
        } else
        {
            str = string.Empty;
        }

        return str;
    }

    public static string getTimeClockTextHHMMSS(long time)
    {
        if (time < 0)
        {
            return string.Empty;
        }

        string str;
        DateTime dateTime = getDateTimeDataByTime(time);
        int day = dateTime.Day - 1;
        int hour = dateTime.Hour;
        int min = dateTime.Minute;
        int sec = dateTime.Second;

        hour += day * 24;

        if (hour > 0)
        {
            str = string.Format("{0:D}h{1:D2}m", hour, min, sec);
        } else if (min > 0)
        {
            str = string.Format("{0:D}m{1:D2}s", min, sec);
        } else if (sec > 0)
        {
            str = string.Format("{0:D}s", sec);
        } else
        {
            str = string.Empty;
        }

        return str;
    }

    public static string getDateText(long time)
    {
        return getDateTextByMS(time * 1000);
    }

    public static string getDateAndTimeText(long time)
    {
        return getDateAndTimeTextByMS(time * 1000);
    }

    public static string getDateTextByMS(long timeMS)
    {
        DateTime dateTime = getDateTime(timeMS);
        return dateTime.ToString("yyyy-M-d");
    }

    public static string getDateAndTimeTextByMS(long timeMS)
    {
        DateTime dateTime = getDateTime(timeMS);
        return dateTime.ToString("yyyy-M-d HH:mm");
    }

    public static string getTimeTextByMS(long timeMS)
    {
        DateTime dateTime = getDateTime(timeMS);
        return dateTime.ToString("HH:mm:ss");
    }

    public static string GetTimeByHMS(long uiSpan)
    {
        ulong timeUlong = (ulong)uiSpan;
        ulong num = timeUlong % 60uL;
        ulong num2 = (timeUlong - num) / 60uL % 60uL;
        ulong num3 = (timeUlong - num2) / 3600uL;
        return string.Format("{0:D2}:{1:D2}:{2:D2}", num3, num2, num);
    }

    public static string GetTimeByMS(long uiSpan)
    {
        ulong timeUlong = (ulong)uiSpan;
        ulong num = timeUlong % 60uL;
        ulong num2 = (timeUlong - num) / 60uL % 60uL;
        ulong num3 = (timeUlong - num2) / 3600uL;
        return string.Format("{0:D2}:{1:D2}", num2, num);
    }

    public static System.DateTime ConvertIntDateTime(double d)
    {
        System.DateTime time = System.DateTime.MinValue;
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        time = startTime.AddSeconds(d);
        return time;
    }

    public static int ConvertDateTimeInt(System.DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        return (int)(time - startTime).TotalSeconds;
    }

    public static DateTime GetTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }

    public static void AddValueToDict(KeyValuePair<int, int> bounsValue, Dictionary<int, int> valueDict)
    {
        if (valueDict.ContainsKey(bounsValue.Key))
        {
            valueDict [bounsValue.Key] += bounsValue.Value;
        } else
        {
            valueDict.Add(bounsValue.Key, bounsValue.Value);
        }
    }

    /// <summary>
    /// 压缩(7zip).
    /// 传出的byte数组为UTF8格式.
    /// </summary>
    /// <param name="content"></param>
    //public static byte[] Compress(string content)
    //{
    //    using (MemoryStream ms = new MemoryStream())
    //    {
    //        GZipOutputStream gzip = new GZipOutputStream(ms);
    //        byte[] binary = Encoding.UTF8.GetBytes(content);
    //        gzip.Write(binary, 0, binary.Length);
    //        gzip.Close();
    //        byte[] press = ms.ToArray();
    //        return press;
    //    }
    //}

    /// <summary>
    /// 解压缩(7zip).
    /// </summary>
    /// <param name="strGZip"></param>
    /// <returns></returns>
    //public static string UnCompress(byte[] bytes)
    //{
    //    using (MemoryStream re = new MemoryStream())
    //    {
    //        GZipInputStream gzi = new GZipInputStream(new MemoryStream(bytes));
    //        int count = 0;
    //        byte[] data = new byte[4096];
    //        while ((count = gzi.Read(data, 0, data.Length)) != 0)
    //        {
    //            re.Write(data, 0, count);
    //        }
    //        byte[] depress = re.ToArray();
    //        re.Close();
    //        return Encoding.UTF8.GetString(depress);
    //    }
    //}
    //private static
    /// <summary>
    /// md5
    /// </summary>
    /// <returns>The M d5 hash.</returns>
    /// <param name="input">Input.</param>
    public static string GetMD5Hash(String str)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
        string strResult = BitConverter.ToString(result).Replace("-", "").ToLower(System.Globalization.CultureInfo.InvariantCulture);
        return strResult;
    }
    

    

    public static string Md5Sum(string input)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash [i].ToString("X2"));
        }

        //Debug.Log("Invoke MD5 : " + sb.ToString());

        return sb.ToString();
    }

}




