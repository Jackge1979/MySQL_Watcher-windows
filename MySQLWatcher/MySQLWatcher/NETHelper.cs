using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace MySQLWatcher
{
    /// <summary>
    /// 网络工具类
    /// author: Jack R. Ge(jack_r_ge@126.com)
    /// </summary>
    public class NETHelper
    {
          /// <summary>
          /// 用于检查IP地址或域名是否可以使用TCP/IP协议访问(使用Ping命令),true表示Ping成功,false表示Ping失败 
          /// </summary>
          /// <param name="strIpOrDName">输入参数,表示IP地址或域名</param>
          /// <returns></returns>
          public static bool PingIpOrDomainName(string strIpOrDName)
          {
              try
              {
                  Ping objPingSender = new Ping();
                  PingOptions objPinOptions = new PingOptions();
                  objPinOptions.DontFragment = true;
                  string data = "";
                  byte[] buffer = Encoding.UTF8.GetBytes(data);
                  int intTimeout = 120;
                  PingReply objPinReply = objPingSender.Send(strIpOrDName, intTimeout, buffer, objPinOptions);
                  string strInfo = objPinReply.Status.ToString();
                  if (strInfo == "Success")
                  {
                      return true;
                  }
                  else
                  {
                      return false;
                  }
              }
              catch (Exception)
              {
                  return false;
              }
          }


    }
}
