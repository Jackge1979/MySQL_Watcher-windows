using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MySQLWatcher
{
    /// <summary>
    /// 主界面
    /// author: Jack R. Ge(jack_r_ge@126.com)
    /// </summary>
    public partial class FrmMain : Form
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\mysqladvisor";

        public FrmMain()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!NETHelper.PingIpOrDomainName(tbHostIP.Text))
            {
                MessageBox.Show("数据库主机无法访问！","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            OperateIniFile.WriteIniData("database", "host", tbHostIP.Text, path + @"\dbset.ini");
            OperateIniFile.WriteIniData("database", "port", tbPort.Text, path + @"\dbset.ini");
            OperateIniFile.WriteIniData("database", "user", tbUserName.Text, path + @"\dbset.ini");
            OperateIniFile.WriteIniData("database", "passwd", tbPwd.Text, path + @"\dbset.ini");
            OperateIniFile.WriteIniData("database", "db", tbDbName.Text, path + @"\dbset.ini");
            OperateIniFile.WriteIniData("database", "operuser", tbOperUserName.Text, path + @"\dbset.ini");
            OperateIniFile.WriteIniData("database", "operpwd", tbOperPwd.Text, path + @"\dbset.ini");
            Watcher watcher = new Watcher();
            try
            {
                string reportType = ReportType.HTML;
                string html = Html.HEAD;
                html += watcher.printCaption(reportType);

                string mysql_overview = OperateIniFile.ReadIniData("option", "mysql_overview", "", path + "\\dbset.ini");
                if (mysql_overview == "ON")
                {
                    string interval = OperateIniFile.ReadIniData("option", "interval", "60", path + "\\dbset.ini");
                    html += watcher.printMySQLStatus(int.Parse(interval), reportType);
                }
                string sys_parm = OperateIniFile.ReadIniData("option", "sys_parm", "", path + "\\dbset.ini");
                if (sys_parm != "OFF")
                {
                    html += watcher.getSystemParameter(reportType);
                }

                string log_error_statistics = OperateIniFile.ReadIniData("option", "log_error_statistics", "", path + "\\dbset.ini");
                if (log_error_statistics == "ON")
                {
                    html += watcher.getLogFileStatistics(reportType);
                }
                string replication = OperateIniFile.ReadIniData("option", "replication", "", path + "\\dbset.ini");
                if (replication == "ON")
                {
                    html += watcher.getReplication(reportType);
                }
                string connect_count = OperateIniFile.ReadIniData("option", "connect_count", "", path + "\\dbset.ini");
                if (connect_count == "ON")
                {
                    html += watcher.getConnectionCount(reportType);
                }
                string avg_query_time = OperateIniFile.ReadIniData("option", "avg_query_time", "", path + "\\dbset.ini");
                if (avg_query_time == "ON")
                {
                    html += watcher.getAvgQueryTime(reportType);
                }

                string slow_query_topN = OperateIniFile.ReadIniData("option", "slow_query_topN", "0", path + "\\dbset.ini");
                if (slow_query_topN != "0" && slow_query_topN != "OFF")
                {
                    html += watcher.getSlowQueryTopN(reportType, int.Parse(slow_query_topN));
                }

                string err_sql_count = OperateIniFile.ReadIniData("option", "err_sql_count", "ON", path + "\\dbset.ini");
                if (err_sql_count == "ON")
                {
                    html += watcher.getErrSqlCount(reportType);
                }

                string err_sql_topN = OperateIniFile.ReadIniData("option", "err_sql_topN", "0", path + "\\dbset.ini");
                if (err_sql_topN != "0" && err_sql_topN != "OFF")
                {
                    html += watcher.getErrSqlTopN(reportType, int.Parse(err_sql_topN));
                }
                string query_analysis_topN = OperateIniFile.ReadIniData("option", "query_analysis_topN", "0", path + "\\dbset.ini");
                if (query_analysis_topN != "0")
                {
                    html += watcher.getQueryAnalysisTopN(reportType, int.Parse(query_analysis_topN));
                }
                string query_full_table_scans_topN = OperateIniFile.ReadIniData("option", "query_full_table_scans_topN", "0", path + "\\dbset.ini");
                if (query_full_table_scans_topN != "0" && query_full_table_scans_topN != "OFF")
                {
                    html += watcher.getQueryFullTableScansTopN(reportType, int.Parse(query_full_table_scans_topN));
                }

                string query_sorting_topN = OperateIniFile.ReadIniData("option", "query_sorting_topN", "0", path + "\\dbset.ini");
                if (query_sorting_topN != "0" && query_sorting_topN != "OFF")
                {
                    html += watcher.getQuerySortingTopN(reportType, int.Parse(query_sorting_topN));
                }

                string query_with_temp_tables_topN = OperateIniFile.ReadIniData("option", "query_with_temp_tables_topN", "0", path + "\\dbset.ini");
                if (query_with_temp_tables_topN != "0" && query_with_temp_tables_topN != "OFF")
                {
                    html += watcher.getQueryWithTempTablesTopN(reportType, int.Parse(query_with_temp_tables_topN));
                }
                string database_size = OperateIniFile.ReadIniData("option", "database_size", "ON", path + "\\dbset.ini");
                if (database_size == "ON")
                {
                    html += watcher.getDatabaseSize(reportType);
                }
                string object_count = OperateIniFile.ReadIniData("option", "object_count", "ON", path + "\\dbset.ini");
                if (object_count == "ON")
                {
                    html += watcher.getObjectCount(reportType);
                }
                string table_info = OperateIniFile.ReadIniData("option", "table_info", "ON", path + "\\dbset.ini");
                if (table_info == "ON")
                {
                    html += watcher.getTableInfo(reportType);
                }

                string index_info = OperateIniFile.ReadIniData("option", "index_info", "ON", path + "\\dbset.ini");
                if (index_info == "ON")
                {
                    html += watcher.getIndexInfo(reportType);
                }
                string schema_index_statistics = OperateIniFile.ReadIniData("option", "schema_index_statistics", "ON", path + "\\dbset.ini");
                if (schema_index_statistics == "ON")
                {
                    html += watcher.getSchemaIndexStatistics(reportType);
                }
                string schema_table_statistics = OperateIniFile.ReadIniData("option", "schema_table_statistics", "ON", path + "\\dbset.ini");
                if (schema_table_statistics == "ON")
                {
                    html += watcher.getSchemaTableStatistics(reportType);
                }

                string schema_table_statistics_with_buffer = OperateIniFile.ReadIniData("option", "schema_table_statistics_with_buffer", "ON", path + "\\dbset.ini");
                if (schema_table_statistics_with_buffer == "ON")
                {
                    html += watcher.getSchemaTableStatisticsWithBuffer(reportType);
                }

                string schema_tables_with_full_table_scans = OperateIniFile.ReadIniData("option", "schema_tables_with_full_table_scans", "ON", path + "\\dbset.ini");
                if (schema_tables_with_full_table_scans == "ON")
                {
                    html += watcher.getSchemaTablesWithFullTableScans(reportType);
                }

                string schema_unused_indexes = OperateIniFile.ReadIniData("option", "schema_unused_indexes", "ON", path + "\\dbset.ini");
                if (schema_unused_indexes == "ON")
                {
                    html += watcher.getSchemaUnusedIndexes(reportType);
                }

                string host_summary = OperateIniFile.ReadIniData("option", "host_summary", "ON", path + "\\dbset.ini");
                if (host_summary == "ON")
                {
                    html += watcher.getHostSummary(reportType);
                }

                string host_summary_by_file_io_type = OperateIniFile.ReadIniData("option", "host_summary_by_file_io_type", "ON", path + "\\dbset.ini");
                if (host_summary_by_file_io_type == "ON")
                {
                    html += watcher.getHostSummaryByFileIoType(reportType);
                }

                string host_summary_by_file_io = OperateIniFile.ReadIniData("option", "host_summary_by_file_io", "ON", path + "\\dbset.ini");
                if (host_summary_by_file_io == "ON")
                {
                    html += watcher.getHostSummaryByFileIo(reportType);
                }

                string host_summary_by_stages = OperateIniFile.ReadIniData("option", "host_summary_by_stages", "ON", path + "\\dbset.ini");
                if (host_summary_by_stages == "ON")
                {
                    html += watcher.getHostSummaryByStages(reportType);
                }

                string host_summary_by_statement_latency = OperateIniFile.ReadIniData("option", "host_summary_by_statement_latency", "ON", path + "\\dbset.ini");
                if (host_summary_by_statement_latency == "ON")
                {
                    html += watcher.getHostSummaryByStatementLatency(reportType);
                }

                string host_summary_by_statement_type = OperateIniFile.ReadIniData("option", "host_summary_by_statement_type", "ON", path + "\\dbset.ini");
                if (host_summary_by_statement_type == "ON")
                {
                    html += watcher.getHostSummaryByStatementType(reportType);
                }

                string user_summary = OperateIniFile.ReadIniData("option", "user_summary", "ON", path + "\\dbset.ini");
                if (user_summary == "ON")
                {
                    html += watcher.getUserSummary(reportType);
                }
                string user_summary_by_file_io_type = OperateIniFile.ReadIniData("option", "user_summary_by_file_io_type", "ON", path + "\\dbset.ini");
                if (user_summary_by_file_io_type == "ON")
                {
                    html += watcher.getUserSummaryByFileIoType(reportType);
                }
                string user_summary_by_file_io = OperateIniFile.ReadIniData("option", "user_summary_by_file_io", "ON", path + "\\dbset.ini");
                if (user_summary_by_file_io == "ON")
                {
                    html += watcher.getUserSummaryByFileIo(reportType);
                }
                string user_summary_by_stages = OperateIniFile.ReadIniData("option", "user_summary_by_stages", "ON", path + "\\dbset.ini");
                if (user_summary_by_stages == "ON")
                {
                    html += watcher.getUserSummaryByStages(reportType);
                }

                string user_summary_by_statement_latency = OperateIniFile.ReadIniData("option", "user_summary_by_statement_latency", "ON", path + "\\dbset.ini");
                if (user_summary_by_statement_latency == "ON")
                {
                    html += watcher.getUserSummaryByStatementLatency(reportType);
                }

                string user_summary_by_statement_type = OperateIniFile.ReadIniData("option", "user_summary_by_statement_type", "ON", path + "\\dbset.ini");
                if (user_summary_by_statement_type == "ON")
                {
                    html += watcher.getUserSummaryByStatementType(reportType);
                }

                string innodb_buffer_stats_by_schema = OperateIniFile.ReadIniData("option", "innodb_buffer_stats_by_schema", "ON", path + "\\dbset.ini");
                if (innodb_buffer_stats_by_schema == "ON")
                {
                    html += watcher.getInnodbBufferStatsBySchema(reportType);
                }
                string innodb_buffer_stats_by_table = OperateIniFile.ReadIniData("option", "innodb_buffer_stats_by_table", "ON", path + "\\dbset.ini");
                if (innodb_buffer_stats_by_table == "ON")
                {
                    html += watcher.getInnodbBufferStatsByTable(reportType);
                }
                string io_by_thread_by_latency_topN = OperateIniFile.ReadIniData("option", "io_by_thread_by_latency_topN", "0", path + "\\dbset.ini");
                if (io_by_thread_by_latency_topN != "0" && io_by_thread_by_latency_topN != "OFF")
                {
                    html += watcher.getIoByThreadByLatencyTopN(reportType, int.Parse(io_by_thread_by_latency_topN));
                }
                string io_global_by_file_by_bytes_topN = OperateIniFile.ReadIniData("option", "io_global_by_file_by_bytes_topN", "0", path + "\\dbset.ini");
                if (io_global_by_file_by_bytes_topN != "0" && io_global_by_file_by_bytes_topN != "OFF")
                {
                    html += watcher.getIoGlobalByFileByBytesTopN(reportType, int.Parse(io_global_by_file_by_bytes_topN));
                }
                string io_global_by_file_by_latency_topN = OperateIniFile.ReadIniData("option", "io_global_by_file_by_latency_topN", "0", path + "\\dbset.ini");
                if (io_global_by_file_by_latency_topN != "0" && io_global_by_file_by_latency_topN != "OFF")
                {
                    html += watcher.getIoGlobalByFileByLatencyTopN(reportType, int.Parse(io_global_by_file_by_latency_topN));
                }
                string io_global_by_wait_by_bytes_topN = OperateIniFile.ReadIniData("option", "io_global_by_wait_by_bytes_topN", "0", path + "\\dbset.ini");
                if (io_global_by_wait_by_bytes_topN != "0" && io_global_by_wait_by_bytes_topN != "OFF")
                {
                    html += watcher.getIoGlobalByWaitByBytesTopN(reportType, int.Parse(io_global_by_wait_by_bytes_topN));
                }
                string io_global_by_wait_by_latency_topN = OperateIniFile.ReadIniData("option", "io_global_by_wait_by_latency_topN", "0", path + "\\dbset.ini");
                if (io_global_by_wait_by_latency_topN != "0" && io_global_by_wait_by_latency_topN != "OFF")
                {
                    html += watcher.getIoGlobalByWaitByLatencyTopN(reportType, int.Parse(io_global_by_wait_by_latency_topN));
                }

                string wait_classes_global_by_avg_latency = OperateIniFile.ReadIniData("option", "wait_classes_global_by_avg_latency", "ON", path + "\\dbset.ini");
                if (wait_classes_global_by_avg_latency == "ON")
                {
                    html += watcher.getWaitClassesGlobalByAvgLatency(reportType);
                }
                string wait_classes_global_by_latency = OperateIniFile.ReadIniData("option", "wait_classes_global_by_latency", "ON", path + "\\dbset.ini");
                if (wait_classes_global_by_latency == "ON")
                {
                    html += watcher.getWaitClassesGlobalByLatency(reportType);
                }

                string waits_by_host_by_latency = OperateIniFile.ReadIniData("option", "waits_by_host_by_latency", "ON", path + "\\dbset.ini");
                if (waits_by_host_by_latency == "ON")
                {
                    html += watcher.getWaitsByHostByLatency(reportType);
                }
                string waits_by_user_by_latency = OperateIniFile.ReadIniData("option", "waits_by_user_by_latency", "ON", path + "\\dbset.ini");
                if (waits_by_user_by_latency == "ON")
                {
                    html += watcher.getWaitsByUserByLatency(reportType);
                }

                string schema_table_lock_waits = OperateIniFile.ReadIniData("option", "schema_table_lock_waits", "ON", path + "\\dbset.ini");
                if (schema_table_lock_waits == "ON")
                {
                    html += watcher.getSchemaTableLockWaits(reportType);
                }
                string innodb_lock_waits = OperateIniFile.ReadIniData("option", "innodb_lock_waits", "ON", path + "\\dbset.ini");
                if (innodb_lock_waits == "ON")
                {
                    html += watcher.getInnodbLockWaits(reportType);
                }

                string memory_by_host_by_current_bytes = OperateIniFile.ReadIniData("option", "memory_by_host_by_current_bytes", "ON", path + "\\dbset.ini");
                if (memory_by_host_by_current_bytes == "ON")
                {
                    html += watcher.getMemoryByHostByCurrentBytes(reportType);
                }

                string memory_by_thread_by_current_bytes = OperateIniFile.ReadIniData("option", "memory_by_thread_by_current_bytes", "ON", path + "\\dbset.ini");
                if (memory_by_thread_by_current_bytes == "ON")
                {
                    html += watcher.getMemoryByThreadByCurrentBytes(reportType);
                }
                string memory_by_user_by_current_bytes = OperateIniFile.ReadIniData("option", "memory_by_user_by_current_bytes", "ON", path + "\\dbset.ini");
                if (memory_by_user_by_current_bytes == "ON")
                {
                    html += watcher.getMemoryByUserByCurrentBytes(reportType);
                }
                string memory_global_by_current_bytes = OperateIniFile.ReadIniData("option", "memory_global_by_current_bytes", "ON", path + "\\dbset.ini");
                if (memory_global_by_current_bytes == "ON")
                {
                    html += watcher.getMemoryGlobalByCurrentBytes(reportType);
                }
                string memory_global_total = OperateIniFile.ReadIniData("option", "memory_global_total", "ON", path + "\\dbset.ini");
                if (memory_global_total == "ON")
                {
                    html += watcher.getMemoryGlobalTotal(reportType);
                }
                string processlist = OperateIniFile.ReadIniData("option", "processlist", "ON", path + "\\dbset.ini");
                if (processlist == "ON")
                {
                    html += watcher.getProcesslist(reportType);
                }
                string session = OperateIniFile.ReadIniData("option", "session", "ON", path + "\\dbset.ini");
                if (session == "ON")
                {
                    html += watcher.getSession(reportType);
                }
                string metrics = OperateIniFile.ReadIniData("option", "metrics", "ON", path + "\\dbset.ini");
                if (metrics == "ON")
                {
                    html += watcher.getMetrics(reportType);
                }
                html += watcher.printBottom();

                if (File.Exists(path + "\\report.html"))
                {
                    File.Delete(path + "\\report.html");
                }
                FileStream fs = File.OpenWrite(path + "\\report.html");
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(html);
                }
                fs.Close();
                webBrowser.Url = new Uri(path + "\\report.html");

                reportSaveToolStripMenuItem.Enabled = true;
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
                
            }
            watcher.Close();
            this.Cursor = Cursors.Default;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + "  v"+ Application.ProductVersion +" Beta";
            panel1.Top = menuStrip1.Height + 2;
            panel1.Width = this.Width - 10;
            webBrowser.Left = panel1.Left+5;
            webBrowser.Width = panel1.Width - 15;
            webBrowser.Top = panel1.Top + panel1.Height + 5;
            webBrowser.Height = this.Height - panel1.Height - menuStrip1.Height - 50 ;

            if (!File.Exists(path + @"\dbset.ini"))
            {
                File.Copy(Application.StartupPath + @"\dbset.ini", path + @"\dbset.ini");
            }
            string host = OperateIniFile.ReadIniData("database", "host", "", path + @"\dbset.ini");
            string user = OperateIniFile.ReadIniData("database", "user", "", path + @"\dbset.ini");
            string passwd = OperateIniFile.ReadIniData("database", "passwd", "", path + @"\dbset.ini");
            string db = OperateIniFile.ReadIniData("database", "db", "", path + @"\dbset.ini");
            string port = OperateIniFile.ReadIniData("database", "port", "", path + @"\dbset.ini");
            string operuser = OperateIniFile.ReadIniData("database", "operuser", "", path + @"\dbset.ini");
            string operpwd = OperateIniFile.ReadIniData("database", "operpwd", "", path + @"\dbset.ini");
            tbHostIP.Text = host;
            tbUserName.Text = user;
            tbPwd.Text = passwd;
            tbDbName.Text = db;
            tbPort.Text = port;
            tbOperUserName.Text = operuser;
            tbOperPwd.Text = operpwd;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            panel1.Top = menuStrip1.Height + 2;
            panel1.Width = this.Width - 10;
            webBrowser.Left = panel1.Left + 5;
            webBrowser.Width = panel1.Width - 15;
            webBrowser.Top = panel1.Top + panel1.Height + 5;
            webBrowser.Height = this.Height - panel1.Height - menuStrip1.Height - 50;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            
        }

        private void customConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfig frm = new frmConfig();
            frm.ShowDialog();
        }

        private void reportSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "HTML文件   (*.html)|*.html";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream stream = webBrowser.DocumentStream;

                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                using (FileStream fileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    fileStream.Write(buffer, 0, buffer.Length);
                    fileStream.Flush();
                }

                stream.Close();
            }
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAboutUs frm = new frmAboutUs();
            frm.ShowDialog();
        }
    }
}
