using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MySQLWatcher
{
    /// <summary>
    /// 配置界面
    /// author: Jack R. Ge(jack_r_ge@126.com)
    /// </summary>
    public partial class frmConfig : Form
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\mysqladvisor";
        public string[] lists = new string[] { "ON", "OFF" };

        public frmConfig()
        {
            InitializeComponent();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            try
            {
                CustomerProperty property = new CustomerProperty();
                property.mysql_overview = OperateIniFile.ReadIniData("option", "mysql_overview", "ON", path + "\\dbset.ini");
                property.interval = int.Parse(OperateIniFile.ReadIniData("option", "interval", "60", path + "\\dbset.ini"));
                property.sys_parm = OperateIniFile.ReadIniData("option", "sys_parm", "ON", path + "\\dbset.ini");
                property.log_error_statistics = OperateIniFile.ReadIniData("option", "log_error_statistics", "ON", path + "\\dbset.ini");
                property.replication = OperateIniFile.ReadIniData("option", "replication", "ON", path + "\\dbset.ini");
                property.connect_count = OperateIniFile.ReadIniData("option", "connect_count", "ON", path + "\\dbset.ini");
                property.avg_query_time = OperateIniFile.ReadIniData("option", "avg_query_time", "ON", path + "\\dbset.ini");
                string slow_query_topN_s = OperateIniFile.ReadIniData("option", "slow_query_topN", "0", path + "\\dbset.ini");
                property.slow_query_topN = int.Parse(slow_query_topN_s == "OFF"?"0": slow_query_topN_s);
                property.err_sql_count = OperateIniFile.ReadIniData("option", "err_sql_count", "ON", path + "\\dbset.ini");
                string err_sql_topN_s = OperateIniFile.ReadIniData("option", "err_sql_topN", "0", path + "\\dbset.ini");
                property.err_sql_topN = int.Parse(err_sql_topN_s == "OFF"?"0": err_sql_topN_s);
                property.query_analysis_topN = int.Parse(OperateIniFile.ReadIniData("option", "query_analysis_topN", "0", path + "\\dbset.ini"));
                string query_full_table_scans_topN_s = OperateIniFile.ReadIniData("option", "query_full_table_scans_topN", "0", path + "\\dbset.ini");
                property.query_full_table_scans_topN = int.Parse(query_full_table_scans_topN_s=="OFF"?"0": query_full_table_scans_topN_s);
                string query_sorting_topN_s = OperateIniFile.ReadIniData("option", "query_sorting_topN", "0", path + "\\dbset.ini");
                property.query_sorting_topN = int.Parse(query_sorting_topN_s=="OFF"?"0": query_sorting_topN_s);

                string query_with_temp_tables_topN_s = OperateIniFile.ReadIniData("option", "query_with_temp_tables_topN", "0", path + "\\dbset.ini");
                property.query_with_temp_tables_topN = int.Parse(query_with_temp_tables_topN_s=="OFF"?"0": query_with_temp_tables_topN_s);
                property.database_size = OperateIniFile.ReadIniData("option", "database_size", "ON", path + "\\dbset.ini");
                property.object_count = OperateIniFile.ReadIniData("option", "object_count", "ON", path + "\\dbset.ini");
                property.table_info = OperateIniFile.ReadIniData("option", "table_info", "ON", path + "\\dbset.ini");
                property.index_info = OperateIniFile.ReadIniData("option", "index_info", "ON", path + "\\dbset.ini");
                property.schema_index_statistics = OperateIniFile.ReadIniData("option", "schema_index_statistics", "ON", path + "\\dbset.ini");
                property.schema_table_statistics = OperateIniFile.ReadIniData("option", "schema_table_statistics", "ON", path + "\\dbset.ini");
                property.schema_table_statistics_with_buffer = OperateIniFile.ReadIniData("option", "schema_table_statistics_with_buffer", "ON", path + "\\dbset.ini");
                property.schema_tables_with_full_table_scans = OperateIniFile.ReadIniData("option", "schema_tables_with_full_table_scans", "ON", path + "\\dbset.ini");
                property.schema_unused_indexes = OperateIniFile.ReadIniData("option", "schema_unused_indexes", "ON", path + "\\dbset.ini");
                property.host_summary = OperateIniFile.ReadIniData("option", "host_summary", "ON", path + "\\dbset.ini");
                property.host_summary_by_file_io_type = OperateIniFile.ReadIniData("option", "host_summary_by_file_io_type", "ON", path + "\\dbset.ini");
                property.host_summary_by_file_io = OperateIniFile.ReadIniData("option", "host_summary_by_file_io", "ON", path + "\\dbset.ini");
                property.host_summary_by_stages = OperateIniFile.ReadIniData("option", "host_summary_by_stages", "ON", path + "\\dbset.ini");
                property.host_summary_by_statement_latency = OperateIniFile.ReadIniData("option", "host_summary_by_statement_latency", "ON", path + "\\dbset.ini");
                property.host_summary_by_statement_type = OperateIniFile.ReadIniData("option", "host_summary_by_statement_type", "ON", path + "\\dbset.ini");
                property.user_summary = OperateIniFile.ReadIniData("option", "user_summary", "ON", path + "\\dbset.ini");
                property.user_summary_by_file_io_type = OperateIniFile.ReadIniData("option", "user_summary_by_file_io_type", "ON", path + "\\dbset.ini");
                property.user_summary_by_file_io = OperateIniFile.ReadIniData("option", "user_summary_by_file_io", "ON", path + "\\dbset.ini");
                property.user_summary_by_stages = OperateIniFile.ReadIniData("option", "user_summary_by_stages", "ON", path + "\\dbset.ini");
                property.user_summary_by_statement_latency = OperateIniFile.ReadIniData("option", "user_summary_by_statement_latency", "ON", path + "\\dbset.ini");
                property.user_summary_by_statement_type = OperateIniFile.ReadIniData("option", "user_summary_by_statement_type", "ON", path + "\\dbset.ini");
                property.innodb_buffer_stats_by_schema = OperateIniFile.ReadIniData("option", "innodb_buffer_stats_by_schema", "ON", path + "\\dbset.ini");
                property.innodb_buffer_stats_by_table = OperateIniFile.ReadIniData("option", "innodb_buffer_stats_by_table", "ON", path + "\\dbset.ini");
                string io_by_thread_by_latency_topN_s = OperateIniFile.ReadIniData("option", "io_by_thread_by_latency_topN", "0", path + "\\dbset.ini");
                property.io_by_thread_by_latency_topN = int.Parse(io_by_thread_by_latency_topN_s=="OFF"?"0": io_by_thread_by_latency_topN_s);
                string io_global_by_file_by_bytes_topN_s = OperateIniFile.ReadIniData("option", "io_global_by_file_by_bytes_topN", "0", path + "\\dbset.ini");
                property.io_global_by_file_by_bytes_topN = int.Parse(io_global_by_file_by_bytes_topN_s=="OFF"?"0": io_global_by_file_by_bytes_topN_s);

                string io_global_by_file_by_latency_topN_s = OperateIniFile.ReadIniData("option", "io_global_by_file_by_latency_topN", "0", path + "\\dbset.ini");
                property.io_global_by_file_by_latency_topN = int.Parse(io_global_by_file_by_latency_topN_s=="OFF"?"0": io_global_by_file_by_latency_topN_s);

                string io_global_by_wait_by_bytes_topN_s = OperateIniFile.ReadIniData("option", "io_global_by_wait_by_bytes_topN", "0", path + "\\dbset.ini");
                property.io_global_by_wait_by_bytes_topN = int.Parse(io_global_by_wait_by_bytes_topN_s=="OFF"?"0": io_global_by_wait_by_bytes_topN_s);

                string io_global_by_wait_by_latency_topN_s = OperateIniFile.ReadIniData("option", "io_global_by_wait_by_latency_topN", "0", path + "\\dbset.ini");
                property.io_global_by_wait_by_latency_topN = int.Parse(io_global_by_wait_by_latency_topN_s=="OFF"?"0": io_global_by_wait_by_latency_topN_s);
                property.wait_classes_global_by_avg_latency = OperateIniFile.ReadIniData("option", "wait_classes_global_by_avg_latency", "ON", path + "\\dbset.ini");
                property.waits_by_host_by_latency = OperateIniFile.ReadIniData("option", "waits_by_host_by_latency", "ON", path + "\\dbset.ini");
                property.waits_by_user_by_latency = OperateIniFile.ReadIniData("option", "waits_by_user_by_latency", "ON", path + "\\dbset.ini");
                property.waits_global_by_latency = OperateIniFile.ReadIniData("option", "waits_global_by_latency", "ON", path + "\\dbset.ini");
                property.schema_table_lock_waits = OperateIniFile.ReadIniData("option", "schema_table_lock_waits", "ON", path + "\\dbset.ini");
                property.innodb_lock_waits = OperateIniFile.ReadIniData("option", "innodb_lock_waits", "ON", path + "\\dbset.ini");
                property.memory_by_host_by_current_bytes = OperateIniFile.ReadIniData("option", "memory_by_host_by_current_bytes", "ON", path + "\\dbset.ini");
                property.memory_by_thread_by_current_bytes = OperateIniFile.ReadIniData("option", "memory_by_thread_by_current_bytes", "ON", path + "\\dbset.ini");
                property.memory_by_user_by_current_bytes = OperateIniFile.ReadIniData("option", "memory_by_user_by_current_bytes", "ON", path + "\\dbset.ini");
                property.memory_global_by_current_bytes = OperateIniFile.ReadIniData("option", "memory_global_by_current_bytes", "ON", path + "\\dbset.ini");
                property.memory_global_total = OperateIniFile.ReadIniData("option", "memory_global_total", "ON", path + "\\dbset.ini");
                property.processlist = OperateIniFile.ReadIniData("option", "processlist", "ON", path + "\\dbset.ini");
                property.session = OperateIniFile.ReadIniData("option", "session", "ON", path + "\\dbset.ini");
                property.metrics = OperateIniFile.ReadIniData("option", "metrics", "ON", path + "\\dbset.ini");
                propertyGrid1.SelectedObject = property;
            }
            catch
            {
                MessageBox.Show("参数文件错误");
            }
        }

        private void save()
        {
            object[] objects =  propertyGrid1.SelectedObjects;
            if (objects.Length > 0)
            {
                CustomerProperty property = (CustomerProperty)objects[0];
                OperateIniFile.WriteIniData("option", "mysql_overview", property.mysql_overview, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "interval", property.interval+"", path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "sys_parm", property.sys_parm, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "log_error_statistics", property.log_error_statistics, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "replication", property.replication, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "connect_count", property.connect_count, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "avg_query_time", property.avg_query_time, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "slow_query_topN", property.slow_query_topN+"", path + "\\dbset.ini");
                
                OperateIniFile.WriteIniData("option", "err_sql_count", property.err_sql_count, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "err_sql_topN", property.err_sql_topN+"", path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "query_analysis_topN", property.query_analysis_topN+"", path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "query_full_table_scans_topN", property.query_full_table_scans_topN+"", path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "query_sorting_topN", property.query_sorting_topN+"", path + "\\dbset.ini");
                
                OperateIniFile.WriteIniData("option", "query_with_temp_tables_topN", property.query_with_temp_tables_topN+"", path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "database_size", property.database_size, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "object_count", property.object_count, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "table_info", property.table_info, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "index_info", property.index_info, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "schema_index_statistics", property.schema_index_statistics, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "schema_table_statistics", property.schema_table_statistics, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "schema_table_statistics_with_buffer", property.schema_table_statistics_with_buffer, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "schema_tables_with_full_table_scans", property.schema_tables_with_full_table_scans, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "schema_unused_indexes", property.schema_unused_indexes, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "host_summary", property.host_summary, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "host_summary_by_file_io_type", property.host_summary_by_file_io_type, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "host_summary_by_file_io", property.host_summary_by_file_io, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "host_summary_by_stages", property.host_summary_by_stages, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "host_summary_by_statement_latency", property.host_summary_by_statement_latency, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "host_summary_by_statement_type", property.host_summary_by_statement_type, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "user_summary", property.user_summary, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "user_summary_by_file_io_type", property.user_summary_by_file_io_type, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "user_summary_by_file_io", property.user_summary_by_file_io, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "user_summary_by_stages", property.user_summary_by_stages, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "user_summary_by_statement_latency", property.user_summary_by_statement_latency, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "user_summary_by_statement_type", property.user_summary_by_statement_type, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "innodb_buffer_stats_by_schema", property.innodb_buffer_stats_by_schema, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "innodb_buffer_stats_by_table", property.innodb_buffer_stats_by_table, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "io_by_thread_by_latency_topN", property.io_by_thread_by_latency_topN+"", path + "\\dbset.ini");
                
                OperateIniFile.WriteIniData("option", "io_global_by_file_by_bytes_topN", property.io_global_by_file_by_bytes_topN+"", path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "io_global_by_file_by_latency_topN", property.io_global_by_file_by_latency_topN+"", path + "\\dbset.ini");

                OperateIniFile.WriteIniData("option", "io_global_by_wait_by_bytes_topN", property.io_global_by_wait_by_bytes_topN+"", path + "\\dbset.ini");

                OperateIniFile.WriteIniData("option", "io_global_by_wait_by_latency_topN", property.io_global_by_wait_by_latency_topN+"", path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "wait_classes_global_by_avg_latency", property.wait_classes_global_by_avg_latency, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "waits_by_host_by_latency", property.waits_by_host_by_latency, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "waits_by_user_by_latency", property.waits_by_user_by_latency, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "waits_global_by_latency", property.waits_global_by_latency, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "schema_table_lock_waits", property.schema_table_lock_waits, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "innodb_lock_waits", property.innodb_lock_waits, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "memory_by_host_by_current_bytes", property.memory_by_host_by_current_bytes, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "memory_by_thread_by_current_bytes", property.memory_by_thread_by_current_bytes, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "memory_by_user_by_current_bytes", property.memory_by_user_by_current_bytes, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "memory_global_by_current_bytes", property.memory_global_by_current_bytes, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "memory_global_total", property.memory_global_total, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "processlist", property.processlist, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "session", property.session, path + "\\dbset.ini");
                OperateIniFile.WriteIniData("option", "metrics", property.metrics, path + "\\dbset.ini");
                propertyGrid1.SelectedObject = property;
            }
        }

        private void frmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            save();
        }
    }
}
