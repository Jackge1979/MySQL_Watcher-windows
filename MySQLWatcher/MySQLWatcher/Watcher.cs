using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.IO;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;

namespace MySQLWatcher
{
    /// <summary>
    /// 巡检类
    /// author: Jack R. Ge(jack_r_ge@126.com)
    /// </summary>
    public class Watcher
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\mysqladvisor";
        
        private string mysql_version;
        private string perfor_or_infor = "information_schema";
        private Dictionary<string, string> mysqlstatus1 = new Dictionary<string, string>();
        private Dictionary<string, string> mysqlstatus2 = new Dictionary<string, string>();

        const string SYS_PARM_FILTER = "'autocommit'," +
    "'binlog_cache_size'," +
    "'bulk_insert_buffer_size'," +
    "'character_set_server'," +
    "'tx_isolation'," +
    "'tx_read_only'," +
    "'sql_mode'," +
    //# connection #
    "'interactive_timeout'," +
    "'wait_timeout'," +
    "'lock_wait_timeout'," +
    "'skip_name_resolve'," +
    "'max_connections'," +
    "'max_connect_errors'," +
    //# table cache performance settings
    "'table_open_cache'," +
    "'table_definition_cache'," +
    "'table_open_cache_instances'," +
    //# performance settings
    "'have_query_cache'," +
    "'join_buffer_size'," +
    "'key_buffer_size'," +
    "'key_cache_age_threshold'," +
    "'key_cache_block_size'," +
    "'key_cache_division_limit'," +
    "'large_pages'," +
    "'locked_in_memory'," +
    "'long_query_time'," +
    "'max_allowed_packet'," +
    "'max_binlog_size'," +
    "'max_length_for_sort_data'," +
    "'max_sort_length'," +
    "'max_tmp_tables'," +
    "'max_user_connections'," +
    "'optimizer_prune_level'," +
    "'optimizer_search_depth'," +
    "'query_cache_size'," +
    "'query_cache_type'," +
    "'query_prealloc_size'," +
    "'range_alloc_block_size'," +
    //# session memory settings #
    "'read_buffer_size'," +
    "'read_rnd_buffer_size'," +
    "'sort_buffer_size'," +
    "'tmp_table_size'," +
    "'join_buffer_size'," +
    "'thread_cache_size'," +
    //# log settings #
    "'log_error'," +
    "'slow_query_log'," +
    "'slow_query_log_file'," +
    "'log_queries_not_using_indexes'," +
    "'log_slow_admin_statements'," +
    "'log_slow_slave_statements'," +
    "'log_throttle_queries_not_using_indexes'," +
    "'expire_logs_days'," +
    "'long_query_time'," +
    "'min_examined_row_limit'," +
    "'binlog-rows-query-log-events'," +
    "'log-bin-trust-function-creators'," +
    "'expire-logs-days'," +
    "'log-slave-updates'," +
    //# innodb settings #
    "'innodb_page_size'," +
    "'innodb_buffer_pool_size'," +
    "'innodb_buffer_pool_instances'," +
    "'innodb_buffer_pool_chunk_size'," +
    "'innodb_buffer_pool_load_at_startup'," +
    "'innodb_buffer_pool_dump_at_shutdown'," +
    "'innodb_lru_scan_depth'," +
    "'innodb_lock_wait_timeout'," +
    "'innodb_io_capacity'," +
    "'innodb_io_capacity_max'," +
    "'innodb_flush_method'," +
    "'innodb_file_format'," +
    "'innodb_file_format_max'," +
    "'innodb_undo_logs'," +
    "'innodb_undo_tablespaces'," +
    "'innodb_flush_neighbors'," +
    "'innodb_log_file_size'," +
    "'innodb_log_files_in_group'," +
    "'innodb_log_buffer_size'," +
    "'innodb_purge_threads'," +
    "'innodb_large_prefix'," +
    "'innodb_thread_concurrency'," +
    "'innodb_print_all_deadlocks'," +
    "'innodb_strict_mode'," +
    "'innodb_sort_buffer_size'," +
    "'innodb_write_io_threads'," +
    "'innodb_read_io_threads'," +
    "'innodb_file_per_table'," +
    "'innodb_stats_persistent_sample_pages'," +
    "'innodb_autoinc_lock_mode'," +
    "'innodb_online_alter_log_max_size'," +
    "'innodb_open_files'," +
    //# replication settings #
    "'master_info_repository'," +
    "'relay_log_info_repository'," +
    "'sync_binlog'," +
    "'gtid_mode'," +
    "'enforce_gtid_consistency'," +
    "'log_slave_updates'," +
    "'binlog_format'," +
    "'binlog_rows_query_log_events'," +
    "'relay_log'," +
    "'relay_log_recovery'," +
    "'slave_skip_errors'," +
    "'slave-rows-search-algorithms'," +
    //# semi sync replication settings #
    "'plugin_load'," +
    "'rpl_semi_sync_master_enabled'," +
    "'rpl_semi_sync_master_timeout'," +
    "'rpl_semi_sync_slave_enabled'," +
    //# password plugin #
    "'validate_password_policy'," +
    "'validate-password'," +
    //# metalock performance settings
    "'metadata_locks_hash_instances'," +
    //# new innodb settings #
    "'loose_innodb_numa_interleave'," +
    "'innodb_buffer_pool_dump_pct'," +
    "'innodb_page_cleaners'," +
    "'innodb_undo_log_truncate'," +
    "'innodb_max_undo_log_size'," +
    "'innodb_purge_rseg_truncate_frequency'," +
    //# new replication settings #
    "'slave-parallel-type'," +
    "'slave-parallel-workers'," +
    "'slave_preserve_commit_order'," +
    "'slave_transaction_retries'," +
    //# other change settings #
    "'binlog_gtid_simple_recovery'," +
    "'log_timestamps'," +
    "'show_compatibility_56'";

        private string host = "";
        private string user = "";
        private string passwd = "";
        private string port = "3306";
        private string db = "";
        private string operuser = "";
        private string operpwd = "";
        public Watcher()
        {
            GetConnection();
            SQLHelper.OpenDB();
        }

        public void Close()
        {
            SQLHelper.CloseDB();
        }
        /// <summary>
        /// 返回配置文件中指定的连接
        /// </summary>
        /// <returns>配置文件中指定的连接</returns>
        private void GetConnection()
        {
            host = OperateIniFile.ReadIniData("database", "host", "", path + @"\dbset.ini");
            user = OperateIniFile.ReadIniData("database", "user", "", path + @"\dbset.ini");
            db = OperateIniFile.ReadIniData("database", "db", "", path + @"\dbset.ini");
            passwd = OperateIniFile.ReadIniData("database", "passwd", "", path + @"\dbset.ini");
            port = OperateIniFile.ReadIniData("database", "port", "", path + @"\dbset.ini");
            operuser = OperateIniFile.ReadIniData("database", "operuser", "", path + @"\dbset.ini");
            operpwd = OperateIniFile.ReadIniData("database", "operpwd", "", path + @"\dbset.ini");
            string connNewString = "Database='" + db + "';Data Source='" + host + "';User Id='" + user + "';Password='" + passwd + "';Port="+port+";charset='utf8';pooling=true;Allow Zero Datetime=True";
        }
        
