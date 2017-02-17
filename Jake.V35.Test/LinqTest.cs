using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Jake.V35.Core.Logger;
using Jake.V35.Core.Thread;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jake.V35.Test
{
    internal class Model
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [TestClass]
    public class LinqTest
    {
        private List<Model> _models;
        [TestInitialize]
        public void Init()
        {
            _models = new List<Model>()
            {
                new Model {Age = 1, Name = "N1"},
                new Model {Age = 2, Name = "N2"},
                new Model {Age = 3, Name = "N3"},
                new Model {Age = 4, Name = "N4"},
                new Model {Age = 5, Name = "N5"},
                new Model {Age = 6, Name = "N6"},
                new Model {Age = 7, Name = "N7"},
                new Model {Age = 8, Name = "N8"},
                new Model {Age = 9, Name = "N9"},
                new Model {Age = 10, Name = "N10"}
            };

        }
        [TestMethod]
        public void ToDictionaryTest()
        {
            Dictionary<string, Model> dic = _models.ToDictionary(m => m.Name, m => m);
            dic["N1"].Age = 2;
            Model n1 = _models.Find(m => m.Name == "N1");
            Assert.AreEqual(n1.Age, 2);

            var temps = _models.Where(m => m.Age > 5).ToList();
            var temp = temps.SingleOrDefault(t => t.Age == 6);
            Assert.IsNotNull(temp);
            temp.Name = "midifyAgeEquals6";
            Assert.IsNotNull(_models.SingleOrDefault(m => m.Name == temp.Name));

        }
    }
}
