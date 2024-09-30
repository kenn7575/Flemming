﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace BL.OpenAI
{
    public class CategorizeEmailResponse
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public CategorizeEmailResponse() { }
        public CategorizeEmailResponse(string jsonRes)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // Enable case-insensitive property names
                    IncludeFields = true
                };
                CategorizeEmailResponse? cer = JsonSerializer.Deserialize<CategorizeEmailResponse>(jsonRes, options);

                if (cer == null)
                {
                    throw new Exception("Error parsing JSON response: response is null");
                }
                CategoryName = cer.CategoryName;
                CategoryId = cer.CategoryId;
            }
            catch (Exception e)
            {
                throw new Exception("Error parsing JSON response: " + e.Message);
            }
        }
    }
}
