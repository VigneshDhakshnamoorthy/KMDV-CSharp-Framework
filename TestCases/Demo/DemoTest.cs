﻿using KMDVFramework.TestCases.Base;
using KMDVFramework.Utils;
using NUnit.Framework;

namespace KMDVFramework.TestCases.Demo
{
    public class DemoTest: TestBase
    {
        [Test]
        public void Demo1()
        {

            new MailUtils().Send();


        }
    }
}
