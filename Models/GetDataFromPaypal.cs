using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace BeesApp.Models
{
    public class GetDataFromPaypal
    {
        public int InvoiceNumber { get; set; }

        public double GrossTotal { set; get; }

        public string PaymentStatus { set; get; }

        public string PayerFirstName { set; get; }

        public string PaymentFee { set; get; }

        public string PayerEmail { set; get; }

        public string BusinessEmail { set; get; }

        public string TxToken { set; get; }

        public string PayerLastName { set; get; }

        public string ReceiverEmail { set; get; }

        public string ItemName { set; get; }

        public string Currency { set; get; }

        public string TransactionId { set; get; }

        public string SubscribeId { set; get; }

        public string Custom { set; get; }

        private static string authToken, txToken, query, strReponse;

        public static string GetPaypalRespons(string tx)
        {
          
            try 
            {
                authToken = WebConfigurationManager.AppSettings["Token"];

                txToken = tx;

                query = string.Format("cmd =_notify-synch&tx={0}&at={1}",txToken,authToken);

                string url = WebConfigurationManager.AppSettings["test_url"];

                HttpWebRequest reqs =  (HttpWebRequest) WebRequest.Create(url);

                reqs.Method = "POST";

                reqs.ContentType = "application/x-www-form-urlencoded";

                reqs.ContentLength = query.Length;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                StreamWriter outStrmWriter = new StreamWriter(reqs.GetRequestStream(), Encoding.ASCII);

                outStrmWriter.Write(query);

                outStrmWriter.Close();

                StreamReader reader = new StreamReader(reqs.GetResponse().GetResponseStream());

                strReponse = reader.ReadToEnd();

                reader.Close();

                if (strReponse.StartsWith("SUCCESS"))
                    return strReponse;
                return string.Empty;
               
            }
            catch(Exception ex)
            {
                
                Console.WriteLine("{0}", ex);
                
                return string.Empty;
            }
           
        }

        public static GetDataFromPaypal InformationsOrder(string data)
        {
            string key, val;

            var ph = new GetDataFromPaypal();

            try
            {
                string[] strArray = data.Split('\n');

                for (int i = 1; i < strArray.Length - 1; i++)
                {
                    string [] ArrayTemp = strArray[i].Split('=');

                    key = ArrayTemp[0];

                    val = HttpUtility.UrlDecode(ArrayTemp[1]);

                    switch(key)
                    {
                        case "mc_gross":
                            ph.GrossTotal = double.Parse(val);
                            break;
                        case "invoice":
                            ph.InvoiceNumber = int.Parse(val);
                            break;
                        case "payment_status":
                            ph.PaymentStatus = val;
                            break;
                        case "first_name":
                            ph.PayerFirstName = val;
                            break;
                        case "mc_fee":
                            ph.PaymentFee = val;
                            break;
                        case "business":
                            ph.BusinessEmail = val;
                            break;
                        case "payer_email":
                            ph.PayerEmail = val;
                            break;
                        case "Tx Token":
                            ph.TxToken = val;
                            break;
                        case "last_name":
                            ph.PayerLastName = val;
                            break;
                        case "receiver_email":
                            ph.ReceiverEmail = val;
                            break;
                        case "item_name":
                            ph.ItemName = val;
                            break;
                        case "mc_currency":
                            ph.Currency = val;
                            break;
                        case "txn_id":
                            ph.ReceiverEmail = val;
                            break;
                        case "custom":
                            ph.Custom = val;
                            break;
                        case "subscr_id":
                            ph.SubscribeId = val;
                            break;
                    }

                }

                return ph;
            }
            catch(Exception)
            {
                return null;
            }   

        }

    
    }
}