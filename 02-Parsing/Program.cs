using System;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace Parser
{
    class Program
    {
        
       static string LoadPage(string url)//функция для загрузки страницы  
        {
            var result = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var receiveStream = response.GetResponseStream();
                if (receiveStream != null)
                {
                    StreamReader readStream;
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    result = readStream.ReadToEnd();
                    readStream.Close();
                }
                response.Close();
            }
            return result;
        }
        
        static void Main(string[] args)
        {
            var pageContent = LoadPage(@"https://pcoding.ru/ref/170.txt");//сайт который надо парсить
            var document = new HtmlDocument();//в переменную сохраняем сайт
            document.LoadHtml(pageContent);//загружаем контент сайта
            HtmlNodeCollection links = document.DocumentNode.SelectNodes(".//h2/a");
            foreach (HtmlNode link in links)//находим нужные ссылки и выводим результат
                Console.WriteLine("{0} - {1}", link.InnerText, link.GetAttributeValue("href", "")); 
            
        }
    }
}
