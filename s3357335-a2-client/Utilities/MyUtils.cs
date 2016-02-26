using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace s3357335_a2_client.Utilities
{
    public class MyUtils
    {

        public MyUtils()
        {
        }

        public static string readTextFromFile(string path)
        {
            string readText = "";
            try
            {
                if (File.Exists(path))
                {
                    // Open the file to read from. 
                    readText = File.ReadAllText(path);
                }
                else
                {
                    Console.WriteLine("There is no such file.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

            return readText;
        }

        public static void writeStringToFile(string path, string source)
        {
            try
            {
                // Create a file to write to. 
                File.WriteAllText(path, source);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }

        // Convert List to ArrayList.
        public static ArrayList ToList<T>(List<T> list)
        {
            ArrayList arrayList = new ArrayList();
            foreach (T instance in list)
            {
                arrayList.Add(instance);
            }
            return arrayList;
        }

        // find values in cookies
        public static HttpCookie findCookie(string name, Controller con)
        {
            HttpCookie cookie = null;
            if (con.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(name))
            {
                cookie = con.ControllerContext.HttpContext.Request.Cookies[name];
                cookie.Expires = DateTime.Now.AddDays(-1);
            }
            return cookie;
        }

        // assign a past expiration date on a cookie
        public static void expireCookie(string name, Controller con)
        {
            if (con.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(name))
            {
                HttpCookie myCookie = new HttpCookie(name);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                con.ControllerContext.HttpContext.Response.Cookies.Add(myCookie);
            }
        }
    }
}