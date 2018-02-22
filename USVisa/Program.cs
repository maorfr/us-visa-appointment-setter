using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frames.Pages;
using System.Threading;

namespace USVisa
{
    public class Program
    {
        public static void Main()
        {
            UserForm form = new UserForm();
            form.ShowDialog();
        }
    }
}
