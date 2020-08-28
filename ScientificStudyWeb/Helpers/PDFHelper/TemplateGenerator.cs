using System;
using System.Collections.Generic;
using System.Text;
using ScientificStudyWeb.DataObjects;
using static ScientificStudyWeb.Helpers.Utility;

namespace ScientificStudyWeb.Helpers.PDFHelper
{
    public  class TemplateGenerator
    {
        public string GetHTMLStringForExperimentReport(ReportType reportType, string header,
                                                              IEnumerable<ReportExperimentData> experiments)
        {
            var sb = new StringBuilder();

            switch (reportType)
            {
                case ReportType.FilteredByStudy:
                    {
                        sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>{0}</h1></div>
                                <div class='header'><h4>Report created at {1}</h4></div>
                                <table align='center'>
                                    <tr>
                                        <th>Group</th>
                                        <th>Test subject</th>
                                        <th>Test subject comment</th>
                                        <th>Test subject entry time</th>
                                        <th>Task</th>
                                        <th>Experiment time</th>
                                        <th>Experiment comment</th>
                                    </tr>", header, DateTime.Now.ToLocalTime().ToString());
                        foreach (var exp in experiments)
                        {
                            sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>
                                  </tr>", exp.Group, exp.TestSubject, exp.TestSubjectComment, exp.TestSubjectEntryTime.ToLocalTime().ToString(),
                                  exp.Task, exp.Time.ToLocalTime().ToShortTimeString(), exp.Comment);
                        }
                        sb.Append(@"
                                </table>
                            </body>
                        </html>");
                        break;
                    }

                case ReportType.FilteredByGroup:
                    {
                        sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>{0}</h1></div>
                                <div class='header'><h2>Report created at {1}</h2></div>
                                <table align='center'>
                                    <tr>
                                        <th>Study</th>
                                        <th>Test subject</th>
                                        <th>Test subject comment</th>
                                        <th>Test subject entry time</th>
                                        <th>Task</th>
                                        <th>Experiment time</th>
                                        <th>Experiment comment</th>
                                    </tr>", header,header, DateTime.Now.ToLocalTime().ToString());
                        foreach (var exp in experiments)
                        {
                            sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>
                                  </tr>", exp.Study, exp.TestSubject, exp.TestSubjectComment, exp.TestSubjectEntryTime.ToLocalTime().ToString(),
                                  exp.Task, exp.Time.ToLocalTime().ToShortTimeString(), exp.Comment);
                        }
                        sb.Append(@"
                                </table>
                            </body>
                        </html>");
                        break;
                    }

                case ReportType.FilteredByTestSubject:
                    {
                        sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>{0}</h1></div>
                                <div class='header'><h2>Report created at {1}</h2></div>
                                <table align='center'>
                                    <tr>
                                        <th>Study</th>
                                        <th>Group</th>
                                        <th>Test subject comment</th>
                                        <th>Test subject entry time</th>
                                        <th>Task</th>
                                        <th>Experiment time</th>
                                        <th>Experiment comment</th>
                                    </tr>", header, DateTime.Now.ToLocalTime().ToString());
                        foreach (var exp in experiments)
                        {
                            sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>
                                  </tr>", exp.Study,exp.Group, exp.TestSubjectComment, exp.TestSubjectEntryTime.ToLocalTime().ToString(),
                                  exp.Task, exp.Time.ToLocalTime().ToShortTimeString(), exp.Comment);
                        }
                        sb.Append(@"
                                </table>
                            </body>
                        </html>");
                        break; ;
                    }

                default:
                    {
                        break;
                    }
            }

            return sb.ToString();
        }
    }
}