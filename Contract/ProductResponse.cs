using System;
using System.Collections.Generic;
using netcoreDapper.Models;
using Newtonsoft.Json;

namespace netcoreDapper.Contracts
{
    public class ProductResponse
    {
        public ProductResponse()
        {  
             Products = new List<Product>();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public List<Product> Products { get; set; }
    }
}