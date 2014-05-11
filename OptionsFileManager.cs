using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*
 * A simple options file manager written in C# by me. Feel free to use, but keep this header in tact!
 */

namespace Option_Parser
{
    public class Option
    {
        public string opName;
        public dynamic opValue;

        public Option(string OptionName, dynamic optionValue)
        {
            this.opName = OptionName;
            this.opValue = optionValue;
        }
    }
    public class OptionsFileManager
    {
        public string RemoveWhitespace(string input){
            return String.Join("", input.Where(c => !char.IsWhiteSpace(c)));
        }
        public List<Option> Options = new List<Option>();
        public OptionsFileManager(string fName)
        {
            string[] FileOptions = System.IO.File.ReadAllLines(fName);
            foreach(string Option in FileOptions){
                Option.Replace(" ", string.Empty);
                string[] enumerated = Option.Split('=');
                Options.Add(new Option(RemoveWhitespace(enumerated[0]), RemoveWhitespace(enumerated[1])));
            }
        }

        public bool GetOptionValueBoolean(string OptionName)
        {
            foreach (Option op in Options.ToArray())
            {
                try
                {
                    if (op.opName == OptionName)
                        return Convert.ToBoolean(op.opValue);
                }
                catch { }
            }
            return false;
        }

        public int GetOptionValueInteger(string OptionName)
        {
            foreach (Option op in Options.ToArray())
            {
                try
                {
                    if (op.opName == OptionName)
                        return Convert.ToInt32(op.opValue);
                }
                catch { }
            }
            return 0;
        }

        public string GetOptionValue(string OptionName)
        {
            foreach (Option op in Options.ToArray())
            {
                if (op.opName == OptionName)
                    return op.opValue;
            }
            return null;
        }

        public bool SetOptionValue(string OptionName, dynamic value)
        {
            bool set = false;
            foreach (Option op in Options.ToArray())
            {
                if (op.opName == OptionName)
                {
                    set = true;
                    op.opValue = value;
                }
            }
            if (!set)
                Options.Add(new Option(OptionName, value));
            return true;
        }

        public bool SaveNewOptionsFile(string newFileName)
        {
            try
            {
                string finArray = string.Empty;
                foreach (Option op in Options.ToArray())
                    finArray += op.opName + "=" + Convert.ToString(op.opValue) + "\n";
                System.IO.File.WriteAllBytes(newFileName, System.Text.ASCIIEncoding.ASCII.GetBytes(finArray));
                return true;
            }
            catch (Exception ex) { }
            return false;
        }
    }
}
