using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frames.Pages;
using System.Threading;

namespace USVisa
{
    public partial class UserForm : Form
    {
        public LoginPage LoginPage
        {
            get { return _loginPage ?? (_loginPage = new LoginPage()); }
        }
        private LoginPage _loginPage;

        HomePage HomePage
        {
            get { return _homePage ?? (_homePage = new HomePage()); }
        }
        HomePage _homePage;

        AppointmentPage AppointmentPage
        {
            get { return _appointmentPage ?? (_appointmentPage = new AppointmentPage()); }
        }
        AppointmentPage _appointmentPage;

        bool shouldContinue = true;

        BackgroundWorker worker;

        public UserForm()
        {
            InitializeComponent();
            worker = new BackgroundWorker { WorkerReportsProgress = true };
            worker.DoWork += new DoWorkEventHandler(startButton_Click);
            worker.DoWork += new DoWorkEventHandler(continueButton_Click);
        }

        private void UserForm_Load(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Start();
            startButton.Text = "Started...";
            startButton.Update();
        }

        public void Start()
        {
            LoginPage.Login(this.emailTextbox.Text, this.passwordTextbox.Text);
        }
        public void Continue()
        {
            string currentDate = "";
            string today = DateTime.Now.ToString("yyyyMMdd");
            bool isFirstRun = true;

            LoginPage.ContinueLogin();

            while (shouldContinue)
            {
                HomePage.Navigate();
                if (isFirstRun)
                {
                    originalDateTextbox.Text = HomePage.AppointmentDate;
                    originalDateTextbox.Update();
                    isFirstRun = false;
                }
                else
                {
                    newDateTextbox.Text = HomePage.AppointmentDate;
                    newDateTextbox.Update();
                }
                currentDate = HomePage.GetParsedAppointmentDate();
                HomePage.Continue();
                HomePage.ScheduleAppointment();
                AppointmentPage.FindDate(today, ref currentDate);
                Thread.Sleep(120 * 1000);
            }

            ResetButtons();
        }

        private void ResetButtons()
        {
            stopButton.Text = "Stop!";
            stopButton.Update();
            startButton.Text = "Start!";
            startButton.Update();
            continueButton.Text = "Continue!";
            continueButton.Update();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            Continue();
            continueButton.Text = "Continued...";
            continueButton.Update();
        }

        private void Stop()
        {
            shouldContinue = false;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            Stop();
            stopButton.Text = "Stopping...";
            stopButton.Update();
            startButton.Text = "Wait...";
            startButton.Update();
            continueButton.Text = "Wait...";
            continueButton.Update();
        }
    }
}
