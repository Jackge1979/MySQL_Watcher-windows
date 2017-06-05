using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace MySQLWatcher
{
    public class CustomerProperty
    {
        [CategoryAttribute("配置选项"), DescriptionAttribute("显示MySQL概述"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string mysql_overview { get; set; }
        [CategoryAttribute("配置选项"), DescriptionAttribute("间隔时间")]
        public int interval { get; set; }
        [CategoryAttribute("配置选项"), DescriptionAttribute("显示系统参数"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string sys_parm { get; set; }
        [CategoryAttribute("配置选项"), DescriptionAttribute("显示日志错误统计"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string log_error_statistics { get; set; }
        [CategoryAttribute("配置选项"), DescriptionAttribute("显示集群复制"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string replication { get; set; }
        [CategoryAttribute("配置选项"), DescriptionAttribute("显示连接数"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string connect_count { get; set; }
        [CategoryAttribute("配置选项"), DescriptionAttribute("显示平均查询时间"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string avg_query_time { get; set; }
        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("慢查询TOP条数")]
        public int slow_query_topN { get; set; }

        [CategoryAttribute("配置选项"), DescriptionAttribute("显示错误SQL数"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string err_sql_count { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("错误SQL TOP条数")]
        public int err_sql_topN { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("查询分析TOP数")]
        public int query_analysis_topN { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("查询全表扫描TOP条数")]
        public int query_full_table_scans_topN { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("查询排序TOP条数")]
        public int query_sorting_topN { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("查询临时表TOP条数")]
        public int query_with_temp_tables_topN { get; set; }

        [CategoryAttribute("配置选项"), DescriptionAttribute("显示数据库大小"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string database_size { get; set; }

        [CategoryAttribute("配置选项"), DescriptionAttribute("显示对象数"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string object_count { get; set; }

        [CategoryAttribute("配置选项"), DescriptionAttribute("显示表信息"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string table_info { get; set; }

        [CategoryAttribute("配置选项"), DescriptionAttribute("显示索引信息"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string index_info { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示schema索引统计信息"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string schema_index_statistics { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示schema表统计信息"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string schema_table_statistics { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示缓冲区schema表统计信息"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string schema_table_statistics_with_buffer { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示schema表全表扫描信息"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string schema_tables_with_full_table_scans { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示schema未使用索引信息"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string schema_unused_indexes { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示主机摘要信息"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string host_summary { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示文件IO类型主机摘要"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string host_summary_by_file_io_type { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示文件IO主机摘要"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string host_summary_by_file_io { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示各阶段主机摘要"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string host_summary_by_stages { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示语句延迟主机摘要"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string host_summary_by_statement_latency { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示语句类型主机摘要"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string host_summary_by_statement_type { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示用户摘要"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string user_summary { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示文件IO类型用户摘要"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string user_summary_by_file_io_type { get; set; }
        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示文件IO用户摘要"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string user_summary_by_file_io { get; set; }
        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示各阶段用户摘要"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string user_summary_by_stages { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示语句延迟用户摘要"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string user_summary_by_statement_latency { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示语句类型用户摘要"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string user_summary_by_statement_type { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示Schema Innodb缓冲区统计"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string innodb_buffer_stats_by_schema { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示表Innodb缓冲区统计"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string innodb_buffer_stats_by_table { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示线程IO延迟TOP条数")]
        public int io_by_thread_by_latency_topN { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示IO global by file by bytes TOP条数")]
        public int io_global_by_file_by_bytes_topN { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示IO global by file by latency TOP条数")]
        public int io_global_by_file_by_latency_topN { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示IO global by wait by bytes TOP条数")]
        public int io_global_by_wait_by_bytes_topN { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示IO global by wait by latency TOP条数")]
        public int io_global_by_wait_by_latency_topN { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示wait classes global by avg latency"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string wait_classes_global_by_avg_latency { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示wait classes global by latency"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string wait_classes_global_by_latency { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示waits_by_host_by_latency"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string waits_by_host_by_latency { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示waits by user by latency"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string waits_by_user_by_latency { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示waits global by latency"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string waits_global_by_latency { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示schema表锁等待"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string schema_table_lock_waits { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示Innodb锁等待"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string innodb_lock_waits { get; set; }
        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示memory by host by current_bytes"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string memory_by_host_by_current_bytes { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示memory by thread by current_bytes"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string memory_by_thread_by_current_bytes { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示memory by user by current_bytes"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string memory_by_user_by_current_bytes { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示memory by global by current_bytes"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string memory_global_by_current_bytes { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示内存全局汇总"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string memory_global_total { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示进程列表"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string processlist { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示会话"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string session { get; set; }

        [CategoryAttribute("MySQL5.7 配置选项"), DescriptionAttribute("显示指标"), TypeConverter(typeof(MyConverter)), ListAttribute()]
        public string metrics { get; set; }
    }
}
