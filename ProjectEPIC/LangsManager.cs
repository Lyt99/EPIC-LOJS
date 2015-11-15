using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EPIC_LOJS
{
    class LangsManager
    {
        public Dictionary<String, Dictionary<String, String>> langsList;
        public String CurrentLanguage;

        public LangsManager() {
            langsList = new Dictionary<string, Dictionary<string, string>>();
            refresh();
            CurrentLanguage = "简体中文";
        }
        
        public void refresh(){
            DirectoryInfo langdirinfo = new DirectoryInfo("Langs");
            if (langdirinfo.Exists) return;
            foreach (var i in langdirinfo.GetFiles("*.lang")) {
                Dictionary<String, String> langsDictionary = new Dictionary<string, string>();
                using (StreamReader sr = new StreamReader(i.FullName, Encoding.GetEncoding("UTF-8"))) {
                    while (!sr.EndOfStream)
                    {
                        String pending = sr.ReadLine();
                        String[] sp = new String[2];
                        {
                            int pos = pending.IndexOf('=');
                            if (pos == -1) continue;
                            sp[0] = pending.Substring(0, pos);
                            sp[1] = pending.Substring(pos + 1);
                        }
                        langsDictionary.Add(sp[0].Trim(), sp[1].Trim());
                    }
                    if(langsDictionary.Count != 0)
                        if (langsDictionary.ContainsKey("LANGUAGENAME"))
                            langsList.Add(langsDictionary["LANGUAGENAME"], langsDictionary);
                        else
                            langsList.Add(Path.GetFileNameWithoutExtension(i.Name), langsDictionary);
                }

            }
        }

        public String getTranslation(String key) {
            try
            {
                if (langsList.ContainsKey(CurrentLanguage))
                    if (langsList[CurrentLanguage].ContainsKey(key))
                        return langsList[CurrentLanguage][key];
                    else
                        return GenerateMissingMessage(key);
                else
                if (langsList.Count == 0)
                    return GenerateMissingMessage(key);
                else
                {
                    CurrentLanguage = langsList.First().Key;
                    return getTranslation(key);
                }
            }
            catch (Exception) {
                return GenerateMissingMessage(key);
            }

        }

        private String GenerateMissingMessage(String str) {
            return "";
        }
    }

}
