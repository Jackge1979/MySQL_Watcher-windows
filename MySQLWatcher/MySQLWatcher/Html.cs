using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLWatcher
{
    /// <summary>
    /// HTML文件头
    /// author: Jack R. Ge(jack_r_ge@126.com)
    /// </summary>
    public class Html
    {
        public const string HEAD = "<html><head><title>MySQL工作负载报告</title>"+
            "<meta http-equiv=\"Content-Type\" content=\"text/html;charset=UTF-8\">"+
            "<style type =\"text/css\">"+
            "body.awr { font: bold 10pt Arial, Helvetica, Geneva, sans-serif; color: black; background: White; }"+
            "pre.awr  {font:8pt Courier; color:black; background:White;}"+
            "h1.awr   {font:bold 20pt Arial, Helvetica, Geneva, sans-serif;color:#336699;background-color:White;border-bottom:1px solid #cccc99;margin-top:0pt; margin-bottom:0pt;padding:0px 0px 0px 0px;}"+
            "h2.awr   {font:bold 18pt Arial, Helvetica, Geneva, sans-serif;color:#336699;background-color:White;margin-top:4pt; margin-bottom:0pt;}"+
            "h3.awr {font:bold 16pt Arial, Helvetica, Geneva, sans-serif;color:#336699;background-color:White;margin-top:4pt; margin-bottom:0pt;}"+
            "li.awr {font: 8pt Arial, Helvetica, Geneva, sans-serif; color:black; background:White;}"+
            "th.awrnobg {font:bold 8pt Arial, Helvetica, Geneva, sans-serif; color:black; background:White;padding-left:4px; padding-right:4px;padding-bottom:2px}"+
            "th.awrbg {font:bold 8pt Arial, Helvetica, Geneva, sans-serif; color:White; background:#0066CC;padding-left:4px; padding-right:4px;padding-bottom:2px}" +
            "td.awrnc {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:White;vertical-align:top;}" +
            "td.awrc    {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:#FFFFCC; vertical-align:top;}" +
            "td.awrnclb {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:White;vertical-align:top;border-left: thin solid black;}" +
            "td.awrncbb {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:White;vertical-align:top;border-left: thin solid black;border-right: thin solid black;}" +
            "td.awrncrb {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:White;vertical-align:top;border-right: thin solid black;}" +
            "td.awrcrb    {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:#FFFFCC; vertical-align:top;border-right: thin solid black;}" +
            "td.awrclb    {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:#FFFFCC; vertical-align:top;border-left: thin solid black;}" +
            "td.awrcbb    {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:#FFFFCC; vertical-align:top;border-left: thin solid black;border-right: thin solid black;}" +
            "a.awr {font:bold 8pt Arial, Helvetica, sans-serif;color:#663300; vertical-align:top;margin-top:0pt; margin-bottom:0pt;}" +
            "td.awrnct {font:8pt Arial, Helvetica, Geneva, sans-serif;border-top: thin solid black;color:black;background:White;vertical-align:top;}" +
            "td.awrct   {font:8pt Arial, Helvetica, Geneva, sans-serif;border-top: thin solid black;color:black;background:#FFFFCC; vertical-align:top;}" +
            "td.awrnclbt  {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:White;vertical-align:top;border-top: thin solid black;border-left: thin solid black;}" +
            "td.awrncbbt  {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:White;vertical-align:top;border-left: thin solid black;border-right: thin solid black;border-top: thin solid black;}" +
            "td.awrncrbt {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:White;vertical-align:top;border-top: thin solid black;border-right: thin solid black;}" +
            "td.awrcrbt     {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:#FFFFCC; vertical-align:top;border-top: thin solid black;border-right: thin solid black;}" +
            "td.awrclbt     {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:#FFFFCC; vertical-align:top;border-top: thin solid black;border-left: thin solid black;}" +
            "td.awrcbbt   {font:8pt Arial, Helvetica, Geneva, sans-serif;color:black;background:#FFFFCC; vertical-align:top;border-top: thin solid black;border-left: thin solid black;border-right: thin solid black;}" +
            "table.tdiff {  border_collapse: collapse; }" +
            "table{table-layout：fixed}" +
            "td{word-wrap:break-word;word-break:break-all;}" +
            "</style></head><body class=\"awr\">"+
            "<h1 class=\"awr\">"+
            "MySQL WORKLOAD REPOSITORY report for"+
            "</h1>";
    }
}
