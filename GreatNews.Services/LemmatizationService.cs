using CORE_;
using GreatNews.Models;
using GreatNews.Services.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GreatNews.Services
{
    public class LemmatizationService : ILemmatizationService
    {
        private readonly ApplicationContext _context;
        private readonly IAFINNService _afinn;


        public LemmatizationService(ApplicationContext context, IAFINNService afinn)
        {
            _context = context;
            _afinn = afinn;
        }

        public async Task<int> GetPositiveIndex(Guid newsId)
        {
            if (newsId!=null)
            {
                try
                {
                    var WordDict = GetDictFromLemms(await GetLemms(newsId));
                    var AffinDict = _afinn.GetDictionary();
                    int PosIndex = 0;
                    int countAffinWordInNews = 0;
                    int CountPos = 0;


                    foreach (var word in WordDict)
                    {
                        if (AffinDict.Keys.Contains<string>(word.Key))
                        {
                            var AffinWordValui = AffinDict[word.Key];
                            CountPos += Convert.ToInt32(AffinWordValui) * word.Value;
                            countAffinWordInNews += word.Value;
                        }
                    }
                    if (countAffinWordInNews != 0)
                    { 
                        PosIndex = Convert.ToInt32(Math.Round((double)CountPos / countAffinWordInNews, 3)*1000);
                    }
                   
                    return PosIndex;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return 0;
           
                       
        }


        public async Task<List<DesModel>> GetLemms(Guid newsId)
        {
            try
            {
                var news = await _context.News_.FirstOrDefaultAsync(x => x.Id.Equals(newsId));
                var newsContent = news.Content;
                               
                string pattern = @"\s+";                
                Regex regex = new Regex(pattern);
                var newsContent1 = regex.Replace(news.Content, " ");



                string splitStr = "test";
                if (newsContent.Length >= 1500)
                {
                    splitStr = newsContent1.Substring(0, 1490);
                }
                else
                {
                    splitStr = newsContent1;
                }
                string cont = $"[{{\"text\":\"{splitStr}\"}}]"; ;
                Task<string> responce = null;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=40246ed5a95bc23c0fc92e98977b615dd94e2d1d");
                    request.Content = new StringContent(cont, Encoding.UTF8, "application/json");//CONTENT-TYPE header
                    var x = client.SendAsync(request).Result;
                    responce = x.Content.ReadAsStringAsync();
                }
                var Lemms = JsonConvert.DeserializeObject<List<DesModel>>(responce.Result);

                return Lemms;
            }
            catch (Exception)
            {

                throw;
            }
           
        }


        public Dictionary<string, int> GetDictFromLemms(List<DesModel> lemms)
        {
            Dictionary<string, int> DictFromLemms = new Dictionary<string, int>();
            if (lemms!=null)
            {
                try
                {
                    var annotations = lemms[0].annotations;

                    foreach (var lemma in annotations.lemma)
                    {
                        if (lemma.value != "")
                        {
                            if (DictFromLemms.ContainsKey(lemma.value))
                            {
                                DictFromLemms[lemma.value] += 1;
                            }
                            else
                            {
                                DictFromLemms[lemma.value] = 1;
                            }
                        }
                    }

                    return DictFromLemms;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            
        }

        /* public List<DesModel> GetLemms(Guid newsId)
         {
             var news =_context.News_.Where(x => x.Id.Equals(newsId));
             var newsContent = news.Select(x=>x.Content).ToString();
             string splitStr = "test";
             if (newsContent.Length >= 1500)
             {
                 splitStr= newsContent.Substring(0, 1499);
             }
             else
             {
                 splitStr = newsContent;
             }
             string cont = $"[{{\"text\":\"{newsContent}\"}}]";
             Task<string> responce = null;
             using (var client = new HttpClient())
             {
                 client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                 HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=40246ed5a95bc23c0fc92e98977b615dd94e2d1d");
                 request.Content = new StringContent(cont, Encoding.UTF8, "application/json");//CONTENT-TYPE header
                 var x = client.SendAsync(request).Result;
                 responce = x.Content.ReadAsStringAsync();
             }
             var Lemms = JsonConvert.DeserializeObject<List<DesModel>>(responce.Result);            
             return Lemms;
         }


         public Dictionary<string, int> GetDictFromLemms(List<DesModel> lemms)
         {
             Dictionary<string, int> DictFromLemms = new Dictionary<string, int>();

             try
             {
                 var annotations = lemms[0].annotations;

                 foreach (var lemma in annotations.lemma)
                 {
                     if (lemma.value != "")
                     {
                         if (DictFromLemms.ContainsKey(lemma.value))
                         {
                             DictFromLemms[lemma.value] += 1;
                         }
                         else
                         {
                             DictFromLemms[lemma.value] = 1;
                         }
                     }
                 }

                 return DictFromLemms;
             }
             catch (Exception ex)
             {
                 return null;
             }
         }*/





    }
}


/*public async Task<List<DesModel>> GetLemms(Guid newsId)
{
    var news = await _context.News_.FirstOrDefaultAsync(x => x.Id.Equals(newsId));
    var newsContent = news.Content;
    string splitStr = "test";
    if (newsContent.Length >= 1500)
    {
        splitStr = newsContent.Substring(0, 1499);
    }
    else
    {
        splitStr = newsContent;
    }
    string cont = $"[{{\"text\":\"{newsContent}\"}}]";
    Task<string> responce = null;
    using (var client = new HttpClient())
    {
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=40246ed5a95bc23c0fc92e98977b615dd94e2d1d");
        request.Content = new StringContent(cont, Encoding.UTF8, "application/json");//CONTENT-TYPE header
        var x = client.SendAsync(request).Result;
        responce = x.Content.ReadAsStringAsync();
    }
    var Lemms = JsonConvert.DeserializeObject<List<DesModel>>(responce.Result);
    return Lemms;
}


public Dictionary<string, int> GetDictFromLemms(List<DesModel> lemms)
{
    Dictionary<string, int> DictFromLemms = new Dictionary<string, int>();

    try
    {
        var annotations = lemms[0].annotations;

        foreach (var lemma in annotations.lemma)
        {
            if (lemma.value != "")
            {
                if (DictFromLemms.ContainsKey(lemma.value))
                {
                    DictFromLemms[lemma.value] += 1;
                }
                else
                {
                    DictFromLemms[lemma.value] = 1;
                }
            }
        }

        return DictFromLemms;
    }
    catch (Exception ex)
    {
        return null;
    }
}*/