        /// <summary>
        /// 判断Sys Schema是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsSysSchemaExist()
        {
            bool exist = false;
            string query = "SHOW DATABASES";
            try
            {
                DataTable dt = SQLHelper.ExcuteQuery(query);
                for (int i=0;i<dt.Rows.Count;i++)
                {
                    if (dt.Rows[i][0].ToString() == "sys")
                    {
                        exist = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
            return exist;
        }

        public string printCaption(string reportType)
        {

            string title = "Basic Information";
            string[] style = { "host,c", "user,c", "db,c", "mysql version,c" };
            string query = "select @@version";

            mysql_version = SQLHelper.ExcuteQueryValue(query);
            if (mysql_version.IndexOf("5.7") > -1)
            {
                perfor_or_infor = "performance_schema";
            } else {
                perfor_or_infor = "information_schema";
            }


            string[] rows = { host, user, db, mysql_version };
            List<string[]> list = new List<string[]>();
            list.Add(rows);
            return printTable(list, title, style, reportType);
        }
        public string optimizerSwitch(string reportType, string perfor_or_infor)
        {
            string title = "Optimizer Switch";
            string[] style = { "switch_name,l", "value,r" };
            string query = "select variable_value from " + perfor_or_infor + ".global_variables where variable_name='optimizer_switch'";
            DataTable dt = SQLHelper.ExcuteQuery(query);
            if (dt.Rows.Count>0)
            {
                string[] cols = dt.Rows[0][0].ToString().Split(',');
                List<string[]> list = new List<string[]>();
                foreach (string col in cols)
                {
                    string[] row = col.Split('=');
                    list.Add(row);
                }
                return printTable(list, title, style, reportType);
            }
            return "";
        }

        public string printTable(List<string[]> rows, string title, string[] style, string reportType)
        {
            if (reportType == ReportType.TXT)
            {
                return printTableTxt(rows, title, style, reportType);
            }
            else if (reportType == ReportType.HTML)
            {
                return printTableHtml(rows, title, style, reportType);
            }
            return null;
        }

        public string printTableTxt(List<string[]> rows, string title, string[] style, string reportType)
        {
            string[] field_names = new string[style.Length];
            
            for (int k = 0; k < style.Length; k++)
            {
                field_names[k] = style[k].Split(',')[0];
            }
            return null;
        }

        /// <summary>
        /// 输出html表格
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="title"></param>
        /// <param name="style"></param>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string printTableHtml(List<string[]> rows, string title, string[] style, string reportType)
        {
            string html = "";
            html += "<p /><h3 class=\"awr\"><a class=\"awr\" name=\"99999\"></a>" + title + "</h3><p />";
            html += "<table border=\"1\" width=\"100%\">";

            html += "<tr>";
            for (int i = 0; i < style.Length; i++)
            {
                html += "<th class=\"awrbg\">";
                html += style[i].Split(',')[0];
                html += "</th>";
            }
            html += "</tr>";

            int linenum = 0;
            foreach (var item in rows)
            {
                linenum += 1;
                html += "<tr>";
                string classs = "awrnc";
                if (linenum % 2 == 0) {
                    classs = "awrc";
                }

                int k = 0;
                foreach (var col in item) {
                    if (style[k++].Split(',')[1] == "r") {
                        html += "<td align=\"right\" class=\"" + classs + "\">" + col + "</td>";
                    } else
                    {
                        html += "<td class=\"" + classs + "\">" + col + "</td>";
                    }
                }

                html += "</tr>";
            }
            html += "</table>";
            html += "<br/><a class=\"awr\" href=\"#top\">Back to Top</a><p /><p /><p /><p />";
            return html;
        }
        
        /// <summary>
        /// 获取MySQL数据库状态
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> getMySQLStstus()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string query = "SHOW GLOBAL STATUS";
            DataTable dt = SQLHelper.ExcuteQuery(query);
            for (int i = 0;i<dt.Rows.Count;i++)
            {
                dict.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
            }
            return dict;
        }

        private long Questions1, Questions2;// 执行查询的总次数
        private long Uptime2;//服务器已经运行的时间（以秒为单位）
        private long Key_reads1, Key_reads2;// 从硬盘读取键的数据块的次数。如果Key_reads较大，则Key_buffer_size值可能太小。
                                            // 可以用Key_reads/Key_read_requests计算缓存损失率
        private long Key_read_requests1, Key_read_requests2;// 从缓存读键的数据块的请求数
        private long Key_writes1, Key_writes2;// 向硬盘写入将键的数据块的物理写操作的次数
        private long Key_write_requests1, Key_write_requests2;// 将键的数据块写入缓存的请求数
        private long Innodb_buffer_pool_reads1, Innodb_buffer_pool_reads2;// 不能满足InnoDB必须单页读取的缓冲池中的逻辑读数量。
        private long Innodb_buffer_pool_read_requests1, Innodb_buffer_pool_read_requests2;// InnoDB已经完成的逻辑读请求数
        private long Qcache_hits1, Qcache_hits2;// 查询缓存被访问的次数
        private long Qcache_inserts1, Qcache_inserts2;// 加入到缓存的查询数量，缓存没有用到
        private long Open_tables1, Open_tables2;// 当前打开的表的数量
        private long Opened_tables1, Opened_tables2;// 已经打开的表的数量。如果Opened_tables较大，table_cache 值可能太小
        private long Threads_created1, Threads_created2;// 创建用来处理连接的线程数。如果Threads_created较大，你可能要
                                                        // 增加thread_cache_size值。缓存访问率的计算方法Threads_created/Connections
        private long Connections1, Connections2;// 试图连接到(不管是否成功)MySQL服务器的连接数。缓存访问率的计算方法Threads_created/Connections
        private long Threads_connected1, Threads_connected2;
        private long Aborted_connects1, Aborted_connects2;
        private long Com_select1, Com_select2;//平均每秒select语句执行次数
        private long Com_insert1, Com_insert2;//平均每秒insert语句执行次数
        private long Com_update1, Com_update2;//平均每秒update语句执行次数
        private long Com_delete1, Com_delete2;//平均每秒delete语句执行次数
        private long Com_replace1, Com_replace2;//平均每秒replace语句执行次数
        private long Com_commit1, Com_commit2;//平均每秒commit语句执行次数
        private long Com_rollback1, Com_rollback2;//平均每秒rollback语句执行次数

        private long Table_locks_waited1, Table_locks_waited2;// 不能立即获得的表的锁的次数。如果该值较高，并且有性能问题，你应首先优化查询，然后拆分表或使用复制。
        private long Table_locks_immediate1, Table_locks_immediate2;//// 立即获得的表的锁的次数
        private long Created_tmp_tables1, Created_tmp_tables2;// 服务器执行语句时自动创建的内存中的临时表的数量。如果Created_tmp_disk_tables较大，
                                                              // 你可能要增加tmp_table_size值使临时 表基于内存而不基于硬盘
        private long Created_tmp_disk_tables1, Created_tmp_disk_tables2;// 服务器执行语句时在硬盘上自动创建的临时表的数量
        private long Slow_queries1, Slow_queries2;// 查询时间超过long_query_time秒的查询的个数 缓慢查询个数
        private long Select_full_join1, Select_full_join2;// 没有主键（key）联合（Join）的执行。该值可能是零。这是捕获开发错误的好方法，因为一些这样的查询可能降低系统的性能。
        //  Percentage of full table scans
        private long Handler_read_rnd_next1, Handler_read_rnd_next2;
        private long Handler_read_rnd1, Handler_read_rnd2;
        private long Handler_read_first1, Handler_read_first2;
        private long Handler_read_next1, Handler_read_next2;
        private long Handler_read_key1, Handler_read_key2;
        private long Handler_read_prev1, Handler_read_prev2;
        // 缓冲池利用率
        private long Innodb_buffer_pool_pages_total1, Innodb_buffer_pool_pages_total2;
        private long Innodb_buffer_pool_pages_free1, Innodb_buffer_pool_pages_free2;

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="reportType"></param>
        public string printMySQLStatus(int interval, string reportType)
        {
            mysqlstatus1 = getMySQLStstus();
            Thread.Sleep(interval);
            mysqlstatus2 = getMySQLStstus();


            Questions1 = long.Parse(mysqlstatus1["Questions"]);
            Questions2 = long.Parse(mysqlstatus2["Questions"]);

            Uptime2 = long.Parse(mysqlstatus2["Uptime"]);
            Com_commit1 = long.Parse(mysqlstatus1["Com_commit"]); ;
            Com_commit2 = long.Parse(mysqlstatus2["Com_commit"]);
            Com_rollback1 = long.Parse(mysqlstatus1["Com_rollback"]);
            Com_rollback2 = long.Parse(mysqlstatus2["Com_rollback"]);

            Key_reads1 = long.Parse(mysqlstatus1["Key_reads"]);
            Key_reads2 = long.Parse(mysqlstatus2["Key_reads"]);
            Key_read_requests1 = long.Parse(mysqlstatus1["Key_read_requests"]);
            Key_read_requests2 = long.Parse(mysqlstatus2["Key_read_requests"]);

            Key_writes1 = long.Parse(mysqlstatus1["Key_writes"]);
            Key_writes2 = long.Parse(mysqlstatus2["Key_writes"]);

            Key_write_requests1 = long.Parse(mysqlstatus1["Key_write_requests"]);
            Key_write_requests2 = long.Parse(mysqlstatus2["Key_write_requests"]);

            Innodb_buffer_pool_reads1 = long.Parse(mysqlstatus1["Innodb_buffer_pool_reads"]);
            Innodb_buffer_pool_reads2 = long.Parse(mysqlstatus2["Innodb_buffer_pool_reads"]);

            Innodb_buffer_pool_read_requests1 = long.Parse(mysqlstatus1["Innodb_buffer_pool_read_requests"]);
            Innodb_buffer_pool_read_requests2 = long.Parse(mysqlstatus2["Innodb_buffer_pool_read_requests"]);

            Qcache_hits1 = long.Parse(mysqlstatus1["Qcache_hits"]);
            Qcache_hits2 = long.Parse(mysqlstatus2["Qcache_hits"]);

            Qcache_inserts1 = long.Parse(mysqlstatus1["Qcache_inserts"]);
            Qcache_inserts2 = long.Parse(mysqlstatus2["Qcache_inserts"]);

            Open_tables1 = long.Parse(mysqlstatus2["Open_tables"]) - long.Parse(mysqlstatus1["Open_tables"]);
            Open_tables2 = long.Parse(mysqlstatus2["Open_tables"]);

            Opened_tables1 = long.Parse(mysqlstatus2["Opened_tables"]) - long.Parse(mysqlstatus1["Opened_tables"]);
            Opened_tables2 = long.Parse(mysqlstatus2["Opened_tables"]);

            Threads_created1 = long.Parse(mysqlstatus1["Threads_created"]);
            Threads_created2 = long.Parse(mysqlstatus2["Threads_created"]);

            Connections1 = long.Parse(mysqlstatus1["Connections"]);
            Connections2 = long.Parse(mysqlstatus2["Connections"]);
            Threads_connected1 = (long.Parse(mysqlstatus2["Threads_connected"]) - long.Parse(mysqlstatus1["Threads_connected"]));
            Threads_connected2 = long.Parse(mysqlstatus2["Threads_connected"]);
            Aborted_connects1 = (long.Parse(mysqlstatus2["Aborted_connects"]) - long.Parse(mysqlstatus1["Aborted_connects"]));
            Aborted_connects2 = long.Parse(mysqlstatus2["Aborted_connects"]);

            Com_select1 = long.Parse(mysqlstatus1["Com_select"]);
            Com_select2 = long.Parse(mysqlstatus2["Com_select"]);
            Com_insert1 = long.Parse(mysqlstatus1["Com_insert"]);
            Com_insert2 = long.Parse(mysqlstatus2["Com_insert"]);
            Com_update1 = long.Parse(mysqlstatus1["Com_update"]);
            Com_update2 = long.Parse(mysqlstatus2["Com_update"]);
            Com_delete1 = long.Parse(mysqlstatus1["Com_delete"]);
            Com_delete2 = long.Parse(mysqlstatus2["Com_delete"]);
            Com_replace1 = long.Parse(mysqlstatus1["Com_replace"]);
            Com_replace2 = long.Parse(mysqlstatus2["Com_replace"]);

            Table_locks_waited1 = long.Parse(mysqlstatus1["Table_locks_waited"]);
            Table_locks_waited2 = long.Parse(mysqlstatus2["Table_locks_waited"]);

            Table_locks_immediate1 = long.Parse(mysqlstatus1["Table_locks_immediate"]);
            Table_locks_immediate2 = long.Parse(mysqlstatus2["Table_locks_immediate"]);

            Created_tmp_tables1 = long.Parse(mysqlstatus1["Created_tmp_tables"]);
            Created_tmp_tables2 = long.Parse(mysqlstatus2["Created_tmp_tables"]);

            Created_tmp_disk_tables1 = long.Parse(mysqlstatus1["Created_tmp_disk_tables"]);
            Created_tmp_disk_tables2 = long.Parse(mysqlstatus2["Created_tmp_disk_tables"]);

            Slow_queries1 = long.Parse(mysqlstatus1["Slow_queries"]);
            Slow_queries2 = long.Parse(mysqlstatus2["Slow_queries"]);

            Select_full_join1 = long.Parse(mysqlstatus1["Select_full_join"]);
            Select_full_join2 = long.Parse(mysqlstatus2["Select_full_join"]);

            Handler_read_rnd_next1 = long.Parse(mysqlstatus1["Handler_read_rnd_next"]);
            Handler_read_rnd_next2 = long.Parse(mysqlstatus2["Handler_read_rnd_next"]);
            Handler_read_rnd1 = long.Parse(mysqlstatus1["Handler_read_rnd"]);
            Handler_read_rnd2 = long.Parse(mysqlstatus2["Handler_read_rnd"]);
            Handler_read_first1 = long.Parse(mysqlstatus1["Handler_read_first"]);
            Handler_read_first2 = long.Parse(mysqlstatus2["Handler_read_first"]);
            Handler_read_next1 = long.Parse(mysqlstatus1["Handler_read_next"]);
            Handler_read_next2 = long.Parse(mysqlstatus2["Handler_read_next"]);
            Handler_read_key1 = long.Parse(mysqlstatus1["Handler_read_key"]);
            Handler_read_key2 = long.Parse(mysqlstatus2["Handler_read_key"]);
            Handler_read_prev1 = long.Parse(mysqlstatus1["Handler_read_prev"]);
            Handler_read_prev2 = long.Parse(mysqlstatus2["Handler_read_prev"]);

            Innodb_buffer_pool_pages_total1 = long.Parse(mysqlstatus1["Innodb_buffer_pool_pages_total"]);
            Innodb_buffer_pool_pages_total2 = long.Parse(mysqlstatus2["Innodb_buffer_pool_pages_total"]);
            Innodb_buffer_pool_pages_free1 = long.Parse(mysqlstatus1["Innodb_buffer_pool_pages_free"]);
            Innodb_buffer_pool_pages_free2 = long.Parse(mysqlstatus2["Innodb_buffer_pool_pages_free"]);


            //#计算参数###################################################################
            string Uptimes1 = (interval) + "s";
            string Uptimes2 = f_sec2dhms(Uptime2);
            // QPS = Questions / Seconds
            string QPS1 = string.Format("{0}", Math.Round((Questions2 - Questions1) * 1.0 / interval, 2));
            string QPS2 = string.Format("{0}", Math.Round(Questions2 * 1.0 / Uptime2, 2));

            string TPS1 = string.Format("{0}", Math.Round((Com_commit2 + Com_rollback2 - Com_commit1 - Com_rollback1) * 1.0 / interval, 2));
            string TPS2 = string.Format("{0}", Math.Round((Com_commit2 + Com_rollback2) * 1.0 / Uptime2, 2));

            long Read1 = Com_select2 + Qcache_hits2 - Com_select1 - Qcache_hits1;
            long Read2 = Com_select2 + Qcache_hits2;
            string ReadS1 = string.Format("{0}", Math.Round(Read1 * 1.0 / interval, 2));
            string ReadS2 = string.Format("{0}", Math.Round(Read2 * 1.0 / Uptime2, 2));

            long Write1 = Com_insert2 + Com_update2 + Com_delete2 + Com_replace2 - Com_insert1 - Com_update1 - Com_delete1 - Com_replace1;
            long Write2 = Com_insert2 + Com_update2 + Com_delete2 + Com_replace2;
            string WriteS1 = string.Format("{0}", Math.Round(Write1 * 1.0 / interval, 2));
            string WriteS2 = string.Format("{0}", Math.Round(Write2 * 1.0 / Uptime2, 2));
            // Read/Write Ratio
            string rwr1 = "";
            string rwr2 = "";
            if (Write1 != 0) {
                rwr1 = string.Format("{0}", Math.Round(Read1 * 1.0 / Write1, 2));
            } else {
                rwr1 = "0.0%";
            }
            if (Write2 != 0) {
                rwr2 = string.Format("{0}", Math.Round(Read2 * 1.0 / Write2, 2));
            }
            else {
                rwr2 = "0.0%";
            }

            string Slow_queries_per_second1 = string.Format("{0}", Math.Round((Slow_queries2 - Slow_queries1) * 1.0 / interval, 2));
            string Slow_queries_per_second2 = string.Format("{0}", Math.Round(Slow_queries2 * 1.0 / Uptime2, 2));
            //Slow_queries / Questions
            string SQ1 = string.Format("{0}", Math.Round(((Slow_queries2 - Slow_queries1) * 1.0 / (Questions2 - Questions1)) * 100, 2)) + "%";
            string SQ2 = string.Format("{0}", Math.Round((Slow_queries2 * 1.0 / Questions2) * 100, 2)) + "%";
            string Thread_cache_hits1 = "";

            if ((Connections2 - Connections1) != 0) {
                Thread_cache_hits1 = string.Format("{0}", Math.Round((1 - (Threads_created2 - Threads_created1) * 1.0 / (Connections2 - Connections1)) * 100, 2)) + "%";
            } else {
                Thread_cache_hits1 = "0.0%";
            }
            string Thread_cache_hits2 = string.Format("{0}", Math.Round((1 - Threads_created2 * 1.0 / Connections2) * 100, 2)) + "%";
            string Innodb_buffer_read_hits1 = "";
            string Innodb_buffer_read_hits2 = "";
            if ((Innodb_buffer_pool_read_requests2 - Innodb_buffer_pool_read_requests1) != 0) {
                Innodb_buffer_read_hits1 = string.Format("{0}", Math.Round((1 - (Innodb_buffer_pool_reads2 - Innodb_buffer_pool_reads1) * 1.0 / (Innodb_buffer_pool_read_requests2 - Innodb_buffer_pool_read_requests1)) * 100, 2)) + "%";
            } else {
                Innodb_buffer_read_hits1 = "0.0%";
            }
            Innodb_buffer_read_hits2 = string.Format("{0}", Math.Round((1 - Innodb_buffer_pool_reads2 * 1.0 / Innodb_buffer_pool_read_requests2) * 100, 2)) + "%";

            string Innodb_buffer_pool_utilization1 = string.Format("{0}", Math.Round((Innodb_buffer_pool_pages_total1 - Innodb_buffer_pool_pages_free1) * 1.0 / Innodb_buffer_pool_pages_total1 * 100, 2)) + "%";
            string Innodb_buffer_pool_utilization2 = string.Format("{0}", Math.Round((Innodb_buffer_pool_pages_total2 - Innodb_buffer_pool_pages_free2) * 1.0 / Innodb_buffer_pool_pages_total2 * 100, 2)) + "%";
            string Key_buffer_read_hits1 = "";
            string Key_buffer_read_hits2 = "";
            if ((Key_read_requests2 - Key_read_requests1) != 0) {
                Key_buffer_read_hits1 = string.Format("{0}", Math.Round((1 - (Key_reads2 - Key_reads1) * 1.0 / (Key_read_requests2 - Key_read_requests1)) * 100, 2)) + "%";
            } else {
                Key_buffer_read_hits1 = "0.0%";
            }
            if (Key_read_requests2 != 0) {
                Key_buffer_read_hits2 = string.Format("{0}", Math.Round((1 - Key_reads2 * 1.0 / Key_read_requests2) * 100, 2)) + "%";
            } else {
                Key_buffer_read_hits2 = "0.0%";
            }
            string Key_buffer_write_hits1 = "";
            string Key_buffer_write_hits2 = "";

            if ((Key_write_requests2 - Key_write_requests1) != 0) {
                Key_buffer_write_hits1 = string.Format("{0}", Math.Round((1 - (Key_writes2 - Key_writes1) * 1.0 / (Key_write_requests2 - Key_write_requests1)) * 100, 2)) + "%";
            } else {
                Key_buffer_write_hits1 = "0.0%";
            }
            if (Key_write_requests2 != 0) {
                Key_buffer_write_hits2 = string.Format("{0}", Math.Round((1 - Key_writes2 * 1.0 / Key_write_requests2) * 100, 2)) + "%";
            } else {
                Key_buffer_write_hits2 = "0.0%";
            }
            string Query_cache_hits1 = "";
            string Query_cache_hits2 = "";
            if ((Qcache_hits2 + Qcache_inserts2 - Qcache_hits1 - Qcache_inserts1) > 0) {
                Query_cache_hits1 = string.Format("{0}", Math.Round((((Qcache_hits2 - Qcache_hits1) * 1.0 / (Qcache_hits2 + Qcache_inserts2 - Qcache_hits1 - Qcache_inserts1)) * 100), 2)) + "%";
            } else {
                Query_cache_hits1 = "0.0%";
            }

            if ((Qcache_hits2 + Qcache_inserts2) > 0) {
                Query_cache_hits2 = string.Format("{0}", Math.Round(((Qcache_hits2 * 1.0 / (Qcache_hits2 + Qcache_inserts2)) * 100), 2)) + "%";
            } else {
                Query_cache_hits2 = "0.0%";
            }
            string Select_full_join_per_second1 = "";
            string Select_full_join_per_second2 = "";
            if ((Select_full_join2 - Select_full_join1) > 0) {
                Select_full_join_per_second1 = string.Format("{0}", Math.Round((Select_full_join2 - Select_full_join1) * 1.0 / interval, 2));
            } else {
                Select_full_join_per_second1 = "0.0%";
            }
            Select_full_join_per_second2 = string.Format("{0}", Math.Round(Select_full_join2 * 1.0 / Uptime2, 2));
            string full_select_in_all_select1 = "";
            string full_select_in_all_select2 = "";
            if ((Com_select2 - Com_select1) > 0) {
                full_select_in_all_select1 = string.Format("{0}", Math.Round(((Select_full_join2 - Select_full_join1) * 1.0 / (Com_select2 - Com_select1)) * 100, 2)) + "%";
            } else {
                full_select_in_all_select1 = "0.0%";
            }
            full_select_in_all_select2 = string.Format("{0}", Math.Round((Select_full_join2 * 1.0 / Com_select2) * 100, 2)) + "%";

            //((Handler_read_rnd_next + Handler_read_rnd) / (Handler_read_rnd_next + Handler_read_rnd + Handler_read_first + Handler_read_next + Handler_read_key + Handler_read_prev)).
            string full_table_scans1 = "";
            string full_table_scans2 = "";
            if ((Handler_read_rnd_next2 - Handler_read_rnd_next1 + Handler_read_rnd2 - Handler_read_rnd1 + Handler_read_first2 - Handler_read_first1 + Handler_read_next2 - Handler_read_next1 + Handler_read_key2 - Handler_read_key2 + Handler_read_prev2 - Handler_read_prev1) > 0) {
                full_table_scans1 = string.Format("{0}", Math.Round((Handler_read_rnd_next2 + Handler_read_rnd2 - Handler_read_rnd_next1 - Handler_read_rnd1) * 1.0 / (Handler_read_rnd_next2 - Handler_read_rnd_next1 + Handler_read_rnd2 - Handler_read_rnd1 + Handler_read_first2 - Handler_read_first1 + Handler_read_next2 - Handler_read_next1 + Handler_read_key2 - Handler_read_key2 + Handler_read_prev2 - Handler_read_prev1) * 100, 2)) + "%";
            } else {
                full_table_scans1 = "0.0%";
            }
            full_table_scans2 = string.Format("{0}", Math.Round((Handler_read_rnd_next2 + Handler_read_rnd2) * 1.0 / (Handler_read_rnd_next2 + Handler_read_rnd2 + Handler_read_first2 + Handler_read_next2 + Handler_read_key2 + Handler_read_prev2) * 100, 2)) + "%";
            string lock_contention1 = "";
            string lock_contention2 = "";
            if ((Table_locks_immediate2 - Table_locks_immediate1) > 0) {
                lock_contention1 = string.Format("{0}", Math.Round(((Table_locks_waited2 - Table_locks_waited1) * 1.00 / (Table_locks_immediate2 - Table_locks_immediate1)) * 100, 2)) + "%";
            } else {
                lock_contention1 = "0.0%";
            }
            lock_contention2 = string.Format("{0}", Math.Round((Table_locks_waited2 * 1.00 / Table_locks_immediate2) * 100, 2)) + "%";
            string Temp_tables_to_disk1 = "";
            string Temp_tables_to_disk2 = "";
            if ((Created_tmp_tables2 - Created_tmp_tables1) > 0)
            {
                Temp_tables_to_disk1 = string.Format("{0}", Math.Round(((Created_tmp_disk_tables2 - Created_tmp_disk_tables1) * 1.0 / (Created_tmp_tables2 - Created_tmp_tables1)) * 100, 2)) + "%";
            }
            else
            {
                Temp_tables_to_disk1 = "0.0%";
            }
            Temp_tables_to_disk2 = string.Format("{0}", Math.Round((Created_tmp_disk_tables2 * 1.0 / Created_tmp_tables2) * 100, 2)) + "%";

            //###打印参数###################################################################
            string title = "MySQL Overview";
            string[] style = { "Key,l", "In " + Uptimes1 + ",r", "Total,r" };
            List<string[]> list = new List<string[]>();
            string[] row1 = { "Uptimes", Uptimes1, Uptimes2 };
            string[] row2 = { "QPS (Questions / Seconds)", QPS1, QPS2 };
            string[] row3 = { "TPS ((Commit + Rollback)/ Seconds)", TPS1, TPS2 };
            string[] row4 = { "Reads per second", ReadS1, ReadS2 };
            string[] row5 = { "Writes per second", WriteS1, WriteS2 };
            string[] row6 = { "Read/Writes", rwr1, rwr2 };
            string[] row7 = { "Slow queries per second", Slow_queries_per_second1, Slow_queries_per_second2 };
            string[] row8 = { "Slow_queries/Questions", SQ1, SQ2 };
            string[] row9 = { "Threads connected", Threads_connected1 + "", Threads_connected2 + "" };
            string[] row10 = { "Aborted connects", Aborted_connects1 + "", Aborted_connects2 + "" };
            string[] row11 = { "Thread cache hits (>90%)", Thread_cache_hits1, Thread_cache_hits2 };
            string[] row12 = { "Innodb buffer hits(96% - 99%)", Innodb_buffer_read_hits1, Innodb_buffer_read_hits2 };
            string[] row13 = { "Innodb buffer pool utilization", Innodb_buffer_pool_utilization1, Innodb_buffer_pool_utilization2 };
            string[] row14 = { "Key buffer read hits(99.3% - 99.9%)", Key_buffer_read_hits1 + "", Key_buffer_read_hits2 + "" };
            string[] row15 = { "Key buffer write hits(99.3% - 99.9%)", Key_buffer_write_hits1 + "", Key_buffer_write_hits2 + "" };
            string[] row16 = { "Query Cache Hits", Query_cache_hits1, Query_cache_hits2 };
            string[] row17 = { "Select full join per second", Select_full_join_per_second1, Select_full_join_per_second2 };
            string[] row18 = { "full select in all select", full_select_in_all_select1, full_select_in_all_select2 };
            string[] row19 = { "full table scans", full_table_scans1, full_table_scans2 };
            string[] row20 = { "MyISAM Lock waiting ratio", lock_contention1, lock_contention2 };
            string[] row21 = { "Current open tables", (Open_tables1 + ""), (Open_tables2 + "") };
            string[] row22 = { "Accumulative open tables", (Opened_tables1 + ""), (Opened_tables2 + "") };
            string[] row23 = { "Temp tables to disk(<10%)", Temp_tables_to_disk1, Temp_tables_to_disk2 };
            list.Add(row1);
            list.Add(row2);
            list.Add(row3);
            list.Add(row4);
            list.Add(row5);
            list.Add(row6);
            list.Add(row7);
            list.Add(row8);
            list.Add(row9);
            list.Add(row10);
            list.Add(row11);
            list.Add(row12);
            list.Add(row13);
            list.Add(row14);
            list.Add(row15);
            list.Add(row16);
            list.Add(row17);
            list.Add(row18);
            list.Add(row19);
            list.Add(row20);
            list.Add(row21);
            list.Add(row22);
            list.Add(row23);
            return printTable(list, title, style, reportType);
        }

        public string getSystemParameter(string reportType)
        {
            string title = "System Parameter ";
            string query = "SELECT variable_name,IF(INSTR(variable_name,'size'), " +
                 "CASE  " +
                 "WHEN variable_value>=1024*1024*1024*1024*1024 THEN CONCAT(variable_value/1024/1024/1024/1024/1024,'P') " +
                 "WHEN variable_value>=1024*1024*1024*1024 THEN CONCAT(variable_value/1024/1024/1024/1024,'T') " +
                 "WHEN variable_value>=1024*1024*1024 THEN CONCAT(variable_value/1024/1024/1024,'G') " +
                 "WHEN variable_value>=1024*1024 THEN CONCAT(variable_value/1024/1024,'M') " +
                 "WHEN variable_value>=1024 THEN CONCAT(variable_value/1024,'K') " +
                 "ELSE variable_value END , " +
                 "variable_value)  " +
                 "FROM " + perfor_or_infor + ".global_variables  " +
                 "where variable_name in (" + SYS_PARM_FILTER + ")";
            string[] style = { "parameter_name,l", "value,l" };
            string result = "";
            result += f_print_query_table(title, query, style, reportType);
            result += optimizerSwitch(reportType, perfor_or_infor);
            return result;
        }
        /// <summary>
        /// 查看集群信息
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getReplication(string reportType)
        {
            string title = "Replication";
            string query = "SELECT USER,HOST,command,CONCAT(FLOOR(TIME/86400),'d',FLOOR(TIME/3600)%24,'h',FLOOR(TIME/60)%60,'m',TIME%60,'s') TIMES,state " +
                   "FROM information_schema.processlist WHERE COMMAND = 'Binlog Dump' OR COMMAND = 'Binlog Dump GTID'";
            string[] style = { "USER,l", "HOST,l", "command,l", "TIMES,r", "state,r" };
            return f_print_query_table(title, query, style, reportType);
        }
        /// <summary>
        /// 获取日志错误信息
        /// </summary>
        /// <param name="perfor_or_infor"></param>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getLogFileStatistics(string reportType) {
            List<string[]> list = new List<string[]>();
            string title = "Log File Statistics";
            string[] style = { "start & shutdown:,l" };

            int WarnLog = 0;
            int ErrLog = 0;
            string query = "SELECT variable_value FROM " + perfor_or_infor + ".global_variables where variable_name ='log_error'";
            string filename = SQLHelper.ExcuteQueryValue(query);
            if (filename.IndexOf("/") > -1)//判断是否为Linux路径
            {
                try
                {
                    if (operuser == "" || operpwd == "") return "";
                    SFTPHelper helper = new SFTPHelper(host, operuser, operpwd);
                    if (File.Exists(path + @"\logfile.log"))
                    {
                        File.Delete(path + @"\logfile.log");
                    }
                    helper.Connect();
                    helper.Get(filename, path + @"\logfile.log");
                    helper.Disconnect();
                    FileStream fs = new FileStream(path + @"\logfile.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(fs, Encoding.Default))
                    {
                        string str;
                        while ((str = sr.ReadLine()) != null)
                        {
                            if (str.ToLower().IndexOf("ready for connections") != -1 || str.ToLower().IndexOf("shutdown completed") != -1)
                            {
                                string[] row0 = { str };
                                list.Add(row0);
                            }
                            if (str.ToLower().IndexOf("warning") != -1)
                            {
                                WarnLog += 1;
                            }
                            if (str.ToLower().IndexOf("error") != -1)
                            {
                                ErrLog += 1;
                            }
                        }
                    }
                }
                catch
                {
                    string[] row1 = { filename + " not exists" };
                    list.Add(row1);
                }
            }
            else
            {
                if (host == "localhost" || host == "127.0.0.1")
                {
                    if (File.Exists(filename))
                    {
                        FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(fs, Encoding.Default))
                        {
                            string str;
                            while ((str = sr.ReadLine()) != null)
                            {
                                if (str.ToLower().IndexOf("ready for connections") != -1 || str.ToLower().IndexOf("shutdown completed") != -1)
                                {
                                    string[] row0 = { str };
                                    list.Add(row0);
                                }
                                if (str.ToLower().IndexOf("warning") != -1)
                                {
                                    WarnLog += 1;
                                }
                                if (str.ToLower().IndexOf("error") != -1)
                                {
                                    ErrLog += 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        string[] row1 = { filename + " not exists" };
                        list.Add(row1);
                    }
                }
            }
            if (list.Count > 0)
            {
                string[] row2 = { "Warning & Error Statistics:" };
                string[] row3 = { filename + " contains " + (WarnLog) + " warning(s)." };
                string[] row4 = { filename + " contains " + (ErrLog) + " error(s)." };

                list.Add(row2);
                list.Add(row3);
                list.Add(row4);
                return printTable(list, title, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取连接数
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getConnectionCount(string reportType)
        {
            string title = "Connect Count";
            string query = "SELECT SUBSTRING_INDEX(HOST,':',1) HOSTS,USER,db,command,COUNT(*),SUM(TIME) " +
                   "FROM information_schema.processlist " +
                   "WHERE Command != '' AND DB != 'information_schema' " +
                   "GROUP BY HOSTS,USER,db,command";
            string[] style = { "HOSTS,l", "USER,l", "db,l", "command,l", "COUNT(*),r", "SUM(TIME),r" };
            return f_print_query_table(title, query, style, reportType);
        }
        /// <summary>
        /// 获取平均查询时间
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getAvgQueryTime(string reportType)
        {
            if (mysql_version.IndexOf("5.7") > -1)
            {
                string title = "Avg Query Time";
                string query = "SELECT schema_name,SUM(count_star) COUNT, ROUND((SUM(sum_timer_wait)/SUM(count_star))/1000000) avg_microsec " +
                   "FROM performance_schema.events_statements_summary_by_digest " +
                   "WHERE schema_name IS NOT NULL " +
                   "GROUP BY schema_name";
                string[] style = { "schema_name,l", "COUNT,r", "avg_microsec,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getSlowQueryTopN(string reportType, int topN)
        {
            if (IsSysSchemaExist()) {
                string title = "Slow Query Top" + (topN);
                string query = "SELECT QUERY,db,exec_count,total_latency,max_latency,avg_latency FROM sys.statements_with_runtimes_in_95th_percentile LIMIT " + (topN);
                string[] style = { "QUERY,l", "db,r", "exec_count,r", "total_latency,r", "max_latency,r", "avg_latency,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        /// <summary>
        /// 错误SQL数
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getErrSqlCount(string reportType)
        {
            if (mysql_version.IndexOf("5.7") > -1)
            {
                string title = "Err Sql Count";
                string query = "SELECT schema_name,SUM(sum_errors) err_count " +
                           "FROM performance_schema.events_statements_summary_by_digest " +
                           "WHERE sum_errors > 0 " +
                           "GROUP BY schema_name";
                string[] style = { "schema_name,l", "err_count,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }


        /// <summary>
        /// 获取TOP错误SQL
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public string getErrSqlTopN(string reportType, int topN)
        {
            if (IsSysSchemaExist())
            {
                string title = "Err SQL Top" + (topN);
                string query = "SELECT QUERY,db,exec_count,ERRORS FROM sys.statements_with_errors_or_warnings ORDER BY ERRORS DESC LIMIT " + (topN);
                string[] style = { "QUERY,l", "db,r", "exec_count,r", "ERRORS,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        /// <summary>
        /// 查询分析TopN
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public string getQueryAnalysisTopN(string reportType, int topN)
        {
            if (IsSysSchemaExist())
            {
                string title = "Query Analysis Top" + (topN);
                string query = "SELECT QUERY,full_scan,exec_count,total_latency,lock_latency,rows_sent_avg,rows_examined_avg," +
                                "tmp_tables,tmp_disk_tables,rows_sorted,last_seen " +
                                "FROM sys.statement_analysis " +
                                "where db = '" + db + "' ORDER BY total_latency DESC  LIMIT " + (topN);
                string[] style = {
                     "QUERY,l", "fscan,l",  "ex_cot,r",  "total_ltc,r", "lock_ltc,r",  "rw_st_avg,r",
                  "rw_exm_avg,9,r", "tmp_table,9,r", "tp_dk_tab,9,r", "rows_sort,9,r", "last_seen,19,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取全表扫描TopN
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public string getQueryFullTableScansTopN(string reportType, int topN)
        {
            if (IsSysSchemaExist())
            {
                string title = "Query Full Table Scans Top" + (topN);
                string query = "SELECT QUERY,exec_count,total_latency,no_index_used_count,no_good_index_used_count,no_index_used_pct,rows_sent_avg,rows_examined_avg,last_seen " +
                               "FROM sys.statements_with_full_table_scans " +
                               "where db = '" + db + "' ORDER BY total_latency DESC  LIMIT " + (topN);
                string[] style = {
                     "QUERY,l", "ex_cot,r", "total_ltc,r", "no_idx_use,r",  "n_g_idx_use,r", "n_i_u_pct,r",
                 "rw_st_avg,r", "rw_exm_avg,r","last_seen,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取查询排序TopN
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public string getQuerySortingTopN(string reportType, int topN)
        {
            if (IsSysSchemaExist())
            {
                string title = "Query Sorting Top" + (topN);
                string query = "SELECT QUERY,exec_count,total_latency,sort_merge_passes,avg_sort_merges,sorts_using_scans,sort_using_range," +
                               "rows_sorted,avg_rows_sorted,last_seen " +
                               "FROM sys.statements_with_sorting " +
                               "where db = '" + db + "' ORDER BY avg_rows_sorted DESC  LIMIT " + (topN);
                string[] style = {
                     "QUERY,l", "ex_cot,r",  "total_ltc,r", "st_mg_ps,r",  "avg_st_mg,r", "st_us_scan,r",
                  "st_us_rag,r", "rows_sort,r", "avg_rw_st,r", "last_seen,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取查询临时表TopN
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public string getQueryWithTempTablesTopN(string reportType, int topN)
        {
            if (IsSysSchemaExist())
            {
                string title = "Query With Temp Tables Top" + (topN);
                string query = "SELECT QUERY,exec_count,total_latency,memory_tmp_tables,disk_tmp_tables,avg_tmp_tables_per_query,tmp_tables_to_disk_pct,last_seen " +
                    "FROM sys.statements_with_temp_tables " +
                    "where db = '" + db + "' ORDER BY avg_tmp_tables_per_query DESC  LIMIT " + (topN);
                string[] style = {
                    "QUERY,l",  "ex_cot,r",  "total_ltc,r", "mem_tmp_tab,r",  "dsk_tmp_tab,r", "avg_tt_per_qry,r",
                  "tt_to_dk_pct,r","last_seen,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取数据库大小
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getDatabaseSize(string reportType)
        {
            string title = "Database Size";
            string query = "SELECT table_schema," +
                   "CONCAT(ROUND(SUM(data_length) / (1024 * 1024), 2), 'MB') AS 'Table Size'," +
                   "CONCAT(ROUND(SUM(index_length) / (1024 * 1024), 2), 'MB') AS 'Index Size' ," +
                   "CONCAT(ROUND(SUM(data_length) / (1024 * 1024), 2) + ROUND(SUM(index_length) / (1024 * 1024), 2), 'MB') AS 'DB Size' " +
                   "FROM information_schema.tables GROUP BY table_schema " +
                   "UNION " +
                   "SELECT '*** all ***' table_schema," +
                   "CONCAT(ROUND(SUM(data_length) / (1024 * 1024), 2), 'MB') AS 'Table Size'," +
                   "CONCAT(ROUND(SUM(index_length) / (1024 * 1024), 2), 'MB') AS 'Index Size' ," +
                   "CONCAT(ROUND(SUM(data_length) / (1024 * 1024), 2) + ROUND(SUM(index_length) / (1024 * 1024), 2), 'MB') AS 'DB Size' " +
                   "FROM information_schema.tables";
            string[] style = { "table_schema,l", "Table Size,r", "Index Size,r", "DB Size,r" };
            return f_print_query_table(title, query, style, reportType);
        }
        /// <summary>
        /// 获取对象数
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getObjectCount(string reportType)
        {
            string title = "Object Count";
            string query = "SELECT information_schema.routines.ROUTINE_TYPE AS object_type, COUNT(0) AS COUNT FROM information_schema.routines " +
                 "WHERE information_schema.routines.ROUTINE_SCHEMA='" + db + "' GROUP BY information_schema.routines.ROUTINE_TYPE UNION " +
                 "SELECT information_schema.tables.TABLE_TYPE AS object_type, COUNT(0) AS COUNT FROM information_schema.tables  " +
                 "WHERE information_schema.tables.TABLE_SCHEMA='" + db + "' GROUP BY information_schema.tables.TABLE_TYPE UNION " +
                 "SELECT CONCAT('INDEX (',information_schema.statistics.INDEX_TYPE,')') AS object_type,COUNT(0) AS COUNT FROM information_schema.statistics " +
                 "WHERE information_schema.statistics.TABLE_SCHEMA='" + db + "' GROUP BY information_schema.statistics.INDEX_TYPE UNION " +
                 "SELECT 'TRIGGER' AS `TRIGGER`,COUNT(0) AS COUNT FROM information_schema.triggers " +
                 "WHERE information_schema.triggers.TRIGGER_SCHEMA='" + db + "' UNION " +
                 "SELECT 'EVENT' AS object_type, COUNT(0) AS COUNT FROM information_schema.events " +
                 "WHERE information_schema.events.EVENT_SCHEMA='" + db + "'";
            string[] style = { "object_type,l", "COUNT,r" };
            //if reportType == "txt":
            return f_print_query_table(title, query, style, reportType);
        }
        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getTableInfo(string reportType) {
            string title = "Table Info";
            string query = "select table_name,engine,row_format as format,table_rows,avg_row_length as avg_row," +
                   "round((data_length) / 1024 / 1024, 2) as data_mb," +
                   "round((index_length) / 1024 / 1024, 2) as index_mb," +
                   "round((data_length + index_length) / 1024 / 1024, 2) as total_mb " +
                   "from information_schema.tables " +
                   "where table_schema = '" + db + "'";
            string[] style = { "table_name,l",  "engine,l",  "format,l",  "table_rows,r", "avg_row,r",
                 "data_mb,r",  "index_mb,r",  "total_mb,r"};
            return f_print_query_table(title, query, style, reportType);
        }
        /// <summary>
        /// 获取索引信息
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getIndexInfo(string reportType)
        {
            string title = "Index Info";
            string query = "select index_name,non_unique,seq_in_index,column_name,collation,cardinality,nullable,index_type " +
                   "from information_schema.statistics " +
                   "where table_schema = '" + db + "'";
            string[] style = {
                  "index_name,l",  "non_unique,l",  "seq_in_index,l",  "column_name,l",
                  "collation,r",  "cardinality,r",  "nullable,r",  "index_type,r"};
            return f_print_query_table(title, query, style, reportType);
        }
        /// <summary>
        /// 获取Schema索引统计
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getSchemaIndexStatistics(string reportType) {
            if (IsSysSchemaExist())
            {
                string title = "Schema Index Statistics";
                string query = "SELECT table_name,index_name ,rows_selected,select_latency,rows_inserted,insert_latency,rows_updated," +
                        "update_latency,rows_deleted,delete_latency " +
                        "FROM sys.schema_index_statistics where table_schema = '" + db + "' ORDER BY table_name";
                string[] style = {  "table_name,l",  "index_name,l",  "rows_selected,r",  "select_latency,r", "rows_inserted,r",
                  "insert_latency,r",  "rows_updated,r",  "update_latency,r", "rows_deleted,r", "delete_latency,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取Schema表统计
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getSchemaTableStatistics(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "Schema Table Statistics";
                string query = "SELECT table_name,total_latency ,rows_fetched,fetch_latency,rows_inserted,insert_latency,rows_updated," +
                        "update_latency,rows_deleted,delete_latency,io_read_requests,io_read,io_read_latency,io_write_requests," +
                        "io_write,io_write_latency,io_misc_requests,io_misc_latency " +
                        "FROM sys.schema_table_statistics where table_schema = '" + db + "' ORDER BY table_name";
                string[] style = { "table_name,l", "tal_ltc,r", "rw_ftc,r",  "ftc_ltc,r",
                  "rw_ins,r",  "ins_ltc,r", "rw_upd,r",  "upd_ltc,r",
                  "rw_del,r",  "del_ltc,r",  "io_rd_rq,r",  "io_read,r",
                  "io_rd_ltc,r",  "io_wt_rq,r",  "io_write,r", "io_wt_ltc,r",
                  "io_ms_rq,r", "io_ms_ltc,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取缓冲区Schema表统计信息
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getSchemaTableStatisticsWithBuffer(string reportType) {
            if (IsSysSchemaExist())
            {
                string title = "Schema Table Statistics With Buffer";
                string query = "SELECT table_name,innodb_buffer_allocated,innodb_buffer_data,innodb_buffer_free,innodb_buffer_pages," +
                       "innodb_buffer_pages_hashed,innodb_buffer_pages_old,innodb_buffer_rows_cached " +
                       "FROM sys.schema_table_statistics_with_buffer where table_schema = '" + db + "' ORDER BY table_name";
                string[] style = {  "table_name,l",  "indb_buf_alc,r",  "indb_buf_data,r",  "indb_buf_free,r",  "indb_buf_page,r",
                 "indb_buf_page_hash,r",  "indb_buf_page_old,r",  "indb_buf_rw_cach,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取Schema表全表扫描
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getSchemaTablesWithFullTableScans(string reportType) {
            if (IsSysSchemaExist()) {
                string title = "Schema Tables With Full Table Scans";
                string query = "SELECT object_schema,object_name,rows_full_scanned,latency FROM sys.schema_tables_with_full_table_scans " +
                    "where object_schema = '" + db + "' ORDER BY object_name";
                string[] style = { "object_schema,l", "object_name,l", "rows_full_scanned,r", "latency,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取Schema未使用索引
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getSchemaUnusedIndexes(string reportType) {
            if (IsSysSchemaExist()) {
                string title = "Schema Unused Indexes";
                string query = "SELECT object_schema,object_name,index_name FROM sys.schema_unused_indexes where object_schema='" + db + "'";
                string[] style = { "object_schema,l", "object_name,l", "index_name,l" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取主机概要信息
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getHostSummary(string reportType) {
            if (IsSysSchemaExist()) {
                string title = "Host Summary";
                //#host 监听连接过的主机 statements 当前主机执行的语句总数 statement_latency 语句等待时间（延迟时间） statement_avg_latency 执行语句平均延迟时间 table_scans 表扫描次数
                //#file_ios io时间总数 file_io_latency 文件io延迟 current_connections 当前连接数 total_connections 总链接数 unique_users 该主机的唯一用户数 current_memory 当前账户分配的内存
                //#total_memory_allocated 该主机分配的内存总数
                string query = "SELECT host,statements,statement_latency,statement_avg_latency,table_scans,file_ios,file_io_latency,current_connections," +
                    "total_connections,unique_users,current_memory,total_memory_allocated " +
                    "FROM sys.host_summary";
                string[] style = { "host,l",  "statements,r", "st_ltc,r", "st_avg_ltc,r",  "table_scan,r",
                 "file_ios,r", "f_io_ltc,r", "cur_conns,r", "total_conn,r",
                 "unq_users,r", "cur_mem,r",  "tal_mem_alc,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取主机IO事件概要信息
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getHostSummaryByFileIoType(string reportType) {
            if (IsSysSchemaExist()) {
                string title = "host_summary_by_file_io_type";
                //#•host 主机 event_name IO事件名称 total 该主机发生的事件 total_latency 该主机发生IO事件总延迟时间 max_latency 该主机IO事件中最大的延迟时间
                string query = "SELECT host,event_name,total,total_latency,max_latency " +
                    "FROM sys.host_summary_by_file_io_type";
                string[] style = { "host,l", "event_name,l", "total,r", "total_ltc,r", "max_ltc,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取主机IO事件概要信息
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getHostSummaryByFileIo(string reportType) {
            if (IsSysSchemaExist()) {
                string title = "Host Summary By File Io Type";
                //#•host 主机 ios IO事件总数 io_latency IO总的延迟时间
                string query = "SELECT host,ios,io_latency " +
                    "FROM sys.host_summary_by_file_io";
                string[] style = { "host,l", "ios,r", "io_latency,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取主机存储概要信息
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getHostSummaryByStages(string reportType) {
            if (IsSysSchemaExist())
            {
                string title = "Host Summary By Stages";
                //#•host 主机  event_name stage event名称 total stage event发生的总数 total_latency stage event总的延迟时间 avg_latency stage event平均延迟时间
                string query = "SELECT host,event_name,total,total_latency,avg_latency " +
                        "FROM sys.host_summary_by_stages";
                string[] style = { "host,l", "event_name,l", "total,r", "total_latency,r", "avg_latency,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        public string getHostSummaryByStatementLatency(string reportType) {
            if (IsSysSchemaExist())
            {
                string title = "Host Summary By Statement Latency";
                //#host 主机  total 这个主机的语句总数  total_latency 这个主机总的延迟时间 max_latency 主机最大的延迟时间 lock_latency 等待锁的锁延迟时间
                //#rows_sent 该主机通过语句返回的总行数 rows_examined 在存储引擎上通过语句返回的行数 rows_affected 该主机通过语句影响的总行数 full_scans 全表扫描的语句总数
                string query = "SELECT host,total,total_latency,max_latency,lock_latency,rows_sent,rows_examined,rows_affected,full_scans " +
                        "FROM sys.host_summary_by_statement_latency";
                string[] style = {
                    "host,l",  "total,r",  "total_latency,r",  "max_latency,r", "lock_latency,r",
                      "rows_sent,r",  "rows_examined,r", "rows_affected,r", "full_scans,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getHostSummaryByStatementType(string reportType) {
            if (IsSysSchemaExist())
            {
                string title = "Host Summary By Statement Type";
                //#host 主机  statement 最后的语句事件名称 total sql语句总数 total_latency sql语句总延迟数 max_latency 最大的sql语句延迟数
                //# lock_latency 锁延迟总数 rows_sent 语句返回的行总数 rows_examined 通过存储引擎的sql语句的读取的总行数 rows_affected 语句影响的总行数 full_scans 全表扫描的语句事件总数
                string query = "SELECT host,statement,total,total_latency,max_latency,lock_latency,rows_sent,rows_examined,rows_affected,full_scans " +
                            "FROM sys.host_summary_by_statement_type";
                string[] style = {
                     "host,l",  "statement,l", "total,r",  "total_latency,r",  "max_latency,r",
                      "lock_latency,r",  "rows_sent,r", "rows_examined,r","rows_affected,r", "full_scans,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取用户概要信息
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getUserSummary(string reportType) {
            if (IsSysSchemaExist())
            {
                string title = "User Summary";
                //# statements 当前用户执行的语句总数 statement_latency 语句等待时间（延迟时间） statement_avg_latency 执行语句平均延迟时间 table_scans 表扫描次数
                //#file_ios io时间总数 file_io_latency 文件io延迟 current_connections 当前连接数 total_connections 总链接数 unique_users 该用户的唯一主机数 current_memory 当前账户分配的内存
                //#total_memory_allocated 该主机分配的内存总数
                string query = "SELECT user,statements,statement_latency,statement_avg_latency,table_scans,file_ios,file_io_latency,current_connections," +
                    "total_connections,unique_hosts,current_memory,total_memory_allocated " +
                    "FROM sys.user_summary";
                string[] style = {  "user,l",  "statements,r",  "st_ltc,r",  "st_avg_ltc,r",  "table_scan,r",
                  "file_ios,r", "f_io_ltc,r", "cur_conns,r",  "total_conn,r",
                  "unq_hosts,r",  "cur_mem,r",  "tal_mem_alc,r"};
                return f_print_query_table( title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取用户IO事件概要
        /// event_name IO事件名称 total 该用户发生的事件 total_latency 该用户发生IO事件总延迟时间 max_latency 该用户IO事件中最大的延迟时间
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getUserSummaryByFileIoType(string reportType) {
            if (IsSysSchemaExist()) {
                string title = "User Summary By File IO Type";
                //#event_name IO事件名称 total 该用户发生的事件 total_latency 该用户发生IO事件总延迟时间 max_latency 该用户IO事件中最大的延迟时间
                string query = "SELECT user,event_name,total,latency,max_latency " +
                    "FROM sys.user_summary_by_file_io_type";
                string[] style = { "user,l", "event_name,l", "total,r", "latency,r", "max_ltc,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        /// <summary>
        /// 获取用户IO事件概要信息
        /// ios IO事件总数 io_latency IO总的延迟时间
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public string getUserSummaryByFileIo(string reportType) {
            if (IsSysSchemaExist())
            {
                string title = "User Summary By File IO Type";
                //# ios IO事件总数 io_latency IO总的延迟时间
                string query = "SELECT user,ios,io_latency FROM sys.user_summary_by_file_io";
                string[] style = { "user,l", "ios,r", "io_latency,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getUserSummaryByStages(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "User Summary By Stages";
                //#  event_name stage event名称 total stage event发生的总数 total_latency stage event总的延迟时间 avg_latency stage event平均延迟时间
                string query = "SELECT user,event_name,total,total_latency,avg_latency " +
                            "FROM sys.user_summary_by_stages";
                string[] style = { "user,l", "event_name,l", "total,r", "total_latency,r", "avg_latency,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
       }
        public string getUserSummaryByStatementLatency(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "User Summary By Statement Latency";
                //#  total 这个主机的语句总数  total_latency 这个主机总的延迟时间 max_latency 主机最大的延迟时间 lock_latency 等待锁的锁延迟时间
                //#rows_sent 该主机通过语句返回的总行数 rows_examined 在存储引擎上通过语句返回的行数 rows_affected 该主机通过语句影响的总行数 full_scans 全表扫描的语句总数
                string query = "SELECT user,total,total_latency,max_latency,lock_latency,rows_sent,rows_examined,rows_affected,full_scans " +
                        "FROM sys.user_summary_by_statement_latency";
                string[] style = {
                "user,l", "total,r", "total_latency,r", "max_latency,r", "lock_latency,r",
                 "rows_sent,r", "rows_examined,r", "rows_affected,r","full_scans,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getUserSummaryByStatementType(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "User Summary By Statement Type";
                //#statement 最后的语句事件名称 total sql语句总数 total_latency sql语句总延迟数 max_latency 最大的sql语句延迟数
                //# lock_latency 锁延迟总数 rows_sent 语句返回的行总数 rows_examined 通过存储引擎的sql语句的读取的总行数 rows_affected 语句影响的总行数 full_scans 全表扫描的语句事件总数
                string query = "SELECT user,statement,total,total_latency,max_latency,lock_latency,rows_sent,rows_examined,rows_affected,full_scans " +
                        "FROM sys.user_summary_by_statement_type";
                string[] style = {
            "user,l",  "statement,l", "total,r", "total_latency,r", "max_latency,r",
                 "lock_latency,r", "rows_sent,r", "rows_examined,r","rows_affected,r","full_scans,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getInnodbBufferStatsBySchema(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "Innodb Buffer Stats By Schema";
                //#object_schema 数据库名称 allocated 分配给当前数据库的总的字节数 data 分配给当前数据库的数据字节数 pages 分配给当前数据库的总页数
                //# pages_hashed 分配给当前数据库的hash页数 pages_old 分配给当前数据库的旧页数  rows_cached 当前数据库缓存的行数
                string query = "SELECT object_schema,allocated,data,pages,pages_hashed,pages_old,rows_cached " +
                        "FROM sys.innodb_buffer_stats_by_schema";
                string[] style = {
                "object_schema,l",  "allocated,r", "data,r", "pages,r", "pages_hashed,r",
                 "pages_old,r", "rows_cached,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getInnodbBufferStatsByTable(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "Innodb Buffer Stats By Table";
                //# object_schema 数据库名称 object_name 表名称 allocated 分配给表的总字节数 data 分配该表的数据字节数 pages 分配给表的页数
                //#  pages_hashed 分配给表的hash页数 pages_old 分配给表的旧页数 rows_cached 表的行缓存数
                string query = "SELECT object_schema,object_name,allocated,data,pages,pages_hashed,pages_old,rows_cached " +
                        "FROM sys.innodb_buffer_stats_by_table";
                string[] style = {
                 "object_schema,l",  "object_name,l",  "allocated,r",  "data,r",  "pages,r",
                  "pages_hashed,r",  "pages_old,r",  "rows_cached,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getIoByThreadByLatencyTopN(string reportType, int topN)
        {
            if (IsSysSchemaExist())
            {
                string title = "Io By Thread By Latency Top" + (topN);
                //#user 对于当前线程来说，这个值是线程被分配的账户，对于后台线程来讲，就是线程的名称 total IO事件的总数 total_latency IO事件的总延迟
                //# min_latency 单个最小的IO事件延迟 avg_latency 平均IO延迟 max_latency 最大IO延迟 thread_id 线程ID processlist_id 对于当前线程就是此时的ID，对于后台就是null
                string query = "SELECT user,total,total_latency,min_latency,avg_latency,max_latency,thread_id,processlist_id " +
                        "FROM sys.io_by_thread_by_latency LIMIT " + (topN);
                string[] style = {
                "user,l", "total,r",  "total_latency,r", "min_latency,r",  "avg_latency,r",
                "max_latency,r", "thread_id,r", "processlist_id,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getIoGlobalByFileByBytesTopN(string reportType, int topN)
        {
            if (IsSysSchemaExist())
            {
                string title = "Io Global By File By Bytes Top" + (topN);
                string query = "SELECT file,count_read,total_read,avg_read,count_write,total_written,avg_write,total,write_pct " +
                        "FROM sys.io_global_by_file_by_bytes LIMIT " + (topN);
                string[] style = {
                "file,l", "count_read,r", "total_read,r", "avg_read,r", "count_write,r",
                "total_written,r", "avg_write,r", "total,r", "write_pct,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getIoGlobalByFileByLatencyTopN(string reportType, int topN)
        {
            if (IsSysSchemaExist())
            {
                string title = "Io Global By File By Latency Top" + (topN);
                string query = "SELECT file,total,total_latency,count_read,read_latency,count_write,write_latency,count_misc,misc_latency " +
                        "FROM sys.io_global_by_file_by_latency LIMIT " + (topN);
                string[] style = {
                "file,l", "total,r", "total_latency,r", "count_read,r", "read_latency,r",
                "count_write,r", "write_latency,r", "count_misc,r", "misc_latency,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getIoGlobalByWaitByBytesTopN(string reportType, int topN)
        {
            if (IsSysSchemaExist())
            {
                string title = "Io Global By Wait By Bytes Top" + (topN);
                string query = "SELECT event_name,total,total_latency,min_latency,avg_latency,max_latency,count_read,total_read,avg_read,count_write," +
                        "total_written,avg_written,total_requested " +
                        "FROM sys.io_global_by_wait_by_bytes LIMIT " + (topN);
                string[] style = {
                "event_name,l", "total,r", "total_latency,r", "min_latency,r", "avg_latency,r",
                "max_latency,r", "count_read,r", "total_read,r", "avg_read,r", "count_write,r",
                "total_written,r", "avg_written,r", "total_requested,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getIoGlobalByWaitByLatencyTopN(string reportType, int topN)
        {
            if (IsSysSchemaExist())
            {
                string title = "Io Global By Wait By Latency Top" + (topN);
                string query = "SELECT event_name,total,total_latency,avg_latency,max_latency,read_latency,write_latency,misc_latency,count_read," +
                        "total_read,avg_read,count_write,total_written,avg_written " +
                        "FROM sys.io_global_by_wait_by_latency LIMIT " + (topN);
                string[] style = {
                "event_name,l", "total,r", "total_latency,r", "avg_latency,r", "max_latency,r",
                "read_latency,r", "write_latency,r", "misc_latency,r", "count_read,r",  "total_read,r",
                "avg_read,r", "count_write,r", "total_written,r", "avg_written,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getWaitClassesGlobalByAvgLatency(string reportType)
        {
            if (IsSysSchemaExist()) {
                string title = "Wait Classes Global By Avg Latency";
                string query = "SELECT event_class,total,total_latency,min_latency,avg_latency,max_latency " +
                    "FROM sys.wait_classes_global_by_avg_latency";
                string[] style = { "event_class,l", "total,r", "total_latency,r", "min_latency,r", "avg_latency,r", "max_latency,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getWaitClassesGlobalByLatency(string reportType)
        {
            if (IsSysSchemaExist()) {
                string title = "Wait Classes Global By Latency";
                string query = "SELECT event_class,total,total_latency,min_latency,avg_latency,max_latency " +
                    "FROM sys.wait_classes_global_by_latency";
                string[] style = { "event_class,l", "total,r", "total_latency,r", "min_latency,r", "avg_latency,r","max_latency,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getWaitsByHostByLatency(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "Waits By Host By Latency";
                string query = "SELECT HOST,EVENT,total,total_latency,avg_latency,max_latency " +
                    "FROM sys.waits_by_host_by_latency ";
                string[] style = { "host,l","event,l", "total,r", "total_latency,r","avg_latency,r","max_latency,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getWaitsByUserByLatency(string reportType)
        {
            if (IsSysSchemaExist()) {
                string title = "Waits By User By Latency";
                string query = "SELECT USER,EVENT,total,total_latency,avg_latency,max_latency " +
                    "FROM sys.waits_by_user_by_latency ";
                string[] style = {  "user,l","event,l", "total,r", "total_latency,r", "avg_latency,r",  "max_latency,r"};
                return f_print_query_table( title, query, style, reportType);
            }
            return "";
        }

        public string getWaitsGlobalByLatency(string reportType)
        {
            string title = "Waits Global By Latency";
            string query = "SELECT EVENTS,total,total_latency,avg_latency,max_latency " +
                    "FROM sys.waits_global_by_latency ";
            string[] style = { "event,l", "total,r", "total_latency,r", "avg_latency,r", "max_latency,r" };
            return f_print_query_table( title, query, style, reportType);
        }

        public string getSchemaTableLockWaits(string reportType)
        {
            if (IsSysSchemaExist()) {
                string title = "Schema Table Lock Waits";
                string query = "SELECT object_schema,object_name,waiting_account,waiting_lock_type,"+
                    "waiting_lock_duration,waiting_query,waiting_query_secs,waiting_query_rows_affected,waiting_query_rows_examined,"+
                    "blocking_account,blocking_lock_type,blocking_lock_duration "+
                    "FROM sys.schema_table_lock_waits";
                //#,sql_kill_blocking_query,sql_kill_blocking_connection
                string[] style = {
                    "object_schema,l", "object_name,r",  "wait_account,r",  "wt_lk_tp,l",  "w_l_dur,l",
                    "waiting_query,l",  "w_qry_s,l", "w_q_r_a,l","w_q_r_e,l", "blk_account,l",
                    "bk_lk_tp,l", "b_l_dur,l"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getInnodbLockWaits(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "Innodb Lock Waits";
                //#wait_started 锁等待发生的时间 wait_age 锁已经等待了多长时间 wait_age_secs 以秒为单位显示锁已经等待的时间
                //#locked_table 被锁的表 locked_index 被锁住的索引 locked_type 锁类型 waiting_trx_id 正在等待的事务ID waiting_trx_started 等待事务开始的时间
                //#waiting_trx_age 已经等待事务多长时间 waiting_trx_rows_locked 正在等待的事务被锁的行数量 waiting_trx_rows_modified 正在等待行重定义的数量
                //#waiting_pid 正在等待事务的线程id waiting_query 正在等待锁的查询 waiting_lock_id 正在等待锁的ID waiting_lock_mode 等待锁的模式
                //#blocking_trx_id 阻塞等待锁的事务id blocking_pid 正在锁的线程id blocking_query 正在锁的查询 blocking_lock_id 正在阻塞等待锁的锁id.
                //#blocking_lock_mode 阻塞锁模式 blocking_trx_started 阻塞事务开始的时间 blocking_trx_age 阻塞的事务已经执行的时间 blocking_trx_rows_locked 阻塞事务锁住的行的数量
                //# blocking_trx_rows_modified 阻塞事务重定义行的数量 sql_kill_blocking_query kill 语句杀死正在运行的阻塞事务 sql_kill_blocking_connection kill 语句杀死会话中正在运行的阻塞事务
                string query = "SELECT wait_started,wait_age,locked_table,locked_index,locked_type,waiting_query,waiting_lock_mode,blocking_query,blocking_lock_mode " +
                        "FROM sys.innodb_lock_waits";
                string[] style = {
                "wait_start,l", "wait_age,r", "locked_table,r", "locked_index,l",  "locked_type,l",
                "waiting_query,l", "wt_lk_md,l", "blocking_query,l", "bk_lk_md,l"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        public string getMemoryByHostByCurrentBytes(string reportType)
        {
            if (IsSysSchemaExist()) {
                string title = "Memory By Host By Current Bytes";
                string query = "SELECT HOST,current_count_used,current_allocated,current_avg_alloc,current_max_alloc,total_allocated " +
                        "FROM sys.memory_by_host_by_current_bytes";
                string[] style = { "HOST,l", "crt_count_used,r", "crt_allocatedc,r", "crt_avg_alloc,r", "crt_max_alloc,r", "tal_alloc,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getMemoryByThreadByCurrentBytes(string reportType)
        {
            if (IsSysSchemaExist()) {
                string title = "Memory By Thread By Current Bytes";
                string query = "SELECT thread_id,USER,current_count_used,current_allocated,current_avg_alloc,current_max_alloc,total_allocated " +
                        "FROM sys.memory_by_thread_by_current_bytes ORDER BY thread_id";
                string[] style = { "thread_id,r", "USER,l", "crt_count_used,r", "crt_allocatedc,r", "crt_avg_alloc,r", "crt_max_alloc,r", "tal_alloc,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getMemoryByUserByCurrentBytes(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "Memory By User By Current Bytes";
                string query = "SELECT USER,current_count_used,current_allocated,current_avg_alloc,current_max_alloc,total_allocated " +
                        "FROM sys.memory_by_user_by_current_bytes";
                string[] style = { "USER,l", "crt_count_used,r", "crt_alloc,r", "crt_avg_alloc,r", "crt_max_alloc,r", "tal_alloc,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getMemoryGlobalByCurrentBytes(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "Memory Global By Current Bytes";
                string query = "SELECT event_name,current_count,current_alloc,current_avg_alloc,high_count,high_alloc,high_avg_alloc " +
                        "FROM sys.memory_global_by_current_bytes ORDER BY current_alloc DESC";
                string[] style = { "event_name,l", "crt_count,r", "crt_alloc,r", "crt_avg_alloc,r", "high_count,r", "high_alloc,r", "high_avg_alloc,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }
        public string getMemoryGlobalTotal(string reportType)
        {
            if (IsSysSchemaExist()) {
                string title = "Memory Global Total";
                string query = "SELECT total_allocated FROM sys.memory_global_total";
                string[] style = { "total_allocated,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getProcesslist(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "Processlist";
                string query = "SELECT thd_id,USER,command,TIME,current_statement,statement_latency,full_scan,last_statement,last_statement_latency " +
                        "FROM sys.processlist " +
                        "where db = '" + db + "'";
                string[] style = {
                "thd_id,r", "USER,l", "command,r", "TIME,r", "current_sql,r","sql_ltc,r",
                "fscan,r","last_sql,r","lsql_ltc,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getSession(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "Session";
                string query = "SELECT thd_id,USER,command,TIME,current_statement,statement_latency,lock_latency,full_scan,last_statement,last_statement_latency " +
                        "FROM sys.session " +
                        "where db = '" + db + "'";
                string[] style = {
                "thd_id,r", "USER,l", "command,r", "TIME,r", "current_sql,r","sql_ltc,r",
                "lock_ltc,r","fscan,r","last_sql,r","lsql_ltc,r"};
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getMetrics(string reportType)
        {
            if (IsSysSchemaExist())
            {
                string title = "Metrics";
                string query = "SELECT Variable_name,Variable_value,TYPE,Enabled " +
                    "FROM sys.metrics " +
                    "WHERE Variable_name <> 'rsa_public_key'  and Variable_name<> 'ssl_cipher_list' and Enabled = 'YES'";
                string[] style = { "Variable_name,l", "Variable_value,r", "TYPE,l", "Enabled,r" };
                return f_print_query_table(title, query, style, reportType);
            }
            return "";
        }

        public string getBinlogs(string reportType)
        {
            string html = "";
            string query = "show master logs";
            DataTable loglist = SQLHelper.ExcuteQuery(query);
            if (loglist != null && loglist.Rows.Count > 0)
            {
                for (int i = 0; i < loglist.Rows.Count; i++)
                {
                    string log_name = loglist.Rows[i]["Log_name"].ToString();
                    html+=getBinlog(log_name, reportType);
                    List<string> list = getBinlog(log_name);
                    foreach(string s in list)
                    {
                        html += s;
                    }
                }
            }
            return html;
        }

        private string getBinlog(string logname,string reportType)
        {
            
            string title = "Binlog "+ logname;
            string query = "show binlog events in '"+logname+"'";
            string[] style = { "Log_name,l", "Pos,r", "Event_type,r", "Server_id,l", "End_log_pos,r", "Info,l" };
            return f_print_query_table(title, query, style, reportType);
        }

        private List<string> getBinlog(string logname)
        {
            string query = "show binlog events in '" + logname + "'";
            List<string> list = new List<string>();
            DataTable dt = SQLHelper.ExcuteQuery(query);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int pos = int.Parse(dt.Rows[i]["Pos"].ToString());
                int end_log_pos = int.Parse(dt.Rows[i]["End_log_pos"].ToString());
                string event_type = dt.Rows[i]["Event_type"].ToString();
                if (event_type == "Format_desc")
                {

                }

                if (event_type == "Table_map")
                {
                    
                }

                if (event_type == "Write_rows")
                {
                    //ReadFileByByte(logname, pos, end_log_pos - pos);
                }
                string result = ReadFileByByte(logname, pos, end_log_pos);
                list.Add(result);
            }
            return list;
        }


        /// <summary>
        /// 用二进制方式读取文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>读取到的数据</returns>
        public string ReadFileByByte(String fileName,int offset,int end_log_pos)
        {
            string result = "";
            FileStream fs = new FileStream(@"C:\Program Files\mysql-5.7.12\data\" + fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            byte[] data = new byte[fs.Length];

            try
            {
                int read = fs.Read(data, 0, data.Length);

                //| timestamp（0:4）| type_code（4:1）| server_id（5:4）| event_length（9:4）| next_position(13:4) | flags（17:2）| extra_header（19：x - 19）|

                byte[] file_header = new byte[4];//fe 62 69 6e
                Array.Copy(data, file_header, 4);

                byte[] _timestamp = new byte[4];
                Array.Copy(data, 0+ offset, _timestamp,0,4);
                byte _type = data[4+ offset];//事件类型
                byte[] _server_id = new byte[4];
                Array.Copy(data, 5+ offset, _server_id, 0, 4);
                byte[] _total_size = new byte[4];
                Array.Copy(data, 9 + offset, _total_size, 0, 4);
                byte[] _end_log_pos = new byte[4];
                Array.Copy(data, 13 + offset, _end_log_pos, 0, 4);
                byte[] _flag = new byte[2];
                Array.Copy(data, 17 + offset, _flag, 0, 2);
                byte extra_header = data[19 + offset];//未使用
                

                if (_type == (int)Log_event_type.START_EVENT_V3)
                {
                    //1、前19个字节为事件头部（参考文件格式部分）
                    //2、第19 - 20字节为版本信息
                    //3、第21 - 70字节为服务器版本信息
                    //4、第71 - 74字节为创建时间
                }
                if (_type == (int)Log_event_type.QUERY_EVENT)
                {
                    //前边30个字节为辅助字段，其余为事件内容
                    //1、前19个字节为事件头部（参考文件格式部分）
                    //2、第19 - 22个字节为代理线程ID
                    //3、第23 - 26字节为事件执行时间
                    //4、第27个字节保存数据库名称的长度
                    //5、第28 - 29字节为错误码
                    //6、第30个字节开始为数据库名，具体长度由第27个字节决定
                    //7、剩余部分：第一个字节为’\0’，剩余部分为具体的查询语句
                    
                    //• 4 bytes.The ID of the thread that issued this statement.Needed for temporary tables. This is also useful for a DBA for knowing who did what on the master.
                    //• 4 bytes.The time in seconds that the statement took to execute.Only useful for inspection by the DBA.
                    //• 1 byte.The length of the name of the database which was the default database when the statement was executed.This name appears later, in the variable data part.It is necessary for statements such as INSERT INTO t VALUES(1) that don't specify the database and rely on the default database previously selected by USE. 
                    //• 2 bytes.The error code resulting from execution of the statement on the master.Error codes are defined in include / mysqld_error.h. 0 means no error.How come statements with a nonzero error code can exist in the binary log ? This is mainly due to the use of nontransactional tables within transactions.For example, if an INSERT...SELECT fails after inserting 1000 rows into a MyISAM table(for example, with a duplicate - key violation), we have to write this statement to the binary log, because it truly modified the MyISAM table.For transactional tables, there should be no event with a nonzero error code(though it can happen, for example if the connection was interrupted (Control-C)). The slave checks the error code: After executing the statement itself, it compares the error code it got with the error code in the event, and if they are different it stops replicating (unless --slave-skip-errors was used to ignore the error). 
                    //• 2 bytes (not present in v1, v3). The length of the status variable block. 


                    byte[] agent_thread = new byte[4];
                    Array.Copy(data, 19 + offset, agent_thread, 0, 4);
                    byte[] event_time = new byte[4];
                    Array.Copy(data, 23 + offset, event_time, 0, 4);
                    byte dbname_length = data[27+offset];
                    byte[] error_code = new byte[2];
                    Array.Copy(data, 28 + offset, error_code, 0, 2);
                    byte[] status = new byte[2];
                    Array.Copy(data, 30 + offset, status, 0, 2);

                    char[] dbname = new char[dbname_length];
                    Array.Copy(data, 60+ offset, dbname, 0, dbname_length);
                    char[] dbname2 = new char[dbname_length];
                    Array.Copy(data, 60 + offset+ dbname_length+1, dbname2, 0, dbname_length);

                    string msql = "";
                    int sql_offset = 58;
                    if (new string(dbname) == new string(dbname2))
                    {
                        sql_offset = 60 + dbname_length + 1;
                    }
                    char[] sql = new char[end_log_pos - offset];
                    Array.Copy(data, offset, sql, 0, end_log_pos - offset);
                    for (int i = 0; i < sql.Length; i++)
                    {
                        if (sql[i] != '\0')
                        {
                            msql += sql[i];
                        }
                    }

                    result += "use '"+ new string(dbname)+"';<br/>";
                    result += msql + ";<br/>";
                    Console.WriteLine(msql);
                }
                if (_type == (int)Log_event_type.ROTATE_EVENT)
                {
                    //当切换到一个新的日志文件时记录（使用顺序文件序号时该事件被废除）：
                    //1、前19个字节为事件头部（参考文件格式部分）
                    //2、20 - 27为位置信息（int64）
                    //3、剩余部分为新日志文件的标识(文件名)
                }
                if (_type == (int)Log_event_type.INTVAR_EVENT)
                {
                    //该事件用于记录特殊变量值，如自动增量值（auto_increment）和LAST_INSERT_ID
                    //1、前19个字节为事件头部（参考文件格式部分）
                    //2、第20个字节是类型字段
                    //3、第21 - 28个字节是值
                }

                if (_type == (int)Log_event_type.TABLE_MAP_EVENT)
                {
                    //Fixed data part:
                    //• 6 bytes.The table ID.
                    //• 2 bytes.Reserved for future use. 
                    //Variable data part: 
                    //• 1 byte.The length of the database name.
                    //• Variable - sized.The database name(null - terminated).
                    //• 1 byte.The length of the table name.
                    //• Variable - sized.The table name(null - terminated).
                    //• Packed integer.The number of columns in the table.
                    //• Variable - sized.An array of column types, one byte per column.To find the meanings of these values, look at enum_field_types in the mysql_com.h header file.
                    //• Packed integer.The length of the metadata block.
                    //• Variable - sized.The metadata block; see log_event.h for contents and format.
                    //• Variable - sized.Bit - field indicating whether each column can be NULL, one bit per column.For this field, the amount of storage required for N columns is INT((N + 7) / 8) bytes.
                    byte[] table_id = new byte[6];
                    Array.Copy(data, 19 + offset, table_id, 0, 6);
                    byte[] reserved = new byte[2];
                    Array.Copy(data, 25 + offset, reserved, 0, 2);
                    byte dbname_length = data[27 + offset];
                    char[] dbname = new char[dbname_length];
                    Array.Copy(data, 28 + offset, dbname, 0, dbname_length);
                    byte tablename_length = data[28 + offset+ dbname_length+1];
                    char[] tablename = new char[tablename_length];
                    Array.Copy(data, 28 + offset + dbname_length + 2, tablename, 0, tablename_length);
                    byte columns_number = data[28 + offset + dbname_length + 2+ tablename_length];
                    byte columns_type = data[28 + offset + dbname_length + 2 + tablename_length + 1];
                    byte metadata_length = data[28 + offset + dbname_length + 2 + tablename_length + 2];
                }

                if (_type == (int)Log_event_type.WRITE_ROWS_EVENT)
                {
                    byte[] table_id = new byte[6];
                    Array.Copy(data, 19 + offset, table_id, 0, 6);
                    byte[] reserved = new byte[2];
                    Array.Copy(data, 25 + offset, reserved, 0, 2);
                    byte columns_number = data[27 + offset];
                    byte[] col_bitmap = new byte[((columns_number + 7) / 8)];
                    Array.Copy(data, 28 + offset , col_bitmap, 0, (columns_number + 7) / 8);

                    byte[] record = new byte[(columns_number + 7) / 8];
                    Array.Copy(data, 28 + offset+ (columns_number + 7) / 8, record, 0, (columns_number + 7) / 8);
                }

                if (_type == (int)Log_event_type.FORMAT_DESCRIPTION_EVENT)
                {
                    char[] _table_id = new char[6];
                    Array.Copy(data, 20 + offset, _table_id, 0, 6);

                    byte[] extra = new byte[2];//未使用
                    Array.Copy(data, 26 + offset, extra, 0, 2);
                    byte schema_length = data[26 + offset + 2];

                    char[] schema_name = new char[schema_length];//未使用
                    Array.Copy(data, 29 + offset, schema_name, 0, schema_length);

                    string table_id = "";
                    for (int i = 0; i < _table_id.Length; i++)
                    {
                        if (_table_id[i] != '\0')
                        {
                            table_id += _table_id[i];
                        }
                    }
                }
                
                
                //Console.WriteLine(table_id);
            }
            catch (Exception e)
            {
                data = null;
                //errorMsg = e.Message;
            }
            fs.Dispose();
            return result;
        }

        public int ToInt32(byte[] v)
        {
            var r = 0;
            var len = v.Length;
            if (len > 4)
            {
                len = 4;
            }
            for (var i = 0; i < len; i++)
            {
                r |= v[i] << 8 * (len - i - 1);
            }
            return r;
        }

        private string f_print_query_table( string title,string query, string[] style, string reportType)
        {
            List<string[]> list = new List<string[]>();
            DataTable dt = SQLHelper.ExcuteQuery(query);
            for (int m = 0;m<dt.Rows.Count;m++)
            {
                string[] row = new string[style.Length];
                for(int i = 0; i < style.Length; i++)
                {
                    row[i] = dt.Rows[m][i].ToString();
                }
                list.Add(row);
            }
            return printTable(list, title, style, reportType);
        }

        private string f_print_binglog(string title, string query, string[] style, string reportType)
        {
            List<string[]> list = new List<string[]>();
            DataTable dt = SQLHelper.ExcuteQuery(query);
            for (int m = 0; m < dt.Rows.Count; m++)
            {
                string[] row = new string[style.Length];
                for (int i = 0; i < style.Length; i++)
                {
                    row[i] = dt.Rows[m][i].ToString();
                }
                list.Add(row);
            }
            return printTable(list, title, style, reportType);
        }

        public string printBottom()
        {
            string html = "";
            html += "<p /><p /><p align=center><a href=\"mailto:jack_r_ge@126.com\">jack_r_ge@126.com</a></p>";
            html += "<p /><p /><p /></body></html>";
            return html;
        }

        private string f_sec2dhms(decimal sec) {
            int day = 24 * 60 * 60;
            int hour = 60 * 60;
            int min = 60;
            if (sec < 60) {
                return Math.Ceiling(sec) + "s";
            } else if (sec > day) {
                int[] days = new int[2];
                days[0] = (int)(sec / day);
                days[1] = (int)(sec % day);
                return days[0]+"d"+f_sec2dhms(days[1])+"s";
            } else if (sec > hour) {
                int[] hours = new int[2];
                hours[0] = (int)(sec / hour);
                hours[1] = (int)(sec % hour);
                return hours[0]+"h"+f_sec2dhms(hours[1])+"s";
            } else {
                int[] mins = new int[2];
                mins[0] = (int)(sec / min);
                mins[1] = (int)(sec % min);
                return mins[0] +"m"+Math.Ceiling((decimal)mins[1])+"s";
            }
        }
    }
}
