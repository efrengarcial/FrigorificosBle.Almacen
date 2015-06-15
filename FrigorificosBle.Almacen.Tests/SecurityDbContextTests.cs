using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrigorificosBle.Security.Dao;
using FrigorificosBle.Security.Migrations;

namespace FrigorificosBle.Almacen.Tests
{
    [TestClass]
    public class SecurityDbContextTests
    {
        [TestMethod]
        public void SetCodeFirstMigrationsInitializerTest()
        {
            using (var context = new SecurityDbContext())
            {
              
            }
        }
    }
}
