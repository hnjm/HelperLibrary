// <copyright file="MySQLDatabaseManagerTest.cs" company="NSO">Copyright © NSO 2017</copyright>
using System;
using HelperLibrary.Database;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperLibrary.Database.Tests
{
    /// <summary>Diese Klasse enthält parametrisierte Komponententests für MySQLDatabaseManager.</summary>
    [PexClass(typeof(MySQLDatabaseManager))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class MySQLDatabaseManagerTest
    {
    }
}
