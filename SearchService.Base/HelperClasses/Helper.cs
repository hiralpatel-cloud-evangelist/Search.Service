using SearchService.Base.BaseResponse;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace SearchService.Base.HelperClasses
{
    public static class Helper
    {
        private static string ENCRYPTIONKEY = "63NFTIFBFHRFGHUJM5RERTYHBN0987TYUJM34RFVBHN";
        public static DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public static string ConvertToDate(DateTime? dateTime)
        {
            if (dateTime == null)
                return "";
            return dateTime.Value.ToString("MM/dd/yyyy");
        }

        public static string GetCurrentMethodName()
        {
            StackTrace stackTrace = new();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            return stackFrame.GetMethod().ReflectedType.FullName;
        }


        public static string ConvertToStringFromByte(byte[] bytes)
        {
            string output = String.Empty;
            MemoryStream stream = new MemoryStream(bytes);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                output = reader.ReadToEnd();
            }
            return output;
        }

        public static string Encrypt(string text)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new(ENCRYPTIONKEY, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using MemoryStream ms = new();
                using (CryptoStream cs = new(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                text = Convert.ToBase64String(ms.ToArray());
            }
            return text;
        }

        public static CommonApiResponse ParseAPIResponse(HttpResponseMessage result)
        {
            CommonApiResponse commonApiResponse = new();
            if (result != null)
            {
                commonApiResponse.StatusCode = (int)result.StatusCode;
                if (result.IsSuccessStatusCode)
                {
                    var responseContent = result.Content;
                    commonApiResponse.Content = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                }
                else if (Convert.ToInt32(result.StatusCode).ToString() == StatusCodes.Status400BadRequest.ToString())
                {
                    commonApiResponse.ErrorCode = (int)result.StatusCode;
                    var responseContent = result.Content;
                    if (responseContent != null)
                    {
                        commonApiResponse.Errormessage = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                }
                else if (Convert.ToInt32(result.StatusCode).ToString() == StatusCodes.Status204NoContent.ToString())
                {
                    commonApiResponse.ErrorCode = (int)result.StatusCode;
                    var responseContent = result.Content;
                    if (responseContent != null)
                    {
                        commonApiResponse.Errormessage = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                }
                else if (Convert.ToInt32(result.StatusCode).ToString() == StatusCodes.Status404NotFound.ToString())
                {
                    commonApiResponse.StatusCode = (int)result.StatusCode;
                    commonApiResponse.ErrorCode = (int)result.StatusCode;
                    var responseContent = result.Content;
                    if (responseContent != null)
                    {
                        commonApiResponse.Errormessage = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                }
                else if (Convert.ToInt32(result.StatusCode).ToString() == StatusCodes.Status401Unauthorized.ToString())
                {
                    commonApiResponse.StatusCode = (int)result.StatusCode;
                    commonApiResponse.ErrorCode = (int)result.StatusCode;
                    var responseContent = result.Content;
                    if (responseContent != null)
                    {
                        commonApiResponse.Errormessage = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                }
                else if (Convert.ToInt32(result.StatusCode).ToString() == StatusCodes.Status500InternalServerError.ToString())
                {
                    commonApiResponse.Errormessage = "API return 500 error";
                    commonApiResponse.StatusCode = (int)result.StatusCode;
                    commonApiResponse.ErrorCode = (int)result.StatusCode;
                    var responseContent = result.Content;
                    if (responseContent != null)
                    {
                        commonApiResponse.Errormessage += responseContent.ReadAsStringAsync();
                    }
                }
            }
            else
            {
                commonApiResponse.ErrorCode = 500;
                commonApiResponse.Errormessage = "apiendpoint result null: ";
            }

            return commonApiResponse;
        }


        public static string Decrypt(string encryptedText)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new(ENCRYPTIONKEY, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using MemoryStream ms = new();
                using (CryptoStream cs = new(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                encryptedText = Encoding.Unicode.GetString(ms.ToArray());
            }
            return encryptedText;
        }

        public static string Generate6Numbers()
        {
            Random random = new();
            const string chars = "1234567890";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public static string Generate12Numbers()
        {
            Random random = new();
            const string chars = "1234567890abcdefghijklmanopqrsty";
            return new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string TransactionNoGenerator()
        {
            return string.Format("{0:yyyyMMddHHmmssff}", GetDateTime());
        }

        public static String GetNameFromJsonProperty(object className, string jsonName)
        {
            string fldName = string.Empty;
            var stringarray = jsonName.Split('.');
            List<string> stringList = new();
            for (int k = 0; k < stringarray.Count(); k++)
            {
                var jsonArray = (className.GetType().GetProperties());
                foreach (var itemJson in jsonArray)
                {
                    if (itemJson.PropertyType.Name == typeof(List<>).Name)
                    {
                        var classType = itemJson.PropertyType.GetGenericArguments()[0].Name;
                        if (itemJson.GetCustomAttribute<JsonPropertyAttribute>() != null && itemJson.GetCustomAttribute<JsonPropertyAttribute>().PropertyName == stringarray[k])
                        {
                            stringList.Add(itemJson.Name);
                            string subResponse = string.Empty;

                            k = k + 1;
                            if (!string.IsNullOrEmpty(subResponse))
                                stringList.Add(subResponse);
                        }
                    }
                    else
                    {
                        if (itemJson.GetCustomAttribute<JsonPropertyAttribute>() != null && itemJson.GetCustomAttribute<JsonPropertyAttribute>().PropertyName == stringarray[k])
                        {
                            stringList.Add(itemJson.Name);
                        }
                    }
                }
            }
            if (stringList.Count > 1)
                return String.Join(".", stringList);
            else if (stringList.Count == 1)
                return stringList[0];
            else
                return string.Empty;
        }

        public static string ReplaceTextBetween(string strSource, string strStart, string strEnd, string strReplace)
        {
            int Start, End, strSourceEnd;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0);
                var nextChar = strSource.Substring(Start, 1);
                int totalLength = Start + strStart.Length;
                int charLentght = 0;
                do
                {
                    nextChar = strSource.Substring(totalLength, 1);
                    totalLength++;
                    charLentght++;
                } while (nextChar != ";" && charLentght < 15);

                End = strSource.IndexOf(strEnd, Start);
                strSourceEnd = strSource.Length;

                string strToReplace = strSource.Substring(Start, strStart.Length + charLentght);
                string newString = string.Concat(strSource.Substring(0, Start), strReplace, strSource.Substring(Start + strToReplace.Length, strSourceEnd - (Start + strToReplace.Length)));
                return newString;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string AddTextAfter(string strSource, string afterText, string textAdded)
        {
            int Start, End, strSourceEnd;
            if (strSource.Contains(afterText))
            {
                Start = strSource.IndexOf(afterText, 0);

                strSourceEnd = strSource.Length;

                string newString = string.Concat(strSource.Substring(0, Start + afterText.Length), textAdded, strSource.Substring(Start + afterText.Length, strSourceEnd - (Start+ afterText.Length)));
                return newString;
            }
            else
            {
                return strSource;
            }
        }


        public static bool IsValidJson(this string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return false;
            }
            var value = stringValue.Trim();
            if ((value.StartsWith("{") && value.EndsWith("}")) || //For object
                (value.StartsWith("[") && value.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(value);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
