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
    public class OptionsFileManager
    {

        Dictionary<string, string> Options = new Dictionary<string, string>();
        public string RemoveWhitespace(string input){
            return String.Join("", input.Where(c => !char.IsWhiteSpace(c)));
        }
        public OptionsFileManager(string fName)
        {
            string[] FileOptions = System.IO.File.ReadAllLines(fName);
            foreach(string Option in FileOptions){
                Option.Replace(" ", string.Empty);
                string[] enumerated = Option.Split('=');
                Options.Add(RemoveWhitespace(enumerated[0]), RemoveWhitespace(enumerated[1]));
            }
        }
		
        public T GetOptionValue<T>(string option, Func<string, T> converter)
        {
            try
            {
                string value = Options[option];
                if (String.IsNullOrWhiteSpace(value))
                {
                    return default(T);
                }
                return converter(value);
            }
            catch
            {

            }
            return default(T);
        }

        public bool SetOptionValue(string OptionName, dynamic value)
        {
            bool set = false;
            try
            {
                Options[OptionName] = Convert.ToString(value);
                set = true;
            }
            catch { }
            if (!set)
                Options.Add(OptionName, Convert.ToString(value));
            return true;
        }

        public bool SaveNewOptionsFile(string newFileName)
        {
            try
            {
                string finArray = string.Empty;
                foreach (KeyValuePair<string, string> op in Options.ToArray())
                    finArray += op.Key+ "=" + Convert.ToString(op.Value) + "\n";
                System.IO.File.WriteAllBytes(newFileName, System.Text.ASCIIEncoding.ASCII.GetBytes(finArray));
                return true;
            }
            catch (Exception ex) { }
            return false;
        }
    }
}
