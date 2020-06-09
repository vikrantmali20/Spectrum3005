using System;
using System.Collections;
using System.Net;
using System.IO;

namespace Spectrum.OnlinePay
{
    public class PaymentGateway
    {
        public static string[] CardPayment(String CardNo, string ExpiryDate, string PayAmt)
        {
            String post_url = "https://test.authorize.net/gateway/transact.dll";
            Hashtable post_values = new Hashtable();

            //the API Login ID and Transaction Key must be replaced with valid values
            post_values.Add("x_login", "3x2j2YJ2cz");
            post_values.Add("x_tran_key", "38v5R22X924uHzMc");

            post_values.Add("x_delim_data", "TRUE");
            post_values.Add("x_delim_char", "|");
            post_values.Add("x_relay_response", "FALSE");

            post_values.Add("x_type", "AUTH_CAPTURE");
            post_values.Add("x_method", "CC");
        
            post_values.Add("x_card_num", CardNo);
            post_values.Add("x_exp_date", ExpiryDate);

            post_values.Add("x_amount", PayAmt);
            post_values.Add("x_description", "Sample Transaction");

            //This section takes the input fields and converts them to the proper format
            //for an http post. For example: "x_login=username&x_tran_key=a1B2c3D4"
            string post_string = string.Empty;

            foreach (DictionaryEntry field in post_values)
                post_string += string.Format("{0}={1}&", field.Key, field.Value);

            post_string = post_string.TrimEnd('&');

            //Create an HttpWebRequest object to communicate with Authorize.net
            HttpWebRequest objRequest = WebRequest.Create(post_url) as HttpWebRequest;
            objRequest.Method = "POST";
            objRequest.ContentLength = post_string.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";

            //Post data is sent as a stream
            StreamWriter myWriter = null;
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(post_string);
            myWriter.Close();

            //Returned values are returned as a stream, then read into a string
            string post_response = string.Empty;
            HttpWebResponse objResponse = objRequest.GetResponse() as HttpWebResponse;

            var responseStream = new StreamReader(objResponse.GetResponseStream());
            post_response = responseStream.ReadToEnd();
            responseStream.Close();

            //The response string is broken into an array
            //The split character specified here must match the delimiting character specified above
            string[] response_array = post_response.Split('|');
            return response_array;
        }
    }
}
