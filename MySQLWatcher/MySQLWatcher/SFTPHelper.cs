using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamir.SharpSsh.jsch;
using Tamir.Streams;
using System.Collections;

namespace MySQLWatcher
{
    /// <summary>
    /// SFTP工具类
    /// author: Jack R. Ge(jack_r_ge@126.com)
    /// </summary>
    public class SFTPHelper
    {
        private Session m_session;
        private Channel m_channel;
        private ChannelSftp m_sftp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host">sftp地址</param>
        /// <param name="user">用户名</param>
        /// <param name="pwd">密码</param>

        public SFTPHelper(string host, string user, string pwd)
        {
            string[] arr = host.Split(':');
            string ip = arr[0];
            int port = 22;
            if (arr.Length > 1) port = Int32.Parse(arr[1]);

            JSch jsch = new JSch();
            m_session = jsch.getSession(user, ip, port);
            MyUserInfo ui = new MyUserInfo();
            ui.setPassword(pwd);
            m_session.setUserInfo(ui);

        }

        /// <summary>
        /// SFTP连接状态
        /// </summary>
        public bool Connected { get { return m_session.isConnected(); } }

        /// <summary>
        /// 连接SFTP
        /// </summary>        
        public bool Connect()
        {
            try
            {
                if (!Connected)
                {
                    m_session.connect();
                    m_channel = m_session.openChannel("sftp");
                    m_channel.connect();
                    m_sftp = (ChannelSftp)m_channel;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 断开SFTP    
        /// </summary>
        public void Disconnect()
        {
            if (Connected)
            {
                m_channel.disconnect();
                m_session.disconnect();
            }
        }

        /// <summary>
        /// SFTP存放文件
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="remotePath"></param>
        /// <returns></returns>

        public bool Put(string localPath, string remotePath)
        {
            try
            {
                Tamir.SharpSsh.java.String src = new Tamir.SharpSsh.java.String(localPath);
                Tamir.SharpSsh.java.String dst = new Tamir.SharpSsh.java.String(remotePath);
                m_sftp.put(src, dst);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// SFTP获取文件
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="localPath"></param>
        /// <returns></returns>

        public bool Get(string remotePath, string localPath)
        {
            try
            {
                Tamir.SharpSsh.java.String src = new Tamir.SharpSsh.java.String(remotePath);
                Tamir.SharpSsh.java.String dst = new Tamir.SharpSsh.java.String(localPath);
                m_sftp.get(src, dst);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除SFTP文件
        /// </summary>
        /// <param name="remoteFile"></param>
        /// <returns></returns>
        public bool Delete(string remoteFile)
        {
            try
            {
                m_sftp.rm(remoteFile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取SFTP文件列表
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public ArrayList GetFileList(string remotePath, string fileType)
        {
            try
            {
                Tamir.SharpSsh.java.util.Vector vvv = m_sftp.ls(remotePath);
                ArrayList objList = new ArrayList();
                foreach (Tamir.SharpSsh.jsch.ChannelSftp.LsEntry qqq in vvv)
                {
                    string sss = qqq.getFilename();
                    if (sss.Length > (fileType.Length + 1) && fileType == sss.Substring(sss.Length - fileType.Length))
                    { objList.Add(sss); }
                    else { continue; }
                }

                return objList;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 登录验证信息
        /// </summary>
        public class MyUserInfo : UserInfo
        {
            String passwd;
            public String getPassword() { return passwd; }
            public void setPassword(String passwd) { this.passwd = passwd; }

            public String getPassphrase() { return null; }
            public bool promptPassphrase(String message) { return true; }

            public bool promptPassword(String message) { return true; }
            public bool promptYesNo(String message) { return true; }
            public void showMessage(String message) { }
        }
    }
}
