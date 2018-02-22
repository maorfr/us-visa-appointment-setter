using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Frames.Pages
{
    public class HomePage
    {
        private string Url = "https://ais.usvisa-info.com/he-il";

        private IWebElement ConsulateAppointmentElement { get { return DriverManager.Driver.FindElement(By.XPath("//p[contains(@class, 'consular-appt')]")); } }

        private IWebElement AppointmentDateElement { get { return ConsulateAppointmentElement; } }

        public string AppointmentDate { get { return AppointmentDateElement.Text.Replace("פגישה קונסולרית ", "").Replace(" IDT at Tel Aviv — קבל הוראות הגעה", "").Trim(); } }

        private IWebElement ScheduleAppointmentContainer { get { return DriverManager.Driver.FindElement(By.XPath("//li//h5[contains(text(), 'קבע פגישה מחדש')]")); } }

        private IWebElement ScheduleAppointmentLink { get { return DriverManager.Driver.FindElement(By.XPath("//a[contains(text(), 'קבע פגישה מחדש')]")); } }

        private IWebElement ContinueButton { get { return DriverManager.Driver.FindElement(By.XPath("//a[text() = 'המשך']")); } }

        public void Navigate()
        {
            DriverManager.Driver.Navigate().GoToUrl(Url);
        }

        public void Continue()
        {
            ContinueButton.Click();
        }

        public void ScheduleAppointment()
        {
            ScheduleAppointmentContainer.Click();
            ScheduleAppointmentLink.Click();
        }

        public string GetParsedAppointmentDate()
        {
            string currentDate = AppointmentDate;
            currentDate = currentDate.Replace("שעה\r\n", "");
            currentDate = currentDate.Replace("IST", "").Trim();
            string month = GetMonth(ref currentDate);
            string[] tokens = currentDate.Split(',');
            string day = tokens[0].Trim();
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            string year = tokens[1].Trim();
            string time = tokens[2].Trim().Replace(":", "");
            string result = year + month + day + time;
            return result;
        }

        private string GetMonth(ref string date)
        {
            string month = "ינואר";
            if (date.Contains(month))
            {
                date = date.Replace(month, "").Trim();
                return "01";
            }

            month = "פברואר";
            if (date.Contains(month))
            {
                date = date.Replace(month, "").Trim();
                return "02";
            }

            month = "מרץ";
            if (date.Contains(month))
            {
                date = date.Replace(month, "").Trim();
                return "03";
            }

            month = "אפריל";
            if (date.Contains(month))
            {
                date = date.Replace(month, "").Trim();
                return "04";
            }

            month = "מאי";
            if (date.Contains(month))
            {
                date = date.Replace(month, "").Trim();
                return "05";
            }

            month = "יוני";
            if (date.Contains(month))
            {
                date = date.Replace(month, "").Trim();
                return "06";
            }

            month = "יולי";
            if (date.Contains(month))
            {
                date = date.Replace(month, "").Trim();
                return "07";
            }

            month = "אוגוסט";
            if (date.Contains(month))
            {
                date = date.Replace(month, "").Trim();
                return "08";
            }

            month = "ספטמבר";
            if (date.Contains(month))
            {
                date = date.Replace(month, "").Trim();
                return "09";
            }

            month = "אוקטובר";
            if (date.Contains(month))
            {
                date = date.Replace(month, "").Trim();
                return "10";
            }

            month = "נובמבר";
            if (date.Contains(month))
            {
                date = date.Replace(month, "").Trim();
                return "11";
            }

            month = "דצמבר";
            if (date.Contains(month))
            {
                date = date.Replace(month, "").Trim();
                return "12";
            }

            throw new Exception("Month parsing error");
        }
    }
}
