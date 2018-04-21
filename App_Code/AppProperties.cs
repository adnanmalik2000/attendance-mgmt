using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Security.Cryptography;
using Microsoft.Win32;
using com.pakhee.common;
using System.Text;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for AppProperties
/// </summary>
public static class AppProperties
{

    private static string _dbConnectionString;

    public static string DateFormatString = "yyyyMMddHHmmss";

    public static string ErrorTypeMobile = "mobile";
    public static string ErrorTypeWeb = "web";


    /// <summary>
    /// connection string to the database; only retrieve it the first time
    /// </summary>
    public static string dbConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_dbConnectionString))
            {

                _dbConnectionString = ConfigurationManager.AppSettings["dbConnString"];
               
            }
            return _dbConnectionString;
        }
    }

    private static int _MaxTryCount;
    /// <summary>
    /// max number of times to try reading the SQL database before giving up
    /// </summary>
    internal static int MaxTryCount
    {
        get
        {
            if (_MaxTryCount <= 0)
            {
                //hasn't been loaded yet, so load it  
                string maxTryCount = ConfigurationManager.AppSettings["MaxTryCount"];
                int intTest = 0;
                bool success = int.TryParse(maxTryCount, out intTest);
                //if it's <= 0, set it to 1.
                if (!success || intTest <= 0)
                    _MaxTryCount = 1;
                else
                    _MaxTryCount = intTest;
              
            }
            return _MaxTryCount;
        }
    }

    private static List<int> _retrySleepTime;
    /// <summary>
    /// amount of time to wait between retries when reading the SQL database
    /// This loads a list, which is then referenced in code. 
    /// This means my intervals are the same, just multiplied by the index.
    /// First retry waits 0 seconds, second waits 2.5, third waits 5, and last is irrelevant.
    /// (It stops if it retries 4 times.)
    /// </summary>
    internal static List<int> retrySleepTime
    {
        get
        {
            if (_retrySleepTime == null || _retrySleepTime.Count <= 0)
            {
                //hasn't been loaded yet, so load it  
                string interval = ConfigurationManager.AppSettings["RetrySleepInterval"];
                int intTest = 0;
                int intInterval = 0;
                bool success = int.TryParse(interval, out intTest);
                if (intTest <= 0)
                    intInterval = 2500; //2.5 seconds
                else
                    intInterval = intTest;
              

                //put these in an array so they are completely dynamic rather than having
                //  variables for each one. You can change the interval and number of times
                //  to retry simply by changing the configuration settings.
                _retrySleepTime = new List<int>();
                //set the sleep times 0, 5, 10, etc.
                intTest = 0;
                _retrySleepTime.Add(0);
                for (int i = 1; i < MaxTryCount; i++)
                {
                    intTest += intInterval;
                    _retrySleepTime.Add(intTest);
                }

                for (int i = 0; i < MaxTryCount; i++)
                {
                   
                }
            }
            return _retrySleepTime;
        }
    }
    public static string GetFileLocation()
    {
        return ConfigurationManager.AppSettings["FileLocation"];
    }

    public static  Guid GuidToString(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] hash = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(input));
            Guid result = new Guid(hash);
            return result;
        }

    }

    public static Version GetIisVersion()
    {
        using (RegistryKey componentsKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\InetStp", false))
        {
            if (componentsKey != null)
            {
                int majorVersion = (int)componentsKey.GetValue("MajorVersion", -1);
                int minorVersion = (int)componentsKey.GetValue("MinorVersion", -1);

                if (majorVersion != -1 && minorVersion != -1)
                {
                    return new Version(majorVersion, minorVersion);
                }
            }

            return new Version(0, 0);
        }
    }

    internal static string getCkey()
    {

          return  CryptLib.getHashSha256(ConfigurationManager.AppSettings["CKey"], 32);
   

    }

    internal static string getIVkey()
    {
      
        return CryptLib.getHashSha256(ConfigurationManager.AppSettings["IVKey"], 16);

    }


    public static void WrtieLog( string path, string strContent)
    {

        using (StreamWriter _testData = new StreamWriter(path, true))
        {
            _testData.WriteLine(strContent); // Write the file.
        }
    }



   public static  string GetRandomString(int length, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
    {
        if (length < 0) throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
        if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentException("allowedChars may not be empty.");

        const int byteSize = 0x100;
        var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
        if (byteSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));

        // Guid.NewGuid and System.Random are not particularly random. By using a
        // cryptographically-secure random number generator, the caller is always
        // protected, regardless of use.
        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            var result = new StringBuilder();
            var buf = new byte[128];
            while (result.Length < length)
            {
                rng.GetBytes(buf);
                for (var i = 0; i < buf.Length && result.Length < length; ++i)
                {
                    // Divide the byte into allowedCharSet-sized groups. If the
                    // random value falls into the last group and the last group is
                    // too small to choose from the entire allowedCharSet, ignore
                    // the value in order to avoid biasing the result.
                    var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                    if (outOfRangeStart <= buf[i]) continue;
                    result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                }
            }
            return result.ToString();
        }
    }



   public static string GetIPAddress()
   {
       System.Web.HttpContext context = System.Web.HttpContext.Current;
       string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

       if (!string.IsNullOrEmpty(ipAddress))
       {
           string[] addresses = ipAddress.Split(',');
           if (addresses.Length != 0)
           {
               return addresses[0];
           }
       }

       return context.Request.ServerVariables["REMOTE_ADDR"];
   }



}