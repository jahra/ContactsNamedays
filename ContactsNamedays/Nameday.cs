using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Diagnostics;
using System.Windows;
using System.IO;

namespace ContactsNamedays
{
    class Nameday
    {
        //public string GetName(string date)
        //{
        //    return "jmeno";
        //}

        public string GetDate(string name)
        {
            if (name == "") return "";
            UrlResolver urlRes = new UrlResolver(name);
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream stream = null;
            StreamReader reader = null;
            string responseFromServer  = "";

            try
            {
                request = (HttpWebRequest)WebRequest.Create(urlRes.GetAdress());
                response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                reader = new StreamReader(stream);
                responseFromServer = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
            finally
            {
                response.Close();
                stream.Close();
                reader.Close();
            }

            if (responseFromServer == "")
                return "";
            else 
                return responseFromServer.Substring(0, 4); ;
        }
    }

}
