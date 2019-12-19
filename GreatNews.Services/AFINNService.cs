using CORE_;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GreatNews.Services
{
    public class AFINNService : IAFINNService
    {
        public Dictionary<string, string> GetDictionary()
        {
            try
            {
                /*var afinnData = File.ReadAllText(@"c:\Users\User\Desktop\RepVS\GreatNews\GreatNews\GreatNews.Services\AFINN.json");*/
                /*var dir = Directory.GetCurrentDirectory(); "C:\\Users\\User\\Desktop\\RepVS\\GreatNews\\GreatNews\\API"*/
                var afinnData = File.ReadAllText(@"..\GreatNews.Services\AFINN.json");
                var afinnDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(afinnData);
                return afinnDictionary;
            }
            catch
            {
                return null;
            }
        }
            
        }
    }

