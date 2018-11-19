using System;
using TSQLLint.Common;

namespace test_plugin
{   
    public class SamplePlugin : IPlugin
    {
        public void PerformAction(IPluginContext context, IReporter reporter)
        {   
            string line;
            var lineNumber = 0;

            var reader = new System.IO.StreamReader(System.IO.File.OpenRead(context.FilePath));

            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;
                var column = line.IndexOf("Identity", StringComparison.OrdinalIgnoreCase);

                if (column > -1)
                {
                    reporter.ReportViolation(new SampleRuleViolation(
                        context.FilePath,
                        "use-scope-identity",
                        line,
                        lineNumber,
                        column,
                        RuleViolationSeverity.Error));
                }
            }
        }
    }

    class SampleRuleViolation : IRuleViolation
    {
        public int Column { get; private set; }
        public string FileName { get; private set; }
        public int Line { get; private set; }
        public string RuleName { get; private set; }
        public RuleViolationSeverity Severity { get; private set; }
        public string Text { get; private set; }

        public SampleRuleViolation(string fileName, string ruleName, string text, int lineNumber, int column, RuleViolationSeverity ruleViolationSeverity)
        {
            FileName = fileName;
            RuleName = ruleName;
            Text = text;
            Line = lineNumber;
            Column = column;
            Severity = ruleViolationSeverity;
        }
    }
}