﻿#region Includes

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Microservices.Components.Serialisation.Specs {
    [Binding]
    public class SpeedComparisonSteps {
        private long _result1, _result2;

        [Given(@"I have parsed a JSON object using these components")]
        public void GivenIHaveParsedAJSONObjectUsingTheseComponents() {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var parser = new JsonObjectParser();
            Json.Parse(parser, new MemoryStream(Encoding.UTF8.GetBytes(Resources.JSONWithDuplicateObjects)), "item");

            stopwatch.Stop();
            _result1 = stopwatch.ElapsedMilliseconds;
        }

        [Given(@"I have parsed a JSON object using JSON\.NET")]
        public void GivenIHaveParsedAJSONObjectUsingJSON_NET() {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var jsonObject = JObject.Parse(Resources.LargeJSON);
            jsonObject["response"].ToString(Formatting.None);

            stopwatch.Stop();
            _result2 = stopwatch.ElapsedMilliseconds;
        }

        [Then(@"the response value associated with these components should be the lowest value")]
        public void ThenTheResponseValueAssociatedWithTheseComponentsShouldBeTheLowestValue() {
            Console.WriteLine(string.Concat("JSON#: ", _result1));
            Console.WriteLine(string.Concat("JSON.NET: ", _result2));
            Assert.Greater(_result2, _result1);
        }
    }
}